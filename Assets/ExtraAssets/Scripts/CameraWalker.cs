
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWalker : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] float _duration = 0.5f;
    [SerializeField] AnimationCurve _shakeCurve;

    public static bool isShake { get; set; }

    private Vector3 _offset;

    void Start()
    {
        isShake = false;
        _offset = transform.position - _player.transform.position;
    }

    private void FixedUpdate()
    {
        if (isShake) //Shake cam and vibrate
        {
            Handheld.Vibrate();
            Invoke(nameof(Handheld.Vibrate), _duration); //To stop vibration

            StartCoroutine(CameraShakeCoroutine());
        }
    }

    void LateUpdate()
    {
       
        //else //Follow player
        if(!isShake)
        {
            transform.position = _player.transform.position + _offset;

            if (transform.position.y > 7.05f)
                transform.position = new Vector3(transform.position.x, 7.05f, transform.position.z);
        }
    }

    private IEnumerator CameraShakeCoroutine()
    {
        float elapsed = 0f;
        Vector3 originalCamPos = transform.position;

        while (elapsed < _duration)
        {
            elapsed += Time.deltaTime;
            float strength = _shakeCurve.Evaluate(elapsed / _duration);
            transform.position = _player.transform.position + _offset + Random.insideUnitSphere * strength;

            yield return null;
        }

        transform.position = originalCamPos;
        isShake = false;
        Debug.Log("Shaked");
    }
}