using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private PlayerInputReader _inputReader;
    [SerializeField] private RectTransform _background;
    [SerializeField] private RectTransform _handle;

    public void OnPointerDown(PointerEventData eventData)
    {
        UpdateStick(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateStick(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _inputReader.JoystickDirection = Vector2.zero;
        _handle.anchoredPosition = Vector2.zero;
    }

    private void UpdateStick(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _background,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPoint);

        float radius = _background.sizeDelta.x * 0.5f;

        Vector2 direction = Vector2.ClampMagnitude(localPoint / radius, 1f);

        _inputReader.JoystickDirection = direction;
        _handle.anchoredPosition = direction * radius;
    }
}