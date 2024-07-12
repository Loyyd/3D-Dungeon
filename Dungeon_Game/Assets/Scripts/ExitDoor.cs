using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    private Controller controller;

    void Start() {
        controller = FindObjectOfType<Controller>();
    }

    void Update()
    {
        
        Vector3 plyPos = Manager.instance.player.transform.position;
        // Debug.Log((plyPos - transform.position).magnitude);
        
        var v = (plyPos - transform.position);
        var vDistance = new Vector2(v.x, v.z);
        if(vDistance.magnitude < 0.7) {
            if(SceneManager.GetActiveScene().buildIndex+1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            controller.nextLevel();
        }
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
