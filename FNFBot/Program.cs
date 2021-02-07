using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using FridayNightFunkin;
using Memory;

namespace FNFBot
{
    internal class Program
    {
        public static int offset = 25;
        
        
        
        public static IntPtr GetModuleBaseAddress(Process proc, string modName)
        {
            IntPtr addr = IntPtr.Zero;

            foreach (ProcessModule m in proc.Modules)
            {
                if (m.ModuleName == modName)
                {
                    addr = m.BaseAddress;
                    break;
                }
            }
            return addr;
        }
        
        [STAThread]
        public static void Main(string[] args)
        {
            Console.Title = "Init";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool fpsPlus = false;
            
            Console.WriteLine("Are you using FPSPlus? Y or N (Using fps plus will allow you to use a new feature, auto start.)");
            string theFPSPlusQuestion = Console.ReadLine()
            if (theFPSPlusQuestion == "y" || theFPSPlusQuestion == "Y")
                fpsPlus = true;
            Console.WriteLine("Auto Start: " + fpsPlus);
            
            Console.WriteLine("Directory: ");
            string dir = Console.ReadLine();
            InputSimulator simulator = new InputSimulator();
            FNFSong song = null;
            bool playing = false;
            Stopwatch watch = new Stopwatch();
            int section = 0;
            bool waitingStart = false;
            
            Console.WriteLine("hooking keyboard shit");
            
            LowLevelKeyboardHook kbh = new LowLevelKeyboardHook();
            kbh.OnKeyPressed += (sender, keys) =>
            {
                switch (keys)
                {
                    case Keys.F1:
                        watch.Reset();
                        watch.Start();
                        playing = true;
                        Console.WriteLine("Started playing...");
                        break;
                    case Keys.F2:
                        playing = false;
                        Console.WriteLine("Stopped playing...");
                        waitingStart = false;
                        break;
                    case Keys.F3:
                        Console.WriteLine("offset: " + offset);
                        offset++;
                        break;
                    case Keys.F4:
                        Console.WriteLine("offset: " + offset);
                        offset--;
                        break;
                    case Keys.F5:
                        playing = false;
                        Console.WriteLine("Restarting...");
                        break;
                }
            };
            kbh.HookKeyboard();

            int crochet = 0;
            int stepCrochet = 0;

            float currentTime = 0;
            
            // Hook FPSPlus to get some gamer data
            
            // first lets actually get the process lmao.

            if (fpsPlus)
            {
                try
                {
                    new Thread(() =>
                    {
                        Process[] processes = Process.GetProcesses();
                        Process fps = null;
                        foreach (var process in processes)
                        {
                            if (process.MainWindowTitle.Contains("FPS Plus"))
                            {
                                fps = process;
                                Console.WriteLine("Found FPS Plus " + fps.MainWindowTitle);
                            }
                        }

                        if (fps == null)
                        {
                            Console.WriteLine("FPSPlus is not open, auto start is off.");
                            fpsPlus = false;
                        }

                        var hProc = ghapi.OpenProcess(ghapi.ProcessAccessFlags.All, false, fps.Id);

                        var modBase = ghapi.GetModuleBaseAddress(fps, "FunkinFPSPlus.exe");

                        string download =
                            new WebClient().DownloadString(
                                "https://raw.githubusercontent.com/KadeDev/FNFBot/main/currentOffset.of");
                        
                        Console.WriteLine("Current OFfsets: " + download);

                        IntPtr pointer = new IntPtr(Convert.ToInt64(download.Split('|')[0], 16));
                        IntPtr offset = new IntPtr(Convert.ToInt64(download.Split('|')[1].Replace("\n",""), 16));
                        
                        var addr = ghapi.FindDMAAddy(hProc, modBase + (int) pointer,
                            new[] {(int)offset});

                        Console.WriteLine("0x" + modBase.ToString("X8") + " | " + "0x" + addr.ToString("X8"));

                        Mem mem = new Mem();
                        mem.OpenProcess(fps.Id);
                        
                        while (true)
                        {
                            currentTime = mem.ReadFloat(addr.ToString("X8"));
                            Console.Title = "FNFBot 1.2 - hi :) " +
                                            (fpsPlus ? "- Conductor Song Position - " + currentTime : "");
                            if (currentTime != 0 && waitingStart && !playing)
                            {
                                watch.Reset();
                                watch.Start();
                                playing = true;
                                Console.WriteLine("Started playing...");
                            }
                            else if (currentTime == 0 && playing)
                            {
                                watch.Stop();
                                playing = false;
                                waitingStart = false; 
                                Console.WriteLine("Stopped!");
                            }
                        }

                    }).Start();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error with auto start!");
                    fpsPlus = false;
                }
            }

            Console.WriteLine("hooked :>");
            int notesPlayed = 0;
            new Thread(() =>
            {
                while (true)
                {
                    if (playing)
                    {
                        section = 0;
                        foreach (FNFSong.FNFSection sect in song.Sections)
                        {
                            section++;
                            List<FNFSong.FNFNote> notesToPlay = new List<FNFSong.FNFNote>();
                            if (sect.Notes.Count == 0)
                                continue;
                            
                            if (sect.MustHitSection)
                            {
                                foreach (FNFSong.FNFNote n in sect.Notes)
                                {
                                    if ((int)n.Type >= 4)
                                        continue;
                                    notesToPlay.Add(n);
                                }
                            }
                            else
                            {
                                foreach (FNFSong.FNFNote n in sect.Notes)
                                {
                                    if ((decimal) n.Type >= 4)
                                        notesToPlay.Add(n);
                                }
                            }

                            notesPlayed = 0;

                            foreach (FNFSong.FNFNote not in notesToPlay)
                            {
                                new Thread(() =>
                                {
                                    bool shouldHold = not.Length > 0;
                                    
                                    if (!playing)
                                        Thread.CurrentThread.Abort();

                                    while (watch.Elapsed.TotalMilliseconds < (double) not.Time - 55) // offset
                                    {
                                        Thread.Sleep(1);
                                        if (!playing)
                                            Thread.CurrentThread.Abort();
                                    }
                                    
                                    if (!playing)
                                        Thread.CurrentThread.Abort();
                                    
                                    switch (not.Type)
                                        {
                                            case FNFSong.NoteType.Left:
                                            case FNFSong.NoteType.RLeft:
                                                if (shouldHold)
                                                {
                                                    simulator.Keyboard.KeyDown(VirtualKeyCode.LEFT);
                                                    Thread.Sleep(Convert.ToInt32(not.Length));
                                                    simulator.Keyboard.KeyUp(VirtualKeyCode.LEFT);

                                                }
                                                else
                                                    KeyPress(0x44, 0x1e);

                                                break;
                                            case FNFSong.NoteType.Down:
                                            case FNFSong.NoteType.RDown:
                                                if (shouldHold)
                                                {

                                                    simulator.Keyboard.KeyDown(VirtualKeyCode.DOWN);
                                                    Thread.Sleep(Convert.ToInt32(not.Length));
                                                    simulator.Keyboard.KeyUp(VirtualKeyCode.DOWN);
                                                }
                                                else KeyPress(0x46, 0x1f);

                                                break;
                                            case FNFSong.NoteType.Up:
                                            case FNFSong.NoteType.RUp:
                                                if (shouldHold)
                                                {

                                                    simulator.Keyboard.KeyDown(VirtualKeyCode.UP);
                                                    Thread.Sleep(Convert.ToInt32(not.Length));
                                                    simulator.Keyboard.KeyUp(VirtualKeyCode.UP);

                                                }
                                                else
                                                    KeyPress(0x4A, 0x11);


                                                break;
                                            case FNFSong.NoteType.Right:
                                            case FNFSong.NoteType.RRight:
                                                if (shouldHold)
                                                {

                                                    simulator.Keyboard.KeyDown(VirtualKeyCode.RIGHT);
                                                    Thread.Sleep(Convert.ToInt32(not.Length));
                                                    simulator.Keyboard.KeyUp(VirtualKeyCode.RIGHT);

                                                }
                                                else
                                                    KeyPress(0x4B, 0x20);

                                                break;
                                        }

                                    notesPlayed++;
                                }).Start();
                            }
                            Console.Clear();
                            if (notesToPlay.Count == 0)
                                continue;
                            Console.WriteLine("Section: " + section + " | Notes: " + notesToPlay.Count + (fpsPlus ? " | Conductor Song Position - " + currentTime : ""));
                            StringBuilder toWrite = new StringBuilder("    ");
                            float currentNoteTime = float.Parse(notesToPlay.First().Time.ToString());
                            float currentY = 0;
                            foreach(FNFSong.FNFNote note in notesToPlay)
                            {
                                if (!playing)
                                    break;
                                float time = float.Parse(note.Time.ToString());
                                float newcurrentY = remapToRange( currentNoteTime, 0,
                                    (float) 16 * stepCrochet, 0, 3);
                                if (currentY < newcurrentY)
                                {
                                    currentNoteTime = time;
                                    toWrite = new StringBuilder("    ");
                                }
                                
                                switch (note.Type)
                                {
                                    case FNFSong.NoteType.Left:
                                    case FNFSong.NoteType.RLeft:
                                        if (note.Length == 0)
                                            toWrite[0] = '←';
                                        else
                                            toWrite[0] = 'H';
                                        break;
                                    case FNFSong.NoteType.Down:
                                    case FNFSong.NoteType.RDown:
                                        if (note.Length == 0)
                                            toWrite[1] = '↓';
                                        else
                                            toWrite[1] = 'H';
                                        break;
                                    case FNFSong.NoteType.Up:
                                    case FNFSong.NoteType.RUp:
                                        if (note.Length == 0)
                                            toWrite[2] = '↑';
                                        else
                                            toWrite[2] = 'H';
                                        break; 
                                    case FNFSong.NoteType.Right:
                                    case FNFSong.NoteType.RRight:
                                        if (note.Length == 0)
                                            toWrite[3] = '→';
                                        else
                                            toWrite[3] = 'H';
                                        break;
                                }
                                Console.WriteLine(toWrite.ToString());
                            }
                            
                            while (notesPlayed != notesToPlay.Count)
                            {
                                
                                Thread.Sleep(1);
                                if (!playing)
                                    break;
                            }

                            Console.WriteLine("Going to next section...");
                        }
                        
                        playing = false;
                        waitingStart = false;
                        Console.WriteLine("Song completed!");
                    }
                    else if (!waitingStart)
                    {
                        watch.Restart();
                        watch.Stop();
                        try
                        {
                            Console.WriteLine("Song Name: ");
                            string name = Console.ReadLine();
                            Console.WriteLine("Diff (none = normal): ");
                            string diff = Console.ReadLine();
                            Console.WriteLine("Trying to load " +
                                              $@"{dir}\assets\data\{name}\{name}{(diff != "" ? "-" + diff : "")}.json");

                            song = new FNFSong(
                                $@"{dir}\assets\data\{name}\{name}{(diff != "" ? "-" + diff : "")}.json");

                            Console.WriteLine("Loaded " + song.SongName + " with " + song.Sections.Count + " sections.");

                            crochet = (int)((60 / song.Bpm) * 1000);
                            stepCrochet = crochet / 4;
                            
                            Console.WriteLine("Press F1 to start");
                            waitingStart = true;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Failed to load that song. Exception: " + e);
                        }
                    }
                }
            }).Start();
            Application.Run();
            kbh.UnHookKeyboard();
        }
        
        
        public static bool essentiallyEqual(float a, float b, float epsilon)
        {
            return Math.Abs(a - b) <= ( (Math.Abs(a) > Math.Abs(b) ? Math.Abs(b) : Math.Abs(a)) * epsilon);
        }
        
        public static float remapToRange(float value, float start1, float stop1, float start2, float stop2) // stolen from https://github.com/HaxeFlixel/flixel/blob/b38c74b85170d7457353881713a796310187ddd2/flixel/math/FlxMath.hx#L285
        {
            return start2 + (value - start1) * ((stop2 - start2) / (stop1 - start1));
        }
        
        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo); 
        
        public const int KEYEVENTF_EXTENDEDKEY = 0x0001; //Key down flag
        public const int KEYEVENTF_KEYUP = 0x0002; //Key up flag

        public static void KeyPress(byte key, byte scan)
        {
            keybd_event(key, scan, KEYEVENTF_EXTENDEDKEY, 0);
            Thread.Sleep(offset);
            keybd_event(key, scan, KEYEVENTF_KEYUP, 0);
        }
    }
}
