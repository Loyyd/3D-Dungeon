using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Mesh corner;
    public Mesh straight;
    public void SetMesh(RoomTile[,] level, int i, int j)
    {
        int adjCount = LevelGenerator.CountAdjacent(RoomTile.Wall, i, j) + LevelGenerator.CountAdjacent(RoomTile.Torch, i, j);
        if (adjCount == 2)
        {
            if ((LevelGenerator.GetLeft(i, j) == RoomTile.Wall || LevelGenerator.GetLeft(i, j) == RoomTile.Torch)
            && (LevelGenerator.GetRight(i, j) == RoomTile.Wall || LevelGenerator.GetRight(i, j) == RoomTile.Torch))
            {
                SetMesh("Straight");
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if ((LevelGenerator.GetAbove(i, j) == RoomTile.Wall || LevelGenerator.GetAbove(i, j) == RoomTile.Torch)
            && (LevelGenerator.GetBelow(i, j) == RoomTile.Wall || LevelGenerator.GetBelow(i, j) == RoomTile.Torch))
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
