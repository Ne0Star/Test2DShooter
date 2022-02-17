using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D.Animation;

public class LegRayCast : MonoBehaviour
{
    public float UpdateSpeed;

    private RaycastHit2D hit;
    /// <summary>
    /// Маска соприкосновений для луча
    /// </summary>
    public LayerMask LayerMask;
    /// <summary>
    /// Точка соприкосновения луча с поверхностью
    /// </summary>
    public Vector2 Position => hit.point;
    /// <summary>
    /// Нормально поверхности соприкосновениия
    /// </summary>
    public Vector2 Normal => hit.normal;


    private void Update()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.down, 100f, LayerMask);

#if UNITY_EDITOR
        Debug.DrawRay(transform.position, Vector2.down * 100f, Color.red);
#endif
    }
}
