using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject Player;
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 torchPos = Player.transform.position;
        torchPos.y += 1.675f;
        if (Vector3.Distance(torchPos, transform.position) <= 0.8f)
        {
            Debug.Log("HEy");
            //Light.SetActive(true);

            //Particle.SetActive(true);
        }
    }
}
