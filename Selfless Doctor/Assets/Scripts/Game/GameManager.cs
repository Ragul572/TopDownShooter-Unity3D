using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Volume postProcessVolume;
    private DepthOfField depthOfField;
    public GameObject gameOverPanel;

    public Slider healthBarVisualSlider;
    public int playerHealth;
    public float enemySpawnTime;
    public Slider lungVisualSlider;
    private float oxygenReduceRate = 8f;
    private float oxygenAmount = 100f;
    private float beepVolume = 0.07f;
    public int virusDamageAmount = 10;           //the damage which virus do to player
    public int playerDamageAmount = 50;          //the damage which player do to virus 


    public static GameManager instance { get; private set; }
    private void Awake()
    {
        postProcessVolume.profile.TryGet<DepthOfField>(out depthOfField);
        instance = this;
    }
    public void Update()
    {
       
        UpdatePlayerHealth();
    }

    public void UpdateLungVisual()
    {
        oxygenAmount -= oxygenReduceRate;         //updates the lung visual by the changing the fill amount of the image
        lungVisualSlider.value = oxygenAmount;
    }
    private void CheckForGameOver()
    {
        if(oxygenAmount < 35)
        {
            // gameOver
            EnableGameOverPanel();
            //Time.timeScale = 0;
        }
        else if(playerHealth < 50)
        {
            EnableGameOverPanel();
            //Time.timeScale = 0;
        }
    }

    public void UpdateBeepVolume()
    {
        beepVolume += 0.08f;  //updates the volume of the beep
        AudioManager.instance.beepSound.volume = beepVolume;
        AudioManager.instance.PlayBeep();
    }

    public void DamagePlayer()
    {
       playerHealth -= virusDamageAmount;
    }
    private void UpdatePlayerHealth()
    {
        CheckForGameOver();
        healthBarVisualSlider.value = playerHealth;
    }

    private void EnableGameOverPanel()
    {
        depthOfField.focalLength.value += Time.time;       // Set the Full Depth of field
        gameOverPanel.SetActive(true);
    }

   

}
