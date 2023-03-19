using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NextSceneManager : MonoBehaviour
{
    [Header("Scene")]
    public GameObject nextScenePositionPanel;
    public NextSceneEntity[] nextSceneEntityArr;
    public NextScene nextSceneCardPrefab;
    public List<NextScene> nextSceneList;

    [Header("Enemy")]
    EnemyEntity[] sendBattleSceneEnemies;
    public List<EnemyEntity> enemyEntityList = new List<EnemyEntity>();
    List<EnemyEntity> stageEnemies;

    //singleton
    public static NextSceneManager instance;
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

    public IEnumerator GenerateNextScene()
    {
        nextScenePositionPanel = GameObject.Find("Canvas/FittingPanel").transform.Find("NextScenePositionPanel").gameObject;
        for (int i = 0; i < nextSceneEntityArr.Length; i++)
        {
            NextScene nextSceneCard = Instantiate(nextSceneCardPrefab, nextScenePositionPanel.transform, false);
            nextSceneList.Add(nextSceneCard);
            nextSceneList[i].GenerateSceneCard(nextSceneEntityArr[i].sceneName);
        }

        nextScenePositionPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        nextScenePositionPanel.transform.DOScale(new Vector3(1f, 0.5f, 0f), 0.2f);

        yield return new WaitForSeconds(0.2f);
    }

    public EnemyEntity[] GetEnemyForCurrentStage()
    {
        stageEnemies = new List<EnemyEntity>();
        sendBattleSceneEnemies = new EnemyEntity[UnityEngine.Random.Range(1, 4)];
        foreach (var enemy in enemyEntityList)
        {
            if (enemy.stageType == Player.instance.currentStageType && enemy.stageDepth == Player.instance.currentStageDepth)
            {
                stageEnemies.Add(enemy);
            }
        }

        for (int i = 0; i < sendBattleSceneEnemies.Length; i++)
        {
            sendBattleSceneEnemies[i] = stageEnemies[UnityEngine.Random.Range(0, stageEnemies.Count)];
        }
        return sendBattleSceneEnemies;
    }
}
