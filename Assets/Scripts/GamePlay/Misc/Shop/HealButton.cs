using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealButton : ShopItemButton
{
    //[SerializeField] float heal;

    protected override void add()
    {
        Hero.instance.ApplyDamage(-Hero.instance.GetStartHP() + Hero.instance.GetHP());
        LevelManager.instance.changeCoinCount(-price);
        gameObject.SetActive(false);
    }
}
