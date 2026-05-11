using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip buttonSound;

    public void PlaySound()
    {
        audioSource.PlayOneShot(buttonSound);
    }
}
