using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TemperatureAlteringObject : MonoBehaviour
{
    public string objectName;
    public GameObject canvas;
    Interact interact;
    TextMeshProUGUI buttonText;
    Button button;
    GameObject player;


    private void Start()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Interact>())
        {
            player = other.gameObject;
            interact = other.GetComponent<Interact>();
            Window();
            Radiator();
            Jumper();
            RoomThermostat();
            TaskTrigger();
            Child();
            interact.interactText.gameObject.SetActive(true);
            interact.inTrigger = true;
            interact.heatObject = gameObject;
            
        }
    }


    public void UpdateText()
    {
        Window();
        Radiator();
        Jumper();
        RoomThermostat();
        TaskTrigger();
    }

    private void Child()
    {
        button = canvas.transform.GetChild(0).GetChild(0).GetComponent<Button>();
        canvas.transform.GetChild(0).GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(player.transform.position);

        if (GetComponent<ChildAIController>())
        {
            //interact.interactText.text = "Press 'E' to Send Child to Room";
            canvas.SetActive(true);
        }
    }

    void TaskTrigger()
    {
        button = canvas.transform.GetChild(0).GetChild(0).GetComponent<Button>();
        canvas.transform.GetChild(0).GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(player.transform.position);

        if (gameObject.CompareTag("FlyTask"))
        {
            button.onClick.AddListener(delegate { gameObject.GetComponent<TaskTrigger>().StartTask(); });
            canvas.SetActive(true);

        }
        else if (gameObject.CompareTag("WireTask"))
        {
            interact.interactText.text = "Press 'E' to Fix TV";
        }
        else if (gameObject.CompareTag("SortTask"))
        {
            interact.interactText.text = "Press 'E' to Sort Paper";
        }
        else if (gameObject.CompareTag("Work"))
        {
            
            if (!WorkTrigger.Instance.onCd)
            {
                canvas.SetActive(true);
                //interact.interactText.text = "Press 'E' to Work";
            }
        }
        else if (gameObject.CompareTag("Exercise"))
        {
            interact.interactText.text = "Press 'E' to Exercise";
        }
    }

    void RoomThermostat()
    {
        buttonText = canvas.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        canvas.transform.GetChild(0).GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(player.transform.position);

        if (GetComponent<RoomThermostat>())
        {
            canvas.SetActive(true);
            buttonText.text = "Change Room Temperature";
        }
    }

    void Jumper()
    {
        buttonText = canvas.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        canvas.transform.GetChild(0).GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(player.transform.position);

        if (GetComponent<Jumper>())
        {
            canvas.SetActive(true);

            buttonText.text = "Wear Jumper";
        }
    }

    void Radiator()
    {
        buttonText = canvas.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        canvas.transform.GetChild(0).GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(player.transform.position);

        if (GetComponent<Broken>() && GetComponent<Broken>().enabled)
        {
            canvas.SetActive(true);
            buttonText.text = "Fix " + objectName + " for £" + GetComponent<Broken>().fixCost.ToString();
            //interact.interactText.text = "Press 'E' to Fix " + objectName + " This will cost £" + GetComponent<Broken>().fixCost.ToString();
            return;
        }


        if (GetComponent<Radiator>() && GetComponent<Radiator>().isOn)
        {
            canvas.SetActive(true);
            buttonText.text = "Turn Off";
            //interact.interactText.text = "Press 'E' to Turn Radiator Off";
        }
        else if (GetComponent<Radiator>() && !GetComponent<Radiator>().isOn)
        {
            canvas.SetActive(true);
            buttonText.text = "Turn On";
            //interact.interactText.text = "Press 'E' to Turn Radiator On";
        }
    }

    void Window()
    {
        buttonText = canvas.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        canvas.transform.GetChild(0).GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(player.transform.position);

        if (GetComponent<Broken>() && GetComponent<Broken>().enabled)
        {
            canvas.SetActive(true);
            buttonText.text = "Fix " + objectName + " for £" + GetComponent<Broken>().fixCost.ToString();
            return;
        }


        if (GetComponent<Window>() && GetComponent<Window>().isOn)
        {
            canvas.SetActive(true);
            buttonText.text = "Close";
        }
        else if (GetComponent<Window>() && !GetComponent<Window>().isOn)
        {
            canvas.SetActive(true);
            buttonText.text = "Open";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interact>())
        {
            other.GetComponent<Interact>().interactText.gameObject.SetActive(false);
            canvas.SetActive(false);
            other.GetComponent<Interact>().inTrigger = false;
            other.GetComponent<Interact>().heatObject = null;
        }
    }
}
