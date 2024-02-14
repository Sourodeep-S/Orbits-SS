#if UNITY_EDITOR
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
  public bool isInitialized { get; set; }
  public int score { get; set; }

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
      Init();
      return;
    }
    else
      Destroy(gameObject);
  }

  public int highScore
  {
    get
    {
      return PlayerPrefs.GetInt("HighScore", 0);
    }

    set
    {
      PlayerPrefs.SetInt("HighScore", value);
    }
  }

  private void Init()
  {
    isInitialized = false;
    score = 0;
  }

  private const string mainMenu = "Main Menu";
  private const string gameplay = "Gameplay";

  public void GoToMainMenu()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenu);
  }

  public void GoToGameplay()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene(gameplay);
  }
}
#endif