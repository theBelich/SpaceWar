using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Profiling;
using UnityEngine.Rendering.PostProcessing;

public class FadeMaterials : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float startFadeDistance;
    [SerializeField] private float fadeDistance;
    [SerializeField] private bool invert;
    
    [SerializeField] private PostProcessProfile profile;
    [SerializeField] private Material material;
    [SerializeField] private Color defaultMaterialColor;
    [SerializeField] private Color defaultEmissionColor;

    private Color color;
    private Color emissionColor;

    private void Awake()
    {
        defaultMaterialColor = material.color;
        defaultEmissionColor = material.GetColor("_EmissionColor");
        color = material.color;
    }

    private void OnDisable()
    {
        material.color = defaultMaterialColor;
        if (profile != null)
        {
            ResetBloom();
        }
    }

    void Update()
    {

        // ¬ычисл€ем рассто€ние между объектом и источником
        float distance = _camera.transform.localPosition.y - startFadeDistance;

        if (invert)
        {
            distance *= -1;
        }


        if (distance > fadeDistance)
        {
            // ≈сли рассто€ние больше заданной дистанции, прозрачность материала устанавливаетс€ на 0 (полностью прозрачный)
            SetAlpha(0);
        }
        else
        {
            // »наче, прозрачность устанавливаетс€ пропорционально рассто€нию от источника
            float opacity = 1.0f - (distance / fadeDistance);
            ChangeAlphaMaterial(1.0f - (distance / fadeDistance));

            if (profile != null)
            {
                ChangeBloomIntencity(opacity);
            }

        }
    }

    private void ChangeAlphaMaterial(float count)
    {

        if (count > 1)
        {
            color.a = 1;
        }
        else
        {
            color.a = count;
        }

        material.color = color;
    }


    private void SetAlpha(float count)
    {
        color.a = count;

        material.color = color;
    }


    private void ChangeBloomIntencity(float count)
    {
        var bloom = profile.GetSetting<Bloom>();
        bloom.intensity.value = count * 10;
        var inv = 1 - count;
        material.SetColor("_EmissionColor", new Color(count, 0, 0, inv));
    }

    private void ResetBloom()
    {
        var bloom = profile.GetSetting<Bloom>();
        bloom.intensity.value = 10;
        material.SetColor("_EmissionColor", defaultEmissionColor);
    }
}
