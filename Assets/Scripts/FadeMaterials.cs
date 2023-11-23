using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FadeMaterials : MonoBehaviour
{

    [SerializeField] private Transform _cameraPosition;
    [SerializeField] private float StartFadeTime;
    [Header("Borders")]
    [SerializeField] private Vector2 PlanetBorder;
    [SerializeField] private Vector2 ClusterBorder;

    [Header("Materials")]
    [SerializeField] private List<Material> materials;
    [SerializeField] private Material lineRendererMaterial;


    private bool IsFadingIn;
    private bool IsFadingOut;

    public Material currentMaterial;
    public float fadeTime;
    public bool enableFading = false;

    private int currentLayerIndex;
    private Vector2 currentBorder;

    private void Awake()
    {
        fadeTime = 0;

        ResetMaterials();
        currentMaterial = materials[currentLayerIndex];
    }

    private void OnDisable()
    {
        ResetMaterials();
    }

    public void Update()
    {
        var mouseWheel = Input.GetAxis("Mouse ScrollWheel");

        if (mouseWheel < 0)
        {
            var dist =  _cameraPosition.localPosition.y - currentBorder.x;

            if (dist > 0)
            {
                fadeTime = dist / currentBorder.y;
            }
        }
        if (mouseWheel > 0)
        {
            var dist = _cameraPosition.localPosition.y - currentBorder.x;

            if (dist > 0)
            {
                fadeTime = dist / currentBorder.y;
            }
        }

        
    }

    private void ResetMaterials()
    {
        ResetAlpha(materials[0], 0);

        ResetAlpha(materials[1], 1);
        ResetEmission(materials[1]);

        ResetAlpha(materials[2], 0);
    }


    public void ChangeLayer(int layer)
    {
        currentLayerIndex = layer;
        currentMaterial = materials[currentLayerIndex];
        if (layer == 0)
        {
            currentBorder = PlanetBorder;
        }
        if (layer == 1)
        {
            currentBorder = ClusterBorder;
        }
    }

    public void FadeIn()
    {
        IsFadingIn = true;
    }

    public void FadeOut()
    {
        IsFadingOut = true;
    }

    public void FadeIn(Material material)
    {
        
        material.color = new Color(material.color.r, material.color.g, material.color.b, fadeTime);
    }

    public void ResetAlpha(Material material, float count)
    {
        material.color = new Color(material.color.r, material.color.g, material.color.b, count);
    }

    public void ResetEmission(Material material)
    {
        material.SetColor("_EmissionColor", new Color(1, 0, 0, 1));
    }

    private void ChangeEmissionColor()
    {
        var lerp = Mathf.Lerp(0,1, fadeTime);
        var invertedLerp = Mathf.Lerp(1, 0, fadeTime);
        materials[1].SetColor("_EmissionColor", new Color(lerp, 0, 0, 1));
    }
}
