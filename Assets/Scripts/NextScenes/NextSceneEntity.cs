using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NextSceneEntity", menuName = "BitSpells/NextSceneEntity", order = 0)]
public class NextSceneEntity : ScriptableObject
{
    public int id;
    public string sceneName;
    public string description;
    public Sprite icon;
}