using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Mesh corner;
    public Mesh straight;
    public void SetMesh(int[,] level, int i, int j)
    {
        int adjCount = LevelGenerator.countAdjacent(0, i, j);
        if (adjCount == 2)
        {
            if (LevelGenerator.getLeft(i, j) == 0 && LevelGenerator.getRight(i, j) == 0)
            {
                SetMesh("Straight");
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (LevelGenerator.getAbove(i, j) == 0 && LevelGenerator.getBelow(i, j) == 0)
            {
                SetMesh("Straight");
            }
            else
            {
                SetMesh("Corner");
            }
        }
        else
        {
            SetMesh("Corner");
        }
    }
    public void SetMesh(string name)
    {
        if (name == "Corner")
        {
            transform.GetChild(0).GetComponent<MeshFilter>().mesh = corner;
        }
        if (name == "Straight")
        {
            transform.GetChild(0).GetComponent<MeshFilter>().mesh = straight;
        }
    }
}
