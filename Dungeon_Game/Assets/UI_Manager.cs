using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{

    [Header("Setup")]
    public GameObject Button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene("SampleScene");
    }
}
