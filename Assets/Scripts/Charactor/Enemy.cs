using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyModel model;
    EnemyView view;

    private void Awake()
    {
        view = GetComponent<EnemyView>();
    }

    public void CreateEnemy()
    {
        model = new EnemyModel();
        view.Show(model);
    }

    public void CheckAlive()
    {
        if (model.isAlive)
        {
            view.Refresh(model);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void WeekAttack(Player player)
    {
        player.Damage(model.str);
        player.Refresh();
    }
}
