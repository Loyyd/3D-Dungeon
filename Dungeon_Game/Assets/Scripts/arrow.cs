using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public bool isLive = true;
    public GameObject player;

    void Start() {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLive && (transform.position - player.transform.position).magnitude < 1)
        {
            player.GetComponent<Player>().pickUpArrow.Play();
            Destroy(gameObject);
            Controller.arrows++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Hp -= 20;
        }

        isLive = false;
    }
}
