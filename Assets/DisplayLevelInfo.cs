using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayLevelInfo : MonoBehaviour
{
    public GameObject levelInfo;
    public Graph graph;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        levelInfo.SetActive(true);
        Debug.Log(LevelManager.Instance.budgetOverDays);
        graph.ShowGraph(LevelManager.Instance.budgetOverDays);

    }

    public void Hide()
    {
        levelInfo.SetActive(false);
    }
}
