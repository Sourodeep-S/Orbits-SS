#if UNITY_EDITOR
using UnityEngine;

public class Obstacles : MonoBehaviour
{
  [SerializeField] private float rotateSpeed;
  [SerializeField] private Transform rotateTransform;

  private void FixedUpdate()
  {
    rotateTransform.Rotate(0, 0, rotateSpeed * Time.fixedDeltaTime);
  }
}
#endif