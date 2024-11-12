using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScene : MonoBehaviour
{
    public string win = "You Win";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player touched the revealed object! Changing scene...");
            Cursor.visible = true; // make mouse visible again and remove lock
            Cursor.lockState = CursorLockMode.None;
            // Change the scene
            SceneManager.LoadScene(win);
        }
    }
}
