using System;
using System.Drawing;
using System.Windows.Forms;
using FNFBot20;
using FridayNightFunkin;

namespace FNFDataManager.Assets
{
    public partial class HeldPart : UserControl
    {
        public HeldPart(FNFSong.FNFNote note)
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;

            int ii = 0;

            
            
            for (float i = 0; i <= (double) note.Length; i += RenderBot.stepCrochet / 2)
            {
                bool end = false;
                foreach (Panel pa in Controls)
                {
                    if (pa.Name.Contains("END"))
                        end = true;
                }

                if (end)
                    break;
                
                Height += 20 * ii;
                Panel p = new Panel();
                p.Name = "pnlTrail_" + i;
                switch (note.Type)
                {
                    case FNFSong.NoteType.Down:
                    case FNFSong.NoteType.RDown:
                        if (i + RenderBot.stepCrochet / 2 > (double) note.Length)
                        {
                            p.BackgroundImage = global::FNFBot20.Properties.Resources.blueEnd;
                            p.Name += "END";
                        }
                        else
                            p.BackgroundImage = global::FNFBot20.Properties.Resources.blueTrail;
                        break;
                    case FNFSong.NoteType.Right:
                    case FNFSong.NoteType.RRight:
                        if (i + RenderBot.stepCrochet / 2 > (double) note.Length)
                        {
                            p.BackgroundImage = global::FNFBot20.Properties.Resources.redEnd;
                            p.Name += "END";
                        }
                        else
                            p.BackgroundImage = global::FNFBot20.Properties.Resources.redTrail;
                        break;
                    case FNFSong.NoteType.Left:
                    case FNFSong.NoteType.RLeft:
                        if (i + RenderBot.stepCrochet / 2 > (double) note.Length)
                        {
                            p.BackgroundImage = global::FNFBot20.Properties.Resources.purpleEnd;
                            p.Name += "END";
                        }
                        else
                            p.BackgroundImage = global::FNFBot20.Properties.Resources.purpleTrail;
                        break;
                    case FNFSong.NoteType.Up:
                    case FNFSong.NoteType.RUp:
                        if (i + RenderBot.stepCrochet / 2 > (double) note.Length)
                        {
                            p.BackgroundImage = global::FNFBot20.Properties.Resources.greenEnd;
                            p.Name += "END";
                        }
                        else
                            p.BackgroundImage = global::FNFBot20.Properties.Resources.greenTrail;
                        break;
                }

                p.BackgroundImageLayout = ImageLayout.Stretch;

                p.Size = new Size(14, 20);
                p.Location = new Point(0, (20 * ii) - p.Height);
                Controls.Add(p);
                ii++;
            }
        }
        
    }
}