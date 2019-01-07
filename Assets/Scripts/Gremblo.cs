using UnityEngine;

public class Gremblo : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rb.isKinematic)
        {
            Split();
        }
    }

    void Split()
    {
        for (int x = 0; x < 4; x++)
        {
            Object.Destroy(gameObject);
            GameObject grem = Instantiate((GameObject)Resources.Load("Gremblo"), transform.position + transform.TransformDirection(new Vector3(0, GetComponent<BoxCollider>().size.x, 0.25f)), Quaternion.Euler(new Vector3(-30, 90 * x, 0)));
            transform.Rotate(Vector3.up * 90);
            grem.transform.localScale *= 0.5f;
            grem.GetComponent<Rigidbody>().AddForce(grem.transform.forward * 50);
        }
    }
}