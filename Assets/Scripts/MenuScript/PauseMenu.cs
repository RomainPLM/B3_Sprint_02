using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Sacha
{
    public class PauseMenu : MonoBehaviour
    {
        public bool m_gameIsPaused = false;
        public EventSystem m_eventSystem;
        public GameObject m_pauseMenuUI;
        public GameObject m_firstButtonPause;
        public GameObject m_startButton;
        public GameObject m_godModeButton;
        public GameObject m_nextLevelButton;
        public GameObject m_quitButton;
        public bool m_godMode;
        public PlayerScript m_playerScript;
        private float m_timeScaleGo = 1;
        private float m_timeScaleStop = 0;

        public AudioClip[] audios;
        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Start"))
            {

                if (m_gameIsPaused == true)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {

                if (m_gameIsPaused == true)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }

        }

        public void Resume()
        {
        SfxManager._instance.PlayAudioClip(audios, transform, false, 1f);

            EventSystem.current.SetSelectedGameObject(null);
            Time.timeScale = m_timeScaleGo;
            m_pauseMenuUI.SetActive(false);
            m_gameIsPaused = false;
        }
        public void Quit()
        {
            SfxManager._instance.PlayAudioClip(audios, transform, false, 1f);

            Resume();
            EventSystem.current.SetSelectedGameObject(null);
            SceneManager.LoadScene("MainMenu");

        }
        public void NextLevel1()
        {
            SfxManager._instance.PlayAudioClip(audios, transform, false, 1f);

            Resume();
            EventSystem.current.SetSelectedGameObject(null);
            SceneManager.LoadScene("Level2");
        }
        public void NextLevel2()
        {
            SfxManager._instance.PlayAudioClip(audios, transform, false, 1f);

            Resume();
            EventSystem.current.SetSelectedGameObject(null);
            SceneManager.LoadScene("Level");
        }
        public void GodMode()
        {
            if (m_godMode == true)
            {
                m_godMode = false;
            }
            else
            {
                m_godMode = true;
            }
        }
        void Pause()
        {
            SfxManager._instance.PlayAudioClip(audios, transform, false, 1f);

            m_pauseMenuUI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(m_firstButtonPause);
            Time.timeScale = m_timeScaleStop;
            m_gameIsPaused = true;
        }
    }
}
