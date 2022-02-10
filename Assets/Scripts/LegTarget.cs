using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using UnityEngine.Jobs;

public class LegTarget : MonoBehaviour
{
    [SerializeField] private float stepSpeed;
    [SerializeField] private AnimationCurve stepCurve;

    private Vector2 position;
    private Movement? movement;
    private Player player;
    public Vector2 Position => position;
    public bool isMoving => (movement != null);

    private TransformAccessArray acces_data;
    private LegTargetJob job;
    private float defaultStepSpeed;
    private void Awake()
    {
        acces_data = new TransformAccessArray(new Transform[1] { transform });
        player = GameObject.FindObjectOfType<Player>();
        defaultStepSpeed = stepSpeed;
        position = transform.position;
    }

    public void OnDestroy()
    {
        acces_data.Dispose();
    }

    private void FixedUpdate()
    {
        //stepSpeed = (defaultStepSpeed * Mathf.Lerp(stepSpeed, player.Data.speed, 0.1f * Time.fixedDeltaTime));
        if (movement != null)
        {
            Movement m = movement.Value;
            m.Progress = Mathf.Clamp01(m.Progress + Time.fixedDeltaTime * stepSpeed);
            position = m.Evaluate(Vector2.up, stepCurve);
            if (m.Progress < 1f)
            {
                movement = m;
            }
            else
            {
                movement = null;
            }
        }
        job = new LegTargetJob
        {
            position = position,
        };
        job.Schedule(acces_data).Complete();
    }

    public void MoveTo(Vector2 targetPosition)
    {
        if (movement == null)
        {
            movement = new Movement
            {
                Progress = 0f,
                FromPosition = position,
                ToPosition = targetPosition
            };
        }
        else
        {
            movement = new Movement
            {
                Progress = movement.Value.Progress,
                FromPosition = movement.Value.FromPosition,
                ToPosition = movement.Value.ToPosition
            };
        }
    }



    [BurstCompile]
    struct LegTargetJob : IJobParallelForTransform
    {
        public Vector2 position;

        public void Execute(int index, TransformAccess transform)
        {
            transform.position = position;
        }
    }


    private struct Movement
    {
        public float Progress;
        public Vector2 FromPosition;
        public Vector2 ToPosition;

        public Vector2 Evaluate(in Vector2 up, AnimationCurve stepCurve)
        {
            return Vector2.Lerp(@FromPosition, ToPosition, Progress) + up * stepCurve.Evaluate(Progress);
        }

    }
}
