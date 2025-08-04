using UnityEngine;

public class FinishlineTrigger : MonoBehaviour
{
    public GameObject victoryMenu;
    public GameObject confettiPrefab;
    public Transform confettiSpawnPoint;

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Player")) return;
        triggered = true;
        SoundManager.Instance.PlayWinning();
        // 🎉 Hiệu ứng confetti
        if (confettiPrefab != null && confettiSpawnPoint != null)
        {
            Instantiate(confettiPrefab, confettiSpawnPoint.position, Quaternion.identity);
        }

        // 🏆 Hiện menu chiến thắng
        if (victoryMenu != null)
        {
            victoryMenu.SetActive(true);
        }
    }
}
