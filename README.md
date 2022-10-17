# Unity-Meta-Quest-VR-Hand-Tracking-Normcore-Example
example of how to use Meta Quests tracked hands in VR with multiplayer framework Normcore

the main online demo scene is at Assets/00_Everything/Scenes/Online Scene

"OVR Rig" is the offline only version of the rig, it shows you're hands/controllers before you're connected to a room (but hides them once your connected). It is always present in the scene, but just hides the hand/controller meshes while connected. It's left/right and head transforms are referenced by RealtimeAvatarManager.

"OVR Rig Normcore" is the online version of the rig, it handles all the syncing and shows your hands/controllers once you've connected to a room (but hides them while disconnected). It is only in the scene once you're connected to a room. It is spawned using Realtime Instantiate from RealtimeAvatarManager.

NOTE: Everything works as expected, but there is one bug I couldn't fix. If you're in hand tracking mode already at the moment you connect to a room, your hands will disappear. If you take them out of view of the Quest and then back in, they will appear. If anyone knows how to fix this please let me know!
