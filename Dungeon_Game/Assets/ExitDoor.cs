using System.Collections;
using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{

    void Update()
    {
        
        Vector3 plyPos = Manager.instance.player.transform.position;
        Debug.Log((plyPos - transform.position).magnitude);
        if((plyPos - transform.position).magnitude < 2) {
            if(SceneManager.GetActiveScene().buildIndex+1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            //controller.nextLevel();
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
