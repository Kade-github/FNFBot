using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using FridayNightFunkin;

namespace FNFBot
{
    internal class Program
    {
        public static int offset = 25;
        
        [STAThread]
        public static void Main(string[] args)
        {
            Console.Title = "Init";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
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

            Console.WriteLine("hooked :>");
            int notesPlayed = 0;
            new Thread(() =>
            {
                while (true)
                {
                    Console.Title = "FNFBot 1.1";
                    if (playing)
                    {
                        foreach (FNFSong.FNFSection sect in song.Sections)
                        {
                            List<FNFSong.FNFNote> notesToPlay = new List<FNFSong.FNFNote>();
                            if (sect.Notes.Count == 0)
                                continue;
                            
                            if (sect.MustHitSection)
                            {
                                foreach (FNFSong.FNFNote n in sect.Notes)
                                    notesToPlay.Add(n);
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
                                    
                                    Console.WriteLine("Queing " + not.Type + " at " + not.Time);
                                    
                                    while ((decimal) watch.Elapsed.TotalMilliseconds < not.Time)
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
                                                    KeyPress(0x41, 0x1e);

                                                break;
                                            case FNFSong.NoteType.Down:
                                            case FNFSong.NoteType.RDown:
                                                if (shouldHold)
                                                {

                                                    simulator.Keyboard.KeyDown(VirtualKeyCode.DOWN);
                                                    Thread.Sleep(Convert.ToInt32(not.Length));
                                                    simulator.Keyboard.KeyUp(VirtualKeyCode.DOWN);
                                                }
                                                else KeyPress(0x53, 0x1f);

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
                                                    KeyPress(0x57, 0x11);


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
                                                    KeyPress(0x44, 0x20);

                                                break;
                                        }

                                    notesPlayed++;
                                }).Start();
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