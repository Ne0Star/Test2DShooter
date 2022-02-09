using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patron : MonoBehaviour
{
    public float angle;
    public Vector2 mousePos;
    public Vector2 direction;
    public Rigidbody2D body;
    public int maxStrength, currentStrength;
    private void Awake()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
    private void Start()
    {
        maxStrength = Random.Range(1, 10);
        transform.GetChild(0).gameObject.SetActive(false);
    }
    public void Attack(Vector2 normalize, float power)
    {
        body.AddForce(normalize * power, ForceMode2D.Impulse);
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void Update()
    {
        //Vector2 direction = new Vector2(body.velocity.normalized.x * 2f, body.velocity.normalized.y * 2f);
        float current = Mathf.Atan2(body.velocity.y, body.velocity.x) * Mathf.Rad2Deg - angle;
        transform.rotation = Quaternion.AngleAxis(current, Vector3.forward);
        if (currentStrength > maxStrength)
        {
            Destroy(gameObject);
        }
        transform.GetChild(0).gameObject.SetActive(true);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        currentStrength++;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //transform.GetChild(0).gameObject.SetActive(false);
        currentStrength++;
        //direction = new Vector2(body.velocity.normalized.x * 2f, body.velocity.normalized.y * 2f);
        //sprite.enabled = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //transform.GetChild(0).gameObject.SetActive(true);
    }

    //public float angle, offset, rotateSpeed;
    //public bool useBlocker;
    //public int strength, lostStrength;
    //public AnimationCurve curva;
    //public Vector2 direction;
    //public Rigidbody2D body;
    //public SpriteRenderer sprite;
    //public Animator animator;
    //private void Start()
    //{
    //    animator = GetComponentInChildren<Animator>();
    //    //sprite = gameObject.GetComponent<SpriteRenderer>();
    //    body = gameObject.GetComponent<Rigidbody2D>();
    //    StartCoroutine(Waiter());
    //}
    //Vector2 lastPos;
    //public float distance = 1f;
    //public bool nextDestroy = false;
    //private void Update()
    //{
    //    if (distance == 0)
    //        Destroy(gameObject);
    //    return;
    //}
    //private IEnumerator Waiter()
    //{
    //    lastPos = transform.position;
    //    yield return new WaitForSeconds(0.01f);
    //    Vector2 dir = (Vector2)transform.position - lastPos;
    //    angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
    //    transform.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(transform.rotation.x, (-angle - offset), curva.Evaluate(Time.deltaTime * rotateSpeed)));
    //    distance = Vector2.Distance(lastPos, transform.position);
    //    if (useBlocker)
    //    {
    //        if (distance < 0.05f)
    //            nextDestroy = true;
    //    }
    //    StartCoroutine(Waiter());
    //    yield break;
    //}
    //public float bugTime_0 = 0f;
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    bugTime_0 += Time.deltaTime / 3f;
    //    if (bugTime_0 > 0.03f)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    sprite.enabled = true;
    //    bugTime_0 = 0f;
    //}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    animator.Play("hits");

    //    lostStrength++;
    //    if (lostStrength >= strength)
    //        Destroy(gameObject);
    //    if (nextDestroy)
    //    {
    //        Destroy(gameObject);
    //        nextDestroy = false;
    //    }
    //    direction = new Vector2(body.velocity.normalized.x * 2f, body.velocity.normalized.y * 2f);
    //    sprite.enabled = false;
    //}
}
