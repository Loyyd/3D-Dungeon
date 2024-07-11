using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_Manager : MonoBehaviour
{

    //[Header("Setup")]
    //public GameObject Button;
    // Start is called before the first frame update
    public Transform canvas;
    TextMeshProUGUI level;
    TextMeshProUGUI health;
    TextMeshProUGUI arrows;
    void Start()
    {
        level = canvas.Find("Level").GetComponent<TextMeshProUGUI>();
        arrows = canvas.Find("Arrows").GetComponent<TextMeshProUGUI>();
        health = canvas.Find("Health").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void UpdateUI(int lvl, int hp, int arrowCount)
    {
        level.text = "Level: " + lvl;
        arrows.text = "Arrows: " + arrowCount;
        health.text = "Health: " + hp;
    }

    public void Restart()
    {
        //Application.LoadLevel(Application.loadedLevel);
        //SceneManager.LoadScene("SampleScene");
    }
}
