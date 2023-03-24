using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventActions
{
    public EventSceneManager eventSceneManager;
    public void FullHealEvent()
    {
        Player.instance.currentHP = Player.instance.maxHP;
        Debug.Log("Event処理完了");
    }

    public void Leave()
    {
        NextSceneManager.instance.GenerateNextScene();
    }


}
