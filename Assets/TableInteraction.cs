using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TableInteraction : MonoBehaviour
{
    public GameObject canvas;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            canvas.SetActive(true);
            Debug.Log("near table");
            SceneManager.LoadScene("Pacman", LoadSceneMode.Additive);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            canvas.SetActive(false);
            SceneManager.UnloadSceneAsync("Pacman");
        }
    }
}
