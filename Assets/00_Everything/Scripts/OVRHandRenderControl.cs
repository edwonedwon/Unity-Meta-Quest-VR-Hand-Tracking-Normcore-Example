using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRHandRenderControl : MonoBehaviour
{
    OVRHand hand;
    OVRHand.TrackingConfidence confidenceLast;
    public Transform renderParent;

    void Awake()
    {
        hand = GetComponent<OVRHand>();
        renderParent.gameObject.SetActive(false);
        confidenceLast = hand.HandConfidence;
    }

    void Update()
    {
        if (hand.HandConfidence != confidenceLast)
        {
            if (hand.HandConfidence == OVRHand.TrackingConfidence.High)
                renderParent.gameObject.SetActive(true);
            else
                renderParent.gameObject.SetActive(false);
        }
        confidenceLast = hand.HandConfidence;
    }
}
