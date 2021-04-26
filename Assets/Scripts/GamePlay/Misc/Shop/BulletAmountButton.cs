using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAmountButton : ShopItemButton
{
    [SerializeField] int Bullets = 50;

    protected override void add()
    {
        LevelManager.instance.changeCoinCount(-price);
        Hero.instance.AddBullets(Bullets);
    }
}
