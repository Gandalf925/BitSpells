using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NextSceneView : MonoBehaviour
{
    [SerializeField] TMP_Text nextSceneName;
    [SerializeField] Image nextSceneicon;

    public void Show(NextSceneModel nextSceneModel)
    {
        nextSceneName.text = nextSceneModel.name;
        nextSceneicon.sprite = nextSceneModel.icon;
    }
}
