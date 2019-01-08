using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GrabObject : MonoBehaviour
{
    protected Rigidbody rigidBody;
    protected bool originalGravityState;
    protected Transform originalParent;

    public Hand cont;

    Vector3 velo = Vector3.zero;
    Vector3 lastPos = Vector3.zero;

    public bool canGrab = true;

    void Start()
    {

    }

    void Update()
    {
        if (cont != null)
        {
            velo = (cont.transform.position - lastPos) / Time.deltaTime;
            lastPos = cont.transform.position;
        }
    }

    void FixedUpdate()
    {
        if (cont != null)
        {
            rigidBody.velocity = (cont.grabPoint.transform.position - transform.position) * 50;
            rigidBody.MoveRotation(cont.grabPoint.transform.rotation);
        }
    }

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        originalGravityState = rigidBody.useGravity;
        originalParent = transform.parent;
    }

    public void Pickup(Hand controller)
    {
        if (!canGrab)
        {
            return;
        }
        print("Grab");

        if (GetComponent<NavMeshAgent>())
        {
            GetComponent<NavMeshAgent>().enabled = false;
        }

        rigidBody.isKinematic = false;
        controller.holdingItem = true;
        controller.heldItem = gameObject;

        if (rigidBody != null)
        {
            rigidBody.useGravity = false;
        }

        canGrab = false;

        cont = controller;
    }

    public void Release(Hand controller)
    {
        print("Release");

        if (rigidBody != null)
        {
            rigidBody.useGravity = originalGravityState;
        }

        if (cont != null)
        {
            if (rigidBody != null)
            {
                rigidBody.velocity = velo * 1;
            }

            cont.timeheld = 0;
            cont.holdingItem = false;
            cont.heldItem = null;
        }

        lastPos = Vector3.zero;
        velo = Vector3.zero;
            
        cont = null;

        canGrab = true;
    }
}

