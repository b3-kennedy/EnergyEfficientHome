using UnityEngine;
using UnityEngine.AI;
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

    [Header("Slope Collision")]
    public float downForce;
    public float slopeRayRange;

    public Animator anim;

    public float rotationSpeed;

    public bool clickToMove;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if (clickToMove)
        {
            ClickToMove();
        }
        else
        {
            NormalMove();
        }

        

    }

    void ClickToMove()
    {

        if (!GetComponent<NavMeshAgent>().enabled)
        {
            GetComponent<NavMeshAgent>().enabled = true;
        }

        GetComponent<NavMeshAgent>().speed = 7 * TimeManager.Instance.timeControlMultiplier;
        //GetComponent<NavMeshAgent>().angularSpeed = 700 * TimeManager.Instance.timeControlMultiplier;

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("move");
            Ray pos = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(pos, out var hit))
            {
                GetComponent<NavMeshAgent>().destination = hit.point;
            }
        }
    }

    void NormalMove()
    {

        if (GetComponent<NavMeshAgent>().enabled)
        {
            GetComponent<NavMeshAgent>().enabled = false;
        }

        move = new Vector3(Input.GetAxisRaw("Horizontal"), gravity, Input.GetAxisRaw("Vertical"));

        //Vector3 moveDir = (transform.forward * move.z + transform.right * move.x).normalized;

        Vector3 horMove = new Vector3(move.x, 0, move.z);

        Vector3 moveVec = new Vector3(move.x, gravity, move.z);

        ch.Move(moveVec * speed * Time.deltaTime);

        if (horMove != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        if (OnSlope())
        {
            Debug.Log("on slope");
            ch.Move(Vector3.down * downForce * Time.deltaTime);


        }

        if (move != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(move.normalized, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, rotationSpeed * Time.deltaTime);
        }

        transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z);
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
