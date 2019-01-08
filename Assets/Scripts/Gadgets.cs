using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gadgets : MonoBehaviour
{

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        switch (selectedGadget)
        {
            case GadgetType.Vacuum:
                break;
            case GadgetType.Torch:
                GetComponentInChildren<Light>().enabled = !GetComponentInChildren<Light>().enabled;
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
}
