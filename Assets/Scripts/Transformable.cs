using System;
using UnityEngine;

[ExecuteAlways]
public abstract class Transformable : MonoBehaviour {
    private Transform m_transform;
    
    [Obsolete] public new Transform transform => Transform;
    public Transform Transform => m_transform ??= gameObject.transform;
}