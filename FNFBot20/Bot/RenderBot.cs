using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using FNFDataManager.Assets;
using FridayNightFunkin;

namespace FNFBot20
{
    public class RenderBot
    {

        public static float stepCrochet { get; set; }

        public RenderBot(float bpm)
        {
            var crochet = (float)(60 / bpm * 1000);
            stepCrochet = (float) (crochet / 4); 
        }
        
        public void ListNotes(List<FNFSong.FNFNote> notes)
        {
            try
            {
                if (Form1.pnlField.InvokeRequired)
                    Form1.pnlField.BeginInvoke((MethodInvoker) delegate { Form1.pnlField.Controls.Clear();});

                foreach (FNFSong.FNFNote n in notes)
                {
                    double newcurrentY = Math.Floor(remapToRange(float.Parse(n.Time.ToString()), 0,
                        (float) 16 * stepCrochet, 0, Form1.pnlField.Height)) % Form1.pnlField.Height;
                    
                    switch (n.Type)
                    {
                        case FNFSong.NoteType.Left:
                        case FNFSong.NoteType.RLeft:
                            LeftArrow arrow = new LeftArrow();
                            arrow.Location = new Point(0, (int) newcurrentY);
                            if (n.Length > 0)
                            {
                                HeldPart h = new HeldPart(n);
                                h.Location = new Point(arrow.Location.X + (arrow.Width / 2) -6 , (int) newcurrentY ) ;
                                if ( Form1.pnlField.InvokeRequired)
                                    Form1.pnlField.BeginInvoke((MethodInvoker) delegate
                                    {
                                        Form1.pnlField.Controls.Add(h);
                                        h.SendToBack();
                                    });
                            }
                            if ( Form1.pnlField.InvokeRequired)
                             Form1.pnlField.BeginInvoke((MethodInvoker) delegate
                             {
                                 Form1.pnlField.Controls.Add(arrow);
                                 arrow.BringToFront();
                             });
                            break;
                        case FNFSong.NoteType.Down:
                        case FNFSong.NoteType.RDown:
                            DownArrow dArrow = new DownArrow();
                            dArrow.Location = new Point(32, (int) newcurrentY );
                            if (n.Length > 0)
                            {
                                HeldPart h = new HeldPart(n);
                                h.Location = new Point(dArrow.Location.X + (dArrow.Width / 2) - 6, (int) newcurrentY);
                                if ( Form1.pnlField.InvokeRequired)
                                    Form1.pnlField.BeginInvoke((MethodInvoker) delegate
                                    {
                                        Form1.pnlField.Controls.Add(h);
                                        h.SendToBack();
                                    });
                            }
                            if ( Form1.pnlField.InvokeRequired)
                                Form1.pnlField.BeginInvoke((MethodInvoker) delegate
                                {
                                    Form1.pnlField.Controls.Add(dArrow);
                                    dArrow.BringToFront();
                                });
                            break;
                        case FNFSong.NoteType.Up:
                        case FNFSong.NoteType.RUp:
                            UpArrow uArrow = new UpArrow();
                            uArrow.Location = new Point(64, (int) newcurrentY);
                            if (n.Length > 0)
                            {
                                HeldPart h = new HeldPart(n);
                                h.Location = new Point(uArrow.Location.X + (uArrow.Width / 2) - 6, (int) newcurrentY);
                                if ( Form1.pnlField.InvokeRequired)
                                    Form1.pnlField.BeginInvoke((MethodInvoker) delegate
                                    {
                                        Form1.pnlField.Controls.Add(h);
                                        h.SendToBack();
                                    });
                            }
                            if ( Form1.pnlField.InvokeRequired)
                                Form1.pnlField.BeginInvoke((MethodInvoker) delegate
                                {
                                    Form1.pnlField.Controls.Add(uArrow);
                                    uArrow.BringToFront();
                                });
                            break;
                        case FNFSong.NoteType.Right:
                        case FNFSong.NoteType.RRight:
                            RightArrow rArrow = new RightArrow();
                            rArrow.Location = new Point(96, (int) newcurrentY);
                            if (n.Length > 0)
                            {
                                HeldPart h = new HeldPart(n);
                                h.Location = new Point(rArrow.Location.X + (rArrow.Width / 2) - 6, (int) newcurrentY);

                                if (Form1.pnlField.InvokeRequired)
                                {
                                    Form1.pnlField.BeginInvoke((MethodInvoker) delegate
                                    {
                                        Form1.pnlField.Controls.Add(h);
                                        h.SendToBack();
                                    });
                                }
                            }
                            if ( Form1.pnlField.InvokeRequired)
                                Form1.pnlField.BeginInvoke((MethodInvoker) delegate
                                {
                                    Form1.pnlField.Controls.Add(rArrow); 
                                    rArrow.BringToFront();
                                });
                            break;
                    }

                }
            }
            catch (Exception e)
            {
                Form1.WriteToConsole("Failed to render notes.\n" + e);
            }
        }
  
        
        public static float remapToRange(float value, float start1, float stop1, float start2, float stop2) // stolen from https://github.com/HaxeFlixel/flixel/blob/b38c74b85170d7457353881713a796310187ddd2/flixel/math/FlxMath.hx#L285
        {
            return start2 + (value - start1) * ((stop2 - start2) / (stop1 - start1));
        }
    }
}