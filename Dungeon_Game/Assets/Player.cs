using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    public bool fpsCam = false;
    public float fpsCamHeight = 1;
    public float walkingSpeed = 3f;
    public float runningSpeed = 4.5f;
    public float jumpSpeed = 4.0f;
    public float gravity = 10.0f;
    public Camera playerCamera;
    public float lookSpeed = 6.0f;
    public float lookXLimit = 45.0f;
    public float Speed = 3.0F;
    public SkinnedMeshRenderer meshRenderer;
    public GameObject upwardPointer;
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    Vector3 lastPos = new Vector3(0, 0, 0);
    public AudioSource stepsSound;
    public AudioSource jump_up;
    public AudioSource jump_land;
    float rotationX = 0;
    bool landed = true;

    [SerializeField]
    private float _pushForce;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        //Debug.Log(transform.Find("Torch Light").position);
        characterController = GetComponent<CharacterController>();
        upwardPointer = Instantiate(upwardPointer);
        stepsSound = GetComponent<AudioSource>();

        if (fpsCam)
        {
            // Lock cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }



    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //if(!characterController.isGrounded && hit.transform.tag ==  "Wall")
        //{ }
        Rigidbody rb = hit.collider.attachedRigidbody;
        if(rb  != null && !rb.isKinematic)
        {
            rb.velocity = Vector3.Scale(hit.moveDirection, new Vector3(1,0,1)) * _pushForce;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (fpsCam)
            {
                fpsCam = false;
                playerCamera.cullingMask = playerCamera.cullingMask | (1 << 3); ;
            }
            else
            {
                fpsCam = true;
                playerCamera.cullingMask = playerCamera.cullingMask & ~(1 << 3);
            }
        }

        if (fpsCam)
        {
            // set cam pos to player pos
            Vector3 pos = transform.position;
            playerCamera.transform.position = new Vector3(pos.x, pos.y + fpsCamHeight, pos.z);
        }

        // We are grounded, so recalculate move direction based on axes
        upwardPointer.transform.rotation = Quaternion.Euler(0, playerCamera.transform.rotation.eulerAngles.y + 90, 0);
        Vector3 forward = upwardPointer.transform.TransformDirection(Vector3.forward);
        Vector3 right = upwardPointer.transform.TransformDirection(Vector3.right);

        GetComponent<UnitAnimator>().Run();

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        int leftSpeed = Input.GetKey(KeyCode.A) ? 1 : 0;
        int rightSpeed = Input.GetKey(KeyCode.D) ? 1 : 0;
        int upSpeed = Input.GetKey(KeyCode.W) ? 1 : 0;
        int downSpeed = Input.GetKey(KeyCode.S) ? 1 : 0;

        Vector3 move = new Vector3(canMove ? rightSpeed - leftSpeed : 0, 0, canMove ? downSpeed - upSpeed : 0);
        move = move.normalized * (isRunning ? runningSpeed : walkingSpeed);
        float curSpeedX = move.x;
        float curSpeedZ = move.z;

        float movementDirectionY = moveDirection.y;

        moveDirection = (forward * curSpeedX) + (right * curSpeedZ);
        if (!moveDirection.Equals(Vector2.zero))
        {
            stepsSound.enabled = true;
        }
        else
        {
            stepsSound.enabled = false;
        }

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
            jump_up.Play();
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            if (jump_up.isPlaying)
            {
                landed = false;
            }
            moveDirection.y -= gravity * Time.deltaTime;
        }
        else
        {
            if (!landed)
            {
                jump_land.Play();
                landed = true;
            }
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
        // Set rotation
        Vector3 _lastPos = new Vector3(lastPos.x, 0, lastPos.z);
        Vector3 _curPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 _dir = _curPos - _lastPos;

        // For 3rd person camera
        if (!fpsCam && _dir.magnitude / Time.deltaTime > 0.1f)
        {
            // // Rotation by moving direction
            // transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_dir), 0.1f);

            // Rotation by input direction
            Quaternion lookDirection = Quaternion.LookRotation(moveDirection);
            lookDirection = Quaternion.Euler(0, lookDirection.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, 0.1f);
        }

        // Player and Camera rotation
        if (canMove && fpsCam)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, transform.eulerAngles.y, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        lastPos = transform.position;
    }
}