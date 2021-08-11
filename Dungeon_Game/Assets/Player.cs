using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller; 
    [SerializeField]
    public float moveSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        int left = Input.GetKey(KeyCode.A) ? 1 : 0;
        int right = Input.GetKey(KeyCode.D) ? 1 : 0;
        int up = Input.GetKey(KeyCode.W) ? 1 : 0;
        int down = Input.GetKey(KeyCode.S) ? 1 : 0;

        Vector3 move = new Vector3(right - left, 0, up - down);
        move = move.normalized * moveSpeed; 
        Vector3 velocity = move;

        _controller.Move(velocity * Time.deltaTime);
    }
}
