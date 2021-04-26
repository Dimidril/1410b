using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class ShopItemButton : MonoBehaviour, IPointerClickHandler
{
    #region Serialize Fields

    [SerializeField] protected int price = 1;

    #endregion

    #region Unity Methods 

    private void OnEnable()
    {
        transform.Find("Price").GetComponent<Text>().text = price.ToString();
    }

    private void buy()
    {
        if (price <= LevelManager.instance.CoinCount && !(Hero.instance.isDeath))
        {
            add();
        }
        else
        {
            print("нужно больше золота!");
        }
    }

    #endregion

    #region protected Methods

    //Добавление покупки
    protected abstract void add();

    #endregion

    #region Public Methods

    //Ловит нажатие
    public void OnPointerClick(PointerEventData eventData)
    {
        buy();
    }

    #endregion
}