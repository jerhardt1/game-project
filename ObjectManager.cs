using System.Collections.Generic;
using UnityEngine;

public class ObjectManager
{
    public List<GameObject> PossibleObjects { get { return _possibleObjects; } }
    public List<GameObject> PossibleEnemies { get { return _possibleEnemies; } }
    public List<GameObject> PossibleDoors { get { return _possibleDoors; } }
    public List<GameObject> PossibleChests { get { return _possibleChests; } }


    private List<GameObject> _possibleObjects = new List<GameObject>();
    private  List<GameObject> _possibleEnemies = new List<GameObject>();
    private  List<GameObject> _possibleDoors = new List<GameObject>();
    private  List<GameObject> _possibleChests = new List<GameObject>();

    public int ListCount { get { return _listCount; } }
    public int EnemyCount { get { return _enemyCount; } }
    public int DoorCount { get { return _doorCount; } }
    public int ChestCount { get { return _chestCount; } }


    private int _listCount = 0;
    private  int _enemyCount = 0;
    private  int _doorCount = 0;
    private int _chestCount = 0;

    public ObjectManager()
    {
        Initialize();
    }


    private void Initialize()
    {
        Object[] subListObjects = Resources.LoadAll("Prefabs/Rooms", typeof(GameObject));
        Object[] subListEnemies = Resources.LoadAll("Prefabs/Enemies", typeof(GameObject));
        Object[] subListDoors = Resources.LoadAll("Prefabs/Doors", typeof(GameObject));
        Object[] subListChests = Resources.LoadAll("Prefabs/TreasureChests", typeof(GameObject));


        foreach (GameObject subListObject in subListObjects)
        {
            GameObject lo = (GameObject)subListObject;
            _possibleObjects.Add(lo);
            _listCount++;
        }

        foreach (GameObject subListEnemy in subListEnemies)
        {
            GameObject lo = (GameObject)subListEnemy;
            _possibleEnemies.Add(lo);
            _enemyCount++;
        }

        foreach (GameObject subListDoor in subListDoors)
        {
            GameObject lo = (GameObject)subListDoor;
            _possibleDoors.Add(lo);
            _doorCount++;
        }

        foreach (GameObject subListChest in subListChests)
        {
            GameObject lo = (GameObject)subListChest;
            _possibleChests.Add(lo);
            _chestCount++;
        }
    }


}
