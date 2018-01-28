namespace SpritesheetTester
{
    public class AnimatedFramePosition
    {
        public AnimatedFramePosition(float x, float y, int index)
        {
            X = x;
            Y = y;
            Index = index;
        }

        public int Index { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }
}