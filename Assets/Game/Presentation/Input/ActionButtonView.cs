using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Presentation.Input
{
    public class ActionButtonView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsPressed { get; private set; }
        public bool WasPressedThisFrame { get; private set; }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsPressed = true;
            WasPressedThisFrame = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsPressed = false;
        }

        private void LateUpdate()
        {
            WasPressedThisFrame = false;
        }
    }
}