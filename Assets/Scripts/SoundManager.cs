#if UNITY_EDITOR
using UnityEngine;

public class SoundManager : MonoBehaviour
{
  public static SoundManager Instance;

  [SerializeField]
  private AudioSource src;

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
      return;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  public void PlaySound(AudioClip clip)
  {
    src.PlayOneShot(clip);
  }
}
#endif