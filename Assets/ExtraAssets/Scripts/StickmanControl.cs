using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanControl : MonoBehaviour
{
    public float _jumpForce = 10f;
    public Animator anim { get; private set; }
    public static bool isDead { get; private set; }

    private Defeat defeatScreen;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        defeatScreen = FindObjectOfType<Defeat>(true);
        isDead = false;
    }

    private void Start()
    {
        foreach (var child in GetComponentsInChildren<Rigidbody>())
        {
            child.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX;
            child.mass = GetComponent<Rigidbody>().mass;
        }
    }

    private void Update()
    {
        //Dead check
        foreach (var child in transform.parent.GetComponentsInChildren<Transform>())
        {
            if (child.CompareTag("Cube"))
                return;
        }

        //If is dead
        isDead = true;
        anim.enabled = false;

        foreach (var child in GetComponentsInChildren<Rigidbody>())
            child.constraints = RigidbodyConstraints.None;

        defeatScreen.OpenScreen();
    }
}
