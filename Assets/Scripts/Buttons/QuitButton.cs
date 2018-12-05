using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour {

    public Button quitButton;

    private void Awake()
    {
        quitButton = this.GetComponent<Button>();
        quitButton.onClick.AddListener(quitGame);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
