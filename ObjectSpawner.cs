using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private int _enemySpawnChance = 60; 

    private float _spawnRange = 2f;

    private ObjectManager _objectManager = null;

    public GameObject test = null;

    private void Awake()
    {
        _objectManager = new ObjectManager();
    }
    public void InitialSpawn(GameObject aObjectToSpawn)
    {
        Debug.Log("Hello");

        GameObject spawnObject = Instantiate(aObjectToSpawn, transform.position, transform.rotation);
        LevelGenerator.instance.addObjectToList(spawnObject.tag, spawnObject);
        LevelGenerator.instance.moveCamera(spawnObject, spawnObject.transform.Find("FocusPoint").gameObject);
        spawnDoors(spawnObject);
    }

    public void SpawnRandom(GameObject aLocationToSpawn)
    {
        int randomInt = Random.Range(0, _objectManager.ListCount);

        int randValue = Random.Range(0, 100);
        GameObject spawnObject = Instantiate(_objectManager.PossibleObjects[randomInt], aLocationToSpawn.transform.position, aLocationToSpawn.transform.rotation);
        spawnDoors(spawnObject);
        LevelGenerator.instance.moveCamera(spawnObject, spawnObject.transform.Find("FocusPoint").gameObject);

        if (randValue < _enemySpawnChance)
        {
            GameObject spawningPoints = spawnObject.transform.Find("SpawningPoints").gameObject;
            SpawnEnemy(spawningPoints);
        }
        else
        {
            GameObject treasurePoints = spawnObject.transform.Find("TreasurePoints").gameObject;
            SpawnTreasureChest(treasurePoints);
        }
    }

    void spawnDoors(GameObject aRoom)
    {
        foreach (Transform child in aRoom.transform)
        {
            if (child.tag == "door" && child.childCount == 0)
            {
                SpawnRandomDoor(child.gameObject);
            }
        }
        
    }

    void DebugCamera()
    {
        Camera camera = Camera.main;
        Vector3 viewPos = camera.WorldToViewportPoint(camera.transform.position);
        Vector3 foreignPos = camera.WorldToViewportPoint(test.transform.position);
        Debug.Log(viewPos);
        Debug.Log(foreignPos);
    }


    void SpawnEnemy(GameObject aParent)
    {
        bool hasEnemies = false;


        int spawnSockets = Random.Range(0, aParent.transform.childCount);

        foreach (Transform child in aParent.transform)
        {
            if (child.gameObject.tag == "enemy" && spawnSockets >= 0)
            {
                hasEnemies = true;
                Vector3 lookVector = gameObject.transform.position - child.position;
                lookVector.y = child.position.y;
                Quaternion spawnRotation = Quaternion.LookRotation(lookVector);
                int randomInt = Random.Range(0, _objectManager.EnemyCount);
                GameObject spawnEnemy = Instantiate(_objectManager.PossibleEnemies[randomInt], randomPosition(child), randomRotation());


                spawnEnemy.transform.parent = child;
                LevelGenerator.instance.addObjectToList(spawnEnemy.tag, spawnEnemy);
                spawnSockets--;
            }
        }

        if (hasEnemies == true)
        {
            LevelGenerator.instance.deactivateDoors();
        }
        
    }



    void SpawnTreasureChest(GameObject aParent)
    {
        int randomIndex = Random.Range(0, _objectManager.ChestCount);

        int spawnSockets = Random.Range(0, aParent.transform.childCount);
        foreach (Transform child in aParent.transform)
        {
            if (child.gameObject.tag == "treasure" && spawnSockets >= 0)
            {

                GameObject spawnObject = Instantiate(_objectManager.PossibleChests[randomIndex], child.transform.position, child.transform.rotation);
                spawnObject.transform.parent = child.transform; // make it a child of the parent
                LevelGenerator.instance.addObjectToList(spawnObject.tag, spawnObject);
            }
        }

    }

    public void SpawnRandomDoor(GameObject aParent)
    {
        int randomIndex = Random.Range(0, _objectManager.DoorCount);

        GameObject spawnObject = Instantiate(_objectManager.PossibleDoors[randomIndex], aParent.transform.position, aParent.transform.rotation);
        spawnObject.transform.parent = aParent.transform; // make it a child of the parent
        LevelGenerator.instance.addObjectToList(spawnObject.tag, spawnObject);

    }

    Vector3 randomPosition(Transform aParent)
    {
        Vector3 position = new Vector3(Random.Range((-1 * _spawnRange), _spawnRange), 0f, Random.Range((-1 * _spawnRange), _spawnRange));
        position = position + aParent.position;
             
        return position;
    }

    Quaternion randomRotation()
    {
        Quaternion rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        return rotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DebugCamera();
        }
    }



}
