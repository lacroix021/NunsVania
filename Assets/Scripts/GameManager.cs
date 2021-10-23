using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum ControllerType
{
    PC,
    MOBILE
}

public class GameManager : MonoBehaviour
{
    public ControllerType controller;

    public GameObject pausePanel;
    public bool gameIsPause = false;

    public GameObject equipPanel;

    public PlayerController player;

    public GameObject weaponPanel;


    public int numBlood;
    public Image[] playerLife;

    // Start is called before the first frame update
    void Start()
    {
        controller = ControllerType.PC;
    }

    // Update is called once per frame
    void Update()
    {
        player.controller = controller;

        for (int i = 0; i < playerLife.Length; i++)
        {
            if (i < player.currentHealth)
            {
                playerLife[i].enabled = true;
            }
            else
            {
                playerLife[i].enabled = false;
            }
        }
    }



    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        gameIsPause = false;
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        gameIsPause = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(""); // poner nombre o numero de la escena a cargar
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void EquipMenu()
    {
        equipPanel.SetActive(true);
    }

    public void BackButton()
    {
        equipPanel.SetActive(false);

        if (weaponPanel.activeSelf == true)
        {
            weaponPanel.SetActive(false);
        }
    }

    public void WeaponButton()
    {
        if (weaponPanel.activeSelf == false)
        {
            weaponPanel.SetActive(true);
        }
        else if (weaponPanel.activeSelf == true)
        {
            weaponPanel.SetActive(false);
        }

    }

    public void leatherButton()
    {
        player.weaponState = 0;
        weaponPanel.SetActive(false);
        equipPanel.SetActive(false);
    }

    public void ChainButton()
    {
        player.weaponState = 1;
        weaponPanel.SetActive(false);
        equipPanel.SetActive(false);
    }

    public void FireButton()
    {
        player.weaponState = 2;
        weaponPanel.SetActive(false);
        equipPanel.SetActive(false);
    }
}
