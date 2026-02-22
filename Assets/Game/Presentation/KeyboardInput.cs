using Game.Core.Input;

namespace Game.Presentation.Input
{
    public class KeyboardInput : IShipInput
    {
        public float Thrust
        {
            get
            {
                return (UnityEngine.Input.GetKey(UnityEngine.KeyCode.W) || UnityEngine.Input.GetKey(UnityEngine.KeyCode.UpArrow)) ? 1f : 0f;
            }
        }

        public float Turn
        {
            get
            {
                float turn = 0f;
                if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.A) || UnityEngine.Input.GetKey(UnityEngine.KeyCode.LeftArrow)) turn -= 1f;
                if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.D) || UnityEngine.Input.GetKey(UnityEngine.KeyCode.RightArrow)) turn += 1f;
                return turn;
            }
        }
    }
}