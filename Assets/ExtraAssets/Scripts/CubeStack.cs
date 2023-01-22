using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeStack : MonoBehaviour
{
    [SerializeField] GameObject _pointsPrefab;

    private ParticleSystem _stackEffect;
    private StickmanControl _stickman;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _stickman = FindObjectOfType<StickmanControl>();
        _stackEffect = GameObject.FindGameObjectWithTag("CubeStackEffect").GetComponent<ParticleSystem>();
    }

    //Checking collision with the wall
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (Physics.Raycast(transform.position, Vector3.forward, 1f))
            {
                if(!CameraWalker.isShake)
                    CameraWalker.isShake = true;

                _rb.freezeRotation = false;
                _rb.constraints = RigidbodyConstraints.None;

                transform.SetParent(null);
            }
        }
    }

    //Pickup the new cube
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CubePickup"))
        {
            other.gameObject.tag = "Cube";

            //Move object to free space
            foreach (var child in transform.parent.GetComponentsInChildren<Transform>())
            {
                if (child.CompareTag("Player") || child.CompareTag("Cube"))
                    child.position += new Vector3(0f, 1f, 0f);
            }

            //Pickup
            other.transform.SetParent(transform.parent, false);
            other.transform.localScale = Vector3.one;

            //Cube stack effect
            _stackEffect.Play();

            //Jump
            _stickman.anim.SetTrigger("Jump");
            _stickman.transform.position += new Vector3(0f, .4f, 0f);

            //Points
            var points = Instantiate(_pointsPrefab, Vector3.zero, Quaternion.identity, _stickman.transform);
            Destroy(points, 1f);
        }
    }
}
