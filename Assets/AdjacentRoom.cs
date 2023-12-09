using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjacentRoom : Room
{

    public Room[] adjacentRooms;
    float roomsTemp = 0;

    // Start is called before the first frame update
    void Start()
    {
        baseTemperature = GetAverageTemperature();
    }

    // Update is called once per frame
    void Update()
    {
        AdjacentRoomTemperature();
    }

    void AdjacentRoomTemperature()
    {
        liveTemperature = GetAverageTemperature();
    }

    float GetAverageTemperature()
    {
        roomsTemp = 0;

        foreach(Room room in adjacentRooms)
        {
            roomsTemp += room.liveTemperature;
        }

        return roomsTemp / adjacentRooms.Length;
    }
}
