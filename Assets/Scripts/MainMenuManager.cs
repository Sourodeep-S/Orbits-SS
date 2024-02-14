#if UNITY_EDITOR
using UnityEngine;
using TMPro;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI score;
  [SerializeField] private TextMeshProUGUI hiscoreText;
  [SerializeField] private TextMeshProUGUI hiscore;
  [SerializeField] private AudioClip clickSound;
  [SerializeField] private float animationTime;
  [SerializeField] AnimationCurve speedCurve;

  private void Awake()
  {
    if (GameManager.Instance.isInitialized)
    {
      StartCoroutine(ShowScore());
    }
    else
    {
      score.gameObject.SetActive(false);
      hiscoreText.gameObject.SetActive(true);
      hiscore.text = GameManager.Instance.highScore.ToString();
      hiscore.gameObject.SetActive(true);
    }
  }

  private IEnumerator ShowScore()
  {
    int tempScore = 0;
    score.text = tempScore.ToString();

    int currentScore = GameManager.Instance.score;
    int hiScore = GameManager.Instance.highScore;

    if (currentScore > hiScore)
    {
      GameManager.Instance.highScore = currentScore;
    }

    hiscore.text = GameManager.Instance.highScore.ToString();

    float speed = 1 / animationTime;
    float timeElapsed = 0f;

    while (timeElapsed < 1f)
    {
      timeElapsed += speed * Time.deltaTime;
      tempScore = (int)(speedCurve.Evaluate(timeElapsed) * currentScore);
      score.text = tempScore.ToString();
      yield return null;
    }

    tempScore = currentScore;
    score.text = tempScore.ToString();

  }

  public void ClickedPlay()
  {
    SoundManager.Instance.PlaySound(clickSound);
    GameManager.Instance.GoToGameplay();
  }
}
#endif