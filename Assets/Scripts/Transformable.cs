using System;
using UnityEngine;

public abstract class Transformable : MonoBehaviour
{
    private Transform _transform;

    [Obsolete] public new Transform transform => Transform;
    
    public Transform Transform => _transform ??= gameObject.transform;
}