using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noted : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    public bool canPress;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!SpawnManager.Instance.start)
        {
            deactivated();
        }
        moveRight();
        if (Input.GetKeyDown(KeyCode.Space) && canPress)
        {
            deactivated();
            ScoreManager.Instance.addScore();
            canPress = false;
        }
    }

    public void moveRight()
    {
        rb.velocity = -transform.right * speed;
    }
    public void deactivated()  
    { 
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Activator"))
        {
            canPress = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Activator"))
        {
            canPress = false;
        }
    }
}
