using System;
using System.Drawing;
using AptitudeEngine.Assets;
using AptitudeEngine.Components.Visuals;
using AptitudeEngine.CoordinateSystem;
using AptitudeEngine.Events;
using AptitudeEngine.Logger;

namespace AptitudeEngine.Components.Flairs
{
    public class Flair : AptComponent
    {
        /// <summary>
        /// The color that is normally drawn to the background of the object.
        /// </summary>
        public Color BackColor { get; set; } = Color.AliceBlue;

        /// <summary>
        /// The color that that is drawn to the background of the object when a mouse button is down over the object.
        /// </summary>
        public Color MouseDownBackColor { get; set; } = Color.AliceBlue;

        /// <summary>
        /// The color that is drawn as the outline of the flair.
        /// </summary>
        public Color OutlineColor { get; set; } = Color.Black;
        
        /// <summary>
        /// 
        /// </summary>
        private AptObject TextRendererObject { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private SpriteRenderer TextRenderer { get; set; }

        /// <summary>
        /// Occurs when the object is clicked. A click is defined as Up and Down within 750 milliseconds.
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> Click;

        /// <summary>
        /// Occurs when the object has the mouse go up over it.
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> Up;

        /// <summary>
        /// Occurs when the object has the mouse go down over it.
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> Down;

        private bool ReturnAllVisible()
        {
            try
            {
                var toCheck = owner.Parent;

                while (toCheck != null)
                {
                    foreach (var ac in toCheck.Components)
                    {
                        if (ac is Flair)
                        {
                            if (!((Flair)ac).Visible)
                            {
                                return false;
                            }
                        }
                    }

                    toCheck = toCheck.Parent;
                }

                return true;
            }
            catch (Exception e)
            {
                LoggingHandler.Log("ERROR:" + e.Message, LogMessageType.Error);

                return true;
            }
        }

        /// <summary>
        /// Whether the flair and it's sub-flairs are drawn.
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Whether the flair handles drawing.
        /// </summary>
        public bool DefaultDraw { get; set; } = true;

        private bool _Visible => (Visible && ReturnAllVisible());

        private string text = "";

        /// <summary>
        /// The text being drawn to the screen.
        /// </summary>
        public string Text
        {
            get => text;
            set
            {
                text = value;

                if (TextRendererObject == null)
                {
                    TextRendererObject = Context.Instantiate();
                    TextRendererObject.Name = "hi";
                    owner.AddChild(TextRendererObject);
                    TextRendererObject.Transform.Size = new Vector2((FontSize * text.Length)/2, FontSize * text.Split('\n').Length);



                    var textSize = TextRendererObject.Transform.Size;
                    var flairSize = owner.Transform.Size;
                    TextRendererObject.Transform.Position = new Vector2((flairSize - textSize) / 2);
                }

                if (TextRenderer == null)
                {
                    TextRenderer = TextRendererObject.AddComponent<SpriteRenderer>();
                }
                TextRenderer.Sprite = Asset.LoadText(value, TextQuality, TextColor);
            }
        }

        /// <summary>
        /// Quality of text. Default 1000.
        /// </summary>
        public float TextQuality { get; set; } = 1000;

        /// <summary>
        /// Size of text on screen.
        /// </summary>
        public float FontSize { get; set; } = 0.06f;

        /// <summary>
        /// Color of text on screen.
        /// </summary>
        public Color TextColor { get; set; } = Color.Black;

        /// <summary>
        /// Whether the Flair is a check box.
        /// </summary>
        public bool CheckBoxMode { get; set; } = false;

        /// <summary>
        /// The color of the margined space when checked and as a check box.
        /// </summary>
        public Color CheckedColor { get; set; } = Color.DarkSlateGray;

        /// <summary>
        /// The margin of the checked color drawn when checked and as a check box.
        /// </summary>
        public float CheckBoxMargin { get; set; } = 0.01f;

        /// <summary>
        /// Whether it was checked as a check box.
        /// </summary>
        public bool Checked { get; set; } = false;

        /// <summary>
        /// Background image.
        /// </summary>
        public SpriteAsset BackImage { get; set; }

        public bool Border { get; set; } = true;

        public override void Render(FrameEventArgs a)
        {
            try
            {
                if (TextRenderer != null)
                {
                    TextRenderer.Drawing = _Visible;
                }

                if (_Visible && DefaultDraw)
                {
                    var c = BackColor;

                    if (MouseStateDown && !CheckBoxMode)
                    {
                        c = MouseDownBackColor;
                    }

                    ScreenHandler.Poly(new PolyVector[] {
                            new PolyVector(Vector2.Zero, c),
                            new PolyVector(new Vector2(Transform.Size.X, 0), c),
                            new PolyVector(new Vector2(Transform.Size.X, Transform.Size.Y), c),
                            new PolyVector(new Vector2(0, Transform.Size.Y), c),
                    }, owner);

                    if (CheckBoxMode)
                    {
                        if (Checked)
                        {
                            ScreenHandler.Poly(new PolyVector[] {
                                new PolyVector(new Vector2(CheckBoxMargin), CheckedColor),
                                new PolyVector(new Vector2(Transform.Size.X - CheckBoxMargin, CheckBoxMargin), CheckedColor),
                                new PolyVector(new Vector2(Transform.Size.X - CheckBoxMargin, Transform.Size.Y - CheckBoxMargin), CheckedColor),
                                new PolyVector(new Vector2(CheckBoxMargin, Transform.Size.Y - CheckBoxMargin), CheckedColor),
                            }, owner);
                        }
                    }
                    else
                    {

                        if (BackImage != null)
                        {
                            ScreenHandler.Tex(BackImage.Texture, owner);
                        }
                    }

                    if (Border)
                    {
                        ScreenHandler.Lines(new Vector2[] {
                                Vector2.Zero,
                                new Vector2(Transform.Size.X, 0),
                                new Vector2(Transform.Size.X, Transform.Size.Y),
                                new Vector2(0, Transform.Size.Y),
                                Vector2.Zero,
                            }, 2f, OutlineColor, owner);
                    }
                }
            }
            catch (Exception e)
            {
                LoggingHandler.Log("ERROR:" + e.Message, LogMessageType.Error);
            }
        }

        public override void MouseClick(MouseButtonEventArgs e)
        {
            if (_Visible)
            {
                if (CheckBoxMode)
                {
                    Checked = !Checked;
                }

                Click?.Invoke(this, e);
            }
        }

        public override void MouseDown(MouseButtonEventArgs e)
        {
            if (_Visible)
            {
                Down?.Invoke(this, e);
            }
        }

        public override void MouseUp(MouseButtonEventArgs e)
        {
            if (_Visible)
            {
                Up?.Invoke(this, e);
            }
        }
    }
}