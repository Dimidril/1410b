using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoysticController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    #region Fields

    #region Private Field

    private Image JoystickBG;
    private Image Joystick;

    public Vector2 InputVector
    {
        get;
        private set;
    }

    #endregion

    #endregion

    #region Methods

    #region Unity Methods

    private void Start()
    {
        JoystickBG = GetComponent<Image>();
        Joystick = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(JoystickBG.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / JoystickBG.rectTransform.sizeDelta.x);
            pos.y = (pos.y / JoystickBG.rectTransform.sizeDelta.y);

            InputVector = new Vector2(pos.x * 2, pos.y * 2);
            InputVector = (InputVector.magnitude > 1f) ? InputVector.normalized : InputVector;

            Joystick.rectTransform.anchoredPosition = new Vector2(InputVector.x * (JoystickBG.rectTransform.sizeDelta.x / 2), InputVector.y * (JoystickBG.rectTransform.sizeDelta.y / 2));
        }
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        InputVector = Vector2.zero;
        Joystick.rectTransform.anchoredPosition = Vector2.zero;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        
    }

    #endregion

    #endregion
}