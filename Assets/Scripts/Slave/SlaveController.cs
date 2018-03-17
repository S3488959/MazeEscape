using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class velocity
{
    public float forward;
    public float backward;
    public float sides;
    public float sprint;
    public float jump;
}

public class SlaveController : MonoBehaviour
{
    public velocity vel;
    private float currentVel;
    private Vector3 input;
    public float turnSmoothing = 0.06f;
    private Vector3 lastDirection;

    private bool isJumping;
    private bool isSprinting;
    private Vector2 capExtents;

    public Camera ThirdPersonCamera;                        
    public Camera FirstPersonCamera;
    private Camera currentCamera;

    public bool isFIrstPersonView = false;

    private Rigidbody rigid;
    private CapsuleCollider capsule;
    private Vector3 groundNormal;
    private Animator anim;
    public GameObject FullBody;
    public GameObject NoHead;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();

        capExtents = capsule.bounds.extents;

        ChangeView();
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        GetInput();
	}

    void FixedUpdate()
    {
        anim.SetBool("isGround", IsGrounded());
        if (IsGrounded())
        {
            anim.SetBool("isMoving", true);
            Rotate();
            //Move();
            AnimatorMove();
            Jump();
        }
    }

    private Vector3 GetCameraLook()
    {
        return currentCamera.transform.forward * input.z + currentCamera.transform.right * input.x;
    }

    void GetInput()
    {
        input = new Vector3
        {
            x = Input.GetAxis("Horizontal"),
            y = 0f,
            z = Input.GetAxis("Vertical")
        };

        if (Input.GetKeyDown("c"))
            ChangeView();
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            isJumping = true;
    }

    void AnimatorMove()
    {
        if (input == Vector3.zero)
        {
            anim.SetBool("isMoving", false);
        }
        
        anim.SetFloat("xVel", input.x);
        anim.SetFloat("zVel", input.z);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            //currentVel *= vel.sprint;
            anim.SetFloat("zVel", input.z * 2);
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
    }

    void Rotate()
    {
        // Get camera forward direction, without vertical component.
        Vector3 targetDirection = currentCamera.transform.forward;

        // Player is moving on ground, Y component of camera facing is not relevant.
        targetDirection.y = 0.0f;
        targetDirection = targetDirection.normalized;

        // Lerp current direction to calculated target direction.
        if ((IsMoving() && targetDirection != Vector3.zero))
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            Quaternion newRotation = Quaternion.Slerp(rigid.rotation, targetRotation, turnSmoothing);
            rigid.MoveRotation(newRotation);
            SetLastDirection(targetDirection);
        }
        // If idle, Ignore current camera facing and consider last moving direction.
        if (!(Mathf.Abs(input.x) > 0.9 || Mathf.Abs(input.z) > 0.9))
        {
            Repositioning();
        }
    }

    void Move()
    {
        UpdateCurrentVel(input);
        Vector3 desiredMove = GetCameraLook();
        desiredMove = Vector3.ProjectOnPlane(desiredMove, Vector3.up).normalized;

        desiredMove.x = desiredMove.x * currentVel;
        desiredMove.z = desiredMove.z * currentVel;
        desiredMove.y = desiredMove.y * currentVel;
        if (rigid.velocity.sqrMagnitude <
            (currentVel * currentVel))
        {
            rigid.AddForce(desiredMove, ForceMode.Impulse);
        }
    }

    public void UpdateCurrentVel(Vector3 input)
    {
        if (input == Vector3.zero) return;
        if (input.x > 0 || input.x < 0)
        {
            //strafe
            currentVel = vel.sides;
        }
        if (input.z < 0)
        {
            //backwards
            currentVel = vel.backward;
        }
        if (input.z > 0)
        {
            //forwards
            //handled last as if strafing and moving forward at the same time forwards speed should take precedence
            currentVel = vel.forward;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentVel *= vel.sprint ;
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
    }

    void Jump()
    {
        if(isJumping && IsGrounded() && !anim.GetBool("isJumping"))
        {
            anim.SetBool("isJumping", true);

            capsule.material.dynamicFriction = 0f;
            capsule.material.staticFriction = 0f;

            float velocity = 2f * Mathf.Abs(Physics.gravity.y) * vel.jump;
            velocity = Mathf.Sqrt(velocity);
            rigid.AddForce(Vector3.up * velocity, ForceMode.VelocityChange);
        }
        else if(anim.GetBool("isJumping"))
        {
            if((rigid.velocity.y < 0) && IsGrounded())
            {
                isJumping = false;

                capsule.material.dynamicFriction = 0.6f;
                capsule.material.staticFriction = 0.6f;

                anim.SetBool("isJumping", false);
            }
        }
        
    }

    public bool IsGrounded()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 2 * capExtents.x, Vector3.down);
        return Physics.SphereCast(ray, capExtents.x, capExtents.x + 0.2f);
    }
    bool IsMoving()
    {
        return (input.x != 0) || (input.z != 0);
    }

    void SetLastDirection(Vector3 direction)
    {
        lastDirection = direction;
    }

    void Repositioning()
    {
        if (lastDirection != Vector3.zero)
        {
            lastDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(lastDirection);
            Quaternion newRotation = Quaternion.Slerp(rigid.rotation, targetRotation, turnSmoothing);
            rigid.MoveRotation(newRotation);
        }
    }

    void ChangeView()
    {
        if(isFIrstPersonView)
        {
            FullBody.SetActive(false);
            NoHead.SetActive(true);

            ThirdPersonCamera.gameObject.SetActive(false);
            FirstPersonCamera.gameObject.SetActive(true);
            currentCamera = FirstPersonCamera;
        }
        else
        {
            FullBody.SetActive(true);
            NoHead.SetActive(false);

            ThirdPersonCamera.gameObject.SetActive(true);
            FirstPersonCamera.gameObject.SetActive(false);
            currentCamera = ThirdPersonCamera;
        }
        isFIrstPersonView = !isFIrstPersonView;
    }
}
