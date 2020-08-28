using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    //public List<GameObject> Lights;
    public int[] table =
    {
        60,
        30,
        10
    };

    public int total = 0;
    public int randomNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in table)
        {
            total += item;
        }

        randomNumber = Random.Range(0, total);

        for (int i = 0; i < table.Length; i++)
        {
            if (randomNumber <= table[i])
            {
                //Lights[i].SetActive(true);
                return;
            }
            else
            {
                randomNumber -= table[i];
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
