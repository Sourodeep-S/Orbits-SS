#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] private AudioClip moveSound, loseSound, scoreSound;
  [SerializeField] private GameplayManager gm;
  [SerializeField] private GameObject explosionPrefab, scorePrefab;
  [SerializeField] private float rotateSpeed;
  [SerializeField] private Transform rotateTransform;
  [SerializeField] private float startRadius;
  [SerializeField] private float moveTime;
  [SerializeField] private List<float> rotateRadius;

  private int level;
  private float currentRadius;

  private bool canClick;

  private void Start()
  {
    canClick = true;
    level = 0;
    currentRadius = startRadius;
  }

  private void Update()
  {
    if (canClick && Input.GetMouseButtonDown(0))
    {
      SoundManager.Instance.PlaySound(moveSound);
      StartCoroutine(ChangeRadius());
    }
  }

  private IEnumerator ChangeRadius()
  {
    canClick = false;

    float moveStartRadius = rotateRadius[level];
    float moveEndRadius = rotateRadius[(level + 1) % rotateRadius.Count];
    float moveOffset = moveEndRadius - moveStartRadius;
    float speed = 1 / moveTime;
    float elapsed = 0f;

    while (elapsed < 1f)
    {
      elapsed += speed * Time.fixedDeltaTime;
      currentRadius = moveStartRadius + moveOffset * elapsed;
      yield return new WaitForFixedUpdate();
    }

    canClick = true;
    level = (level + 1) % rotateRadius.Count;
    currentRadius = rotateRadius[level];
  }

  private void FixedUpdate()
  {
    transform.localPosition = Vector3.up * currentRadius;
    float rotateValue = rotateSpeed * Time.fixedDeltaTime;
    rotateValue *= startRadius / currentRadius;
    rotateTransform.Rotate(0, 0, rotateValue);
  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Obstacle"))
    {
      SoundManager.Instance.PlaySound(loseSound);
      Instantiate(explosionPrefab, transform.position, Quaternion.identity);
      Destroy(gameObject);
      gm.GameEnded();
      return;
    }

    else if (other.CompareTag("Score"))
    {
      SoundManager.Instance.PlaySound(scoreSound);
      Destroy(Instantiate(scorePrefab, transform.position, Quaternion.identity), 2f);
      gm.UpdateScore();
      other.gameObject.GetComponent<Score>().ScoreAdded();
      return;
    }
  }
}
#endif