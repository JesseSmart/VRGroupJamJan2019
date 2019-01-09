using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisBall : MonoBehaviour
{
    public GameObject spawnObj;

    private AudioSource audio;
    public AudioClip thoompSound;
    public AudioClip throwSound;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = throwSound;
        audio.Play();
        audio.clip = thoompSound;
    }

    void Update()
    {
        Gadgets[] g = GameObject.FindObjectsOfType<Gadgets>();
        for (int x = 0; x < g.Length; x++)
        {
            if(transform.localScale.x < .15f)
            {
                if (g[x].selectedGadget == Gadgets.GadgetType.Vacuum)
                {
                    if (Vector3.Distance(transform.position, g[x].transform.position) < 1f)
                    {
                        GameObject.FindObjectOfType<GrembloContainer>().AddGremblo(gameObject);
                    }

                    if (g[x].active)
                    {
                        if (Vector3.Distance(g[x].transform.position, transform.position) < 5)
                        {
                            GetComponent<Rigidbody>().useGravity = false;
                            GetComponent<Rigidbody>().isKinematic = true;
                            transform.position = Vector3.Lerp(transform.position, g[x].toggle.transform.position, Time.deltaTime * 5f);
                            return;
                        }
                        else
                        {
                            GetComponent<Rigidbody>().useGravity = true;
                            GetComponent<Rigidbody>().isKinematic = false;
                        }
                    }
                    else
                    {
                        GetComponent<Rigidbody>().useGravity = true;
                        GetComponent<Rigidbody>().isKinematic = false;
                    }
                }
            }
        }
        GetComponent<Rigidbody>().useGravity = true;

        if (GetComponent<GrabObject>().canGrab)
        {
            RaycastHit hit;
            if (Physics.Raycast(new Ray(transform.position, Vector3.down), out hit))
            {
                if (hit.distance <= transform.localScale.y / 2 + 0.1f)
                {
                    if (transform.localScale.x < .15f)
                    {
                        GameObject grem = Instantiate((GameObject)Resources.Load("Gremblo 125 Variant"), transform.position + (Vector3.up * 0.05f), transform.rotation);
                        Object.Destroy(gameObject);
                    }
                    else
                    {
                        GameObject grem = Instantiate(spawnObj, transform.position + (Vector3.up * 0.05f), transform.rotation);
                        Object.Destroy(gameObject);
                    }
                }
            }
        }
        else
        {
            if (transform.localScale.x < .15f)
            {
                GameObject grem = Instantiate((GameObject)Resources.Load("Gremblo 125 Variant"), transform.position + (Vector3.up * 0.05f), transform.rotation);
                Object.Destroy(gameObject);
            }
            else
            {
                GameObject grem = Instantiate(spawnObj, transform.position + (Vector3.up * 0.05f), transform.rotation);
                Object.Destroy(gameObject);
            }
        }
    }
}