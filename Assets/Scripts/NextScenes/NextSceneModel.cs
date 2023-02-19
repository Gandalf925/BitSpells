using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class NextSceneModel
{
    public string name;
    public Sprite icon;

    public NextSceneModel(string nextSceneName)
    {
        var op = Addressables.LoadAssetAsync<NextSceneEntity>("NextScenes/" + nextSceneName);

        // WaitForCompletionで同期的にロード完了を待機
        var nextSceneEntity = op.WaitForCompletion();

        name = nextSceneEntity.sceneName;
        icon = nextSceneEntity.icon;

        // 使い終わったらリリース
        Addressables.Release(op);
    }
}
