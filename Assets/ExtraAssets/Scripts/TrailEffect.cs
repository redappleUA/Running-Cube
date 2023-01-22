using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEffect : MonoBehaviour
{
    [SerializeField] float _time = 1f; // how long the trail will be visible
    [SerializeField] float _width = 0.1f; // width of the trail
    [SerializeField] Color _startColor = Color.white; // start color of the trail
    [SerializeField] Color _endColor = Color.white; // end color of the trail

    private TrailRenderer _trailRenderer;

    void Start()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
        _trailRenderer.time = _time;
        _trailRenderer.startWidth = _width;
        _trailRenderer.endWidth = _width;
        _trailRenderer.startColor = _startColor;
        _trailRenderer.endColor = _endColor;
    }
}
