using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventActions
{
    public void FullHealEvent()
    {
        Player.instance.currentHP = Player.instance.maxHP;
        Debug.Log("Event処理完了");
    }
}
