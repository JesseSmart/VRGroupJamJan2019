using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Hand : MonoBehaviour
{
    public GameObject grabPoint;

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
            timeheld += Time.deltaTime;
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
