using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Broken : MonoBehaviour
{


    GameObject indicator;
    public float yHeight;
    [HideInInspector] public UnityEvent enable;
    [HideInInspector] public UnityEvent disable;
    public MonoBehaviour scriptToDisable;
    public Transform indicatorPos;
    public float fixCost;
    public GameObject windowIndicator;
    public GameObject radiatorIndicator;

    public float windowScaleX;
    public float windowScaleY;
    public float windowScaleZ;

    public float radiatorScaleX = 5;
    public float radiatorScaleY = 2.5f;
    public float radiatorScaleZ = 1;
        

    private void OnEnable()
    {
        if (GetComponent<Window>())
        {
            indicator = Instantiate(windowIndicator, indicatorPos.position, transform.rotation);
            indicator.transform.localScale = new Vector3(transform.localScale.x * windowScaleX, transform.localScale.y * windowScaleY, transform.localScale.z * windowScaleZ);

        }
        else if (GetComponent<Radiator>())
        {
            indicator = Instantiate(radiatorIndicator, indicatorPos.position, transform.rotation);
            indicator.transform.localScale = new Vector3(transform.localScale.x * radiatorScaleX, transform.localScale.y * radiatorScaleY, transform.localScale.z * radiatorScaleZ);
        }
        
        //indicator.transform.SetParent(transform);
        enable.Invoke();
        UIManager.Instance.DisplayNotification(("A " + GetComponent<RoomTempChanger>().objectName + " HAS BROKEN IN THE " + transform.parent.name).ToUpper());
        if (GetComponent<Radiator>())
        {
            AudioSource.PlayClipAtPoint(AudioManager.Instance.radiatorBreakSound, transform.position,0.5f);
            scriptToDisable.enabled = false;
        }
        else if(GetComponent<Window>())
        {
            AudioSource.PlayClipAtPoint(AudioManager.Instance.windowSmash, transform.position, 0.5f);
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
