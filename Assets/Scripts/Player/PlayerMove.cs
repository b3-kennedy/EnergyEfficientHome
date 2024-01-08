using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{

    CharacterController ch;
    public float speed;
    public float gravityMultiplier;

    Vector3 move;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float range;
    public float gravity;
    private Animator animator;

    [Header("Slope Collision")]
    public float downForce;
    public float slopeRayRange;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        ch = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Gravity();
        Move();
        GroundCheck();

    }

    private void Move()
    {
        move = new Vector3(Input.GetAxisRaw("Horizontal"), gravity, Input.GetAxisRaw("Vertical"));

        Vector3 moveDir = (transform.forward * move.z + transform.right * move.x).normalized;

        Vector3 moveVec = new Vector3(moveDir.x, gravity, moveDir.z);

        ch.Move(moveVec * speed * Time.deltaTime);

        if (OnSlope())
        {
            Debug.Log("on slope");
            ch.Move(Vector3.down * downForce * Time.deltaTime);

            if (moveDir == Vector3.zero)
            {
                animator.SetBool("isMoving", true);
            }

            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }

    

    void Gravity()
    {
        gravity = Mathf.Clamp(gravity, -9.81f, 0);
        if (!GroundCheck())
        {
            gravity -= Time.deltaTime * gravityMultiplier;
        }
        else
        {
            gravity = 0;
        }
    }

    bool GroundCheck()
    {
        return Physics.Raycast(groundCheck.position, Vector3.down, range);
    }

    bool OnSlope()
    {
        if (Physics.Raycast(groundCheck.position, Vector3.down, out RaycastHit hit, slopeRayRange))
        {
            if (hit.normal != Vector3.up)
            {
                return true;
            }
        }
        return false;

        

        
    }
}
