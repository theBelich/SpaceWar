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

    private float fadeTime;

    public void Init(Ellipse ellipse)
    {
        this.ellipse = ellipse;
        CalculateEllipse();
    }

    private void OnEnable()
    {
        fadeTime = 0;
        FadeIn(_lineRenderer.material);
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

    private void Update()
    {
        var mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        if (mouseWheel != 0)
        {
            fadeTime += mouseWheel;


            FadeIn(_lineRenderer.material);
        }
    }

    void FadeIn(Material material)
    {
        material.color = new Color(material.color.r, material.color.g, material.color.b, Mathf.Lerp(0, 1, fadeTime));
        
    }
   
}
