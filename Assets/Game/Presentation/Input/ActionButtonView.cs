using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Presentation.Input
{
    public class ActionButtonView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool IsPressed { get; private set; }
        private bool _hasPendingPress;

        public void OnPointerDown(PointerEventData eventData)
        {
            IsPressed = true;
            _hasPendingPress = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsPressed = false;
        }

        public bool ConsumePress()
        { 
            if (!_hasPendingPress)
                return false;

            _hasPendingPress = false;
            return true;
        }
    }
}