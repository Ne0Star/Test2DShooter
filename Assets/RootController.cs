using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RootController : MonoBehaviour
{
    public float direction;
    public Color leftColor, rightColor, centerColor;
    [SerializeField] Transform leftDetector, rightDetector;
    [SerializeField] float Y = 1, angle;
    private Vector2 leftPoint, rightPoint, centerPoint, lastPos;
    public Vector2 StartPos, EndPos, MoveDirect, Direct;
    private void Start()
    {

        StartPos = transform.position;
        EndPos = transform.position;
    }
    private float GetTehAngle(Vector3 infrom, Vector3 into)
    {
        Vector2 from = Vector2.right;
        Vector3 to = into - infrom;

        float ang = Vector2.Angle(from, to);
        Vector3 cross = Vector3.Cross(from, to);

        if (cross.z > 0)
            ang = 360 - ang;

        ang *= -1f;

        return ang;
        //transform.rotation = Quaternion.Euler(0f, 0f, 90f) * Quaternion.AngleAxis(GetTehAngle(this.transform.position, info.point), new Vector3(0, 0, 1));
    }
    private void FixedUpdate()
    {
        lastPos = (Vector2)transform.position;

    }
    private void Update()
    {
        StartPos = transform.position;
        if (Vector2.Distance(StartPos, EndPos) > 1f)
        {
            Direct = StartPos - EndPos;
            MoveDirect = transform.TransformDirection(Direct) * 100;



            RaycastHit2D center = Physics2D.Raycast(transform.position + new Vector3(0f, 100f, 0f), Vector2.down, 1000f);
            RaycastHit2D left = Physics2D.Raycast(leftDetector.position + new Vector3(0f, 100f, 0f), Vector2.down, 1000f);
            RaycastHit2D right = Physics2D.Raycast(rightDetector.position + new Vector3(0f, 100f, 0f), Vector2.down, 1000f);
            if (center.collider != null)
            {
                centerPoint = center.point;
            }
            if (left.collider != null)
            {
                leftPoint = left.point;
            }
            if (right.collider != null)
            {
                rightPoint = right.point;
            }
            angle = (direction < 0) ? Vector3.Angle(transform.position, rightPoint) : Vector3.Angle(transform.position, leftPoint);
            angle = (direction < 0) ? angle * -1f : angle;
            //float y = (direction > 0) ? 180f : 0f;
            transform.localScale = new Vector3(MoveDirect.normalized.x > 0 ? -0.2f : 0.2f, 0.2f, 0.2f);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            EndPos = StartPos;
            direction = MoveDirect.x;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = leftColor;
        Gizmos.DrawSphere(leftPoint, 0.5f);
        Gizmos.color = rightColor;
        Gizmos.DrawSphere(rightPoint, 0.5f);
        Gizmos.color = centerColor;
        Gizmos.DrawSphere(centerPoint, 0.5f);
    }
}
