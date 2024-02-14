#if UNITY_EDITOR
using UnityEngine;
using TMPro;
using System.Collections;

public class GameplayManager : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI scoreText;
  [SerializeField] private GameObject scorePrefab;

  private int score;

  private void Awake()
  {
    GameManager.Instance.isInitialized = true;

    score = 0;
    scoreText.text = score.ToString();
    SpawnScore();
  }

  private void SpawnScore()
  {
    Instantiate(scorePrefab);
  }

  public void UpdateScore()
  {
    score++;
    scoreText.text = score.ToString();
    SpawnScore();
  }

  public void GameEnded()
  {
    GameManager.Instance.score = score;
    StartCoroutine(GameOver());
  }

  private IEnumerator GameOver()
  {
    yield return new WaitForSeconds(2f);
    GameManager.Instance.GoToMainMenu();
  }
}
#endif
