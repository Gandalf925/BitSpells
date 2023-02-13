using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Artifact : MonoBehaviour
{
    public Player player;
    public List<ArtifactEntity> playerHasArts = new List<ArtifactEntity>();

    public List<ArtifactEntity> equipedArts = new List<ArtifactEntity>();

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
        // player = FindObjectOfType<Player>();
    }

    // Game開始時に効果を発するArtifactEffectはここに追加する
    public void playArtifactEffect()
    {
        foreach (ArtifactEntity art in equipedArts)
        {
            switch (art.name)
            {
                case "Locket":
                    player.maxHP += art.value;
                    break;
            }
        }
        player.Refresh();
    }
}
