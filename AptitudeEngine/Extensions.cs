using AptitudeEngine.Enums;

namespace AptitudeEngine
{
    public static class Extensions
    {
        public static KeyCode ToApt(this OpenTK.Input.MouseButton button)
        {
            switch (button)
            {
                case OpenTK.Input.MouseButton.Left: return KeyCode.Mouse0;
                case OpenTK.Input.MouseButton.Right: return KeyCode.Mouse1;
                case OpenTK.Input.MouseButton.Middle: return KeyCode.Mouse2;
                case OpenTK.Input.MouseButton.Button1: return KeyCode.Mouse3;
                case OpenTK.Input.MouseButton.Button2: return KeyCode.Mouse4;
                case OpenTK.Input.MouseButton.Button3: return KeyCode.Mouse5;
                case OpenTK.Input.MouseButton.Button4: return KeyCode.Mouse6;
                case OpenTK.Input.MouseButton.Button5: return KeyCode.Mouse7;
                case OpenTK.Input.MouseButton.Button6: return KeyCode.Mouse8;
                case OpenTK.Input.MouseButton.Button7: return KeyCode.Mouse9;
                case OpenTK.Input.MouseButton.Button8: return KeyCode.Mouse10;
                case OpenTK.Input.MouseButton.Button9: return KeyCode.Mouse11;
                case OpenTK.Input.MouseButton.LastButton: return KeyCode.Mouse12;

                default: return KeyCode.Mouse0;
            }
        }

        public static KeyCode ToApt(this OpenTK.Input.Key key)
            => (KeyCode) key;
    }
}