using AptitudeEngine.Enums;

namespace AptitudeEngine
{
    public static class Extensions
    {
        public static InputCode ToApt(this OpenTK.Input.MouseButton button)
        {
            switch (button)
            {
                case OpenTK.Input.MouseButton.Left: return InputCode.Mouse0;
                case OpenTK.Input.MouseButton.Right: return InputCode.Mouse1;
                case OpenTK.Input.MouseButton.Middle: return InputCode.Mouse2;
                case OpenTK.Input.MouseButton.Button1: return InputCode.Mouse3;
                case OpenTK.Input.MouseButton.Button2: return InputCode.Mouse4;
                case OpenTK.Input.MouseButton.Button3: return InputCode.Mouse5;
                case OpenTK.Input.MouseButton.Button4: return InputCode.Mouse6;
                case OpenTK.Input.MouseButton.Button5: return InputCode.Mouse7;
                case OpenTK.Input.MouseButton.Button6: return InputCode.Mouse8;
                case OpenTK.Input.MouseButton.Button7: return InputCode.Mouse9;
                case OpenTK.Input.MouseButton.Button8: return InputCode.Mouse10;
                case OpenTK.Input.MouseButton.Button9: return InputCode.Mouse11;
                case OpenTK.Input.MouseButton.LastButton: return InputCode.Mouse12;

                default: return InputCode.Mouse0;
            }
        }

        public static InputCode ToApt(this OpenTK.Input.Key key)
            => (InputCode) key;
    }
}