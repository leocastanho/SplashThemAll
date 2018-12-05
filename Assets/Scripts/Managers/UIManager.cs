using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;
    //panels
    [SerializeField]
    private GameObject losePanel, pausePanel, winPanel, winPanel2, winPanel3;
    //Pause buttons
    [SerializeField]
    private Button resumeButton, mainMenuButtonP;
    //Lose buttons
    [SerializeField]
    private Button mainMenuButtonL, tryAgainButton;
    //Win buttons
    [SerializeField]
    private Button mainMenuButtonW, nextLevelButtonW, mainMenuButtonW2, nextLevelButtonW2, mainMenuButtonW3, creditsW3;

    public GameObject healthBoss;

    public GameObject camTargetGroup;

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
        LoadUI();
    }


    // Use this for initialization
    void Start () {

 
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        time();


    }



    void Load (Scene sceneName, LoadSceneMode mode)
    {

        if (SceneController.instance.scene != 0 && SceneController.instance.scene != 1 && SceneController.instance.scene != 2 && SceneController.instance.scene != 3)
        {
            LoadUI();
        }
    }

    public void TurnOnTurnOffPanel()
    {
        StartCoroutine(time());
    }

    IEnumerator time()
    {
        yield return new WaitForSeconds(0.00000000000000000000000000001f);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        winPanel2.SetActive(false);
        winPanel3.SetActive(false);
        pausePanel.SetActive(false);
        healthBoss.SetActive(false);
        if (SceneController.instance.scene == 6)
        {
            camTargetGroup.SetActive(false);
        }
    }

    void Pause ()
    {
        pausePanel.SetActive(true);
        pausePanel.GetComponent<Animator>().Play("MoveUIPause");
        Time.timeScale = 0.0000001f;
    }

    void PauseReturn()
    {
        pausePanel.GetComponent<Animator>().Play("MoveUIPauseR");
        Time.timeScale = 1;
        StartCoroutine(waitPause());
    }

    IEnumerator waitPause()
    {
        yield return new WaitForSeconds(0.8f);
        pausePanel.SetActive(false);
    }

    public IEnumerator GameOverUI()
    {
        yield return new WaitForSeconds(1.2f);
        losePanel.SetActive(true);
    }

    public void WinUI()
    {
        winPanel.SetActive(true);
    }
    public void WinUI2()
    {
        winPanel2.SetActive(true);
    }
    public void WinUI3()
    {
        winPanel3.SetActive(true);
    }

    void TryAgain ()
    {
        SceneManager.LoadScene(SceneController.instance.scene);
    }

    void MainMenu ()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    void LevelMenu ()
    {
        SceneManager.LoadScene(3);
        Time.timeScale = 1;
    }

    void NextLevel()
    {
        if (GameManager.instance.win == true)
        {
            int temp = SceneController.instance.scene + 1;
            SceneManager.LoadScene(temp);
        }
        
    }

    void Credits()
    {
        if (GameManager.instance.win == true)
        {
            SceneManager.LoadScene(2);
        }

    }

    void LoadUI ()
    {
        //win
        winPanel = GameObject.Find("WinPanel");
        mainMenuButtonW = GameObject.Find("MainMenuW").GetComponent<Button>();
        nextLevelButtonW = GameObject.Find("NextLevelW").GetComponent<Button>();
        nextLevelButtonW.onClick.AddListener(NextLevel);
        mainMenuButtonW.onClick.AddListener(MainMenu);

        winPanel2 = GameObject.Find("WinPanel2");
        mainMenuButtonW2 = GameObject.Find("MainMenuW2").GetComponent<Button>();
        nextLevelButtonW2 = GameObject.Find("NextLevelW2").GetComponent<Button>();
        nextLevelButtonW2.onClick.AddListener(NextLevel);
        mainMenuButtonW2.onClick.AddListener(MainMenu);

        winPanel3 = GameObject.Find("WinPanel3");
        mainMenuButtonW3 = GameObject.Find("MainMenuW3").GetComponent<Button>();
        creditsW3 = GameObject.Find("CreditsW3").GetComponent<Button>();
        creditsW3.onClick.AddListener(Credits);
        mainMenuButtonW3.onClick.AddListener(MainMenu);

        //lose
        losePanel = GameObject.Find("LosePanel");
        tryAgainButton = GameObject.Find("TryAgain").GetComponent<Button>();
        mainMenuButtonL = GameObject.Find("MainMenuL").GetComponent<Button>();
        tryAgainButton.onClick.AddListener(TryAgain);
        mainMenuButtonL.onClick.AddListener(MainMenu);

        //pause
        pausePanel = GameObject.Find("PausePanel");
        resumeButton = GameObject.Find("Resume").GetComponent<Button>();
        mainMenuButtonP = GameObject.Find("MainMenuP").GetComponent<Button>();
        resumeButton.onClick.AddListener(PauseReturn);
        mainMenuButtonP.onClick.AddListener(MainMenu);

        //healthBoss
        healthBoss = GameObject.Find("HealthBoss");

        if (SceneController.instance.scene == 6)
        {
            camTargetGroup = GameObject.Find("CMvcam2");
        }
    }

    public IEnumerator CamTrasitionDelay()
    {
        yield return new WaitForSecondsRealtime(2f);

        healthBoss.SetActive(false);
        camTargetGroup.SetActive(false);
    }

}
