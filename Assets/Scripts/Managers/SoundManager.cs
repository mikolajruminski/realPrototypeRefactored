using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipGameSO audioClipGameSO;
    [SerializeField] private AudioSource mainCamAudioSource;

    private float volume;

    private void Awake()
    {
        Instance = this;
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME);
    }

    private void Start()
    {
        KeyBehaviourScript.OnAnyKeyPicked += KeyBehaviourScript_OnAnyKeyPicked;
        Player.Instance.OnPlayerJumped += Player_Instance_OnPlayerJumped;
        Player.Instance.OnPlayerLanded += Player_Instance_OnPlayerLanded;
        Player.Instance.OnPlayerDashed += Player_Instance_OnPlayerDashed;
        Player.Instance.OnPlayerGripped += Player_Instance_OnPlayerGripped;
        Player.Instance.OnDirectionChanged += Player_Instance_OnDirectionChanged;
        Enemy.OnAnyEnemyGetDamaged += Enemy_OnAnyEnemyGetDamaged;
        PlayerDamage.Instance.OnGetDamaged += PlayerDamage_Instance_OnGetDamaged;
        PlayerDamage.Instance.OnPlayerDeath += PlayerDamage_Instance_OnPlayerDeath;
        HeadStomperAttack.Instance.OnHeadStomperAttack += HeadStomperAttack_Instance_OnHeadStomperAttack;
        HeartObjectScript.OnHeartPowerUpPickedUp += HeartObjectScript_OnHeartPowerUpPicked;
        JumpPadScript.OnAnyLaunchPerformed += JumpPadScript_OnAnyLaunchPerformed;
        ExitDoorScript.Instance.DoorOpening += ExitDoorScript_Instance_DoorOpening;
        GameManager.Instance.OnGamePaused += GameManager_Instance_OnGamePaused;
        GameManager.Instance.OnGameUnPaused += GameManager_Instance_OnGamePaused;
    }

    private void KeyBehaviourScript_OnAnyKeyPicked(object sender, System.EventArgs e)
    {

        PlaySound(audioClipGameSO.objectPickUp, Player.Instance.transform.position, 1f);
    }

    private void Player_Instance_OnDirectionChanged(object sender, System.EventArgs e)
    {
        PlaySound(audioClipGameSO.changingDirection, Player.Instance.transform.position, 1f);
    }

    private void GameManager_Instance_OnGamePaused(object sender, System.EventArgs e)
    {
        PlayMenuOptionsSound();
    }

    private void ExitDoorScript_Instance_DoorOpening(object sender, System.EventArgs e)
    {
        PlaySound(audioClipGameSO.exitDoorSounds, ExitDoorScript.Instance.transform.position, 1f);
    }

    private void JumpPadScript_OnAnyLaunchPerformed(object sender, System.EventArgs e)
    {
        JumpPadScript jumpPadScript = sender as JumpPadScript;
        PlaySound(audioClipGameSO.jumpPadSound, jumpPadScript.transform.position, 0.6f);
    }

    private void HeartObjectScript_OnHeartPowerUpPicked(object sender, System.EventArgs e)
    {
        HeartObjectScript heartObjectScript = sender as HeartObjectScript;
        PlaySound(audioClipGameSO.objectPickUp, heartObjectScript.transform.position, 1f);
    }

    private void PlayerDamage_Instance_OnPlayerDeath(object sender, System.EventArgs e)
    {
        PlaySound(audioClipGameSO.playerDeathSounds, Player.Instance.transform.position, 1f);
    }

    private void Enemy_OnAnyEnemyGetDamaged(object sender, System.EventArgs e)
    {
        Enemy enemy = sender as Enemy;
        PlaySound(audioClipGameSO.enemyHitSounds, enemy.transform.position, 1f);
    }

    private void PlayerDamage_Instance_OnGetDamaged(object sender, System.EventArgs e)
    {
        PlaySound(audioClipGameSO.hitSounds, Player.Instance.transform.position, 1f);
    }

    private void Player_Instance_OnPlayerGripped(object sender, System.EventArgs e)
    {
        PlaySound(audioClipGameSO.gripSounds, Player.Instance.transform.position, 1f);
    }

    private void Player_Instance_OnPlayerDashed(object sender, System.EventArgs e)
    {
        PlaySound(audioClipGameSO.dashSounds, Player.Instance.transform.position, 1f);
    }

    private void Player_Instance_OnPlayerLanded(object sender, System.EventArgs e)
    {
        PlaySound(audioClipGameSO.landSounds, Player.Instance.transform.position, 1f);
    }

    private void HeadStomperAttack_Instance_OnHeadStomperAttack(object sender, System.EventArgs e)
    {
        PlaySound(audioClipGameSO.headStomperSound, HeadStomperAttack.Instance.transform.position, 1f);
    }

    private void Player_Instance_OnPlayerJumped(object sender, System.EventArgs e)
    {
        PlaySound(audioClipGameSO.jumpSounds, Player.Instance.transform.position, 1f);
    }

    private void PlaySound(AudioClip[] audioClipsArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipsArray[Random.Range(0, audioClipsArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    public void PlayFootstepSound(Vector3 position, float volumeMultiplier)
    {
        PlaySound(audioClipGameSO.runSounds, position, volumeMultiplier * volume);
    }

    public void PlayMenuOptionsSound()
    {
        mainCamAudioSource.clip = audioClipGameSO.pauseMenuOpenSound;
        mainCamAudioSource.volume = volume;
        mainCamAudioSource.Play();
    }

    public void PlayMenuOptionsRefSound()
    {
        mainCamAudioSource.clip = audioClipGameSO.pauseMenuRefSound;
        mainCamAudioSource.volume = volume;
        mainCamAudioSource.Play();
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

    public AudioClipGameSO ReturnAudioClipGameSO()
    {
        return audioClipGameSO;
    }

}
