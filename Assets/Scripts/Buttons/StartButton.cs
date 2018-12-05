using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

    public Button startButton;

    private void Awake()
    {
        startButton = this.GetComponent<Button>();
        startButton.onClick.AddListener(LevelMenu);
    }

    void LevelMenu()
    {
        SceneManager.LoadScene(3);
    }
}
