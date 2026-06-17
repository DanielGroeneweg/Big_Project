using System.Collections;
using UnityEngine;
public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    public void SetSpeed(float speed)
    {
        audioSource.pitch = speed;
    }
    public void Set3D()
    {
        audioSource.spatialBlend = 1;
    }
    public void Play(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(gameObject, clip.length);
    }
}