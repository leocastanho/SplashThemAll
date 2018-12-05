using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    public int pause = -1;

    //musicBG
    public AudioClip[] clipBG;
    public AudioSource musicBG;

    //waterFX
    public AudioClip clipWaterFX;
    public AudioSource waterFX;

    //bucketFx
    public AudioClip clipBucketFX;
    public AudioSource bucketFX;

    //JumpFX
    public AudioClip clipJumpFX;
    public AudioSource jumpFX;

    //GameOverFX
    public AudioClip clipGameOverFX;
    public AudioSource gameOverFX;

    //HurtFX
    public AudioClip clipHurtFX;
    public AudioSource hurtFX;

    //HitFX
    public AudioClip clipHitFX;
    public AudioSource hitFX;

    //EnemyDieFX
    public AudioClip clipEnemyDiedFX;
    public AudioSource enemyDiedFX;

    //BossDieFx
    public AudioClip clipBossDiedFX;
    public AudioSource bossDiedFX;

    public bool bossAlive = true;

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

        musicBG.clip = MusicMainMenu();
    }

	void Update () {

        if (SceneController.instance.scene == 6 && bossAlive == true)
        {
            musicBG.clip = MusicBoss();
        }

        if (SceneController.instance.scene != 6)
        {
            musicBG.clip = MusicMainMenu();
        }

        if (!musicBG.isPlaying)
        {
            musicBG.Play();
        }
	}

    AudioClip MusicMainMenu ()
    {
        return clipBG[0];
    }

    AudioClip MusicBoss()
    {
        return clipBG[1];
    }

    AudioClip MusicNone()
    {
        return clipBG[2];
    }


    public static void AudioPlayer (AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        if (!source.isPlaying)
        {
            source.Play();
        }
    }

    public void waterFXshoot ()
    {
        AudioPlayer(waterFX, clipWaterFX);
    }
    public void bucketFXpick()
    {
        AudioPlayer(bucketFX, clipBucketFX);
    }
    public void jumpingFX()
    {
        AudioPlayer(jumpFX, clipJumpFX);
    }
    public void losingFX()
    {
        AudioPlayer(gameOverFX, clipGameOverFX);
    }
    public void gettingHurtFX()
    {
        AudioPlayer(hurtFX, clipHurtFX);
    }
    public void hittingFX()
    {
        AudioPlayer(hitFX, clipHitFX);
    }
    public void enemyDieFX()
    {
        AudioPlayer(enemyDiedFX, clipEnemyDiedFX);
    }
    public void bossDieFX()
    {
        AudioPlayer(bossDiedFX, clipBossDiedFX);
    }

    public IEnumerator bossDeathMusicDelay()
    {
        musicBG.clip = MusicNone();
        musicBG.volume = 0.01f;

        yield return new WaitForSeconds(1.5f);
        musicBG.clip = MusicMainMenu();

        yield return new WaitForSeconds(0.1f);
        musicBG.volume = 0.015f;

        yield return new WaitForSeconds(0.1f);
        musicBG.volume = 0.02f;

        yield return new WaitForSeconds(0.1f);
        musicBG.volume = 0.025f;

        yield return new WaitForSeconds(0.1f);
        musicBG.volume = 0.03f;

        yield return new WaitForSeconds(0.1f);
        musicBG.volume = 0.035f;

        yield return new WaitForSeconds(0.1f);
        musicBG.volume = 0.04f;

        yield return new WaitForSeconds(0.1f);
        musicBG.volume = 0.045f;

        yield return new WaitForSeconds(0.1f);
        musicBG.volume = 0.05f;

        yield return new WaitForSeconds(0.1f);
        musicBG.volume = 0.055f;

        yield return new WaitForSeconds(0.1f);
        musicBG.volume = 0.06f;

        yield return new WaitForSeconds(0.1f);
        musicBG.volume = 0.065f;

        yield return new WaitForSeconds(0.1f);
        musicBG.volume = 0.07f;

        yield return new WaitForSeconds(0.1f);
        musicBG.volume = 0.075f;

        yield return new WaitForSeconds(0.1f);
        musicBG.volume = 0.08f;

        yield return new WaitForSeconds(0.1f);
        musicBG.volume = 0.085f;

        yield return new WaitForSeconds(0.1f);
        musicBG.volume = 0.09f;

        yield return new WaitForSeconds(0.1f);
        musicBG.volume = 0.095f;

        yield return new WaitForSeconds(0.1f);
        musicBG.volume = 0.1f;
    }

}
