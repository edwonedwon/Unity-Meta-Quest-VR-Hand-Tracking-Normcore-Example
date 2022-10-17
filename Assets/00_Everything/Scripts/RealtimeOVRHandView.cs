using UnityEngine;
using Normal.Realtime;
using System.Collections.Generic;

// public class RealtimeOVRHandView : MonoBehaviour {}

// syncs and shows hand tracking vs controller views on each VR Hand
public class RealtimeOVRHandView : RealtimeComponent<OVRHandViewModel>
{
    public List<Transform> controllerParents;
    public List<Transform> handParents;

    void Update()
    {
        if (isOwnedLocallyInHierarchy)
        {
            // update hand tracking vs. controller view on model
            bool handTrackingEnabled = OVRPlugin.GetHandTrackingEnabled();
            if (handTrackingEnabled)
                model.handViewType = 1;
            else
                model.handViewType = 0;
        }
    }

    void OnHandViewTypeDidChange(OVRHandViewModel model, int handViewType)
    {
        SwitchHandView(handViewType);
    }

    void SwitchHandView(int handViewType)
    {
        switch(handViewType)
        {
            case 0: // controller
            {
                foreach(var parent in controllerParents)
                    parent.gameObject.SetActive(true);
                foreach(var parent in handParents)
                    parent.gameObject.SetActive(false);
            }
            break;
            case 1: // hand tracking
            {
                foreach(var parent in controllerParents)
                    parent.gameObject.SetActive(false);
                foreach(var parent in handParents)
                    parent.gameObject.SetActive(true);
            }
            break;
        }
    }

    protected override void OnRealtimeModelReplaced(OVRHandViewModel previousModel, OVRHandViewModel currentModel)
    { 
        if (currentModel != null)
        {
            // If this is a model that has no data set on it, set the default value
            if (currentModel.isFreshModel)
                currentModel.handViewType = 0; // default view is controller view

            // Register for events so we'll know if the color changes later
            currentModel.handViewTypeDidChange += OnHandViewTypeDidChange;
        }
        if (previousModel != null) 
        {
            // Unregister from events
            currentModel.handViewTypeDidChange -= OnHandViewTypeDidChange;
        }
    }
}