#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
  [SerializeField] private Transform centre;
  [SerializeField] private List<float> spawnPosX;

  private void Awake()
  {
    transform.localPosition = Vector3.right * spawnPosX[Random.Range(0, spawnPosX.Count)];
    centre.rotation = Quaternion.Euler(0f, 0f, Random.Range(0, 36) * 10f);
  }

  public void ScoreAdded()
  {
    Destroy(centre.gameObject);
  }

}
#endif