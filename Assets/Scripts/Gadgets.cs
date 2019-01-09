using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gadgets : MonoBehaviour
{
    public bool active;
    public bool toggleObject;
    public GameObject toggle;
    public BoxCollider coll;
    public GameObject button;

    bool corutinetogg = false;

    private AudioSource audio;
    public AudioClip clickSound;
    public AudioClip gadSound;

    public enum GadgetType
    {
        Vacuum,
        Torch,
        Cassette,
        Radar,
        Taser,
        Container
    }

    public GadgetType selectedGadget;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (toggleObject)
        {
            toggle.SetActive(active);
            coll.enabled = GetComponent<GrabObject>().canGrab;

            if (active)
            {
                button.GetComponent<Renderer>().material.color = Color.green;
            }
            else
            {
                button.GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }

    public void Activate()
    {
        switch (selectedGadget)
        {
            case GadgetType.Vacuum:
                active = !active;
                audio.PlayOneShot(clickSound);
                StartCoroutine("Vacuum");
                break;
            case GadgetType.Torch:
                GetComponentInChildren<Light>().enabled = !GetComponentInChildren<Light>().enabled;
                audio.PlayOneShot(clickSound);
                break;
            case GadgetType.Cassette:
                break;
            case GadgetType.Radar:
                break;
            case GadgetType.Taser:
                break;
            case GadgetType.Container:
                break;
        }
    }

    IEnumerator Vacuum()
    {
        if (!corutinetogg)
        {
            corutinetogg = true;
            Gremblo[] gremblos = GameObject.FindObjectsOfType<Gremblo>();

            for (int x = 0; x < gremblos.Length; x++)
            {
                if (gremblos[x].transform.GetComponent<BoxCollider>().size.x < .15f)
                {
                    if (Vector3.Distance(gremblos[x].transform.position, toggle.transform.position) < 3)
                    {
                        if (Vector3.Angle((gremblos[x].transform.position - toggle.transform.position), toggle.transform.forward) < 30)
                        {
                            GameObject a = Instantiate((GameObject)Resources.Load("Gremblo Ball"), gremblos[x].transform.position + (Vector3.up * 0.1f), gremblos[x].transform.rotation);
                            if (gremblos[x].GetComponent<BoxCollider>().size.x < .15f)
                            {
                                a.GetComponent<TennisBall>().spawnObj = gremblos[x].gameObject;
                            }
                            else
                            {
                                a.GetComponent<TennisBall>().spawnObj = gremblos[x].nextSize;
                            }

                            a.transform.localScale = gremblos[x].transform.GetComponent<BoxCollider>().size;

                            Object.Destroy(gremblos[x].gameObject);
                        }
                    }
                }
            }

            if (!audio.isPlaying)
            {
                audio.Play();
            }

            yield return new WaitForSeconds(0.1f);
            corutinetogg = false;
            if (active)
            {
                StartCoroutine("Vacuum");
            }
            else
            {
                audio.Stop();
                StopCoroutine("Vacuum");
            }
        }
    }
}