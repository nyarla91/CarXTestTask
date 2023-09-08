using UnityEngine;

namespace Monster
{
    public interface ITowerTarget
    {
        Vector3 CurrentPosition { get; }
        Vector3 PredictPosition(float timeAfter);
    }
}