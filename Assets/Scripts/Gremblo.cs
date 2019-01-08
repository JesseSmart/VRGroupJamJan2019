using UnityEngine;
using UnityEngine.AI;

public class Gremblo : MonoBehaviour
{
    Rigidbody rb;
    float timer;
    bool grabbed = false;
    public bool hiding;
    public bool foundSpot;
    public int spot;
    public GameObject nextSize;
    bool a;
    public int size = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //being held
        if (!GetComponent<GrabObject>().canGrab)
        {
            if (!grabbed)
            {
                timer = 0;
                grabbed = true;
                hiding = false;
                foundSpot = false;
            }

            timer += Time.deltaTime;
        }
        else //not being held
        {
            Vector3 pos = Camera.main.transform.position;
            pos.y = 0;
            Vector3 pos1 = transform.position;
            pos1.y = 0;

            //able to be spooked
            if (Vector3.Distance(pos, pos1) < 2)
            {
                //in spook angle
                if (Vector3.Angle(transform.position - Camera.main.transform.position, Camera.main.transform.forward) < (Camera.main.fieldOfView / 3))
                {
                    if (hiding)
                    {
                        ////////////////add raycast check
                        //spook
                        GetComponentInChildren<Renderer>().material.color = Color.red;
                        timer += Time.deltaTime;
                    }
                }
            }
            else
            {
                GetComponentInChildren<Renderer>().material.color = Color.blue;

                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    timer = 0;
                }
            }
        }

        if (timer > 1f)
        {
            if (transform.GetComponent<BoxCollider>().size.x > .126f)
            {
                Split();
            }
            else
            {
                foundSpot = false;
                hiding = false;
                timer = 0;
            }
        }

        if (!hiding && GetComponent<NavMeshAgent>())
        {
            if (!foundSpot)
            {
                spot = Random.Range(0, GameObject.FindObjectOfType<Spots>().spots.Length);

                if (GameObject.FindObjectOfType<Spots>().spots[spot].GetComponent<HidingSpot>().size >= size)
                {
                    foundSpot = true;
                }
                else
                {
                    foundSpot = false;
                }

                a = false;
            }
            else
            {
                if (GetComponent<NavMeshAgent>().enabled)
                {
                    if (a == false)
                    {
                        GetComponent<NavMeshAgent>().destination = GameObject.FindObjectOfType<Spots>().spots[spot].transform.position;
                        a = true;
                    }

                    GetComponent<NavMeshAgent>().isStopped = false;
                    GetComponent<Rigidbody>().useGravity = false;
                }

                Vector3 pos0 = transform.position;
                Vector3 pos1 = GetComponent<NavMeshAgent>().destination;
                if (Vector3.Distance(pos0, pos1) < 0.6f)
                {
                    /////////////set pos if little
                    //transform.position = GetComponent<NavMeshAgent>().destination;
                    if (!GameObject.FindObjectOfType<Spots>().spots[spot].occupied)
                    {
                        GetComponent<NavMeshAgent>().isStopped = true;
                        hiding = true;
                    }
                    else
                    {
                        foundSpot = false;
                        hiding = false;
                    }
                }
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
            grem.GetComponent<TennisBall>().spawnObj = nextSize;

            transform.Rotate(Vector3.up * 90);

            grem.transform.localScale = transform.GetComponent<BoxCollider>().size * 0.5f;
            if (grem.transform.localScale.x < 0.125f)
            {
                grem.transform.localScale = new Vector3(0.125f, 0.125f, 0.125f);
            }

            grem.GetComponent<Rigidbody>().AddForce(grem.transform.forward * 1000);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        RaycastHit hit;
        if (Physics.Raycast(new Ray(transform.position, Vector3.down), out hit, 100))
        {
            if(hit.distance < 2)
            {
                if (GetComponent<GrabObject>().canGrab)
                {
                    grabbed = false;
                    GetComponent<NavMeshAgent>().enabled = true;
                    GetComponent<Rigidbody>().useGravity = false;
                    GetComponent<Rigidbody>().isKinematic = true;

                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //if i touch the container
        if(other.GetComponent<GrembloContainer>())
        {
            //if were being held
            if (!GetComponent<GrabObject>().canGrab)
            {
                other.transform.GetComponent<GrembloContainer>().AddGremblo(gameObject);
            }
        }
    }
}