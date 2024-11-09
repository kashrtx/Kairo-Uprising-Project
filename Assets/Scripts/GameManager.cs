using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // UI elements
    public TMP_Text artifactCountText;
    public TMP_Text bossCountText;
    public TMP_Text objectiveText;
    
    // Wall that triggers win condition
    public GameObject victoryWall;
    
    // Scene name to load on victory
    [SerializeField] private string winSceneName = "You Win";
    
    // Tracking variables
    public int artifactsCollected = 0;
    public int bossesDefeated = 0;
    
    // Constants
    private const int REQUIRED_ARTIFACTS = 5;
    private const int REQUIRED_BOSSES = 5;

    private void Start()
    {
        UpdateUI();
        SetObjectiveText();
    }

    // Call this when player collects an artifact
    public void CollectArtifact()
    {
        artifactsCollected++;
        UpdateUI();
    }

    // Call this when a boss is defeated
    public void DefeatBoss()
    {
        bossesDefeated++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (artifactCountText != null)
        {
            artifactCountText.text = $"Artifacts: {artifactsCollected}/{REQUIRED_ARTIFACTS}";
        }
        
        if (bossCountText != null)
        {
            bossCountText.text = $"Bosses Defeated: {bossesDefeated}/{REQUIRED_BOSSES}";
        }
    }

    private void SetObjectiveText()
    {
        if (objectiveText != null)
        {
            objectiveText.text = "Objective: Kill 5 Kairo Bosses. Go to the gate at the end of the village to escape!";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
         Debug.LogWarning("touching block!");
        // Check if the player touched the victory wall
        if (other.CompareTag("Player"))
        {
             Debug.LogWarning("player touching block!");
            CheckVictoryCondition();
        }
    }

    private void CheckVictoryCondition()
    {
        if (artifactsCollected >= REQUIRED_ARTIFACTS && bossesDefeated >= REQUIRED_BOSSES)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            // Load win scene
            if (!string.IsNullOrEmpty(winSceneName))
            {
                SceneManager.LoadScene(winSceneName);
            }
            else
            {
                Debug.LogWarning("Win scene name is not set.");
            }
        }
    }

    // Optional: Method to get current progress (can be used by other scripts)
    public (int artifacts, int bosses) GetProgress()
    {
        return (artifactsCollected, bossesDefeated);
    }
}
