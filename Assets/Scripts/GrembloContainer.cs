using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrembloContainer : MonoBehaviour
{
    public void AddGremblo(GameObject gremblo)
    {
        Object.Destroy(gremblo);
    }
}
