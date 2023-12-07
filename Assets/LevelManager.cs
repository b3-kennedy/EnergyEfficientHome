using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;

    public Room[] rooms;

    public float budget;

    public float dailyCost;

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

    }

    public void OnNewDay()
    {
        budget -= dailyCost;
        dailyCost = 0;
    }

    public void AddCost()
    {
        foreach (Room room in rooms)
        {
            foreach (var item in room.objects)
            {
                if (item.GetComponent<Radiator>())
                {
                    if (item.GetComponent<Radiator>().isOn)
                    {
                        dailyCost += 1;
                    }
                }
            }
        }
    }
}
