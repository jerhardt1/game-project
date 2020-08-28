using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLibrary : MonoBehaviour
{
    public static AudioLibrary instance = null;

    public List<AudioClip> doorSlamClips = new List<AudioClip>();
    public List<AudioClip> doorBreakClips = new List<AudioClip>();
    public List<AudioClip> slashClips = new List<AudioClip>();

    private int doorSlamCount = 0;
    private int doorBreakCount = 0;
    private int slashCount = 0;

    void Awake()
    {
        Object[] doorSlamObjects = Resources.LoadAll("Sounds/Door/Slam", typeof(AudioClip));
        Object[] doorBreakObjects = Resources.LoadAll("Sounds/Door/Break", typeof(AudioClip));
        Object[] slashObjects = Resources.LoadAll("Sounds/Attack", typeof(AudioClip));


        foreach (AudioClip clip in doorSlamObjects)
        {
            AudioClip c = (AudioClip)clip;
            doorSlamClips.Add(c);
            doorSlamCount++;
        }

        foreach (AudioClip clip in doorBreakObjects)
        {
            AudioClip c = (AudioClip)clip;
            doorBreakClips.Add(c);
            doorBreakCount++;
        }

        foreach (AudioClip clip in slashObjects)
        {
            AudioClip c = (AudioClip)clip;
            slashClips.Add(c);
            slashCount++;
        }

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public AudioClip getRandomClip()
    {
        int randomIndex = Random.Range(0, doorSlamCount);
        return doorSlamClips[randomIndex];
    }

    public AudioClip getDoorBreakClip()
    {
        int randomIndex = Random.Range(0, doorBreakCount);
        return doorBreakClips[randomIndex];
    }

    public AudioClip getSlashClip()
    {
        int randomIndex = Random.Range(0, slashCount);
        return slashClips[randomIndex];
    }
}
