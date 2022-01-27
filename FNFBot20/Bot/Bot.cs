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

        public static bool ended = false;
        public KeyBot kBot;
        public MapBot mBot;
        public RenderBot rBot;

        public float[] holdTimes = {0,0,0,0};

        public List<FNFSong.FNFNote> nPlay = new List<FNFSong.FNFNote>();
        
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
            ended = false;
            Form1.WriteToConsole("Play Thread created...");
            nPlay.Clear();
            int currentSect = 0;
            int notesPPlayed = 0;
            int lastRendered = 0;
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


                    
                    int i = 0;

                    
                    
                    FNFSong.FNFSection sect = mBot.song.Sections[currentSect];
                    
                    if (notesPPlayed >= sect.Notes.Count)
                    {
                        currentSect++;
                        notesPPlayed = 0;
                        sect = mBot.song.Sections[currentSect];
                        Form1.WriteToConsole("Next section!");
                    }
                    
                    List<FNFSong.FNFNote> notesToPlay = mBot.GetHitNotes(sect);

                    if (notesToPlay.Count == 0)
                    {
                        currentSect++;
                        Form1.WriteToConsole("Skiping to section " + currentSect);
                    }
                    else if (lastRendered != currentSect)
                    {
                        lastRendered = currentSect;
                        if (Form1.Rendering)
                        {
                            Form1.watchTime.Text = "Time: " + watch.Elapsed.TotalSeconds.ToString();
                            Thread list = new Thread(() => rBot.ListNotes(mBot.GetHitNotes(sect)));
                            Form1.currentThreads.Add(list);
                            list.Start();
                        }
                    }

                    foreach (FNFSong.FNFNote n in notesToPlay)
                    {

                        // ke default hit windows or something like em

                        if ((float)watch.Elapsed.TotalMilliseconds + kBot.offset >= (double) (n.Time - 22) && !nPlay.Contains(n))
                        {
                            HandleNote(n);
                            nPlay.Add(n);
                            notesPPlayed++;
                        }

                        i++;

                    }
                    
                    // check if we should let go of holds
                    
                    for (int ii = 0; ii < 4; ii++)
                    {
                        if (watch.ElapsedMilliseconds > holdTimes[ii] && holdTimes[ii] != 0)
                        {
                            holdTimes[ii] = 0;
                            switch (ii)
                            {
                                case 0:
                                    simulator.Keyboard.KeyUp(VirtualKeyCode.LEFT);
                                    break;
                                case 1:
                                    simulator.Keyboard.KeyUp(VirtualKeyCode.DOWN);
                                    break;
                                case 2:
                                    simulator.Keyboard.KeyUp(VirtualKeyCode.UP);
                                    break;
                                case 3:
                                    simulator.Keyboard.KeyUp(VirtualKeyCode.RIGHT);
                                    break;
                            }
                        }
                    }

                    if (!Playing)
                        break;
                    

                    // Form1.WriteToConsole("Section See: " + sectionSee);
                    notesPlayed = 0;
                }
                Form1.console.Text = "";
                Playing = false;
                Form1.WriteToConsole("Completed!");
                ended = true;
            }
            catch (Exception e)
            {
                Form1.WriteToConsole("Exception on Play Thread\n" + e);
            }
        }

        private bool[] boolAr = new[] {false, false, false, false};
        
        public void HandleNote(FNFSong.FNFNote n)
        {
            bool shouldHold = n.Length > 0;
            if (shouldHold)
                holdTimes[(int)n.Type % 4] = ((float)n.Length + (float)watch.Elapsed.TotalMilliseconds) + 10;
            
            switch (n.Type)
            {
                case FNFSong.NoteType.Left:
                case FNFSong.NoteType.RLeft:
                    if (shouldHold)
                    {
                        simulator.Keyboard.KeyDown(VirtualKeyCode.LEFT);
                            
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
                    }
                    else
                        kBot.KeyPress(0x28, 0x1f);

                    break;
                case FNFSong.NoteType.Up:
                case FNFSong.NoteType.RUp:
                    if (shouldHold)
                    {

                        simulator.Keyboard.KeyDown(VirtualKeyCode.UP);

                    }
                    else
                        kBot.KeyPress(0x26, 0x11);


                    break;
                case FNFSong.NoteType.Right:
                case FNFSong.NoteType.RRight:
                    if (shouldHold)
                    {

                        simulator.Keyboard.KeyDown(VirtualKeyCode.RIGHT);

                    }
                    else
                        kBot.KeyPress(0x27, 0x20);

                    break;
            }
            notesPlayed++;
        }
    }
}