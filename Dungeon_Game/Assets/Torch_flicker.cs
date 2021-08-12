using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch_flicker : MonoBehaviour
{

    float rand_number;
    public float minnumber;
    public float maxnumber;

    public float startwert;
  
    void Start()
    {
        GetComponent<Light>().range = 7;
        startwert = 4;
        rand_number = 4;
    }

    void Update()
    {
        startwert = rand_number;
        GetComponent<Light>().intensity = startwert;
        rand_number = Random.Range(minnumber, maxnumber);
        Fade();
        //GetComponent<Light>().intensity = rand_number;
       
    }

    void Fade()
    {
        if (startwert > rand_number)
        {
            for (float ft = startwert; ft >= rand_number; ft -= 0.1f)
            {
                GetComponent<Light>().intensity = ft;
                //Color c = renderer.material.color;
                //c.a = ft;
                //renderer.material.color = c;
            }
        }

        if (startwert > rand_number)
        {
            for (float ft = startwert; ft >= rand_number; ft -= 0.1f)
            {
                GetComponent<Light>().intensity = ft;
                //Color c = renderer.material.color;
                //c.a = ft;
                //renderer.material.color = c;
            }
        }
    }
}
