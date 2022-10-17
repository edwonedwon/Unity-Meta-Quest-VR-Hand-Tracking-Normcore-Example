using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRHandTrackingConfidenceView : MonoBehaviour
{
    OVRHand hand;
    OVRHand.TrackingConfidence confidenceLast;
    public Transform renderParent;

    void Awake()
    {
        hand = GetComponent<OVRHand>();
        UpdateRenderLogic();
        confidenceLast = hand.HandConfidence;
    }

    void Update()
    {
        if (hand.HandConfidence != confidenceLast)
        {
            UpdateRenderLogic();
        }
        confidenceLast = hand.HandConfidence;
    }

    void UpdateRenderLogic()
    {
        if (hand.HandConfidence == OVRHand.TrackingConfidence.High)
            renderParent.gameObject.SetActive(true);
        else
            renderParent.gameObject.SetActive(false);
    }
}
