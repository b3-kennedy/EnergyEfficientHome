using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{

    public enum Colour {RED, GREEN, BLUE};
    public Colour colour;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Folder>())
        {
            if (other.GetComponent<Folder>().colour == colour)
            {
                Destroy(gameObject);
            }
        }
    }
}
