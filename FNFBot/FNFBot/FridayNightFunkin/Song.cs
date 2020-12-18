using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace FridayNightFunkin
{
    public class Song
    {
        public class Root    {
            public Song song { get; set; } 
            public int bpm { get; set; } 
            public int sections { get; set; } 
            public List<Note> notes { get; set; } 
        }
        
        public Song(string player2, string player1, long speed, bool voices, string songName, long bpm)
        {
            Player1 = player1;
            Player2 = player2;
            Speed = speed;
            NeedsVoices = voices;
            SongSong = songName;
            Bpm = bpm;
        }
        
        [JsonProperty("player2")]
        public string Player2 { get; set; }

        [JsonProperty("player1")]
        public string Player1 { get; set; }

        [JsonProperty("speed")]
        public long Speed { get; set; }

        [JsonProperty("needsVoices")]
        public bool NeedsVoices { get; set; }

        [JsonProperty("sectionLengths")]
        public object[] SectionLengths { get; set; }

        [JsonProperty("song")]
        public string SongSong { get; set; }

        [JsonProperty("notes")]
        public Note[] Notes { get; set; }

        [JsonProperty("bpm")]
        public long Bpm { get; set; }

        [JsonProperty("sections")]
        public long Sections { get; set; }

        public static Root LoadSong(string filePath)
        {
            return JsonConvert.DeserializeObject<Root>(File.ReadAllText(filePath));
        }
        
    }
}