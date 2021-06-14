using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource gunshot;
    public AudioSource playerHit;
    public AudioSource beepSound;
    public AudioSource lastBeepSound;
    public AudioSource enemyBurstSound;
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }
    public void PlayGunShot()
    {
        gunshot.Play();
    }
    public void PlayHit()
    {
        playerHit.Play();
    }
    public void PlayBeep()
    {
        beepSound.Play();
    }
    public void PlayLastBeep()
    {
        lastBeepSound.Play();
    }

    public void StopOneBeep()
    {
        beepSound.Stop();
    }
    public void PlayVirusBurst()
    {
        enemyBurstSound.Play();
    }
}
