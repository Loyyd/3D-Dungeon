using UnityEngine;

public class Torch_flicker : MonoBehaviour
{

    float targetIntensity;
    public float minnumber;
    public float maxnumber;

    bool increasing = true;
    float curIntensity;

    void Update()
    {
        if (increasing)
        {
            if (curIntensity <= targetIntensity)
            {
                curIntensity += 0.3f;
                GetComponent<Light>().intensity = curIntensity;
            }
            else
            {
                SetTargetValue();
            }
        }
        else
        {
            if (curIntensity >= targetIntensity)
            {
                curIntensity -= 0.3f;
                GetComponent<Light>().intensity = curIntensity;
            }
            else
            {
                SetTargetValue();
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
}
