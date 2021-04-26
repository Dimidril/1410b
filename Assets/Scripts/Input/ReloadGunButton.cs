using UnityEngine.EventSystems;
using UnityEngine;

public class ReloadGunButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Hero.instance.Recharge();
    }
}
