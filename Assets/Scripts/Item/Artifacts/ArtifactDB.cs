using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArtifactDB", menuName = "BitSpells/ArtifactDB", order = 0)]
public class ArtifactDB : ScriptableObject
{
    public List<ArtifactEntity> artifactDB = new List<ArtifactEntity>();
}
