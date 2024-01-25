using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Graph : MonoBehaviour
{
    public Sprite circle;
    public RectTransform container;
    public RectTransform labelTemplateX;
    public RectTransform labelTemplateY;

    // Start is called before the first frame update
    void Start()
    {

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

        GameObject lastPoint = null;
        for (int i = 0; i < values.Count; i++)
        {
            float xPos = (xSize/5) + i * xSize;
            float yPos = (values[i] / yMax) * graphHeight;
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
