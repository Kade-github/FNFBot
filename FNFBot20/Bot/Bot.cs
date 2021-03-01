using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;
using FridayNightFunkin;

namespace FNFBot20
{
    public class Bot
    {
        public static bool Playing = false;
        
        public static Stopwatch watch { get; set; }
        
        public string sngDir { get; set; }
        public KeyBot kBot;
        public MapBot mBot;
        public RenderBot rBot;

        public InputSimulator simulator = new InputSimulator();
        
        public Thread currentPlayThread { get; set; }
        
        public Bot()
        {
            // Create keyhooks with KeyBot
            kBot = new KeyBot();
            kBot.InitHooks();
            
        }
        
        public void Load(string songDirectory)
        {
            Form1.WriteToConsole("attempting to load " + songDirectory);
            if (!File.Exists(songDirectory))
            {
                Form1.WriteToConsole("Path doesn't exist");
                return;
            }

            currentPlayThread?.Abort();
            Form1.currentThreads?.Remove(currentPlayThread);

            sngDir = songDirectory;
            
            // basic loading
            // get a FNFSong through map bot
            mBot = new MapBot(songDirectory);

            // Create the render bot (renders notes to the screen)
            rBot = new RenderBot((int) mBot.song.Bpm);
            
            currentPlayThread = new Thread(PlayThread);
            currentPlayThread.Start();
            
            Form1.currentThreads?.Add(currentPlayThread);
            
            Form1.WriteToConsole("Loaded "  + mBot.song.SongName + " with " + mBot.song.Sections.Count + " sections.");

            watch = new Stopwatch();
            
            Form1.offset.Text = "Offset: " + kBot.offset;
        }
        
        private int notesPlayed = 0;
        private void PlayThread()
        {
            Form1.WriteToConsole("Play Thread created...");
            try
            {
                while (true)
                {
                    if (!watch.IsRunning && Playing)
                    {
                        Form1.watchTime.Text = "Time: 0";
                        watch.Start();
                    }
                    else if (!Playing && watch.IsRunning)
                    {
                        Form1.console.Text = "";
                        watch.Reset();
                    }


                    if (!Playing)
                    {
                        Thread.Sleep(100);
                        continue;   
                    }



                    int sectionSee = 0;

                    foreach (FNFSong.FNFSection sect in mBot.song.Sections)
                    {
                        sectionSee++;
                        List<FNFSong.FNFNote> notesToPlay = mBot.GetHitNotes(sect);

                        foreach (FNFSong.FNFNote n in notesToPlay)
                        {
                            Thread t = new Thread(() => HandleNote(n));
                            Form1.currentThreads.Add(t);
                            t.Start();
                        }

                        if (!Playing)
                            break;


                        if (Form1.Rendering)
                        {
                            Thread list = new Thread(() => rBot.ListNotes(notesToPlay));
                            Form1.currentThreads.Add(list);
                            list.Start();
                        }

                        while (notesPlayed != notesToPlay.Count && sectionSee == Form1.SectionSee)
                        {
                            Form1.watchTime.Text = "Time: " + watch.Elapsed.TotalSeconds.ToString();
                            Thread.Sleep(1);
                            if (!Playing)
                                break;
                        }

                        Form1.WriteToConsole("Section See: " + sectionSee);
                        
                        if (sectionSee == Form1.SectionSee)
                        {
                            notesPlayed = 0;
                            Form1.WriteToConsole("---");
                            sectionSee = 0;
                        }
                    }
                    Form1.console.Text = "";
                    Playing = false;
                    Form1.WriteToConsole("Completed!");
                }
            }
            catch (Exception e)
            {
                Form1.WriteToConsole("Exception on Play Thread\n" + e);
            }
        }

        private bool[] boolAr = new[] {false, false, false, false};
        
        public void HandleNote(FNFSong.FNFNote n)
        {
            while (watch.Elapsed.TotalMilliseconds < (double) n.Time - kBot.offset)
            {
                Thread.Sleep(1);
                if (!Playing)
                    Thread.CurrentThread.Abort();
            }
            
            bool shouldHold = n.Length > 0;

            new Thread(() =>
            {
                switch (n.Type)
                {
                    case FNFSong.NoteType.Left:
                    case FNFSong.NoteType.RLeft:
                        if (shouldHold)
                        {
                            simulator.Keyboard.KeyDown(VirtualKeyCode.LEFT);
                            Thread.Sleep(Convert.ToInt32(n.Length));
                            simulator.Keyboard.KeyUp(VirtualKeyCode.LEFT);
                        }
                        else
                        {
                            kBot.KeyPress(0x25, 0x1e);
                        }

                        break;
                    case FNFSong.NoteType.Down:
                    case FNFSong.NoteType.RDown:
                        if (shouldHold)
                        {

                            simulator.Keyboard.KeyDown(VirtualKeyCode.DOWN);
                            Thread.Sleep(Convert.ToInt32(n.Length));
                            simulator.Keyboard.KeyUp(VirtualKeyCode.DOWN);
                        }
                        else
                            kBot.KeyPress(0x28, 0x1f);

                        break;
                    case FNFSong.NoteType.Up:
                    case FNFSong.NoteType.RUp:
                        if (shouldHold)
                        {

                            simulator.Keyboard.KeyDown(VirtualKeyCode.UP);
                            Thread.Sleep(Convert.ToInt32(n.Length));
                            simulator.Keyboard.KeyUp(VirtualKeyCode.UP);

                        }
                        else
                            kBot.KeyPress(0x26, 0x11);


                        break;
                    case FNFSong.NoteType.Right:
                    case FNFSong.NoteType.RRight:
                        if (shouldHold)
                        {

                            simulator.Keyboard.KeyDown(VirtualKeyCode.RIGHT);
                            Thread.Sleep(Convert.ToInt32(n.Length));
                            simulator.Keyboard.KeyUp(VirtualKeyCode.RIGHT);

                        }
                        else
                            kBot.KeyPress(0x27, 0x20);

                        break;
                }
            }).Start();
            notesPlayed++;
        }
    }
}