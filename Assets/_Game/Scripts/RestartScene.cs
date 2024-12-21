using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace _Game.Scripts
{
    public class RestartScene : MonoBehaviour
    {
        public InputActionReference restartButton;

        void Update()
        {
            if (restartButton.action.WasPressedThisFrame())
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
