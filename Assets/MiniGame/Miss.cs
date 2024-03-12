using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miss : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Noted"))
        {
            other.GetComponent<Noted>().deactivated();
            ScoreManager.Instance.resetCombo();
        }
    }
}
