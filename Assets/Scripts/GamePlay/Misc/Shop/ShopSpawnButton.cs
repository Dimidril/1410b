using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSpawnButton : ShopItemButton
{
    [SerializeField] private StationaryCombat buyObject;

    protected override void add()
    {
        if (Hero.instance.GetSpawnPointItem().isCanCreate())
        {
            LevelManager.instance.changeCoinCount(-price);
            Instantiate(buyObject, Hero.instance.GetSpawnPointItem().transform.position, Hero.instance.GetSpawnPointItem().transform.rotation);
        }
        else
        {
            Debug.Log("Нельзя сотворить сдесь!");
        }
    }
}
