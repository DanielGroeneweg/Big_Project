using UnityEngine;

public class SoundPresenter : Presenter
{
    [SerializeField] AudioClip[] sounds = new AudioClip[0];
    public override void Present(float min, float max, float current)
    {
        SoundManager.instance.PlaySound(sounds[Random.Range(0, sounds.Length)], transform.position);
    }
}