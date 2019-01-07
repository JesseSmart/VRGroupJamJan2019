using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    protected Rigidbody rigidBody;
    protected bool originalKinematicState;
    protected Transform originalParent;

    private Hand cont;

    Vector3 velo = Vector3.zero;
    Vector3 lastPos = Vector3.zero;

    public bool canGrab = true;

    void Start()
    {

    }

    void Update()
    {
        if(cont != null)
        {
            velo = (cont.transform.position - lastPos) / Time.deltaTime;
            lastPos = cont.transform.position;
        }
    }

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        originalKinematicState = rigidBody.isKinematic;
        originalParent = transform.parent;
    }

    public void Pickup(Hand controller)
    {
        if (!canGrab)
        {
            return;
        }
        print("Grab");

        controller.holdingItem = true;
        controller.heldItem = gameObject;

        rigidBody.isKinematic = true;

        canGrab = false;

        transform.SetParent(controller.grabPoint.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        cont = controller;
    }

    public void Release(Hand controller)
    {
        if (transform.parent == controller.grabPoint.gameObject.transform)
        {
            print("Release");
            rigidBody.isKinematic = originalKinematicState;

            if (originalParent != controller.grabPoint.gameObject.transform)
            {
                transform.SetParent(originalParent);
            }
            else
            {
                transform.SetParent(null);
            }

            if (cont != null)
            {
                rigidBody.velocity = velo * 1;
            }

            lastPos = Vector3.zero;
            velo = Vector3.zero;

            cont.timeheld = 0;
            cont.holdingItem = false;
            cont.heldItem = null;
            cont = null;

            canGrab = true;
            
        }
    }
}

