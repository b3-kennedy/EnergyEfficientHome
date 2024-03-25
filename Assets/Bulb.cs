using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulb : MonoBehaviour
{
    public enum BulbType {LED, FLOURESCENT, HALOGEN, INCANDESCENT};
    public BulbType type;
    public Vector3 spawnPos;
}
