using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject patron;
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        foreach (var rb in FindObjectsOfType<Rigidbody2D>())
        {
            bodies.Add(rb, new BodyData());
        }
    }

    private LineRenderer lineRenderer;
    public bool blocker;
    public Dictionary<Rigidbody2D, BodyData> bodies = new Dictionary<Rigidbody2D, BodyData>();
    public IEnumerator ShowTrajectory(Vector2 origin, Vector2 speed)
    {
        // Подготовка:
        foreach (var body in bodies)
        {
            body.Value.position = (Vector2)body.Key.transform.position;
            body.Value.rotation = body.Key.transform.rotation;
            body.Value.velocity = body.Key.velocity;
            body.Value.angularVelocity = body.Key.angularVelocity;
        }

        GameObject bullet = Instantiate(patron, origin, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(speed, ForceMode2D.Impulse);

        Physics2D.autoSimulation = false;

        Vector3[] points = new Vector3[20];
        lineRenderer.positionCount = points.Length;
        points[0] = origin;
        for (int i = 1; i < points.Length; i++)
        {
            Physics2D.Simulate(0.2f);

            points[i] = (Vector2)bullet.transform.position;
            yield return new WaitForSeconds(0.1f);
        }

        lineRenderer.SetPositions(points);

        // Зачистка:
        Physics2D.autoSimulation = true;

        foreach (var body in bodies)
        {
            body.Key.transform.position = (Vector2)body.Value.position;
            body.Key.transform.rotation = body.Value.rotation;
            body.Key.velocity = body.Value.velocity;
            body.Key.angularVelocity = body.Value.angularVelocity;
        }

        Destroy(bullet.gameObject);
    }
    public class BodyData
    {
        public Vector2 position;
        public Quaternion rotation;
        public Vector3 velocity;
        public float angularVelocity;
    }
}
