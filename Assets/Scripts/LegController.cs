using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegController : MonoBehaviour
{
    [SerializeField]
    private LegData[] legs;
    [SerializeField]
    private float stepLength = 0.75f;

    private void Update()
    {
        for (int index = 0; index < legs.Length; index++)
        {
            ref var legData = ref legs[index];
            if (!CanMove(index)) continue;
            if (!legData.Leg.isMoving && 
                !(Vector2.Distance(legData.Leg.Position, legData.Raycast.Position) > stepLength)) continue;
            legData.Leg.MoveTo(legData.Raycast.Position);
        }

    }

    public bool CanMove(int legIndex)
    {
        var legsCount = legs.Length;
        var n1 = legs[(legIndex + legsCount - 1) % legsCount];
        var n2 = legs[(legIndex + 1) % legsCount];
        return !n1.Leg.isMoving && !n2.Leg.isMoving;
    }

    [System.Serializable]
    private struct LegData
    {
        public LegTarget Leg;
        public LegRayCast Raycast;
    }
}
