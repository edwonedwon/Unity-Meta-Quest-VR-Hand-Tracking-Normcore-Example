using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

[RequireComponent(typeof(RealtimeView))]
public class RealtimeLocalComponents : MonoBehaviour
{
    public List<Behaviour> localComponents;
    public List<GameObject> localGameObjects;

    RealtimeView realtimeView;
    bool isOwnedLocallyInHierarchyLast = false;

    void Awake()
    {
        realtimeView = GetComponent<RealtimeView>();
        EnableAll(false);
    }

    void Update()
    {
        if (isOwnedLocallyInHierarchyLast != realtimeView.isOwnedLocallyInHierarchy)
        {
            if (realtimeView.isOwnedLocallyInHierarchy)
                EnableAll(true);
            else
                EnableAll(false);
        }
        isOwnedLocallyInHierarchyLast = realtimeView.isOwnedLocallyInHierarchy;
    }

    void OnDidReplaceAllComponentModels(RealtimeView realtimeView)
    {
        if (realtimeView.realtime != null)
            if (!realtimeView.realtime.isActiveAndEnabled)
                return;

        if (realtimeView.isOwnedLocallyInHierarchy)
            EnableAll(true);            
        else
            EnableAll(false);
    }

    void EnableAll(bool enable)
    {
        foreach(var component in localComponents)
            component.enabled = enable;
        foreach(var gameObject in localGameObjects)
            gameObject.SetActive(enable);
    }

    void OnEnable()
    {
        realtimeView.didReplaceAllComponentModels += OnDidReplaceAllComponentModels;
    }

    void OnDisable()
    {
        realtimeView.didReplaceAllComponentModels -= OnDidReplaceAllComponentModels;
    }
}
