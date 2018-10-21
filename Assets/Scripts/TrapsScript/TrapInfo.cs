using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/TrapInfo")]
public class TrapInfo : ScriptableObject {

    public string trapName;
    [Multiline]
    public string trapDescription;
    public Sprite trapIconSprite;
    public int cost;
    public GameObject trapIconObject;
}
