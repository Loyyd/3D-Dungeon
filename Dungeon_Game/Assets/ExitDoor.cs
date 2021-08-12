using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public GameObject player;
    public Controller controller;

    void Update()
    {
        Vector3 plyPos = player.transform.position;
        if((plyPos - transform.position).magnitude < 0.3) {
            controller.newMap(20,20);
        }
    }
}
