using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    [SerializeField] private AudioClipMenuRefSO audioClipMenuRefSO;
    [SerializeField] private AudioSource audioSource;
    private float volume = .5f;
    private float congratulationsScreenSoundsMultiplier = 0.1f;

    private void Awake()
    {
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 0.5f);
    }

    public void PlaySoundBoink()
    {
        audioSource.clip = audioClipMenuRefSO.clickBoink;
        audioSource.volume = volume;
        audioSource.Play();
    }

    public void PlaySoundMenuRef()
    {
        audioSource.clip = audioClipMenuRefSO.soundEffectMenuReference;
        audioSource.volume = volume;
        audioSource.Play();
    }

    public void ChangeVolume()
    {
        volume += .1f;

        if (volume > 1f)
        {
            volume = 0f;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float ReturnGetVolume()
    {
        return volume;
    }

    public void PlayCongratulationsSound()
    {
        audioSource.clip = audioClipMenuRefSO.congratulationsSound;
        audioSource.volume = volume;
        audioSource.Play();
    }

    public void PlayFootstepSound()
    {
        AudioSource.PlayClipAtPoint(audioClipMenuRefSO.footstepSound[Random.Range(0, audioClipMenuRefSO.footstepSound.Length)], Camera.main.transform.position, volume * congratulationsScreenSoundsMultiplier);
    }
    public void PlayCreakyPointerSound()
    {
        AudioSource.PlayClipAtPoint(audioClipMenuRefSO.creakyPointerSound, Camera.main.transform.position, volume * congratulationsScreenSoundsMultiplier);
    }
    public void PlayWoodenSound(int x)
    {
        AudioSource.PlayClipAtPoint(audioClipMenuRefSO.woodenSound[x], Camera.main.transform.position, volume * congratulationsScreenSoundsMultiplier);
    }



}
