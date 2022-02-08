using System.Collections;
using Unity.Burst;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Jobs;

public class CameraController : MonoBehaviour
{


    public Transform target;
    public float speed, radius, endSize;
    private Camera cam;
    private float startSize;
    [SerializeField]
    private bool block = false;
    public Vector3 offset;
    Vector2 mousePos, addtive = Vector2.zero;
    private Transform[] data;
    private TransformAccessArray acces_data;
    void Start()
    {
        data = new Transform[1];
        data[0] = transform;

        acces_data = new TransformAccessArray(data);
        cam = gameObject.GetComponent<Camera>();
        startSize = cam.orthographicSize;
        StartCoroutine(Zoom(endSize));
    }
    public IEnumerator Zoom(float value)
    {
        while (cam.orthographicSize < value)
        {
            block = true;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, value + 0.2f, Time.deltaTime * 4);
            yield return new WaitForFixedUpdate();
        }
        block = false;
        yield break;
    }

    private void OnDestroy()
    {
        acces_data.Dispose();
    }

    void FixedUpdate()
    {
        if (block)
            return;

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 pos = (Vector2)mousePos - (Vector2)target.position;
        addtive = (pos.normalized * radius);
        CameraMoveJob job = new CameraMoveJob
        {
            DeltaTime = Time.deltaTime,
            Speed = speed,
            Offset = offset,
            Addtive = addtive,
            MousePos = mousePos,
            ResultPosition = target.position
        };
        job.Schedule(acces_data).Complete();
    }


    [BurstCompile]
    struct CameraMoveJob : IJobParallelForTransform
    {
        public float DeltaTime, Speed;
        public Vector3 Offset, Addtive, MousePos, ResultPosition;

        public void Execute(int index, TransformAccess transform)
        {
            transform.position = Offset + Vector3.Lerp(transform.position, new Vector3(ResultPosition.x + Addtive.x, ResultPosition.y + Addtive.y, transform.position.z), Speed * DeltaTime);
        }
    }
}
