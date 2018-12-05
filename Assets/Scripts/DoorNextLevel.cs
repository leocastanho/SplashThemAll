using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorNextLevel : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (SceneController.instance.scene == 4)
            {
                GameManager.instance.Win();
                UIManager.instance.WinUI();
            }
            if (SceneController.instance.scene == 5)
            {
                GameManager.instance.Win();
                UIManager.instance.WinUI2();
            }
            if (SceneController.instance.scene == 6)
            {
                GameManager.instance.Win();
                UIManager.instance.WinUI3();
            }
        }
    }
}
