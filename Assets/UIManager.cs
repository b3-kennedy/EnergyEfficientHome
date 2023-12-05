using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI playerTempText;
    public GameObject player;
    [HideInInspector] public string degrees = "°C";

    private void Awake()
    {
        Instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float roundedTemp = Mathf.Round(player.GetComponent<CharacterTemperature>().liveTemp * 10) * 0.1f;

        playerTempText.text = "Player Temperature: " + roundedTemp.ToString() + " " + UIManager.Instance.degrees;
    }
}
