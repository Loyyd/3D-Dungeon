using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public bool isLive = true;
    public GameObject player;
    public AudioClip hitSound;
    public AudioClip impactStoneSound;
    private AudioSource audioSource;

    void Start() {
        player = GameObject.Find("Player");
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void PlaySound(AudioClip clip) {
        audioSource.clip = clip;
        audioSource.Play();
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
        if (!isLive) return;

        var force = Vector3.Dot(collision.contacts[0].normal,collision.relativeVelocity) * GetComponent<Rigidbody>().mass;
        Debug.Log("Force: " + force);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PlaySound(hitSound);
            collision.gameObject.GetComponent<Enemy>().Hp -= 20;
        } else {
            PlaySound(impactStoneSound);
        }

        isLive = false;
    }
}
