using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller_Single : MonoBehaviour
{
    Transform playerTransform;
    public Vector3 cameraOffset = new Vector3(0,8,-2.5f);
    public Vector3 cameraRotation = new Vector3(70,0,0);

    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.03f;
    public GameObject follower;


    void Start()
    {
        Application.targetFrameRate = 60;
        playerTransform = follower.GetComponent<Transform>();
        //_cameraOffset = transform.position - PlayerTransform.position;
        
    }

    void Update()
    {
        
            Vector3 newPos = follower.transform.position + cameraOffset;

            transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(cameraRotation), smoothFactor);
        


        //if (PlayerTransform != null)
        //{
        //    Vector3 newPos = PlayerTransform.position + _cameraOffset;

        //    transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
        //}
    }
    /*

    public void GetSelectedUnit()
    {
        GameObject[] Units = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject ply in Units)
        {
            if (ply.GetComponent<isSelected>().IsSelected == true)
            {
                selectedUnit = ply;
                return;
            }
            if (selectedUnit == null)
            {
                selectedUnit = null;
            }
        }
    }
    */
}