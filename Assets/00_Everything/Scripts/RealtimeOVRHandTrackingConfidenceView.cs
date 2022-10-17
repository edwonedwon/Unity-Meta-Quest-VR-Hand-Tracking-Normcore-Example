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
        viewParent.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        realtime.didConnectToRoom += OnDidConnectToRoom;
    }

    void OnDisable()
    {
        realtime.didConnectToRoom -= OnDidConnectToRoom;
    }

    void OnDidConnectToRoom(Realtime realtime)
    {
        int handTrackingConfidence = hand.HandConfidence == OVRHand.TrackingConfidence.High ? 1 : 0;
        ShowHandViewOrNot(handTrackingConfidence);
    }
    
    void Update()
    {
        if (isOwnedLocallyInHierarchy)
        {
            if (hand.HandConfidence != confidenceLast)
            {
                int handTrackingConfidence = hand.HandConfidence == OVRHand.TrackingConfidence.High ? 1 : 0;
                model.handTrackingConfidence = handTrackingConfidence;
            }
            confidenceLast = hand.HandConfidence;
        }
    }

    void OnHandTrackingConfidenceDidChange(OVRHandTrackingConfidenceModel model, int handTrackingConfidence)
    {
        ShowHandViewOrNot(handTrackingConfidence);
    }

    protected override void OnRealtimeModelReplaced(OVRHandTrackingConfidenceModel previousModel, OVRHandTrackingConfidenceModel currentModel)
    {
        if (currentModel != null)
        {
            int handTrackingConfidence = hand.HandConfidence == OVRHand.TrackingConfidence.High ? 1 : 0;

            // If this is a model that has no data set on it, set the default value
            if (currentModel.isFreshModel)
                currentModel.handTrackingConfidence = handTrackingConfidence; // default is low confidence

            ShowHandViewOrNot(handTrackingConfidence);

            // Register for events so we'll know if the color changes later
            currentModel.handTrackingConfidenceDidChange += OnHandTrackingConfidenceDidChange;
        }
        if (previousModel != null) 
        {
            // Unregister from events
            currentModel.handTrackingConfidenceDidChange -= OnHandTrackingConfidenceDidChange;
        }
    }

    void ShowHandViewOrNot(int handTrackingConfidence)
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
}
