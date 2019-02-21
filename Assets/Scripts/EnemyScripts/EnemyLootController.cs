using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/EnemyLoot")]
public class EnemyLootController : ScriptableObject {
    

    public List<LootItem> lootList;
	
    public GameObject GetLootToDrop()
    {
        lootList = lootList.OrderBy(i => i.chance).ToList();
        for(int i = 0; i <lootList.Count; i++)
        {
            if (Random.Range(0f, 1f) <= lootList[i].chance)
                return lootList[i].loot;
        }

        return null;
    }
}
