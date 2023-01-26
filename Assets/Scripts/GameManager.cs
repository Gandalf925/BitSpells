using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    BattleSceneManager battleSceneManager;
    public Player player;
    public Artifact artifact;
    [SerializeField] GameObject configPanel;
    bool isOpenConfig = false;

    //singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void toggleConfigPanel()
    {
        if (!isOpenConfig)
        {
            isOpenConfig = true;
            configPanel.SetActive(true);
        }
        else
        {
            isOpenConfig = false;
            configPanel.SetActive(false);
        }
    }

}
