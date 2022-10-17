using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using System.Linq;

public class RealtimeMakeAllChildrenLocal : MonoBehaviour
{
    List<RealtimeView> childViews;
    List<RealtimeTransform> childTransforms;
    RealtimeView mainView;

    void Awake()
    {
        mainView = GetComponent<RealtimeView>();
        childViews = GetComponentsInChildren<RealtimeView>().ToList();
        childTransforms = GetComponentsInChildren<RealtimeTransform>().ToList();
        mainView.didReplaceAllComponentModels += OnDidReplaceAllComponentModels;
    }

    void OnDidReplaceAllComponentModels(RealtimeView view)
    {
        if (view.isOwnedLocallyInHierarchy)
        {
            Debug.Log("did replace component modles locally");
            foreach (RealtimeView childView in childViews)
                if (childView != view)
                    childView.RequestOwnership();
                    
            foreach (RealtimeTransform childTransform in childTransforms)
                if (childTransform != view)
                    childTransform.RequestOwnership();
        }
    }
}
