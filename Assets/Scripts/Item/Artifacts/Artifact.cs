using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Artifact : MonoBehaviour
{
    GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Artifactの効果はここに追加する
    public void playArtifactEffect()
    {
        if (gameManager.player.hasArts("Locket") == true)
        {
            var op = Addressables.LoadAssetAsync<ArtifactEntity>("Artifacts/Locket");
            var artifact = op.WaitForCompletion();
            gameManager.player.maxHP += artifact.value;

            Addressables.Release(op);
        }
    }
}
