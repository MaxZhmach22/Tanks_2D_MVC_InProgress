using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GlowAnimation : MonoBehaviour
{
    [SerializeField] private Image _imageWithMaterial;
    private float _maxValue = 1;
    private float _minValue = 0f;
    private float _time = 0;
    private float _transparency;
    private Material _material;

    void Awake()
    {
        Init();
        StartCoroutine(GLow());
    }

    private void Init() =>
        _material = _imageWithMaterial.material;

    private IEnumerator GLow()
    {
        while (true)
        {
            _time += Time.deltaTime;
            _transparency = Mathf.Lerp(_minValue, _maxValue, _time);
            _material.color = new Color(1, 1, 1, _transparency);
            CheckTime();
            yield return null;
        }
        
    }

    private void CheckTime()
    {
        if (_time < 1f)
            return;

        var temp = _maxValue;
        _maxValue = _minValue;
        _minValue = temp;
        _time = 0;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
