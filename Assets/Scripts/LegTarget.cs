using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegTarget : MonoBehaviour
{
    [SerializeField] private float stepSpeed;
    [SerializeField] private AnimationCurve stepCurve;

    private Vector2 position;
    private Movement? movement;
    private Transform transform;

    public Vector2 Position => position;
    public bool isMoving => (movement != null);

    private void Awake()
    {
        transform = base.transform;
        position = transform.position;
    }

    private void Update()
    {
        if (movement != null)
        {
            Movement m = movement.Value;
            m.Progress = Mathf.Clamp01(m.Progress + Time.deltaTime * stepSpeed);
            position = m.Evaluate(Vector2.up, stepCurve);
            if(m.Progress < 1f)
            {
                movement = m;
            } else
            {
                movement = null;
            }
        }
        transform.position = position;
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
