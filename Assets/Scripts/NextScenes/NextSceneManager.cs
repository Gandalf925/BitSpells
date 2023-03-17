using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class NextSceneManager : MonoBehaviour
{
    public List<NextSceneEntity> forestSceneEntityList = new List<NextSceneEntity>();
    public List<NextSceneEntity> usedSceneList = new List<NextSceneEntity>();
    public NextSceneEntity[] nextSceneEntityArr;
    public NextScene nextSceneCard;
    public NextScene nextSceneCardPrefab;
    public GameObject nextScenePositionPanel;
    public List<NextScene> nextSceneList;

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
        for (int i = 0; i < nextSceneEntityArr.Length; i++)
        {
            // // ランダムで登録されたシーンをピックアップ
            // var randomNextScene = nextSceneManager.forestSceneEntityList[Random.Range(0, nextSceneManager.forestSceneEntityList.Count)];

            // //　一度選ばれたシーンをusedSceneListに追加
            // nextSceneManager.nextSceneEntityArr.Add(randomNextScene);

            nextSceneCard = Instantiate(nextSceneCardPrefab, nextScenePositionPanel.transform, false);
            nextSceneList.Add(nextSceneCard);
            nextSceneList[i].GenerateSceneCard(nextSceneEntityArr[i].sceneName);
        }

        nextScenePositionPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        nextScenePositionPanel.transform.DOScale(new Vector3(1f, 0.5f, 0f), 0.2f);

        yield return new WaitForSeconds(0.2f);
    }
}
