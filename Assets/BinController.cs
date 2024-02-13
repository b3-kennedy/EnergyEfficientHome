using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinController : MonoBehaviour
{
    public bool isRecycleBin;


    public event System.Action<Vector3> OnCorrect;

    public event System.Action notCorrect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        bool correct = (other.gameObject.GetComponent<RecycleItem>().isRecyclable == isRecycleBin);

        if (correct)
        {
            other.gameObject.SetActive(false);
            Debug.Log("good job");

            OnCorrect?.Invoke(transform.position);
        }
        else {  notCorrect?.Invoke(); }
    }

}
