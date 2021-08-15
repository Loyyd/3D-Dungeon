using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{

    public GameObject Spikes;
    public Transform Player;
    Animator Animation;
    private void Start()
    {
        Animation = Spikes.GetComponent<Animator>();
        Player = GameObject.Find("Player").transform;
    }
    void Update()
    {
        
        if (Vector3.Distance(Player.position, transform.position) <= 0.5f)
        {
            Debug.Log("moin");
            print("Distance to other: ");
            Animation.Play("Spikes");
        }
    }
}
