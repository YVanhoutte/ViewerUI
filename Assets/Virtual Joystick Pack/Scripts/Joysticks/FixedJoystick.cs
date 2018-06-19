using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick
{
    [Header("Fixed Joystick")] 
    Vector2 joystickPosition = Vector2.zero;
    //private Camera cam = new Camera();

    private float MaxDistance { get { return background.sizeDelta.x / 2f * handleLimit; } }

    void Start()
    {
        joystickPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, background.position);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        //OLD CODE
        //Vector2 direction = eventData.position - joystickPosition;
        //inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        //handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out position);
        Vector2 direction = position.normalized;
        position = (position.magnitude > MaxDistance) ? direction * MaxDistance : position;
        handle.anchoredPosition = (position);
        Vector2 axis = Vector2.one;
        inputVector = (position / MaxDistance) * AxisVector;
        //Debug.Log(string.Format("click position {3} -> local clamped click position {0} over limit {1}? {2}", position, MaxDistance, position.magnitude > MaxDistance, eventData.position));
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        joystickPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, background.position);
        OnDrag(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}