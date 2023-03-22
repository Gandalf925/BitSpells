using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Artifact : MonoBehaviour
{
    public List<ArtifactEntity> playerHasArts = new List<ArtifactEntity>();

    UIManager uIManager;

    //singleton
    public static Artifact instance;

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

    public void Equiped()
    {
        playArtifactEffect();
    }

    // Game開始時に効果を発するArtifactEffectはここに追加する
    public void playArtifactEffect()
    {
        uIManager = FindObjectOfType<UIManager>();
        foreach (ArtifactEntity art in Player.instance.equipedArts)
        {
            switch (art.name)
            {
                case "Locket":
                    Player.instance.maxHP += art.value;
                    break;
            }
        }

        uIManager.Refresh();
    }
}
