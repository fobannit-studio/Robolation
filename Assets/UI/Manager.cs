using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Simulation.UI
{
    public class Manager : MonoBehaviour
    {
        public MaterialsEditor materialsEditor;
        public Canvas MainMenu;

        private void Start()
        {
            
        }
        public void StartSimulation()
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);

        }

        public void ShowMainMenu()
        {
            MainMenu.enabled = true;
            materialsEditor.gameObject.SetActive(false);
        }
        public void ShowMaterialsMenu()
        {
            MainMenu.enabled = false;
            materialsEditor.OpenEditor();
        }

        public void Quit()
        {
#if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
#else
                 Application.Quit();
#endif
        }
    }
}