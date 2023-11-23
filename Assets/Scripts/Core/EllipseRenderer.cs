using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EllipseRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _starSystem;

    [Range(3, 36)]
    [SerializeField] private int segments;
    [SerializeField] private Ellipse ellipse;

    

    public void Init(Ellipse ellipse)
    {
        this.ellipse = ellipse;
        CalculateEllipse();
    }

    public void CalculateEllipse()
    {
        Vector3[] points = new Vector3[segments+1];
        for (int i = 0; i < segments; i++)
        {
            Vector3 pos = _starSystem.position + ellipse.Evaluate((float)i / segments);

            points[i] = pos;
        }

        points[segments] = points[0];
        _lineRenderer.positionCount = segments + 1;
        _lineRenderer.SetPositions(points);
    }

   
}
