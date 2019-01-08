using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spots : MonoBehaviour
{
    public HidingSpot[] spots;

    void Start()
    {
        spots = GameObject.FindObjectsOfType<HidingSpot>();
    }
}
