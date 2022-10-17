using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Normal.Realtime;

public class RealtimeOnConnectedEvent : MonoBehaviour
{
    public List<UnityEvent> onConnectedEvents;
    Realtime realtime;

    void Awake()
    {
        realtime = FindObjectOfType<Realtime>();
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
        foreach(var onConnectedEvent in onConnectedEvents)
            onConnectedEvent.Invoke();
    }
}
