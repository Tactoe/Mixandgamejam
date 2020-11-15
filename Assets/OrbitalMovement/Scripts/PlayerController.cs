using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Image staminaBar;
    public float staminaDepletion, staminaRecovery;
    public bool canMove = true;
    public bool canJump = false;
    bool isSprinting;

    float stamina = 100;
    [Space]

    public float Velocity;
    [Space]

    public float InputX;
    public float InputZ;
    public Vector3 desiredMoveDirection;
    public bool blockRotationPlayer;
    public float desiredRotationSpeed = 0.1f;
    public Animator anim;
    public float Speed;
    public float allowPlayerRotation = 0.1f;
    public Camera cam;
    public CharacterController controller;
    public bool isGrounded;

    [Header("Animation Smoothing")]
    [Range(0, 1f)]
    public float HorizontalAnimSmoothTime = 0.2f;
    [Range(0, 1f)]
    public float VerticalAnimTime = 0.2f;
    [Range(0, 1f)]
    public float StartAnimTime = 0.3f;
    [Range(0, 1f)]
    public float StopAnimTime = 0.15f;

    public float verticalVel;
    private Vector3 moveVector;

    public float moveSpeed, rotationSpeed;

	private Vector3 moveDirection;
    Rigidbody rb;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    void Update () 
	{
        InputMagnitude();
	}

    void InputMagnitude()
    {
        //Calculate Input Vectors
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        //anim.SetFloat ("InputZ", InputZ, VerticalAnimTime, Time.deltaTime * 2f);
        //anim.SetFloat ("InputX", InputX, HorizontalAnimSmoothTime, Time.deltaTime * 2f);

        //Calculate the Input Magnitude
        Speed = new Vector2(InputX, InputZ).sqrMagnitude;

        //Physically move player
        stamina += Time.deltaTime * (Speed > 0.5f ? -staminaDepletion : staminaRecovery) * (isSprinting ? 2 : 1);
        stamina = Mathf.Clamp(stamina, 0, 100);
        Vector3 tmp = staminaBar.transform.localScale;
        tmp.x = stamina / 100;
        staminaBar.transform.localScale = tmp;

        if (Speed > allowPlayerRotation)
        {
            anim.SetFloat("Blend", Speed, StartAnimTime, Time.deltaTime);
            PlayerMoveAndRotation();
        }
        else if (Speed < allowPlayerRotation)
        {
            anim.SetFloat("Blend", Speed, StopAnimTime, Time.deltaTime);
        }
    }

    void PlayerMoveAndRotation()
    {
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");
        var forward = transform.forward;
        var right = transform.right;

        forward.y = 0f;
        right.y = 0f;


        forward.Normalize();
        right.Normalize();

        desiredMoveDirection = forward * InputZ + right * InputX;
        Debug.DrawRay(transform.position, forward * 10, Color.green);
        Debug.DrawRay(transform.position, right * 10, Color.red);
        moveDirection = new Vector3(InputX, 0, InputZ).normalized;
    }

    Vector3 lastDirection;
    void FixedUpdate () 
	{
		rb.MovePosition(rb.position + transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
        Quaternion xRotation = Quaternion.LookRotation(moveDirection);
        xRotation.y = 0;
        

        //if (xRotation.x != 0)
        //{
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed * Time.deltaTime);
        //    rb.MoveRotation(Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed * Time.deltaTime));
        //}
        if ((rb.velocity.magnitude > 0 && moveDirection != Vector3.zero))
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            Quaternion newRotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            rb.MoveRotation(newRotation);
            transform.rotation = newRotation;
            lastDirection = moveDirection;
        }

        if (!(Mathf.Abs(InputX) > 0.9 || Mathf.Abs(InputZ) > 0.9))
        {
            Repositioning();
        }
    }

    public void Repositioning()
    {
        if (lastDirection != Vector3.zero)
        {
            lastDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(lastDirection);
            Quaternion newRotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            rb.MoveRotation(newRotation);
            transform.rotation = newRotation;
        }
    }
}