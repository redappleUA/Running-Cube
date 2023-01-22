using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldToStart : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0f;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
