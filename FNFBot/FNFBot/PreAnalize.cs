using System;
using System.Collections.Generic;
using FridayNightFunkin;

namespace FNFBot
{
    public class PreAnalize
    {
        public static List<NoteHit> Anal(Song.Root songRoot)
        {
            List<NoteHit> hit = new List<NoteHit>();
            int section = 0;
            int skipped = 0;
            foreach (Note note in songRoot.notes)
            {
                if (!note.MustHitSection || note.sectionNotes.Count == 0)
                {
                    skipped++;
                    continue;
                }

                section++;
                Console.WriteLine("Analizing section " + section);
                for (int i = 0; i < note.sectionNotes.Count; i++)
                {
                    double time = note.sectionNotes[i][0];
                    double type = note.sectionNotes[i][1];
                    double length = note.sectionNotes[i][2];
                    Console.WriteLine("[NOTE] Note at " + time + " and it is " + type + " with a lenght of " + length);
                    hit.Add(new NoteHit(){time = (long) time,length = (int)length, type = (int) type});
                }
            }
            Console.WriteLine("Finished scanning " + section + " sections. Skipped " + skipped);
            return hit;
        }




        public class NoteHit
        {
            public int type { get; set; }
            public int length { get; set; }
            public long time { get; set; }
        }
    }
}