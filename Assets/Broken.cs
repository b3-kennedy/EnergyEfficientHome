using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Broken : MonoBehaviour
{


    public GameObject brokenIndicator;
    GameObject indicator;
    public float yHeight;
    [HideInInspector] public UnityEvent enable;
    [HideInInspector] public UnityEvent disable;
    public MonoBehaviour scriptToDisable;
    public float fixCost;

    private void OnEnable()
    {
        indicator = Instantiate(brokenIndicator, new Vector3(transform.position.x, transform.position.y + yHeight, transform.position.z), Quaternion.identity);
        enable.Invoke();
        UIManager.Instance.DisplayNotification(("A " + GetComponent<RoomTempChanger>().objectName + " HAS BROKEN IN THE " + transform.parent.name).ToUpper());
        if (GetComponent<Radiator>())
        {
            AudioSource.PlayClipAtPoint(AudioManager.Instance.radiatorBreakSound, transform.position);
            scriptToDisable.enabled = false;
        }
        else if(GetComponent<Window>())
        {
            AudioSource.PlayClipAtPoint(AudioManager.Instance.windowSmash, transform.position);
            GetComponent<Window>().isOn = true;
        }
        
    }

    private void OnDisable()
    {
        Destroy(indicator);
        disable.Invoke();
        scriptToDisable.enabled = true;
    }
}
