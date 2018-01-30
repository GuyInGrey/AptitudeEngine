using AptitudeEngine.Enums;

namespace AptitudeEngine.Tests
{
    public class InputWriter : AptComponent
    {
        private InputState? keyState = null;

        public override void Update()
        {
            var latestState = Input.GetKeyState(InputCode.A);
            if (keyState != null && keyState.Value != latestState)
            {
                //Logger.Log<InputWriter>(latestState.ToString());
            }

            keyState = latestState;
        }
    }
}