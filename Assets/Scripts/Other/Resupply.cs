using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resupply : MonoBehaviour
{
    public WaterGun scriptWaterGun;
    
    private float r_realValue;
    private float r_maxValue;

    private Image r_waterBar;
    private Text r_waterCount;


    private void Start()
    {
        scriptWaterGun = GameObject.Find("WaterGun").GetComponent<WaterGun>();

        /*if (scriptWaterGun == null)
        {
            scriptWaterGun = GameObject.Find("WaterGun").GetComponent<WaterGun>();
            print("StartscriptWaterGun Nulo");
        }*/
    }

    private void Update()
    {
        if (scriptWaterGun == null)
        {
            scriptWaterGun = GameObject.Find("WaterGun").GetComponent<WaterGun>();
            //print("scriptWaterGun Nulo");
        }
        r_realValue = scriptWaterGun.realValue;
        r_maxValue = scriptWaterGun.maxValue;
        r_waterBar = scriptWaterGun.waterBar;
        r_waterCount = scriptWaterGun.waterCount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (r_realValue != r_maxValue) {
            if (collision.gameObject.tag == "Player") {

                this.gameObject.SetActive(false);
                r_waterBar.fillAmount = 1f;
                r_realValue = 100f;
                scriptWaterGun.realValue = r_realValue;
                string temp = r_realValue.ToString();
                r_waterCount.text = temp;
                AudioManager.instance.bucketFXpick();

            }
        }
    }
}
