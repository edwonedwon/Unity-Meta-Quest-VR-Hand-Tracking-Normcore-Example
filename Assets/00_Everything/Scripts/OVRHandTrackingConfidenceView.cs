using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class OVRHandTrackingConfidenceView : MonoBehaviour
{
    public Transform viewParent;

    OVRHand hand;
    OVRHand.TrackingConfidence confidenceLast;

    void Awake()
    {
        hand = GetComponent<OVRHand>();
        confidenceLast = hand.HandConfidence;
    }

    void Start()
    {
        viewParent.gameObject.SetActive(false);
    }

    void Update()
    {
        if (hand.HandConfidence != confidenceLast)
        {
            if (hand.HandConfidence == OVRHand.TrackingConfidence.High)
                viewParent.gameObject.SetActive(true);
            else
                viewParent.gameObject.SetActive(false);
        }
        confidenceLast = hand.HandConfidence;
    }
}
