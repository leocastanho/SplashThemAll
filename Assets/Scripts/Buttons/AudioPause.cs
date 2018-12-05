using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPause : MonoBehaviour {

    public Image btn;

	void Update () {
		
        if (AudioManager.instance.pause == 1)
        {
            btn.color = new Color(0.2f, 0.2f, 0.2f, 0.5f);
        }
        else
        {
            btn.color = new Color(1, 1, 1, 1);
        }
	}
}
