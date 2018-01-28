using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading;
using System.Drawing.Drawing2D;
using System.IO;

namespace SpritesheetTester
{
    public partial class AnimEditorForm : Form
    {
        private Bitmap loadedSheet;
        private Bitmap buffer;
        private object locker = new object();
        private bool formLoaded = true;
        private bool playOnce = false;

        private float x = 0;
        private float y = 0;
        private float width = 1;
        private float height = 1;

        private int intX = 0;
        private int intY = 0;
        private int intWidth = 0;
        private int intHeight = 0;

        private bool playingAnimation = false;

        private bool loaded = false;

        public AnimEditorForm()
        {
            InitializeComponent();

            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty
                                                         | BindingFlags.Instance | BindingFlags.NonPublic, null,
                drawPnl, new object[] {true});


            var t = new Thread(AnimationHandler);
            t.Start();
        }

        private void BrowseBtn_Click(object sender, EventArgs e)
        {
            var f = new OpenFileDialog();

            if (f.ShowDialog() == DialogResult.OK)
            {
                pathBx.Text = f.FileName;
            }
        }

        public void AnimationHandler()
        {
            while (formLoaded)
            {
                if (playingAnimation)
                {
                    if (msTimer >= animation.FrameDelay)
                    {
                        foreach (var a in animation.Frames)
                        {
                            lock (locker)
                            {
                                if (a.Index == currentIndex)
                                {
                                    x = a.X;
                                    y = a.Y;
                                    width = animation.CellWidth;
                                    height = animation.CellHeight;

                                    intX = (int) ((float) buffer.Width * x);
                                    intY = (int) ((float) buffer.Height * y);
                                    intWidth = (int) ((float) buffer.Width * width);
                                    intHeight = (int) ((float) buffer.Height * height);

                                    using (var g = Graphics.FromImage(buffer))
                                    {
                                        g.FillRectangle(Brushes.Gray, new Rectangle(0, 0, buffer.Width, buffer.Height));
                                        g.DrawImage(loadedSheet, new Rectangle(0, 0, buffer.Width, buffer.Height),
                                            new Rectangle(intX, intY, intWidth, intHeight),
                                            GraphicsUnit.Pixel);
                                    }
                                }
                            }
                        }

                        currentIndex++;
                        msTimer = 0;
                    }

                    if (playOnce)
                    {
                        playingAnimation = false;
                        playOnce = false;
                        currentIndex = 0;
                    }
                    else if (currentIndex > animation.Frames.Count - 1)
                    {
                        currentIndex = 0;
                    }

                    msTimer++;
                    Thread.Sleep(1);
                }
            }
        }

        private int msTimer = 0;
        private int currentIndex = 0;
        private Animation animation = new Animation();

        private InterpolationMode mode = InterpolationMode.NearestNeighbor;

        private void DrawPnl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = mode;
            e.Graphics.FillRectangle(Brushes.Gray, new Rectangle(0, 0, drawPnl.Width, drawPnl.Height));
            if (loaded)
            {
                lock (locker)
                {
                    e.Graphics.DrawImage(buffer, new Rectangle(0, 0, drawPnl.Width, drawPnl.Height));
                }
            }

