using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Variations = null;


    private void OnEnable()
    {

        int randomIndex = Random.Range(0, Variations.Length);

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);

        }

        Variations[randomIndex].SetActive(true);



    }
}
