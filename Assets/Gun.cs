using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject BulletPrefab;
    [Range(0.5f, 6f)]
    public float Power = 1f;
    public Player player;
    //public Controller Trajectory;
    //public TrajectoryRendererAdvanced Trajectory;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.FindObjectOfType<Player>();
        if (!player)
            return;
    }
    private void Update()
    {
        Vector2 mouseInWorld = mainCamera.ScreenToWorldPoint((Vector2)Input.mousePosition);

        Vector2 speed = (mouseInWorld - (Vector2)transform.position).normalized * Power;

        if (Input.GetMouseButton(0))
        {
            if (player.Data.moveStatus != Player.MoveStatus.stopLeft && player.Data.moveStatus != Player.MoveStatus.stopRight)
            {
            if (player.Data.moveStatus == Player.MoveStatus.moveLeft)
            {
                if (mouseInWorld.x > transform.position.x)
                    return;
            }
            else if (player.Data.moveStatus == Player.MoveStatus.moveRight)
            {
                if (mouseInWorld.x < transform.position.x)
                    return;
            }
            }


        }
        if (Input.GetMouseButtonDown(0))
        {
            if (Vector2.Distance(mouseInWorld, transform.position) > 1f)
            {
                GameObject bullet = Instantiate(BulletPrefab, (Vector2)transform.position, Quaternion.identity);
                Patron p = bullet.GetComponent<Patron>();
                p.direction = (mouseInWorld - (Vector2)transform.position).normalized * 10f;
                p.Attack((mouseInWorld - (Vector2)transform.position).normalized, Power);
            }
        }
    }
}