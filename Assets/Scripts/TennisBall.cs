using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisBall : MonoBehaviour
{
    public GameObject spawnObj;

    void Start()
    {

    }

    void Update()
    {
        if (GetComponent<GrabObject>().canGrab)
        {
            RaycastHit hit;
            if (Physics.Raycast(new Ray(transform.position, Vector3.down), out hit))
            {
                if (hit.distance <= GetComponent<BoxCollider>().size.y / 2 + 0.1f)
                {
                    Object.Destroy(gameObject);
                    GameObject grem = Instantiate(spawnObj, transform.position + (Vector3.up * 0.05f), transform.rotation);
                }
            }
        }
        else
        {
            Object.Destroy(gameObject);
            GameObject grem = Instantiate(spawnObj, transform.position + (Vector3.up * 0.05f), transform.rotation);
        }
    }
}