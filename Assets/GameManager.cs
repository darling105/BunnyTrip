using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI carrotText;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private int[] levelSceneIndexes = { 1, 2, 3, 4, 5 };
    private int carrotPicked;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Lấy số cà rốt đã lưu
        carrotPicked = PlayerPrefs.GetInt("CarrotCount", 0);
        UpdateCarrotUI();
    }

    public void AddCarrot()
    {
        carrotPicked++;
        PlayerPrefs.SetInt("CarrotCount", carrotPicked); // Lưu lại
        PlayerPrefs.Save(); // Đảm bảo lưu ngay
        UpdateCarrotUI();
    }

    private void UpdateCarrotUI()
    {
        if (carrotText != null)
            carrotText.text = "" + carrotPicked;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void RestartGame()
    {
        DOTween.KillAll();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnHomeGame()
    {
        DOTween.KillAll();
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        DOTween.KillAll();
        Time.timeScale = 1f;

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Lấy danh sách các level đã chơi
        string playedLevelsStr = PlayerPrefs.GetString("LevelsPlayed", "");
        HashSet<int> playedLevels = new HashSet<int>();

        if (!string.IsNullOrEmpty(playedLevelsStr))
        {
            foreach (string s in playedLevelsStr.Split(','))
            {
                if (int.TryParse(s, out int val))
                    playedLevels.Add(val);
            }
        }

        // Thêm màn hiện tại vào danh sách đã chơi nếu chưa có
        if (!playedLevels.Contains(currentSceneIndex) && System.Array.Exists(levelSceneIndexes, index => index == currentSceneIndex))
        {
            playedLevels.Add(currentSceneIndex);
        }

        // Lưu lại danh sách
        PlayerPrefs.SetString("LevelsPlayed", string.Join(",", playedLevels));
        PlayerPrefs.Save();

        // Nếu chưa chơi đủ tất cả level, chọn level tiếp theo chưa chơi
        if (playedLevels.Count < levelSceneIndexes.Length)
        {
            foreach (int levelIndex in levelSceneIndexes)
            {
                if (!playedLevels.Contains(levelIndex))
                {
                    SceneManager.LoadScene(levelIndex);
                    return;
                }
            }
        }
        else
        {
            // Đã chơi hết => random các màn (loại trừ màn hiện tại)
            List<int> possibleScenes = new List<int>(levelSceneIndexes);
            possibleScenes.Remove(currentSceneIndex);

            if (possibleScenes.Count == 0)
            {
                Debug.LogWarning("Không còn scene nào khác để random!");
                return;
            }

            int randomIndex = possibleScenes[Random.Range(0, possibleScenes.Count)];
            SceneManager.LoadScene(randomIndex);
        }
    }

    // Nếu bạn muốn reset về 0 ở menu hoặc nút riêng:
    public void ResetCarrots()
    {
        PlayerPrefs.DeleteKey("CarrotCount");
        carrotPicked = 0;
        UpdateCarrotUI();
    }
}
