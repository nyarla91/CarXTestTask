using UnityEngine;

namespace Monster
{
    public interface ITowerTarget
    {
        bool Alive { get; }
        Vector3 CurrentPosition { get; }
        Vector3 PredictPosition(float timeAfter);
    }
}