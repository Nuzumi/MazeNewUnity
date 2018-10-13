using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTrap : MonoBehaviour {

    [SerializeField]
    private List<BaseTrapComponent> trapComponents;

    protected List<GameObject> enemiesInTrapRange;

    private void Start()
    {
        enemiesInTrapRange = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!enemiesInTrapRange.Contains(collision.gameObject) && !collision.isTrigger)
            enemiesInTrapRange.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemiesInTrapRange.Remove(collision.gameObject);
    }

    public void ActivateTrap(bool destroyAfter)
    {
        foreach (var tc in trapComponents)
            tc.ActivateTrapComponent(enemiesInTrapRange);

        Debug.Log("actiavtated " + gameObject.name);
        if(destroyAfter)
            Destroy(gameObject);
    }

}
