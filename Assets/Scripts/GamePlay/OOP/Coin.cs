using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Coin : NonCombat
{
    #region Fields

    #region Serialized Fields

    [SerializeField] private byte coinValue = 1;

    #endregion

    #endregion

    #region Methods

    #region Unity Methods

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.GetComponent<Hero>())
        {
            LevelManager.instance.changeCoinCount((int)coinValue);
            Destroy(gameObject);
        }
    }

    #endregion

    #endregion
}