using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI playerTempText;
    public TextMeshProUGUI budgetText;
    [HideInInspector] public GameObject player;
    [HideInInspector] public string degrees = "�C";

    public GameObject NightIcon;
    public GameObject MorningIcon;


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
        float roundedTemp = Mathf.Round(player.GetComponent<CharacterTemperature>().liveTemp ) ;



        playerTempText.text = "Player Temperature : " + roundedTemp.ToString() + " " + UIManager.Instance.degrees;
        budgetText.text = "Budget: �" + LevelManager.Instance.budget.ToString();

        string[] timeTxt = timeText.text.Split(':');
        string hour = timeTxt[0];
        string minute = timeTxt[1];

        if(int.Parse(hour)>6 && int.Parse(hour) < 18)
        {
            MorningIcon.SetActive(true);
            NightIcon.SetActive(false);

        } else
        {
            MorningIcon.SetActive(false);
            NightIcon.SetActive(true);
        }
        
    }
    //public IEnumerator ChangeDayNightIcon()
    //{
    //    return;
    //}
}