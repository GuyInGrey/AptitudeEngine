using AptitudeEngine.Enums;

namespace AptitudeEngine.Tests
{
    public class InputWriter : AptComponent
    {
        private KeyState? keyState = null;

        public override void Update()
        {
            var latestState = Input.GetKeyState(KeyCode.A);
            if (keyState != null && keyState.Value != latestState)
            {
                Logger.Log<InputWriter>(latestState.ToString());
            }

            keyState = latestState;
        }
    }
}