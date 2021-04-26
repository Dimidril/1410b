using UnityEngine.EventSystems;
using UnityEngine;

public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    #region Fields

    #region Serialize Fields

    [SerializeField] private Hero hero;

    #endregion

    #endregion

    #region Methods

    #region Unity Methods

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        //hero.AttackOn();
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        //hero.AttackOff();
    }

    #endregion

    #endregion
}