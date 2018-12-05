using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public static TimeManager instance;

    public float[] slowMotionGradientLength;
    public float[] slowMotionLength;
    public float[] slowMotionFactor;

    private float tempBoss = -0.1f;
    private float tempEnemies = -0.1f;

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

    }

    // Update is called once per frame
    void Update () {

        StopSlowMotionBoss();
        StopSlowMotionEnemies();

        //print(tempBoss.ToString());
        //print(tempEnemies.ToString());

    }
    

    //Boss
    public IEnumerator CallSlowMotionBoss()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = slowMotionFactor[0];
        tempBoss = 0;
    }
    public void StopSlowMotionBoss()
    {
        if (Time.timeScale >= slowMotionFactor[0] && Time.timeScale < 1 && tempBoss >=0)
        {
            //temp += (1f / slowMotionLength) / 60;
            tempBoss += (1f / slowMotionLength[0]) * Time.unscaledDeltaTime;
            //print(tempBoss.ToString());
            tempBoss = Mathf.Clamp(tempBoss, 0f, 1f);

            if (tempBoss == 1)
            {
                Time.timeScale += (1f / slowMotionGradientLength[0]) * Time.unscaledDeltaTime;
                Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
                //print(Time.timeScale.ToString());
                if (Time.timeScale >= 1)
                {
                    tempBoss = -0.1f;
                }
            }
        }
    }


    //Enemies
    public IEnumerator CallSlowMotionEnemies()
    {
        yield return new WaitForSecondsRealtime(0.0001f);
        Time.timeScale = slowMotionFactor[1];
        tempEnemies = 0;
    }
    public void StopSlowMotionEnemies()
    {
        if (Time.timeScale >= slowMotionFactor[1] && Time.timeScale < 1 && tempEnemies >=0)
        {
            //temp += (1f / slowMotionLength) / 60;
            tempEnemies += (1f / slowMotionLength[1]) * Time.unscaledDeltaTime;
            //print(tempEnemies.ToString());
            tempEnemies = Mathf.Clamp(tempEnemies, 0f, 1f);         

            if (tempEnemies == 1)
            {
                Time.timeScale += (1f / slowMotionGradientLength[1]) * Time.unscaledDeltaTime;
                Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
                //print(Time.timeScale.ToString());
                if (Time.timeScale >= 1)
                {
                    tempEnemies = -0.1f;
                }
            }
        }
    }
}
