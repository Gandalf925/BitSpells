using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactDB : MonoBehaviour
{
    public List<ArtifactEntity> artifactDataBase = new List<ArtifactEntity>();


    //singleton
    public static ArtifactDB instance;

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
}
