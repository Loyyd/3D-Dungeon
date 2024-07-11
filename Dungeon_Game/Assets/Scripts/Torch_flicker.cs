using UnityEngine;

public class Torch_flicker : MonoBehaviour
{

    float targetIntensity;
    public float minnumber;
    public float maxnumber;
    float speedchangevalue;

    bool increasing = true;
    float curIntensity;

    void Start()
    {
        speedchangevalue = 0.3f;
    }

    void Update()
    {
        if (increasing)
        {
            if (curIntensity <= targetIntensity)
            {
                curIntensity += speedchangevalue;
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
                curIntensity -= speedchangevalue;
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
            targetIntensity = Random.Range(minnumber, targetIntensity);
            increasing = false;
        }
        else
        {
            targetIntensity = Random.Range(targetIntensity, maxnumber);
            increasing = true;
        }
        //rand_number = Mathf.Round(rand_number * 10.0f) / 10.0f;
    }

    void SetSpeedChangeValue()
    {
        speedchangevalue = Random.Range(0.1f, 0.5f);
    }
}
