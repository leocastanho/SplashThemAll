using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    [System.Serializable]
    public class Levels
    {
        public string levelText;
        public bool enabled;
        public int unlocked;
        public bool activeText;
    }

    public GameObject button;
    public Transform buttonLocation;

    public List<Levels> levelList;

    private void Awake()
    {
        SceneManager.sceneLoaded += Load;
        Destroy(GameObject.Find("UIManager(Clone)"));
        Destroy(GameObject.Find("GameManager(Clone)"));
    }

    void Load(Scene sceneName, LoadSceneMode mode)
    {
        //StartButton = GameObject.Find("Start").GetComponent<Button>();
        //StartButton.onClick.AddListener(LevelMenu);
    }

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        ListAdd();
    }

    void ListAdd ()
    {
        foreach (Levels level in levelList)
        {
            GameObject newButton = Instantiate(button) as GameObject;
            LevelButton newBTN = newButton.GetComponent<LevelButton>();
            newBTN.levelTextBTN.text = level.levelText;
            

            if (PlayerPrefs.GetInt ("Level "+newBTN.levelTextBTN.text) == 1)
            {
                level.unlocked = 1;
                level.enabled = true;
                level.activeText = true;
            }

            newBTN.unlockedBTN = level.unlocked;
            newBTN.GetComponent<Button>().interactable = level.enabled;
            newBTN.GetComponentInChildren <Text>().enabled = level.activeText;

            newBTN.GetComponent<Button>().onClick.AddListener(() => ClickLevel("Level " + newBTN.levelTextBTN.text));

            newButton.transform.SetParent(buttonLocation, false);
        }
    }

    public void ClickLevel (string scene)
	{
		SceneManager.LoadScene (scene);
	}

    public void quitGame ()
	{
		Application.Quit();
	}
}
