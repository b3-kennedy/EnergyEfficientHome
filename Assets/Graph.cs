using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PricePoint
{
    public float budgetValue;
    public float percentage;

    public PricePoint (float budget, float percent)
    {
        budgetValue = budget;
        percentage = percent;
    }
}

public class Graph : MonoBehaviour
{
    public static Graph Instance;

    public Sprite circle;
    public RectTransform container;
    public RectTransform labelTemplateX;
    public RectTransform labelTemplateY;
    public List<PricePoint> pricePointPercentages = new List<PricePoint>();

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //List<float> values = new List<float>() {100, 90, 80, 70, 30, 20, 10};
        //ShowGraph(values);
    }

    GameObject CreateGraphPoint(Vector2 anchorPos)
    {
        GameObject go = new GameObject("point", typeof(Image));
        go.transform.SetParent(container, false);
        go.GetComponent<Image>().sprite = circle;
        RectTransform rectTransform = go.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchorPos;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return go;
    }

    public void ShowGraph(List<float> values)
    {
        Debug.Log(container);
        float graphHeight = container.sizeDelta.y;
        float xSize = container.sizeDelta.x / values.Count;
        float yMax = 500f;

        float totalSizeX = 0;
        float totalSizeY = 0;

        float firstPointX = 0;
        float lastPointX = 0;

        float firstPointY = 0;
        float lastPointY = 0;

        GameObject lastPoint = null;
        for (int i = 0; i < values.Count; i++)
        {

            float xPos = (xSize/5) + i * xSize;
            float yPos = (values[i] / yMax) * graphHeight;
            if (i == 0)
            {
                firstPointX = xPos;
            }
            if(i == values.Count - 1)
            {
                lastPointX = xPos;
                totalSizeX = lastPointX - firstPointX;
            }
            GameObject point = CreateGraphPoint(new Vector2(xPos, yPos));
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
            if(i == 0)
            {
                firstPointY = value * graphHeight;
            }
            if(i == separators)
            {
                lastPointY = value * graphHeight;
                totalSizeY = lastPointY - firstPointY;
                
            }
            labelY.GetComponent<TextMeshProUGUI>().text = Mathf.RoundToInt(value * yMax).ToString();
        }

        Debug.Log(totalSizeX);

        for (int i = 0; i < pricePointPercentages.Count; i++)
        {
            float xPos = firstPointX + ((pricePointPercentages[i].percentage * totalSizeX) / 100);
            float yPercent = (pricePointPercentages[i].budgetValue / 500) * 100;
            Debug.Log(yPercent);
            float yPos = firstPointY + ((yPercent * totalSizeY) / 100);
            GameObject go = new GameObject("PricePoint", typeof(Image));
            go.transform.SetParent(container, false);
            go.GetComponent<Image>().sprite = circle;
            RectTransform rectTransform = go.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(xPos, yPos);
            rectTransform.sizeDelta = new Vector2(11, 11);
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(0, 0);



            Debug.Log(xPos);
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
