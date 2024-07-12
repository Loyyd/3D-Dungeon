using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
{
    public bool isLive = true;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Coll enter");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Coll enemy");
            Destroy(collision.gameObject);
        }

        isLive = false;
    }
}
