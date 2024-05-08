using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateExercise : MonoBehaviour
{
    public GameObject exerciseScreen;
    public GameObject endUI;

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMove>())
        {
            exerciseScreen.SetActive(false);
            endUI.SetActive(false);
            SpawnManager.Instance.Reset();
        }
    }
}
