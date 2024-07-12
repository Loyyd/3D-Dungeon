using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using Unity.AI.Navigation;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject levelObject;
    public GameObject wallPrefab;
    public GameObject groundPrefab;
    public GameObject torchPrefab;
    public GameObject spikesPrefab;
    public GameObject planePrefab;
    public GameObject exitPrefab;
    public GameObject skeletonPrefab;
    private GameObject player;
    private GameObject plane;

    [HideInInspector]
    public static int currentBrush;
    public static int maxHP = 100;
    public static int hp = 100;
    public static int arrows = 3;
    public static int levelNum = 1;

    // Start is called before the first frame update
    void Start()
    {       
        // Set the fog color to be blue
        RenderSettings.fogColor = Color.black;

        player = GameObject.Find("Player");
        plane = Instantiate(planePrefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
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
        Debug.Log("Level " + levelNum);
        newMap(18 + levelNum * 2, 18 + levelNum * 2);
    }
    public void newMap(int width, int height)
    {
        plane.GetComponent<BoxCollider>().size = new Vector3(width, 0.1f, height);
        plane.transform.position = new Vector3(width / 2, 0, height / 2);
        plane.GetComponent<NavMeshSurface>().BuildNavMesh();
        Destroy(levelObject, 0);
        levelObject = new GameObject("Level");
        LevelGenerator.level = LevelGenerator.Generate(width, height);
        Level level = LevelGenerator.level;
        Vector2Int _newPos;

        // CREATE EXIT
        List<Vector2Int> possiblePositions = LevelGenerator.freePosNotAtEntrance(2, 2);
        _newPos = possiblePositions[Random.Range(0, possiblePositions.Count)];
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

        // Spikes aren't functional yet
        // level.PlaceSpikes(10);

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
            Vector3 spawnPos;
            do {
                _newPos = LevelGenerator.RandomFreePos();
                spawnPos = new Vector3(_newPos.x, 0.4f, _newPos.y);
            } while ((player.transform.position - spawnPos).magnitude < 5f);
            Instantiate(skeletonPrefab, spawnPos, new Quaternion(), levelObject.transform);
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
