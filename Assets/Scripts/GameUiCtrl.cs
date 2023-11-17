using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUiCtrl : MonoBehaviour
{
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private Slider oxygenBar;
    [SerializeField]
    private float oxygenFillOrDepleteSpeed = 1f;


    private float _oxygenReplenishSpeed = 10f;

    private bool isHealthDecreasing = false;

    private float maxSliderValue = 100f;
    void Start()
    {
        healthBar.maxValue = maxSliderValue;
        oxygenBar.maxValue = maxSliderValue;
        ResetValues();
    }

    // Update is called once per frame
    void Update()
    {
        GameManager gameManager = GameManager.instance;
        if (gameManager != null  &&!gameManager.IsPlayerDead)
        {
            if(gameManager.PlayerCtrl != null)
            {
                oxygenFillOrDepleteSpeed = (gameManager.PlayerCtrl.DistanceOfPlayerFromSurface / 25) + 1f;
                if (gameManager.isPlayerDiving)
                {
                    ControlOxygenBarWithHealthDependent(-1 *oxygenFillOrDepleteSpeed * Time.deltaTime);
                }
                else if(oxygenBar.value < maxSliderValue)
                {
                    ControlOxygenBarWithHealthDependent(_oxygenReplenishSpeed * Time.deltaTime);
                }
            }
            else if(oxygenBar.value < maxSliderValue)
            {
                ControlOxygenBarWithHealthDependent(_oxygenReplenishSpeed * Time.deltaTime);
            }
        }
        if (gameManager.IsPlayerDead)
        {
            StopAllCoroutines();
            SceneManager.LoadSceneAsync("02GameOver");
        }
    }

    private void ControlOxygenBarWithHealthDependent(float value)
    {
        if(oxygenBar != null)
        {
            oxygenBar.value += value;
        }
        if(oxygenBar.value <= 0 && !isHealthDecreasing)
        {
            isHealthDecreasing=true;
            StartCoroutine("StartReducingHealth");
        }
        else if(oxygenBar.value > 0 && isHealthDecreasing)
        {
            StopCoroutine("StartReducingHealth");
            isHealthDecreasing = false;
        }
    }

    private IEnumerator StartReducingHealth()
    {
        while (isHealthDecreasing)
        {
            yield return new WaitForSeconds(1f);
            healthBar.value -= 10f;
            if(healthBar.value <= 0)
            {
                GameManager.instance.IsPlayerDead = true;
                ResetValues();
            }
        }
    }

    private void ResetValues()
    {
        isHealthDecreasing = false;
        healthBar.value = maxSliderValue;
        oxygenBar.value = maxSliderValue;
    }

    public void OnClickHomeButton()
    {
        StopAllCoroutines();
        SceneManager.LoadSceneAsync("00Start");
    }
}
