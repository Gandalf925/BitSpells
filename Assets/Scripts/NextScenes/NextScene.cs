using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public NextSceneModel model;
    public NextSceneView view;

    public NextSceneManager nextSceneManager;
    // public GameObject currentScene;

    private void Start()
    {
        view = GetComponent<NextSceneView>();
        nextSceneManager = FindObjectOfType<NextSceneManager>();
    }

    public void GenerateSceneCard(string nextSceneName)
    {
        model = new NextSceneModel(nextSceneName);
        view.Show(model);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(model.name);
    }


    public void usedSceneCheck()
    {

    }
}
