using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreditsButton : MonoBehaviour {

    public Button creditsButton;

    private void Awake()
    {
        creditsButton = this.GetComponent<Button>();
        creditsButton.onClick.AddListener(Credits);
    }

    void Credits()
    {
        SceneManager.LoadScene(2);
    }
}
