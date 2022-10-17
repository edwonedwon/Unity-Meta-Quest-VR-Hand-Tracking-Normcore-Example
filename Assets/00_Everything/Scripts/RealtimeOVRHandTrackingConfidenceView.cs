using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class RealtimeOVRHandTrackingConfidenceView : RealtimeComponent<OVRHandTrackingConfidenceModel>
{
    public Transform viewParent;

    OVRHand hand;
    OVRHand.TrackingConfidence confidenceLast;

    void Awake()
    {
        hand = GetComponent<OVRHand>();
        confidenceLast = hand.HandConfidence;
    }

    void Update()
    {
        if (isOwnedLocallyInHierarchy)
        {
            if (hand.HandConfidence != confidenceLast)
            {
                if (hand.HandConfidence == OVRHand.TrackingConfidence.High)
                    model.handTrackingConfidence = 1;
                else
                    model.handTrackingConfidence = 0;
            }
            confidenceLast = hand.HandConfidence;
        }
    }

    void OnHandTrackingConfidenceDidChange(OVRHandTrackingConfidenceModel model, int handTrackingConfidence)
    {
        switch(handTrackingConfidence)
        {
            case 0: // low confidence
            {
                viewParent.gameObject.SetActive(false);
            }
            break;
            case 1: // high confidence
            {
                viewParent.gameObject.SetActive(true);
            }
            break;
        }
    }

    protected override void OnRealtimeModelReplaced(OVRHandTrackingConfidenceModel previousModel, OVRHandTrackingConfidenceModel currentModel)
    {
        if (currentModel != null)
        {
            // If this is a model that has no data set on it, set the default value
            if (currentModel.isFreshModel)
                currentModel.handTrackingConfidence = 0; // default is low confidence

            // Register for events so we'll know if the color changes later
            currentModel.handTrackingConfidenceDidChange += OnHandTrackingConfidenceDidChange;
        }
        if (previousModel != null) 
        {
            // Unregister from events
            currentModel.handTrackingConfidenceDidChange -= OnHandTrackingConfidenceDidChange;
        }
    }
}
