using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    public bool isOpen = true;
    public int group;
    public GameObject child1;
    public GameObject child2;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    public void Switch(bool open)
    {
        if(open)
        {
            isOpen = true;
            child1.GetComponent<BoxCollider>().enabled = true;
            child2.GetComponent<BoxCollider>().enabled = true;
            anim.Play("anim_close_open");

        } else
        {
            isOpen = false;
            child1.GetComponent<BoxCollider>().enabled = false;
            child2.GetComponent<BoxCollider>().enabled = false;
            anim.Play("anim_gate_close");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
