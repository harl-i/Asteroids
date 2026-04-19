using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Presentation.Input
{
    public class VirtualJoystickView : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private RectTransform _background;
        [SerializeField] private RectTransform _handle;
        [SerializeField] private float _maxRadius = 80f;

        public Vector2 InputVector { get; private set; }

        public void OnDrag(PointerEventData eventData)
        {
            if (_background == null || _handle == null)
                return;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _background,
                    eventData.position,
                    eventData.pressEventCamera,
                    out var localPoint))
            {
                var clamped = Vector2.ClampMagnitude(localPoint, _maxRadius);
                InputVector = clamped / _maxRadius;
                _handle.anchoredPosition = clamped;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            InputVector = Vector2.zero;

            if (_handle != null)
            {
                _handle.anchoredPosition = Vector2.zero;
            }
        }
    }
}