using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image bgJoystick;
    private Image frontJoystick;
    private Vector2 inputVector;

    private void Start()
    {
        bgJoystick = GetComponent<Image>();
        frontJoystick = transform.GetChild(0).GetComponentInChildren<Image>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgJoystick.rectTransform, eventData.position, eventData.pressEventCamera, out pos));
        {
            pos.x = (pos.x / bgJoystick.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgJoystick.rectTransform.sizeDelta.y);
        }
        inputVector = new Vector2(pos.x * 2 - 1, pos.y * 2 - 1);
        inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

        frontJoystick.rectTransform.anchoredPosition = new Vector2(inputVector.x * (bgJoystick.rectTransform.sizeDelta.x / 2), inputVector.y * (bgJoystick.rectTransform.sizeDelta.y / 2));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        frontJoystick.rectTransform.anchoredPosition = Vector2.zero;

    }

    public float Horizontal()
    {
        if (inputVector.x != 0)
        {
            return inputVector.x;
        }
        else
        {
            return Input.GetAxis("Horizontal");
        }
    }
    public float Vertical()
    {
        if (inputVector.y != 0)
        {
            return inputVector.y;
        }
        else
        {
            return Input.GetAxis("Vertical");
        }

    }
}
