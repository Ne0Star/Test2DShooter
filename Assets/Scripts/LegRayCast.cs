using UnityEngine;

public class LegRayCast : MonoBehaviour
{

    private RaycastHit2D hit;

    public LayerMask LayerMask;
    public Vector2 Position => hit.point;
    public Vector2 Normal => hit.normal;

    private void Awake()
    {

    }

    private void Update()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.down, 100f, LayerMask);

#if UNITY_EDITOR
        Debug.DrawRay(transform.position, Vector2.down * 100f, Color.red);
#endif
    }
}
