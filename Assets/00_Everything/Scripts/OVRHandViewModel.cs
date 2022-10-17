using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class OVRHandViewModel
{
    // controller = 0, hand tracking = 1
    [RealtimeProperty(1, false, true)] private int _handViewType;
}

/* ----- Begin Normal Autogenerated Code ----- */
public partial class OVRHandViewModel : RealtimeModel {
    public int handViewType {
        get {
            return _handViewTypeProperty.value;
        }
        set {
            if (_handViewTypeProperty.value == value) return;
            _handViewTypeProperty.value = value;
            InvalidateUnreliableLength();
            FireHandViewTypeDidChange(value);
        }
    }
    
    public delegate void PropertyChangedHandler<in T>(OVRHandViewModel model, T value);
    public event PropertyChangedHandler<int> handViewTypeDidChange;
    
    public enum PropertyID : uint {
        HandViewType = 1,
    }
    
    #region Properties
    
    private UnreliableProperty<int> _handViewTypeProperty;
    
    #endregion
    
    public OVRHandViewModel() : base(null) {
        _handViewTypeProperty = new UnreliableProperty<int>(1, _handViewType);
    }
    
    private void FireHandViewTypeDidChange(int value) {
        try {
            handViewTypeDidChange?.Invoke(this, value);
        } catch (System.Exception exception) {
            UnityEngine.Debug.LogException(exception);
        }
    }
    
    protected override int WriteLength(StreamContext context) {
        var length = 0;
        length += _handViewTypeProperty.WriteLength(context);
        return length;
    }
    
    protected override void Write(WriteStream stream, StreamContext context) {
        var writes = false;
        writes |= _handViewTypeProperty.Write(stream, context);
        if (writes) InvalidateContextLength(context);
    }
    
    protected override void Read(ReadStream stream, StreamContext context) {
        var anyPropertiesChanged = false;
        while (stream.ReadNextPropertyID(out uint propertyID)) {
            var changed = false;
            switch (propertyID) {
                case (uint) PropertyID.HandViewType: {
                    changed = _handViewTypeProperty.Read(stream, context);
                    if (changed) FireHandViewTypeDidChange(handViewType);
                    break;
                }
                default: {
                    stream.SkipProperty();
                    break;
                }
            }
            anyPropertiesChanged |= changed;
        }
        if (anyPropertiesChanged) {
            UpdateBackingFields();
        }
    }
    
    private void UpdateBackingFields() {
        _handViewType = handViewType;
    }
    
}
/* ----- End Normal Autogenerated Code ----- */
