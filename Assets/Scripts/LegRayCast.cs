using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegRayCast : MonoBehaviour
{

    private RaycastHit2D hit;
    private Transform transform;

    public LayerMask LayerMask;
    public Vector2 Position => hit.point;
    public Vector2 Normal => hit.normal;

    private void Awake()
    {
        transform = base.transform;
    }

    private void Update()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.down, 100f, LayerMask);

        Debug.DrawRay(transform.position, Vector2.down * 100f, Color.red);
    }
}
