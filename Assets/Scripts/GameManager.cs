using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // UI elements
    public Text artifactCountText;
    public Text bossCountText;
    
    // Wall that triggers win condition
    public GameObject victoryWall;
    
    // Scene name to load on victory
    public string winSceneName = "WinScene";
    
    // Tracking variables
    public int artifactsCollected = 0;
    public int bossesDefeated = 0;
    
    // Constants
    private const int REQUIRED_ARTIFACTS = 5;
    private const int REQUIRED_BOSSES = 5;

    private void Start()
    {
        UpdateUI();
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

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player touched the victory wall
        if (other.CompareTag("Player") && other.gameObject.transform.position.y > 0)
        {
            CheckVictoryCondition();
        }
    }

    private void CheckVictoryCondition()
    {
        if (artifactsCollected >= REQUIRED_ARTIFACTS && bossesDefeated >= REQUIRED_BOSSES)
        {
            // Load win scene
            SceneManager.LoadScene(winSceneName);
        }
    }

    // Optional: Method to get current progress (can be used by other scripts)
    public (int artifacts, int bosses) GetProgress()
    {
        return (artifactsCollected, bossesDefeated);
    }
}