using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_LaunchLine : MonoBehaviour
{
    public LineRenderer lr;
    public GameObject Ball;
    public float velocity;
    public float angle;
    public int resolution;

    public float g; // Gravity force on Y axis. (Valor Absoluto)
    float radianAngle;

    public Gradient White;
    public Gradient Transparent;

    public bool active;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        g = Mathf.Abs(Physics2D.gravity.y);
    }

    // Start is called before the first frame update.
    void Start()
    {

        RenderArc();
    }

    public void RenderArc()
    {
        // Set Line Renderer Settings.
        lr.positionCount = resolution + 1;
        lr.SetPositions(CalculateArcArray());

    }


    //Create an array of points.
    Vector3[] CalculateArcArray()
    {
        Vector3[] ArcArray = new Vector3[resolution + 1];

        radianAngle = Mathf.Deg2Rad * angle;

        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

        for (int i = 0; i <= resolution ; i++)
        {
            float t = (float) i / (float) resolution;
            ArcArray[i] = calculateArcPoint(t, maxDistance);
        }

        return ArcArray;
    }

    //Calculate height and distance of each vertex;
    Vector3 calculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = (x * Mathf.Tan(radianAngle)) - ( (g * x * x)/(2 * (velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle))));

        return new Vector3(x, y);
    }

    private void LateUpdate()
    {
        if (!active || angle < 0)
        {
            lr.colorGradient = Transparent;
        }
        else
        {
            lr.colorGradient = White;
        }

        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
