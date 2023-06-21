using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventActions
{
    public void FullHeal()
    {
        Player.instance.currentHP = Player.instance.maxHP;
    }

    public void HalfHeal()
    {
        Player.instance.currentHP += (Player.instance.maxHP / 2);
        // もし回復後のHPが最大HPを超えた場合、最大HPに戻す
        if (Player.instance.currentHP > Player.instance.maxHP)
        {
            Player.instance.currentHP = Player.instance.maxHP;
        }
    }

    public void Leave() { }




}
