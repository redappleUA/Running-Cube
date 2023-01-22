using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Defeat : MonoBehaviour
{
    private Button restartButton;
    private bool isClicked = false;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    void RestartButtonClicked()
    {
        if (!isClicked) //It clicked many times cause event system is bugging
        {
            SceneManager.LoadScene("GameScene");
            isClicked = true;
        }
    }

    public void OpenScreen()
    {
        gameObject.SetActive(true);

        var root = GetComponent<UIDocument>().rootVisualElement;
        restartButton = root.Q<Button>("Restart");

        restartButton.clicked += RestartButtonClicked;
    }
}
