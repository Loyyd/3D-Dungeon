using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{

    public GameObject Player;
    public GameObject Light;
    public GameObject Particle;
    
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 torchPos = Player.transform.position;
        torchPos.y += 1.675f;
        if (Vector3.Distance(torchPos, transform.Find("Torch Light").position) <= 0.8f)
        {
            Light.SetActive(true);

            Particle.SetActive(true);
        }
    }
}
