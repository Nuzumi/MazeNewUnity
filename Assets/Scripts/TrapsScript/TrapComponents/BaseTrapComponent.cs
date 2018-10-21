using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTrapComponent : ScriptableObject {

    public Sprite componentImage;
    public string componentNamage;
    public float effectValue;

    public void ActivateTrapComponent(List<GameObject> enemies)
    {
        foreach (var go in enemies)
            ActivateTrapComponent(go);
    }

    protected abstract void ActivateTrapComponent(GameObject enemy);
}
