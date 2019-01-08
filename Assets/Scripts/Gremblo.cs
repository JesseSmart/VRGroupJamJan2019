using UnityEngine;
using UnityEngine.AI;

public class Gremblo : MonoBehaviour
{
    Rigidbody rb;
    float timer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //being held
        if (!GetComponent<GrabObject>().canGrab)
        {
            timer += Time.deltaTime;
            if (timer > 1f)
            {
                if (transform.localScale.x > .126f)
                {
                    Split();
                }
            }
        }
        else
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = 0;
            }
        }
    }

    void Split()
    {
        GetComponent<GrabObject>().Release(GetComponent<GrabObject>().cont);

        for (int x = 0; x < 4; x++)
        {
            Object.Destroy(gameObject);
            GameObject grem = Instantiate((GameObject)Resources.Load("Gremblo Ball"), transform.position + transform.TransformDirection(new Vector3(0, GetComponent<BoxCollider>().size.x, 0.25f)), Quaternion.Euler(new Vector3(-30, 90 * x, 0)));
            transform.Rotate(Vector3.up * 90);

            grem.transform.localScale = transform.localScale * 0.5f;
            if (grem.transform.localScale.x < 0.125f)
            {
                grem.transform.localScale = new Vector3(0.125f, 0.125f, 0.125f);
            }

            grem.GetComponent<Rigidbody>().AddForce(grem.transform.forward * 1000);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (GetComponent<GrabObject>().canGrab)
        {
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<Rigidbody>().useGravity = false;
        }
    }
}