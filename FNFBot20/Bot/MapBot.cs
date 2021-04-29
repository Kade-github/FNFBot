using System;
using System.Collections.Generic;
using FridayNightFunkin;

namespace FNFBot20
{
    public class MapBot
    {
        
        public FNFSong Song { get; set; }
        
        public MapBot(string songDir)
        {
            Song = new FNFSong(songDir);
        }

        public List<FNFSong.FNFNote> GetHitNotes(FNFSong.FNFSection sect)
        {
            List<FNFSong.FNFNote> notes = new List<FNFSong.FNFNote>();
            foreach (FNFSong.FNFNote n in sect.Notes)
            {
                n.Time = Math.Round(n.Time);
                if (sect.MustHitSection && n.Type < (FNFSong.NoteType) 4)
                    notes.Add(n);
                else if (n.Type >= (FNFSong.NoteType) 4 && !sect.MustHitSection)
                    notes.Add(n);
            }

            return notes;
        }
        
        public void Compile(string path)
        {
            Song.SaveSong(path);
        }
    }
}