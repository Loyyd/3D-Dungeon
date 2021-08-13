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
    public GameObject exitPrefab;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        newMap(20, 20);
    }

    public void newMap(int width, int height)
    {
        Destroy(levelObject, 0);
        levelObject = new GameObject("Level");
        LevelGenerator.level = LevelGenerator.Generate(width, height);
        Level level = LevelGenerator.level;
        Vector2Int _newPos;


        List<Vector2Int> possiblePositions = new List<Vector2Int>();
        for (int i = 0; i < level.tiles.GetLength(0) - 1; i++)
        {
            for (int j = 0; j < level.tiles.GetLength(1) - 1; j++)
            {
                if (LevelGenerator.GetTile(i, j) == RoomTile.Ground
                && LevelGenerator.GetTile(i + 1, j) == RoomTile.Ground
                && LevelGenerator.GetTile(i, j + 1) == RoomTile.Ground
                && LevelGenerator.GetTile(i + 1, j + 1) == RoomTile.Ground)
                {
                    possiblePositions.Add(new Vector2Int(i, j));
                }
            }
        }
        // CREATE EXIT
        System.Random random = new System.Random();
        _newPos = possiblePositions[(int)UnityEngine.Random.Range(0,possiblePositions.Count)];
        // Update tiles array
        level.tiles[_newPos.x, _newPos.y] = RoomTile.Exit;
        level.tiles[_newPos.x + 1, _newPos.y] = RoomTile.Exit;
        level.tiles[_newPos.x, _newPos.y + 1] = RoomTile.Exit;
        level.tiles[_newPos.x + 1, _newPos.y + 1] = RoomTile.Exit;
        // Instantiate stairs object
        GameObject exit = Instantiate(exitPrefab, new Vector3(_newPos.x + 0.5f, 0, _newPos.y + 0.5f), Quaternion.Euler(0, 0, 0));
        exit.GetComponent<ExitDoor>().player = player;
        exit.GetComponent<ExitDoor>().controller = this;
        exit.transform.SetParent(levelObject.transform, false);


        // PLACE OBJECTS
        level.PlaceTorches(1);

        // SPAWN GROUND AND WALLS
        for (int i = 0; i < level.tiles.GetLength(0); i++)
        {
            for (int j = 0; j < level.tiles.GetLength(1); j++)
            {
                if (level.tiles[i, j] == RoomTile.Ground)
                {
                    // Spawn Ground
                    GameObject newGround = Instantiate(groundPrefab, new Vector3(i, 0, j), Quaternion.Euler(0, 0, 0));
                    newGround.transform.SetParent(levelObject.transform, false);
                }

                else if (level.tiles[i, j] == RoomTile.Wall)
                {
                    // Spawn Wall
                    GameObject newWall = Instantiate(wallPrefab, new Vector3(i, 0, j), Quaternion.Euler(0, 0, 0));
                    newWall.GetComponent<Wall>().SetMesh(level.tiles, i, j);
                    newWall.transform.SetParent(levelObject.transform, false);

                    GameObject newGround = Instantiate(groundPrefab, new Vector3(i, 0, j), Quaternion.Euler(0, 0, 0));
                    newGround.transform.SetParent(levelObject.transform, false);
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
                    GameObject o = Instantiate(torchPrefab, new Vector3(i, 0, j), Quaternion.Euler(0, level.objectAngles[i, j], 0));
                    o.transform.SetParent(levelObject.transform, false);
                }
            }
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
        if (Input.GetKeyDown(KeyCode.N))
        {
            newMap(20, 20);
        }
    }
}
