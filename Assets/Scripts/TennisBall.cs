using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisBall : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if (GetComponent<GrabObject>().canGrab)
        {
            RaycastHit hit;
            if(Physics.Raycast(new Ray(transform.position, Vector3.down), out hit))
            {
                Debug.DrawRay(transform.position, Vector3.down * ((GetComponent<BoxCollider>().size.y / 2) + 0.1f));
                if (hit.distance <= GetComponent<BoxCollider>().size.y / 2 + 0.1f)
                {
                    print("YEHSIKLHNFLSKAJ");
                    Object.Destroy(gameObject);
                    GameObject grem = Instantiate((GameObject)Resources.Load("Gremblo"), transform.position + (Vector3.up * 0.05f), transform.rotation);
                    grem.transform.localScale = transform.localScale;
                    if (grem.transform.localScale.x < 0.125f)
                    {
                        grem.transform.localScale = new Vector3(0.125f, 0.125f, 0.125f);
                    }
                }
            }
        }
    }
}