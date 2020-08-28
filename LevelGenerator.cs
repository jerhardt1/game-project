using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public GameObject initialObjectToSpawn = null;

    private GameObject currentRoom;

    private ObjectSpawner objectSpawner;

    public int ReceivedGold = 0;

    private List<GameObject> currentDoors = new List<GameObject>();
    private List<GameObject> currentRooms = new List<GameObject>();
    private List<GameObject> currentEnemies = new List<GameObject>();
    private List<GameObject> currentChests = new List<GameObject>();


    public static LevelGenerator instance = null;

    private void AddGold(int aAmount)
    {
        ReceivedGold += aAmount;
    }

    void Awake()
    {
        objectSpawner = gameObject.GetComponent<ObjectSpawner>();

        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        Enemy.OnDropGold += AddGold;
        TreasureChest.OnDropGold += AddGold;

    }

    private void Start()
    {
        Initialize();

    }


    public void Initialize()
    {
        if (initialObjectToSpawn != null)
        {
            objectSpawner.InitialSpawn(initialObjectToSpawn);
        }
        else
        {
            Debug.Log("Starting Object has not been defined!");
        }
    }

    public void setCurrentRoom(GameObject room)
    {
        currentRoom = room;
    }

    public void deactivateDoors()
    {
        foreach (GameObject door in currentDoors)
        {
            Collider collider = door.GetComponent<BoxCollider>();
            collider.enabled = false;
        }
    }

    public void activateDoors()
    {
        foreach (GameObject door in currentDoors)
        {
            if (door!= null)
            {
                Collider collider = door.GetComponent<BoxCollider>();
                collider.enabled = true;
            }
            else
            {
                Debug.Log("Fatal Error: No doors in pool to activate!");
            }
        }
    }

    public void updateCamera(GameObject newLocation)
    {
        GameObject objectToLookAt = GameObject.Find(newLocation.name +  "/FocusPoint");
        PlayerController.instance.FocusAt(objectToLookAt);
    }

    public void moveCamera (GameObject positionToMoveTo, GameObject objectToLookAt)
    {
        PlayerController.instance.MoveToNext(positionToMoveTo, objectToLookAt);
        setCurrentRoom(positionToMoveTo);
    }

    public void triggerSpawn(string spawnToPerform, GameObject locationToPerform)
    {
        switch (spawnToPerform)
        {
            case "SpawnRandom":
                objectSpawner.SpawnRandom(locationToPerform);
                break;
        }
    }

    public void addObjectToList(string objectType, GameObject objectToBeAdded)
    {
        switch(objectType){
            case "door":
                currentDoors.Add(objectToBeAdded);
                break;
            case "room":
                currentRooms.Add(objectToBeAdded);
                break;
            case "enemy":
                currentEnemies.Add(objectToBeAdded);
                
                break;
            case "treasure":
                currentChests.Add(objectToBeAdded);
                break;
        }
        
    }

    public void checkForEnemies()
    {
        if (currentEnemies.Count == 0)
        {
            activateDoors();
        }
    }
    public void removeObjectFromList(string objectType, GameObject objectToBeRemoved)
    {
        switch (objectType)
        {
            case "door":
                currentDoors.Remove(objectToBeRemoved);
                break;
            case "room":
                currentRooms.Remove(objectToBeRemoved);
                break;
            case "enemy":
                currentEnemies.Remove(objectToBeRemoved);
                break;
            case "treasure":
                currentChests.Remove(objectToBeRemoved);
                break;

        }
    }


}
