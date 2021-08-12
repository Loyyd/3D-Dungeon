using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    public GameObject player;
    public GameObject controllerObject;
    Controller controller;

    void Start() {
        controllerObject.GetComponent<Controller>();
    }

    void Update()
    {
        Vector3 plyPos = player.transform.position;
        Debug.Log(plyPos - transform.position);
        if((plyPos - transform.position).magnitude < 2) {
            controller.newMap(20,20);
        }
    }
}
