using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class JobTest : MonoBehaviour
{
    public Transform[] data;
    private TransformAccessArray accesData;
    public bool useJob;
    private NativeArray<Vector2> _Positions;
    private NativeArray<Vector2> _Velocities;

    void Start()
    {
        _Positions = new NativeArray<Vector2>(data.Length, Allocator.Persistent);
        _Velocities = new NativeArray<Vector2>(data.Length, Allocator.Persistent);
        for (int i = 0; i < data.Length; i++)
        {
            _Positions[i] = data[i].position;
            _Velocities[i] = new Vector2(Random.insideUnitSphere.x, Random.insideUnitSphere.y) * 2f;
        }
        accesData = new TransformAccessArray(data);
    }
    private void OnDestroy()
    {
        _Positions.Dispose();
        _Velocities.Dispose();
        accesData.Dispose();
    }
    private void Update()
    {
        if (useJob)
        {
            var d = new MoveForwardJob
            {
                DeltaTime = Time.deltaTime,
                Positions = _Positions,
                Velocities = _Velocities
            };
            d.Schedule(accesData).Complete();
        }
        else
        {

        }
    }
    //private JobHandle CreateMoveForwardJob(Transform[] transformData)
    //{
    //    // create an transform access array that wraps around the given transforms.
    //    accesData = new TransformAccessArray(transformData);
    //    // you could also manually set up the array here using
    //    //    transformsAccess[index] = ....
    //    return new MoveForwardJob
    //    {
    //        DeltaTime = Time.deltaTime,
    //        Positions = Positions,
    //        Velocities = Velocities
    //    }.Schedule(accesData);
    //}

}

[BurstCompile]
struct MoveForwardJob : IJobParallelForTransform
{
    public float DeltaTime;
    public NativeArray<Vector2> Positions;
    public NativeArray<Vector2> Velocities;

    public void Execute(int index, TransformAccess transform)
    {
        var velocity = Velocities[index];
        transform.position += (Vector3)velocity * DeltaTime;
        float angle = AngleBetweenPoints(transform.position, velocity);
        var targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, DeltaTime);
        Positions[index] = transform.position;
    }
    float AngleBetweenPoints(Vector2 a, Vector2 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
