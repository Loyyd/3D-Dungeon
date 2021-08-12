using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject levelObject;
    public GameObject wallPrefab;
    public GameObject groundPrefab;
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
        for (int i = 0; i < level.tiles.GetLength(0); i++)
        {
            for (int j = 0; j < level.tiles.GetLength(1); j++)
            {
                if (level.tiles[i, j] == RoomTile.Ground)
                {
                    //Debug.Log("Ground");
                    GameObject newGround = Instantiate(groundPrefab, new Vector3(i, 0, j), Quaternion.Euler(0, 0, 0));
                    newGround.transform.SetParent(levelObject.transform, false);
                }

                else if (level.tiles[i, j] == RoomTile.Wall)
                {
                    //Debug.Log("Wall");
                    GameObject newWall = Instantiate(wallPrefab, new Vector3(i, 0, j), Quaternion.Euler(0, 0, 0));
                    newWall.GetComponent<Wall>().SetMesh(level.tiles, i, j);
                    newWall.transform.SetParent(levelObject.transform, false);

                    GameObject newGround = Instantiate(groundPrefab, new Vector3(i, 0, j), Quaternion.Euler(0, 0, 0));
                    newGround.transform.SetParent(levelObject.transform, false);
                }
            }
        }
        // Create Exit
        Vector2Int newPos = LevelGenerator.RandomFreePos();
        GameObject exit = Instantiate(exitPrefab, new Vector3(newPos.x, 0, newPos.y), Quaternion.Euler(0, 0, 0));
        exit.GetComponent<ExitDoor>().player = player;
        exit.GetComponent<ExitDoor>().controller = this;
        exit.transform.SetParent(levelObject.transform, false);

        // Set Player Pos
        newPos = LevelGenerator.RandomFreePos();
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = new Vector3(newPos.x, 0, newPos.y);
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
