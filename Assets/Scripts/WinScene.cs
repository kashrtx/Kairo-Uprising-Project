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

            // Change the scene
            SceneManager.LoadScene(win);
        }
    }
}
