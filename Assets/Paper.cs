using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{

    public enum Colour {RED, GREEN, BLUE};
    public Colour colour;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.GetComponent<Folder>())
        {
            Debug.Log("paper");
            if (other.GetComponent<Folder>().colour == colour)
            {
                Destroy(gameObject);
            }
        }
    }
}
