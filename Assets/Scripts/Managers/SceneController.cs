using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public int scene = -1;
    [SerializeField]
    private GameObject UIManagerGo,GameManagerGo, TimeManagerGo;

    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += sceneCheck;

    }



    void sceneCheck (Scene sceneName, LoadSceneMode modo)
    {
        scene = SceneManager.GetActiveScene().buildIndex;

        if (scene != 0 && scene != 1 && scene != 2 && scene != 3)
        {
            Instantiate(UIManagerGo);
            Instantiate(GameManagerGo);
            Instantiate(TimeManagerGo);
        }
    }
}
