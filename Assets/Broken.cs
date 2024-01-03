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
        scriptToDisable.enabled = false;
    }

    private void OnDisable()
    {
        Destroy(indicator);
        disable.Invoke();
        scriptToDisable.enabled = true;
    }
}
