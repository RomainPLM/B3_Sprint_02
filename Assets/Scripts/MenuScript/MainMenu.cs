using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


public class MainMenu : MonoBehaviour
{
    public GameObject m_firstButtonStartMenu;
    public GameObject m_firstButtonOptions;
    public GameObject m_firstButtonCredits;
    public GameObject m_startButton;
    public GameObject m_optionButton;
    public GameObject m_creditsButton;
    public GameObject m_quitButton;
    public GameObject m_startMenuBox;
    public GameObject m_creditsCanvas;
    public GameObject m_optionsCanvas;

    public AudioClip[] audios;
    // Update is called once per frame
    void Update()
    {



    }
    public void Start()
    {
        EventSystem.current.SetSelectedGameObject(m_firstButtonStartMenu);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level");
    }
    public void Menu()
    {
        SfxManager._instance.PlayAudioClip(audios, transform, false, 1f);

        m_startMenuBox.SetActive(true);
        EventSystem.current.SetSelectedGameObject(m_firstButtonStartMenu);
        m_optionsCanvas.SetActive(false);
        m_creditsCanvas.SetActive(false);
    }
    public void Credits()
    {
        SfxManager._instance.PlayAudioClip(audios, transform, false, 1f);

        m_startMenuBox.SetActive(false);
        m_optionsCanvas.SetActive(false);
        m_creditsCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(m_firstButtonCredits);
    }
    public void Options()
    {
        SfxManager._instance.PlayAudioClip(audios, transform, false, 1f);

        m_startMenuBox.SetActive(false);
        m_optionsCanvas.SetActive(true);
        EventSystem.current.SetSelectedGameObject(m_firstButtonOptions);
        m_creditsCanvas.SetActive(false);
    }
    public void Quit()
    {
        SfxManager._instance.PlayAudioClip(audios, transform, false, 1f);

        Application.Quit();

    }
}

