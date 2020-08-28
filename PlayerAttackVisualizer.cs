using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackVisualizer : MonoBehaviour
{
    [SerializeField]
    private GameObject _attackFX = null;

    [SerializeField]
    private GameObject _critFX = null;

    [SerializeField]
    private GameObject _goldFX = null;

    private GameObject _visualInstance = null;
    private GameObject _critInstance = null;

    public static PlayerAttackVisualizer instance = null;

    private void Awake()
    {
        Enemy.OnDeath += triggerGoldFX;    
    }

    void Start()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        _visualInstance = Instantiate(_attackFX, transform.position, Quaternion.identity);
        _critInstance = Instantiate(_critFX, transform.position, Quaternion.identity);


    }

    public void triggerFX(string aType)
    {
        switch (aType)
        {
            case "normal":
                playFX(_visualInstance);
                break;
            case "crit":
                playFX(_critInstance);
                break;
        }
    }

    private void playFX(GameObject aVisual)
    {

        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100.0f))
        {
            if (hit.collider.tag=="enemy" || hit.collider.tag == "door")
            {
                aVisual.transform.position = hit.point;
                ParticleSystem ps = aVisual.GetComponentInChildren<ParticleSystem>();
                ps.Play();
            }
        }
        
    }

    private void triggerGoldFX(GameObject aGameObject)
    {
        GameObject goldInstance = Instantiate(_goldFX, aGameObject.transform.position, Quaternion.identity);
        ParticleSystem ps = goldInstance.GetComponent<ParticleSystem>();
        ps.Play();
        float particleDuration = ps.main.duration + ps.main.startLifetimeMultiplier;
        Destroy(goldInstance, particleDuration);


    }

    private void OnDisable()
    {
        Enemy.OnDeath -= triggerGoldFX;
    }
}
