using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    public Transform target;
    public AnimationCurve interpolator;
    public float speed, radius, endSize;
    private Camera cam;
    private float startSize;
    private bool block = false;
    public Vector3 offset;
    Vector2 mousePos, addtive = Vector2.zero;
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        startSize = cam.orthographicSize;
        StartCoroutine(Zoom(endSize));
    }
    public IEnumerator Zoom(float value)
    {
        while (cam.orthographicSize < value)
        {
            block = true;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, value, interpolator.Evaluate(Time.deltaTime * 4));
            yield return new WaitForFixedUpdate();
        }

        yield break;
    }

    void FixedUpdate()
    {
        transform.position = offset + Vector3.Lerp(transform.position, new Vector3(target.position.x + addtive.x, target.position.y + addtive.y, transform.position.z), interpolator.Evaluate(speed * Time.deltaTime));
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 pos = (Vector2)mousePos - (Vector2)target.position;
        addtive = (pos.normalized * radius);

    }
}
