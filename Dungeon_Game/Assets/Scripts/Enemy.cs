using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Controller controller;
    Transform player;
    int hitIntervall = 30;
    int hitCount;
    public int Hp = 100;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Controller").GetComponent<Controller>();
        player = GameObject.Find("Player").transform;

        hitCount = hitIntervall;
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }

        if (Vector3.Distance(transform.position, player.position) < 1.3f)
        {
            if (hitCount > 0)
            {
                hitCount--;
            }
            else
            {
                player.GetComponent<Player>().TakeDamageSound();
                Controller.hp -= 20;
                hitCount = hitIntervall;
            }
        } else {
            hitCount = hitIntervall;
        }
    }
}
