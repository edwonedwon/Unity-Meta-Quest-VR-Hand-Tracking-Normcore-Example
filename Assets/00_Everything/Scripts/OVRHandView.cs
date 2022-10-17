using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRHandView : MonoBehaviour
{
    public List<Transform> controllerParents;
    public List<Transform> handParents;
    
    void Start()
    {
        HideAllHandViews();
    }

    void Update()
    {
        bool handTrackingEnabled = OVRPlugin.GetHandTrackingEnabled();
        if (handTrackingEnabled)
            SwitchHandView(1);
        else
            SwitchHandView(0);
    }

    void HideAllHandViews()
    {
        foreach(var parent in controllerParents)
            parent.gameObject.SetActive(false);
        foreach(var parent in handParents)
            parent.gameObject.SetActive(false);
    }

    // 1 to show hands, 1 to show controllers
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
}