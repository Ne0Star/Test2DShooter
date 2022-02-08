using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using UnityEngine.Jobs;

public class LegController : MonoBehaviour
{
    [SerializeField]
    private LegData[] legs;
    [SerializeField]
    private float stepLength = 0.75f;

    private TransformAccessArray acces_legs;

    private void Start()
    {
    }

    private void FixedUpdate()
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


    [BurstCompile]
    struct CameraMoveJob : IJobParallelForTransform
    {
        public void Execute(int index, TransformAccess transform)
        {
            throw new System.NotImplementedException();
        }
    }
}
