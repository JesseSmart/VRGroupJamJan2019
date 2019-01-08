using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour
{
    public GameObject grabPoint;
    public GameObject vrArea;
    public GameObject hand;

    public enum SelectedHand
    {
        Left,
        Right
    }

    public SelectedHand selected;

    public bool holdingItem = false;
    public GameObject heldItem;
    float cd;
    public float timeheld;

    void Start()
    {
        
    }

    void Update()
    {
        if (holdingItem)
        {
            timeheld += Time.deltaTime;
            hand.SetActive(false);
            //heldItem.transform.position = Vector3.Lerp(heldItem.transform.position, grabPoint.transform.position, Time.deltaTime * 5);
        }
        else
        {
            hand.SetActive(true);
        }

        cd -= Time.deltaTime;

        if (selected == SelectedHand.Left && SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            if (holdingItem)
            {
                if (heldItem != null && timeheld > 0.5f)
                {
                    if (heldItem.CompareTag("Tool"))
                    {
                        heldItem.GetComponent<GrabObject>().Release(this);
                        cd = .5f;
                        print("toggle drop");
                    }
                }
            }
        }

        if (selected == SelectedHand.Right && SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            if (holdingItem)
            {
                if (heldItem != null && timeheld > 0.5f)
                {
                    if (heldItem.CompareTag("Tool"))
                    {
                        heldItem.GetComponent<GrabObject>().Release(this);
                        cd = .5f;
                        print("toggle drop");
                    }
                }
            }
        }


        if (selected == SelectedHand.Left)
        {
            if (SteamVR_Input._default.inActions.MenuClick.GetState(SteamVR_Input_Sources.LeftHand))
            {
                
                RaycastHit hit;
                if (Physics.Raycast(new Ray(transform.position, transform.forward), out hit, 100.0f))
                {
                    GetComponent<LineRenderer>().SetPosition(0, transform.position);
                    GetComponent<LineRenderer>().SetPosition(1, hit.point);

                    if (hit.transform.gameObject.CompareTag("Floor"))
                    {
                        GetComponent<LineRenderer>().startColor = Color.blue;
                        GetComponent<LineRenderer>().endColor = Color.blue;
                    }
                    else
                    {
                        GetComponent<LineRenderer>().startColor = Color.red;
                        GetComponent<LineRenderer>().endColor = Color.red;
                    }
                }
                else
                {
                    GetComponent<LineRenderer>().SetPosition(0, transform.position);
                    GetComponent<LineRenderer>().SetPosition(1, transform.forward * 100);
                    GetComponent<LineRenderer>().startColor = Color.red;
                    GetComponent<LineRenderer>().endColor = Color.red;
                }
            }
            if (SteamVR_Input._default.inActions.MenuClick.GetStateUp(SteamVR_Input_Sources.LeftHand))
            {
                RaycastHit hit;
                if (Physics.Raycast(new Ray(transform.position, transform.forward), out hit, 100))
                {
                    if (hit.transform.gameObject.CompareTag("Floor"))
                    {
                        vrArea.transform.position = hit.point;
                    }
                }

                GetComponent<LineRenderer>().SetPosition(0, transform.position + Vector3.up * 10000);
                GetComponent<LineRenderer>().SetPosition(1, hit.point + Vector3.up * 10000);
            }
        }
    
        if (selected == SelectedHand.Right)
        {
            if (SteamVR_Input._default.inActions.MenuClick.GetState(SteamVR_Input_Sources.RightHand))
            {
                RaycastHit hit;
                if (Physics.Raycast(new Ray(transform.position, transform.forward), out hit, 100.0f))
                {
                    GetComponent<LineRenderer>().SetPosition(0, transform.position);
                    GetComponent<LineRenderer>().SetPosition(1, hit.point);

                    if (hit.transform.gameObject.CompareTag("Floor"))
                    {
                        GetComponent<LineRenderer>().startColor = Color.blue;
                        GetComponent<LineRenderer>().endColor = Color.blue;
                    }
                    else
                    {
                        GetComponent<LineRenderer>().startColor = Color.red;
                        GetComponent<LineRenderer>().endColor = Color.red;
                    }
                }
                else
                {
                    GetComponent<LineRenderer>().SetPosition(0, transform.position);
                    GetComponent<LineRenderer>().SetPosition(1, transform.forward * 100);
                    GetComponent<LineRenderer>().startColor = Color.red;
                    GetComponent<LineRenderer>().endColor = Color.red;
                }
            }
            if (SteamVR_Input._default.inActions.MenuClick.GetStateUp(SteamVR_Input_Sources.RightHand))
            {
                print("release");

                RaycastHit hit;
                if (Physics.Raycast(new Ray(transform.position, transform.forward), out hit, 100.0f))
                {
                    if (hit.transform.gameObject.CompareTag("Floor"))
                    {
                        vrArea.transform.position = hit.point;
                    }
                }

                GetComponent<LineRenderer>().SetPosition(0, transform.position + Vector3.up * 10000);
                GetComponent<LineRenderer>().SetPosition(1, hit.point + Vector3.up * 10000);
            }
        }

        if (heldItem != null)
        {
            if (heldItem.CompareTag("Tool"))
            {
                if (selected == SelectedHand.Left)
                {
                    if (SteamVR_Input._default.inActions.Teleport.GetStateDown(SteamVR_Input_Sources.LeftHand))
                    {
                        heldItem.GetComponent<Gadgets>().Activate();
                    }
                }

                if (selected == SelectedHand.Right)
                {
                    if (SteamVR_Input._default.inActions.Teleport.GetStateDown(SteamVR_Input_Sources.RightHand))
                    {
                        heldItem.GetComponent<Gadgets>().Activate();
                    }
                }
            }
        }

        /*if (SteamVR_Input._default.inActions.Teleport.GetStateDown(SteamVR_Input_Sources.Any))
        {
            print("Track pad clicked");
        }

        if (SteamVR_Input._default.inActions.GrabGrip.GetStateDown(SteamVR_Input_Sources.Any))
        {
            print("grab grip clicked");
        }

        float triggerValue = SteamVR_Input._default.inActions.Squeeze.GetAxis(SteamVR_Input_Sources.Any);
        if (triggerValue > 0.25f)
        {
            print("Trigger was pulled: " + triggerValue);
        }

        if (SteamVR_Input._default.inActions.MenuClick.GetStateDown(SteamVR_Input_Sources.Any))
        {
            print("menu clicked");
        }

        if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(SteamVR_Input_Sources.Any))
        {
            print("Trigger used as button");
        }

        Vector2 touchPadPos = SteamVR_Input._default.inActions.trackPadPosition.GetAxis(SteamVR_Input_Sources.Any);
        if (touchPadPos.magnitude > 0.0f)
        {
            print("Touchpad Position" + touchPadPos);
        }*/
    }

    void OnTriggerStay(Collider collide)
    {
        if (collide.gameObject.GetComponent<GrabObject>())
        {
            if (selected == SelectedHand.Left)
            {
                float triggerValue = SteamVR_Input._default.inActions.Squeeze.GetAxis(SteamVR_Input_Sources.LeftHand);
                if (triggerValue > 0.25f)
                {
                    if (!holdingItem && cd <= 0)
                    {
                        collide.GetComponent<GrabObject>().Pickup(this);
                    }
                    return;
                }

                if (triggerValue < 0.25f)
                {
                    if (!collide.CompareTag("Tool"))
                    {
                        collide.GetComponent<GrabObject>().Release(this);
                    }
                    return;
                }
            }

            if (selected == SelectedHand.Right)
            {
                float triggerValue = SteamVR_Input._default.inActions.Squeeze.GetAxis(SteamVR_Input_Sources.RightHand);
                if (triggerValue > 0.25f)
                {
                    if (!holdingItem && cd <= 0)
                    {
                        collide.GetComponent<GrabObject>().Pickup(this);
                    }
                    return;
                }

                if (triggerValue < 0.25f)
                {
                    if (!collide.CompareTag("Tool"))
                    {
                        collide.GetComponent<GrabObject>().Release(this);
                    }
                    return;
                }
            }
        }
    }

   
}
