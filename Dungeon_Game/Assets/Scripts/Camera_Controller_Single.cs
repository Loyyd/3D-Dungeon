using UnityEngine;

public class Camera_Controller_Single : MonoBehaviour
{
    public Vector3 cameraOffset = new Vector3(0,8,-2.5f);
    public Vector3 birdPerspectiveRotation = new Vector3(70,0,0);

    [Range(0.01f, 1.0f)]
    public float smoothFactor = 0.03f;
    public GameObject follower;
    private Player player;

    void Start()
    {
        Application.targetFrameRate = 60;
        player = FindObjectOfType<Player>();
    }

    void Update()
    {   
        if (!player.fpsCam) {
            Vector3 newPos = follower.transform.position + cameraOffset;
            transform.position = Vector3.Slerp(transform.position, newPos, smoothFactor);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(birdPerspectiveRotation), smoothFactor);
        }
    }
}