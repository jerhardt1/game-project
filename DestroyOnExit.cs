using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnExit : MonoBehaviour
{
    private LevelGenerator levelGenerator;

    void Awake()
    {
        levelGenerator = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>();   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "room")
        {
            Destroy(other.gameObject);
            levelGenerator.removeObjectFromList(other.gameObject.tag, other.gameObject);
        }
    }

}
