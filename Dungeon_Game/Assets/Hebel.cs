using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hebel : MonoBehaviour
{
    public Transform center;
    public int[] groups;
    public bool openOnly;
    public bool closeOnly;
    List<Gate> gates = new List<Gate>();
    Player player;
    // Start is called before the first frame update
    void Start()
    {        
        player = (Player)FindObjectOfType(typeof(Player));

        Gate[] allGates = (Gate[])FindObjectsOfType(typeof(Gate));
        foreach(Gate gate in allGates)
        {
            foreach (int group in groups) {
                if (gate.group == group && !gates.Contains(gate))
                {
                    gates.Add(gate);
                }
            }
        }
    }

    void Switch()
    {
        Debug.Log(player.transform.position);
        if(openOnly)
        {
            foreach(Gate gate in gates)
            {
                gate.Switch(true);
            }
        }
        else if (closeOnly)
        {
            foreach (Gate gate in gates)
            {
                gate.Switch(true);
            }
        } else
        {
            foreach(Gate gate in gates)
            {
                gate.Switch(!gate.isOpen);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Vector3.Distance(center.position, player.transform.position) < 0.3)
            {
                Switch();
            }
        }
    }
}
