using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class RealtimeAvatarPositioner : MonoBehaviour
{
    public List<Color> playerColors;
    public List<Transform> avatarSpawnPoints;

    RealtimeAvatarManager avatarManager;

    void Awake()
    {
        avatarManager = GetComponent<RealtimeAvatarManager>();
    }

    void OnAvatarCreated(RealtimeAvatarManager avatarManager, RealtimeAvatar avatar, bool isLocalAvatar)
    {
        if (isLocalAvatar)
        {
            int ownerID = avatar.ownerIDInHierarchy;
            avatarManager.localAvatar.localPlayer.root.position = avatarSpawnPoints[ownerID].position;
            avatarManager.localAvatar.localPlayer.root.rotation = avatarSpawnPoints[ownerID].rotation;
        }
    }

    void OnAvatarDestroyed(RealtimeAvatarManager avatarManager, RealtimeAvatar avatar, bool isLocalAvatar)
    {

    }

    void OnEnable()
    {
        avatarManager.avatarCreated += OnAvatarCreated;
        avatarManager.avatarDestroyed += OnAvatarDestroyed;
    }

    void OnDisable()
    {
        avatarManager.avatarCreated -= OnAvatarCreated;
        avatarManager.avatarDestroyed -= OnAvatarDestroyed;
    }
}
