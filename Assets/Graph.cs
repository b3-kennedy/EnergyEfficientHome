using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GraphData
{
    public int dayValue;
    public float percentage;
    public List<string> information = new List<string>();

    public GraphData (int day)
    {
        dayValue = day;
    }

    public void AddInfoToList(string info)
    {
        information.Add(info);
    }
}

public class Graph : MonoBehaviour
{
    public static Graph Instance;

    public Sprite circle;
    public RectTransform container;
    public RectTransform labelTemplateX;
    public RectTransform labelTemplateY;
    public List<GameObject> graphPoints;
    public GameObject graphPoint;
    public GameObject graphInfoText;

    //public PricePoint testPoint;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {


        //for (int i = 0; i < pricePointPercentages.Length; i++)
        //{
        //    pricePointPercentages[i].information = new List<string>();
        //}
        //List<float> values = new List<float>() { 100, 90, 80, 70, 30, 20, 10 };
        //ShowGraph(values);
    }

    GameObject CreateGraphPoint(Vector2 anchorPos, int day)
    {
        GameObject go = Instantiate(graphPoint);
        go.transform.SetParent(container, false);
        go.GetComponent<Image>().sprite = circle;
        RectTransform rectTransform = go.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchorPos;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);

        go.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Day " + day.ToString();
        

        for (int i = 0; i < LevelManager.Instance.infoForGraph[day].information.Count; i++)
        {
            GameObject infoText = Instantiate(graphInfoText, go.transform.GetChild(0).GetChild(0));
            infoText.GetComponent<TextMeshProUGUI>().text = LevelManager.Instance.infoForGraph[day].information[i];
        }

        //go.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(container.sizeDelta.x / 2, container.sizeDelta.y / 2);


        return go;
    }

    public void ShowGraph(List<float> values)
    {
        Debug.Log(container);
        float graphHeight = container.sizeDelta.y;
        float xSize = container.sizeDelta.x / values.Count;
        float yMax = 500f;

        GameObject lastPoint = null;
        for (int i = 0; i < values.Count; i++)
        {

            float xPos = (xSize/5) + i * xSize;
            float yPos = (values[i] / yMax) * graphHeight;
            GameObject point = CreateGraphPoint(new Vector2(xPos, yPos), i);
            graphPoints.Add(point);
            if(lastPoint != null)
            {
                CreateDotConnection(lastPoint.GetComponent<RectTransform>().anchoredPosition, point.GetComponent<RectTransform>().anchoredPosition);
            }
            lastPoint = point;

            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(container, false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPos, -20f);
            labelX.GetComponent<TextMeshProUGUI>().text = i.ToString();
        }

        int separators = 10;
        for (int i = 0;i <= separators;i++) 
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(container, false);
            labelY.gameObject.SetActive(true);
            float value = i * 1f / separators;
            labelY.anchoredPosition = new Vector2(-50f, value * graphHeight);
            labelY.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(value * yMax).ToString();
        }


    }

    void CreateDotConnection(Vector2 posA, Vector2 posB)
    {
        GameObject go = new GameObject("connection", typeof(Image));
        go.transform.SetParent(container, false);

        RectTransform rectTransform = go.GetComponent<RectTransform>();
        Vector2 dir = (posB - posA).normalized;
        float dist = Vector2.Distance(posA, posB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(dist, 3);
        rectTransform.anchoredPosition = posA + dir * dist * 0.5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));

        //GraphInfo();
    }

    //void GraphInfo()
    //{
    //    for (int i = 0; i < pricePointPercentages.Length; i++)
    //    {
    //        float distance = Vector3.Distance(graphPoints[pricePointPercentages[i].dayValue].GetComponent<RectTransform>().anchoredPosition, 
    //            graphPoints[pricePointPercentages[i].dayValue+1].GetComponent<RectTransform>().anchoredPosition);

    //        Vector2 direction = (graphPoints[pricePointPercentages[i].dayValue].GetComponent<RectTransform>().anchoredPosition -
    //            graphPoints[pricePointPercentages[i].dayValue+1].GetComponent<RectTransform>().anchoredPosition).normalized;
    //        CreateLineOnGraph(distance, direction, pricePointPercentages[i].percentage, pricePointPercentages[i].dayValue);
    //    }

    //}

    void CreateLineOnGraph(float distance, Vector2 dir, float perc, int dayValue)
    {
        GameObject go = new GameObject("line", typeof(Image));
        go.transform.SetParent(container, false);
        RectTransform rectTransform = go.GetComponent<RectTransform>();

        float percentDist = (perc / 100f) * distance;

        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(2, 30);
        rectTransform.anchoredPosition = graphPoints[dayValue].GetComponent<RectTransform>().anchoredPosition;



    }


    float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
