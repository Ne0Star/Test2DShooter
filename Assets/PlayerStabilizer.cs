using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Отвечает за стабилизацию тела игрока
/// </summary>
public class PlayerStabilizer : MonoBehaviour
{
    [Header("Внимательно проверяйте слои масок, что-бы лучи не соприкасались с родителем")]

    [SerializeField] private Transform Center;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private float CurveSpeed, Height;
    [SerializeField] private LayerMask InteractionMask;
    [SerializeField] private AnimationCurve Curve;
    [SerializeField] private Player player;
    void Update()
    {
        Debug.DrawRay(Center.position, Vector2.down * Height, Color.yellow);
        RaycastHit2D hit = Physics2D.Raycast(Center.position, Vector2.down, Height + 1f, InteractionMask);
        if (hit.collider != null)
        {
            player.Data.debugData.CenterPoint = hit.point;
            body.position = Vector2.Lerp(
                body.position,
                new Vector2(body.position.x, hit.point.y + Height),
               Curve.Evaluate(CurveSpeed * Time.fixedDeltaTime)
                );
            Vector2 vel = body.velocity;
            vel.y = 0;
            body.velocity = vel;
        }
    }
}
