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
        
        public static Stopwatch Watch { get; set; }
        
        public string SongDirectory { get; set; }
        public KeyBot kBot;
        public MapBot mBot;
        public RenderBot rBot;

        public InputSimulator simulator = new InputSimulator();
        
        public Thread CurrentPlayThread { get; set; }
        
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

            CurrentPlayThread?.Abort();
            Form1.currentThreads?.Remove(CurrentPlayThread);

            SongDirectory = songDirectory;
            
            // basic loading
            // get a FNFSong through map bot
            mBot = new MapBot(songDirectory);

            // Create the render bot (renders notes to the screen)
            rBot = new RenderBot((int) mBot.Song.Bpm);
            
            CurrentPlayThread = new Thread(PlayThread);
            CurrentPlayThread.Start();
            
            Form1.currentThreads?.Add(CurrentPlayThread);
            
            Form1.WriteToConsole("Loaded "  + mBot.Song.SongName + " with " + mBot.Song.Sections.Count + " sections.");

            Watch = new Stopwatch();
            
            Form1.Offset.Text = "Offset: " + kBot.offset;
        }
        
        private int _notesPlayed = 0;
        private void PlayThread()
        {
            Form1.WriteToConsole("Play Thread created...");
            try
            {
                while (true)
                {
                    if (!Watch.IsRunning && Playing)
                    {
                        Form1.WatchTime.Text = "Time: 0";
                        Watch.Start();
                    }
                    else if (!Playing && Watch.IsRunning)
                    {
                        Form1.Console.Text = "";
                        Watch.Reset();
                    }


                    if (!Playing)
                    {
                        Thread.Sleep(100);
                        continue;   
                    }



                    int sectionSee = 0;

                    foreach (FNFSong.FNFSection sect in mBot.Song.Sections)
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

                        while (_notesPlayed != notesToPlay.Count && sectionSee == Form1.SectionSee)
                        {
                            Form1.WatchTime.Text = "Time: " + Watch.Elapsed.TotalSeconds.ToString();
                            Thread.Sleep(1);
                            if (!Playing)
                                break;
                        }

                        Form1.WriteToConsole("Section See: " + sectionSee);
                        
                        if (sectionSee == Form1.SectionSee)
                        {
                            _notesPlayed = 0;
                            Form1.WriteToConsole("---");
                            sectionSee = 0;
                        }
                    }
                    Form1.Console.Text = "";
                    Playing = false;
                    Form1.WriteToConsole("Completed!");
                }
            }
            catch (Exception e)
            {
                Form1.WriteToConsole("Exception on Play Thread\n" + e);
            }
        }

        private bool[] _booleanArray = new[] {false, false, false, false};
        
        public void HandleNote(FNFSong.FNFNote n)
        {
            while (Watch.Elapsed.TotalMilliseconds < (double) n.Time - kBot.offset)
            {
                Thread.Sleep(1);
                if (!Playing)
                    Thread.CurrentThread.Abort();
            }
            
            bool shouldHold = n.Length > 0;

 
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
            _notesPlayed++;
        }
    }
}
