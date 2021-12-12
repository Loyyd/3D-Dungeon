using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public bool isOpen = true;
    public int group;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Switch(bool open)
    {
        if(open)
        {
            isOpen = true;
            gameObject.SetActive(true);
        } else
        {
            isOpen = false;
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
