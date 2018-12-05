using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoDelay : MonoBehaviour {


    private void Update()
    {
        if (SceneController.instance.scene == 0)
        {
            StartCoroutine(logoDelay());

            if (Input.GetButtonDown("Fire1") || Input.GetKey(KeyCode.KeypadEnter))
            {
                StopCoroutine(logoDelay());
                SceneManager.LoadScene(1);
            }

        }
    }

    IEnumerator logoDelay()
    {
        Animator animLogo = GetComponent<Animator>();
        yield return new WaitForSecondsRealtime(3f);
        animLogo.SetBool("FadeOff", true);

        yield return new WaitForSecondsRealtime(1.7f);
        SceneManager.LoadScene(1);

    }
}
