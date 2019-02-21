using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTrap : MonoBehaviour {

    public List<BaseTrapComponent> trapComponents;
    [SerializeField]
    private float animationTime;
    [SerializeField]
    private AudioClip activationSound;

    private List<GameObject> enemiesInTrapRange;
    private bool canActivateAnimation = true;
    private bool canTrapBeActivated = true;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        if (canTrapBeActivated)
        {
            audioSource.Play();

            foreach (var tc in trapComponents)
                tc.ActivateTrapComponent(enemiesInTrapRange);

            if (canActivateAnimation)
            {
                GetComponent<Animator>().SetTrigger("Activate");
                canActivateAnimation = false;
            }

            Debug.Log("actiavtated " + gameObject.name);
            if (destroyAfter)
            {
                Destroy(gameObject, animationTime);
                canTrapBeActivated = false;
            }


        }
            
    }

}
