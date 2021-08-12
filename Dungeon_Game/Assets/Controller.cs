using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;

public class Controller : MonoBehaviour
{
    int[,] level;
    public GameObject wall;
    public GameObject ground;

    // Start is called before the first frame update
    void Start()
    {
        level = LevelGenerator.Generate(20, 20);
        for (int i = 0; i < level.GetLength(0); i++)
        {
            for (int j = 0; j < level.GetLength(1); j++)
            {
                if (level[i, j] == 1)
                {
                    Debug.Log("Ground");
                    Instantiate(ground, new Vector3(i, 0, j), Quaternion.Euler(0, 0, 0));
                }
                else if (level[i, j] == 0)
                {
                    Debug.Log("Wall");
                    GameObject newWall = Instantiate(wall, new Vector3(i, 0, j), Quaternion.Euler(0, 0, 0));
                    newWall.GetComponent<Wall>().SetMesh(level,i,j);
                    Instantiate(ground, new Vector3(i, 0, j), Quaternion.Euler(0, 0, 0));
                }

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
