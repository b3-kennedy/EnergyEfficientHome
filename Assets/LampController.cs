using TMPro;
using UnityEngine;

public class LampController : MonoBehaviour
{
    public GameObject lampPopUp;

    bool isNearLamp;

    public bool isOn = true;

    public GameObject[] light;

    private void Update()
    {
        if (isNearLamp)
        {
            if (Input.GetKeyUp(KeyCode.O))
            {
                isOn = !isOn;
                foreach (GameObject obj in light)
                {
                    obj.SetActive(isOn);
                }
                lampPopUp.GetComponent<TextMeshPro>().text = "Press O to turn the lamp" + (isOn ? " off" : " on");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            lampPopUp.SetActive(true);
            lampPopUp.GetComponent<TextMeshPro>().text = "Press O to turn the lamp"+ (isOn ? " off" : " on");
            isNearLamp = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            lampPopUp.SetActive(false);
            isNearLamp = false;
        }
    }
}
