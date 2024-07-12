using UnityEngine;

public class Torch_flicker : MonoBehaviour
{

    float targetIntensity;
    public float min = 3.5f;
    public float max = 4f;
    public float minSpeed = 0.02f;
    public float maxSpeed = 0.1f;
    float speed = 0.3f;

    bool increasing = true;
    float curIntensity;

    void Start() {
        SetSpeedChangeValue();
    }

    void Update()
    {
        if (increasing)
        {
            if (curIntensity <= targetIntensity)
            {
                curIntensity += speed;
                GetComponent<Light>().intensity = curIntensity;
            }
            else
            {
                SetTargetValue();
                SetSpeedChangeValue();
            }
        }
        else
        {
            if (curIntensity >= targetIntensity)
            {
                curIntensity -= speed;
                GetComponent<Light>().intensity = curIntensity;
            }
            else
            {
                SetTargetValue();
                SetSpeedChangeValue();
            }
        }
    }

    void SetTargetValue()
    {
        if (increasing)
        {
            targetIntensity = Random.Range(min, targetIntensity);
            increasing = false;
        }
        else
        {
            targetIntensity = Random.Range(targetIntensity, max);
            increasing = true;
        }
        //rand_number = Mathf.Round(rand_number * 10.0f) / 10.0f;
    }

    void SetSpeedChangeValue()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }
}
