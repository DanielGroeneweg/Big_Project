using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioPlayer audioPrefab;

    public static SoundManager instance;
    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    public void PlaySound(AudioClip clip, float speed = 1)
    {
        Vector3 pos = PlayerController.instance.transform.position;
        PlaySound(clip, pos, false);
    }
    public void PlaySound(AudioClip clip, Vector3 position, bool playIn3D = true, float speed = 1)
    {
        AudioPlayer player = Instantiate(audioPrefab, position, Quaternion.identity);
        if (playIn3D) player.Set3D();
        if (speed != 1) player.SetSpeed(speed);
        player.Play(clip);
    }
}