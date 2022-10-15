using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRControllerRenderControl : MonoBehaviour
{
    public Transform renderParent;
    bool handTrackingEnabledLast = false;

    void Awake()
    {
        renderParent.gameObject.SetActive(false);
        UpdateRenderLogic(OVRPlugin.GetHandTrackingEnabled());
    }

    void Update()
    {
        bool handTrackingEnabled = OVRPlugin.GetHandTrackingEnabled();
        if (handTrackingEnabledLast != handTrackingEnabled)
        {
            UpdateRenderLogic(handTrackingEnabled);
        }
        handTrackingEnabledLast = handTrackingEnabled;
    }

    void UpdateRenderLogic(bool handTrackingEnabled)
    {
        if (handTrackingEnabled)
            renderParent.gameObject.SetActive(false);
        else
            renderParent.gameObject.SetActive(true);
    }
}
