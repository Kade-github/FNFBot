using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            Console.WriteLine("Directory: ");
            string dir = Console.ReadLine();
            InputSimulator simulator = new InputSimulator();
            Song.Root song = null;
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
                }
            };
            kbh.HookKeyboard();

            List<PreAnalize.NoteHit> hit = null;
            
            Console.WriteLine("hooked :>");
            
            new Thread(() =>
            {
                while (true)
                {
                    if (playing)
                    {
                        foreach(PreAnalize.NoteHit h in hit)
                        {
                            bool shouldHold = h.length > 0;
                            while (watch.ElapsedMilliseconds < h.time)
                            {
                                if (!playing)
                                    break;
                            }
                            if (!playing)
                                break;
                            switch (h.type)
                            {
                                case 0:
                                    if (shouldHold)
                                    {
                                        simulator.Keyboard.KeyDown(VirtualKeyCode.LEFT);
                                        Thread.Sleep(Convert.ToInt32(h.length));
                                        simulator.Keyboard.KeyUp(VirtualKeyCode.LEFT);
               
                                    }
                                    else
                                        KeyPress(VirtualKeyCode.LEFT, simulator);
                                    break;
                                case 1:
                                    if (shouldHold)
                                    {

                                        simulator.Keyboard.KeyDown(VirtualKeyCode.DOWN);
                                        Thread.Sleep(Convert.ToInt32(h.length));
                                        simulator.Keyboard.KeyUp(VirtualKeyCode.DOWN);
                                    }
                                    else KeyPress(VirtualKeyCode.DOWN, simulator);

                                    break;
                                case 2:
                                    if (shouldHold)
                                    {

                                        simulator.Keyboard.KeyDown(VirtualKeyCode.UP);
                                        Thread.Sleep(Convert.ToInt32(h.length));
                                        simulator.Keyboard.KeyUp(VirtualKeyCode.UP);
                   
                                    }
                                    else
                                        KeyPress(VirtualKeyCode.UP, simulator);


                                    break;
                                case 3:
                                    if (shouldHold)
                                    {

                                        simulator.Keyboard.KeyDown(VirtualKeyCode.RIGHT);
                                        Thread.Sleep(Convert.ToInt32(h.length));
                                        simulator.Keyboard.KeyUp(VirtualKeyCode.RIGHT);
                      
                                    }
                                    else
                                        KeyPress(VirtualKeyCode.RIGHT, simulator);

                                    break;
                            }
                        }
                        playing = false;
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

                            song = Song.LoadSong(
                                $@"{dir}\assets\data\{name}\{name}{(diff != "" ? "-" + diff : "")}.json");

                            hit = PreAnalize.Anal(song);
                            
                            Console.WriteLine("Loaded " + song.song.SongSong + " with " + song.sections + " sections.");

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

        public static void KeyPress(VirtualKeyCode key,InputSimulator sim)
        {
            new Thread(() =>
            {
                sim.Keyboard.KeyDown(key);
                Thread.Sleep(offset);
                sim.Keyboard.KeyUp(key);
            }).Start();
        }
    }
}