using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public GameObject player;
    public Player scriptPlayer;
    public float r_health;
    public bool alive;

    public bool win;


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

        SceneManager.sceneLoaded += Load;

    }

    // Use this for initialization
    void Start() {

        StartGame();


    }


    void Load(Scene sceneName, LoadSceneMode mode)
    {
        if (SceneController.instance.scene != 0 && SceneController.instance.scene != 1 && SceneController.instance.scene != 2 && SceneController.instance.scene != 3) 
        {
            StartGame();
        }
    }

    public void GameOver()
    {
        UIManager.instance.StartCoroutine(UIManager.instance.GameOverUI());
        AudioManager.instance.losingFX();
        alive = false;
    }


    public void Win()
    {
        win = true;
        int temp = SceneController.instance.scene - 3;
        temp++;
        PlayerPrefs.SetInt("Level " + temp, 1);
        player.gameObject.SetActive(false);
    }

    void StartGame()
    {
        UIManager.instance.TurnOnTurnOffPanel();
        win = false;
        player = GameObject.Find("Player");
        scriptPlayer = player.GetComponent<Player>();
        alive = true;
    }
}
 
