using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    private AudioSource _audioSource = null;

    public Inventory Inventory = new Inventory(0);

    public static Player instance = null;

    public static event System.Action OnDeath = delegate { };

    public override void Awake()
    {
        base.Awake();
        currentHealth = StatHealth.Value;

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

      

    }

    public void triggerSound()
    {
        _audioSource.clip = AudioLibrary.instance.getSlashClip();
        _audioSource.Play(0);
    }

    private void LateUpdate()
    {
        if (!isAlive)
        {
            OnDeath();
        }
    }




}
