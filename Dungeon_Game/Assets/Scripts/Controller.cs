using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject levelObject;
    public GameObject wallPrefab;
    public GameObject groundPrefab;
    public GameObject torchPrefab;
    public GameObject spikesPrefab;
    public GameObject exitPrefab;
    public GameObject skeletonPrefab;
    public static GameObject player;
    public GameObject hey;
    public static GameObject playerqwet;
    public static GameObject playerasedf;
    public static GameObject playerwerq;
    public static GameObject playerzxcvb;

    [HideInInspector]
    public static int currentBrush;
    public static int maxHP = 100;
    public static int hp = 100;
    public static int arrows = 3;
    public static int levelNum = 1;

    // Start is called before the first frame update
    void Start()
    {
        levelNum--;
        nextLevel();
    }
    public void Restart()
    {
        levelNum = 0;
        hp = maxHP;
        nextLevel();
    }
    public void nextLevel()
    {
        levelNum++;
        newMap(18 + levelNum * 2, 18 + levelNum * 2);
    }
    public void newMap(int width, int height)
    {
        Destroy(levelObject, 0);
        levelObject = new GameObject("Level");
        LevelGenerator.level = LevelGenerator.Generate(width, height);
        Level level = LevelGenerator.level;
        Vector2Int _newPos;

        // CREATE EXIT
        List<Vector2Int> possiblePositions = LevelGenerator.freePosNotAtEntrance(2, 2);
        System.Random random = new System.Random();
        _newPos = possiblePositions[(int)UnityEngine.Random.Range(0, possiblePositions.Count)];
        // Update tiles array
        level.tiles[_newPos.x, _newPos.y] = RoomTile.Exit;
        level.tiles[_newPos.x + 1, _newPos.y] = RoomTile.Exit;
        level.tiles[_newPos.x, _newPos.y + 1] = RoomTile.Exit;
        level.tiles[_newPos.x + 1, _newPos.y + 1] = RoomTile.Exit;
        // Instantiate stairs object
        GameObject exit = Instantiate(exitPrefab, new Vector3(_newPos.x + 0.5f, 0, _newPos.y + 0.5f), Quaternion.Euler(0, 0, 0));
        exit.transform.SetParent(levelObject.transform, false);

        // PLACE OBJECTS
        level.PlaceTorches(1);
        level.PlaceSpikes(10);

        // SPAWN GROUND AND WALLS
        for (int i = 0; i < level.tiles.GetLength(0); i++)
        {
            for (int j = 0; j < level.tiles.GetLength(1); j++)
            {
                if (level.tiles[i, j] == RoomTile.Ground)
                {
                    // Spawn Ground
                    GameObject newGround = Instantiate(groundPrefab, new Vector3(i, 0, j), Quaternion.Euler(0, 0, 0), levelObject.transform);
                }

                else if (level.tiles[i, j] == RoomTile.Wall)
                {
                    // Spawn Wall
                    GameObject newWall = Instantiate(wallPrefab, new Vector3(i, 0, j), Quaternion.Euler(0, 0, 0), levelObject.transform);
                    newWall.GetComponent<Wall>().SetMesh(level.tiles, i, j);

                    GameObject newGround = Instantiate(groundPrefab, new Vector3(i, 0, j), Quaternion.Euler(0, 0, 0), levelObject.transform);
                }
            }
        }

        // SPAWN OBJECTS
        for (int i = 0; i < level.objects.GetLength(0); i++)
        {
            for (int j = 0; j < level.objects.GetLength(1); j++)
            {
                if (level.objects[i, j] == RoomTile.Torch)
                {
                    // Spawn Torch
                    GameObject o = Instantiate(torchPrefab, new Vector3(i, 0, j), Quaternion.Euler(0, level.objectAngles[i, j], 0), levelObject.transform);
                }
                if (level.objects[i, j] == RoomTile.Spikes)
                {
                    // Spawn Spikes
                    GameObject o = Instantiate(spikesPrefab, new Vector3(i, 0, j), Quaternion.Euler(0, level.objectAngles[i, j], 0), levelObject.transform);
                }
            }
        }

        // SPAWN ENEMIES
        int enemyCount = levelNum - 1;
        Debug.Log(levelNum);
        for (int i = 0; i < enemyCount; i++)
        {
            _newPos = LevelGenerator.RandomFreePos();
            Instantiate(skeletonPrefab, new Vector3(_newPos.x, 0, _newPos.y), new Quaternion(), levelObject.transform);
        }

        // Set Player Pos
        _newPos = LevelGenerator.RandomFreePos();
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = new Vector3(_newPos.x, 0, _newPos.y);
        player.GetComponent<CharacterController>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<UI_Manager>().UpdateUI(levelNum, hp, arrows);
        if (Input.GetKeyDown(KeyCode.N))
        {
            nextLevel();
        }
        if (hp <= 0)
        {
            Restart();
        }
    }
}
