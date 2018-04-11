using System.Collections.Generic;

namespace AptitudeEngine
{
    public class Animation
    {
        public Rectangle[] Frames { get; set; }
        public int FrameRate { get; set; }

        public static Animation EasyMake(int partsHor, int partsVer, float sizeHor, float sizeVer, int frameRate)
        {
            var toReturn = new Animation
            {
                Frames = new Rectangle[partsHor * partsVer]
            };
            var horSize = sizeHor / partsHor;
            var verSize = sizeVer / partsVer;

            var index = 0;
            for (var y = 0; y < partsVer; y++)
            {
                for (var x = 0; x < partsHor; x++)
                {
                    toReturn.Frames[index] = new Rectangle(
                        x * horSize,
                        y * verSize,
                        horSize,
                        verSize
                        );
                    index++;
                }
            }

            toReturn.FrameRate = frameRate;
            return toReturn;
        }
    }
}