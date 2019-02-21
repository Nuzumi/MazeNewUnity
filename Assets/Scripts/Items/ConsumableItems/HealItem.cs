using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour {

    public float healPoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.GetComponent<Damagable>();
        if(damagable != null)
        {
            bool healed = damagable.DealDamage(-healPoints);
            if (healed)
                Destroy(gameObject);
        }
    }
}
