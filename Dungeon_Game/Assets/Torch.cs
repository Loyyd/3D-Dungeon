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
        if (Vector3.Distance(Player.transform.position, transform.position) <= 1.5f)
        {
            Light.SetActive(true);

            Particle.SetActive(true);
        }
    }
}
