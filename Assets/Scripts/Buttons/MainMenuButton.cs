using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour {

    public Button mainMenuButton;

    private void Awake()
    {
        mainMenuButton = this.GetComponent<Button>();
        mainMenuButton.onClick.AddListener(LevelMenu);
    }

    void LevelMenu()
    {
        SceneManager.LoadScene(1);
    }
}
