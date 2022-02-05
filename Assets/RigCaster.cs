using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigCaster : MonoBehaviour
{
    public Transform target;
    public LayerMask mask;
    public RigCaster other;
    public bool isMove;
    public float stepHeight, speed, maxDistance;
    [SerializeField]
    private float lerp;
    [SerializeField]
    private Vector2 newPos, lastPos, currentPos;
    public bool fastDetect;
    private void Start()
    {
        lerp = 1f;
        newPos = lastPos = currentPos = transform.position;
        LevelManager.Instance.Events.onKeysUp.AddListener(() => StartCoroutine(DefaultPose()));
    }

    private IEnumerator DefaultPose()
    {
        yield return new WaitForSeconds(0.7f);
        lastPos = transform.position;
        fastDetect = true;
    }

    private void Update()
    {
        transform.position = currentPos;
        RaycastHit2D ray = Physics2D.Raycast(target.position, Vector2.down, 10f, mask);
        if (ray.collider != null)
        {
            if (Vector2.Distance(newPos, ray.point) >= maxDistance && lerp >= 0 && !other.isMove || fastDetect)
            {
                    newPos = ray.point;
                    lerp = 0f;
                    isMove = true;

                fastDetect = false;
            }
        }
        if (lerp <= 1f)
        {
            Vector2 tempPosition = Vector2.Lerp(lastPos, newPos, lerp);
            tempPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

            currentPos = tempPosition;

            lerp += Time.fixedDeltaTime * speed;
        }
        else
        {
            isMove = false;
            lastPos = transform.position;
        }
    }


    //public LayerMask mask;
    //public Transform target;
    //public RigCaster other;


    //public bool isMoving = false;
    //public float maxDistance, animationSpeed, stepHeight;

    //[SerializeField]
    //private float lerp;
    //[SerializeField]
    //private Vector2 currentPos, lastPos, newPos;

    //private void Start()
    //{
    //    lerp = 1f;
    //    currentPos = lastPos = newPos = transform.position;

    //}
    //private void FixedUpdate()
    //{
    //    transform.position = currentPos;
    //    RaycastHit2D ray = Physics2D.Raycast(target.position, Vector2.down, 100f, mask);
    //    if (ray.collider != null)
    //    {
    //        if (Vector2.Distance(newPos, ray.point) >= maxDistance && !other.isMoving && lerp >= 0f)
    //        {
    //            newPos = ray.point;
    //            lerp = 0f;
    //        }
    //    }
    //    if (lerp <= 1f)
    //    {
    //        isMoving = true;
    //        Vector2 tempPosition = Vector2.Lerp(lastPos, newPos, lerp);
    //        tempPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

    //        currentPos = tempPosition;

    //        lerp += Time.deltaTime * animationSpeed;
    //    }
    //    else
    //    {
    //        isMoving = false;
    //        lastPos = transform.position;
    //    }

    //}





    // Если дистанция между первой и второй ногой больше чем А, первая нога начинает движение и блокирует движение второй ноги до тех пор пока не придёт к точке
    //
    // Пустить луч, и двигать ногу точке пересечения если дистанция между старым и новым полождением ноги > n
    //
    //
    //



    //public Vector2 offset;
    //public LayerMask mask;
    //public RigCaster other;
    //public Transform target;
    //private Player player;
    //public float stepSpeed, stepHeight, stepDistanceView, stepDistance, speed, endOffset, lerp, maxStepHeight;
    //public bool distanceExit, isMoving;
    //[SerializeField]
    //private Vector2 newPos, lastPos, currentPos, lastStepPos;
    //private void Start()
    //{
    //    player = Player.Instance;
    //    currentPos = newPos = lastPos = transform.position;
    //    StartCoroutine(MoveBehaviour());
    //}

    //private IEnumerator MoveBehaviour()
    //{
    //    currentPos = transform.position;
    //    lastPos = currentPos;
    //    yield return new WaitUntil(() => distanceExit && !other.isMoving);
    //    RaycastHit2D ray = Physics2D.Raycast(target.position, Vector2.down, 100f, mask);
    //    yield return new WaitUntil(() => ray.collider != null);
    //    newPos = ray.point;
    //    if (newPos.y <= (lastPos.y + maxStepHeight))
    //    {
    //        lerp = 0f;
    //        while (lerp < 1f)
    //        {
    //            isMoving = true;
    //            Vector2 tempPosition = Vector2.Lerp(lastPos, newPos, lerp);
    //            tempPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;

    //            currentPos = tempPosition;
    //            lerp += Time.deltaTime * speed;
    //            yield return null;
    //        }
    //    }
    //    isMoving = false;
    //    StartCoroutine(MoveBehaviour());
    //    yield break;
    //}
    //public void SetPose()
    //{
    //    RaycastHit2D ray = Physics2D.Raycast(target.position, Vector2.down, 100f, mask);
    //    if (ray.collider != null)
    //    {
    //        transform.position = ray.point;
    //    }
    //}
    //private void Update()
    //{
    //    transform.position = currentPos;
    //    stepDistanceView = Vector2.Distance(new Vector2(transform.position.x, 0f), new Vector2(target.position.x, 0f));
    //    distanceExit = (stepDistanceView > stepDistance);
    //    RaycastHit2D ray = Physics2D.Raycast(target.position, Vector2.down, 100f, mask);
    //    if (distanceExit)
    //    {
    //        if (ray.collider != null && lerp >= 1)
    //        {
    //            newPos = ray.point;
    //            lerp = 0f;
    //        }
    //    }
    //    if (lerp < 1)
    //    {
    //        Vector2 tempPosition = Vector2.Lerp(lastPos, newPos, lerp);
    //        tempPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;
    //        currentPos = tempPosition;
    //        lerp += Time.deltaTime * speed;
    //    }
    //    else
    //    {
    //        lastPos = newPos;
    //    }


    //}
    //private void FixedUpdate()
    //{
    //    speed = player.currentControllerData.speed;
    //    transform.position = currentPos;
    //    stepDistanceView = Vector2.Distance(new Vector2(transform.position.x, 0f), new Vector2(target.position.x, 0f));
    //    distanceExit = (stepDistanceView > stepDistance);
    //}
    //private void OnDrawGizmos()
    //{

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(newPos, 0.2f);
    //}
    // Если дистанция между ногой и таргетом >= критической, сделать шаг, к таргету

    //public LineRenderer line;
    //public LayerMask mask;
    //public Transform targetPoint;
    //public RigCaster other;
    //public float stepDistance, speed, lerp, stepHeight, moveSpeed;
    //public bool isMoving;
    //void Start()
    //{
    //    StopAllCoroutines();
    //    StartCoroutine(Waiter());
    //}
    //public IEnumerator MoveStep()
    //{
    //    lerp = 0;
    //    RaycastHit2D ray = Physics2D.Raycast(targetPoint.position, Vector2.down, 100f, mask);

    //    if (ray.collider != null)
    //    {

    //    }
    //    //Vector2 newPos = ray.point;
    //    //Vector2 lastPos = transform.position;
    //    //while (lerp < 1f)
    //    //{
    //    //    Vector2 tempPosition = Vector2.Lerp(lastPos, newPos, lerp);
    //    //    tempPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;
    //    //    transform.position = tempPosition;
    //    //    lerp += Time.deltaTime * speed;
    //    //    yield return new WaitForFixedUpdate();
    //    //}
    //    yield break;
    //}
    //private IEnumerator Waiter()
    //{
    //    yield return new WaitForSeconds(moveSpeed);
    //    StartCoroutine(MoveStep());
    //    StartCoroutine(Waiter());
    //    yield break;
    //}
}
