using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Druckplatte : MonoBehaviour
{
    public int[] groups;
    List<Gate> gates = new List<Gate>();
    int objectsOnTop = 0;

    // Start is called before the first frame update
    void Start()
    {
        Gate[] allGates = (Gate[])FindObjectsOfType(typeof(Gate));
        foreach (Gate gate in allGates)
        {
            foreach (int group in groups)
            {
                if (gate.group == group && !gates.Contains(gate))
                {
                    gates.Add(gate);
                }
            }
        }
    }

    private void Update()
    {
    }

    void Switch()
    {
        {
            foreach (Gate gate in gates)
            {
                gate.Switch(!gate.isOpen);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag.Contains("Heavy"))
        {
            Debug.Log("Enter");
            if(objectsOnTop == 0)
            {
                transform.localScale -= new Vector3(0,0,30); 
                Switch();
            }
            objectsOnTop++;
        }
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.tag.Contains("Heavy"))
        {
            Debug.Log("Stay");
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.tag.Contains("Heavy"))
        {
            Debug.Log("Exit");
            if (objectsOnTop > 0)
            {
                transform.localScale += new Vector3(0, 0, 30);
                Switch();
            }
            objectsOnTop--;
        }
    }
}
