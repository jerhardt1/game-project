using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TreasureChest : MonoBehaviour
{
    [SerializeField]
    Animator animController = null;

    [SerializeField]
    ParticleSystem openingFX = null;

    [SerializeField]
    Collider _collider = null;

    [SerializeField]
    private int _minGold = 100;

    [SerializeField]
    private int _maxGold = 100;

    public static event Action<int> OnDropGold = delegate { };

    void TriggerAnimation()
    {
        if(animController != null)
        {
           animController.SetTrigger("open");
            openingFX.Play();
            PullParticles.instance.setParticlesToPull(openingFX);
            _collider.enabled = false;            
        }
    }

    private void OnMouseDown()
    {
        TriggerAnimation();
        OnDropGold(_goldDrop());
        LevelGenerator.instance.removeObjectFromList(gameObject.tag, gameObject);
    }

    private int _goldDrop()
    {
        int goldAmount = UnityEngine.Random.Range(_minGold, _maxGold);
        return goldAmount;
    }
}