            drawPnl.Invalidate();
        }

        private void SelectBtn_Click(object sender, EventArgs e)
        {
            playingAnimation = false;
            x = float.Parse(xBx.Text);
            y = float.Parse(yBx.Text);
            width = float.Parse(widthBx.Text);
            height = float.Parse(heightBx.Text);

            intX = (int) ((float) buffer.Width * x);
            intY = (int) ((float) buffer.Height * y);
            intWidth = (int) ((float) buffer.Width * width);
            intHeight = (int) ((float) buffer.Height * height);

            loaded = false;
            try
            {
                buffer = new Bitmap(intWidth, intHeight);
            }
            catch
            {
                MessageBox.Show("Image is no longer valid...");
                return;
            }

            using (var g = Graphics.FromImage(buffer))
            {
                g.InterpolationMode = mode;
                g.DrawImage(loadedSheet, new Rectangle(0, 0, buffer.Width, buffer.Height),
                    new Rectangle(intX, intY, intWidth, intHeight),
                    GraphicsUnit.Pixel);
            }

            loaded = true;
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            playingAnimation = false;
            loaded = false;
            try
            {
                loadedSheet = new Bitmap(pathBx.Text);
            }
            catch
            {
                return;
            }

            buffer = new Bitmap(loadedSheet.Width, loadedSheet.Height);
            var buffergraphics = Graphics.FromImage(buffer);
            buffergraphics.DrawImage(loadedSheet, new Rectangle(0, 0, buffer.Width, buffer.Height));
            loaded = true;
            viewBtn.Enabled = true;
            genAnimationBtn.Enabled = true;
            g1.Enabled = true;
            g2.Enabled = true;

            spritesheetBx.Text = Path.GetFileNameWithoutExtension(pathBx.Text);
            animationNameBx.Text = Path.GetFileNameWithoutExtension(pathBx.Text);
        }

        private void GenAnimationBtn_Click(object sender, EventArgs e)
        {
            var a = new Animation
            {
                FrameDelay = int.Parse(frameDelayBx.Text),
                Spritesheet = spritesheetBx.Text,
                Name = animationNameBx.Text
            };
            var rowCnt = int.Parse(rowCntBx.Text);
            var columnCnt = int.Parse(columnCntBx.Text);

            var startX = float.Parse(startXBx.Text);
            var startY = float.Parse(startYBx.Text);

            var columnWidth = float.Parse(columnWidthBx.Text);
            var rowHeight = float.Parse(rowHeightBx.Text);
            a.CellWidth = columnWidth;
            a.CellHeight = rowHeight;

            var currentX = startX;
            var currentY = startY;
            var CurrentIndex = 0;

            for (var x = 0; x < rowCnt; x++)
            {
                for (var y = 0; y < columnCnt; y++)
                {
                    a.Frames.Add(new AnimatedFramePosition(currentX, currentY, CurrentIndex));

                    currentX += columnWidth;
                    CurrentIndex++;
                }

                currentX = startX;
                currentY += rowHeight;
            }

            if (checkBox2.Checked)
            {
                var framesReversed = DuplicateFrames(a.Frames);
                framesReversed.Reverse();

                var cIndex = a.FrameCount;
                foreach (var afp in framesReversed)
                {
                    afp.Index = cIndex;
                    cIndex++;
                }

                framesReversed.AddRange(a.Frames);
                a.Frames = framesReversed;
            }

            var json = "";

            json = JsonConvert.SerializeObject(a, checkBox1.Checked ? Formatting.Indented : Formatting.None);

            JSonBx.Text = json;
            button1.Enabled = true;
            button2.Enabled = true;
            exportBtn.Enabled = true;
            JSonBx.Enabled = true;
        }

        public List<AnimatedFramePosition> DuplicateFrames(List<AnimatedFramePosition> framesOriginal)
        {
            var toReturn = new List<AnimatedFramePosition>();

            foreach (var afp in framesOriginal)
            {
                var afp2 = new AnimatedFramePosition(afp.X, afp.Y, afp.Index);
                toReturn.Add(afp2);
            }

            return toReturn;
        }

        private void DrawPnl_MouseMove(object sender, MouseEventArgs e)
        {
            var mouseX = e.X;
            var mouseY = e.Y;

            var mouseXF = (float) decimal.Divide(mouseX, drawPnl.Width);
            var mouseYF = (float) decimal.Divide(mouseY, drawPnl.Height);
            mouseXF = (float) Math.Round(mouseXF, 2);
            mouseYF = (float) Math.Round(mouseYF, 2);
            mousePosLbl.Text = "X: " + mouseXF + "; Y: " + mouseYF;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var jo = (Newtonsoft.Json.Linq.JObject) JsonConvert.DeserializeObject(JSonBx.Text);
            animation = jo.ToObject<Animation>();
            playingAnimation = true;
        }

        private void AnimationEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            formLoaded = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var jo = (Newtonsoft.Json.Linq.JObject) JsonConvert.DeserializeObject(JSonBx.Text);
            animation = jo.ToObject<Animation>();
            playOnce = true;
            playingAnimation = true;
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            var s = new SaveFileDialog
            {
                Filter = "JSON (.json)|*json"
            };
            if (s.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(s.FileName + ".json", JSonBx.Text);
                MessageBox.Show("Save Complete!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void SingleExport_Click(object sender, EventArgs e)
        {
        }

        private void DrawPnl_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void DrawPnl_MouseUp(object sender, MouseEventArgs e)
        {
        }
    }
}