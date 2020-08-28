using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int SlamCount = 3;

    private Animator anim;
    [SerializeField]
    private GameObject doorObject = null;
    private AudioSource audioSource;

    void Awake()
    {
        LevelGenerator.instance.addObjectToList(gameObject.tag, gameObject);

        audioSource = GetComponent<AudioSource>();
        anim = gameObject.GetComponent<Animator>();
    }


    private void OnMouseDown()
    {
        if (SlamCount >= 1)
        {
            PlayAnimation();
        }
        else
        {
            RemoveDoor();
        }
    }

    private void PlayAnimation()
    {
        SlamCount--;
        audioSource.clip = AudioLibrary.instance.getRandomClip();
        audioSource.Play(0);
        if (anim != null)
        {

            anim.SetTrigger("slam");
        }
    }

    private void RemoveDoor()
    {
        if (doorObject != null)
        {

            doorObject.SetActive(false);
        }
        audioSource.clip = AudioLibrary.instance.getDoorBreakClip();
        audioSource.Play(0);
        Collider collider = gameObject.GetComponent<BoxCollider>();
        collider.enabled = !collider.enabled;
        LevelGenerator.instance.triggerSpawn("SpawnRandom", gameObject.gameObject);
        LevelGenerator.instance.removeObjectFromList(gameObject.tag, gameObject);
    }



    

    
}
