using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    //0 = tiny, 1 = small, 2 = medium, 3 = large
    public int size = 0;
    public bool occupied;
    public bool almostOccupied;

    int amount = 0;

    void LateUpdate()
    {
        RaycastHit[] hit = Physics.SphereCastAll(new Ray(transform.position, transform.forward), 0.5f, 0.0f);
        amount = 0;

        for (int x = 0; x < hit.Length; x++)
        {
            if (hit[x].transform.GetComponent<Gremblo>())
            {
                amount++;
            }
        }

        if (amount > 0)
        {
            if (size > 0)
            {
                StartCoroutine("Occupy");
            }
            else
            {
                if (amount < 4)
                {
                    occupied = false;
                }
                else
                {
                    StartCoroutine("Occupy");
                }
            }
        }
        else
        {
            occupied = false;
        }
    }

    IEnumerator Occupy()
    {
        if (!almostOccupied)
        {
            almostOccupied = true;
            yield return new WaitForSeconds(0.5f);
            occupied = true;
            almostOccupied = false;
        }
    }

    void OnDrawGizmos()
    {
        Color c = Color.blue;
        c.a = 0.5f;
        Gizmos.color = c;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}