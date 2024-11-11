using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    public GameObject gate;           
    public GameObject cube;           
    public GameObject gun;            
    public Text dialogueText;         
    private bool playerInRange = false;
    private bool interactionComplete = false;

    void Start()
    {
        // Disable the gun at the start of the game
        if (gun != null)
        {
            gun.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !interactionComplete)
        {
            StartInteraction();
        }
    }

    void StartInteraction()
    {
        Debug.Log("Player is interacting with the NPC!");
        ShowDialogue();
        CompleteInteraction();
    }

    void ShowDialogue()
    {
        if (dialogueText != null)
        {
            dialogueText.text = "Hey YOU! I need your help! Here, QUICK! Take this gun! Defeat them please! I HAVE A FAMILY!";
            dialogueText.gameObject.SetActive(true);

            // Hide the dialogue text after a delay
            Invoke("HideDialogue", 5f);  // Hides text after 5 seconds
        }
    }

    void HideDialogue()
    {
        if (dialogueText != null)
        {
            dialogueText.gameObject.SetActive(false);
        }
    }

    void CompleteInteraction()
    {
        interactionComplete = true;  // Marks interaction as complete

        // Deactivate the gate
        if (gate != null)
        {
            gate.SetActive(false);
            Debug.Log("The gate has disappeared!");
        }

        // Deactivate the cube
        if (cube != null)
        {
            cube.SetActive(false);
            Debug.Log("The cube has disappeared!");
        }

        // Activate the gun
        if (gun != null)
        {
            gun.SetActive(true);  // Makes the gun visible after interaction
            Debug.Log("The player has received the gun!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
