using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightProperties : MonoBehaviour
{
    public float inGameIntensity;
    new Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        light.intensity = inGameIntensity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
