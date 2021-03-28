using UnityEngine;
using ScriptableObjectArchitecture;

public class AudioController : MonoBehaviour
{
    #region Audio Variables
    [SerializeField] private FloatReference bgmVolume = default(FloatReference);
    [SerializeField] private FloatReference sfxVolume = default(FloatReference);
    [SerializeField] private AudioSource bgmAudioSource = default(AudioSource);
    [SerializeField] private AudioSource sfxAudioSource = default(AudioSource);
    #endregion

    private void Start()
    {
        bgmAudioSource.volume = bgmVolume.Value;
        sfxAudioSource.volume = sfxVolume.Value;
    }

    public void MusicVolumeRefreshed()
    {
        bgmAudioSource.volume = bgmVolume.Value;
    }

    public void SFXVolumeRefreshed()
    {
        sfxAudioSource.volume = sfxVolume.Value;
    }

    public void PlaySFX(AudioClip targetAudio)
    {
        float randomPitch = Random.Range(.95f, 1.05f);
        sfxAudioSource.pitch = randomPitch;
        sfxAudioSource.clip = targetAudio;
        sfxAudioSource.loop = false;
        sfxAudioSource.Play();
    }

    public void PlayBGM(AudioClip targetAudio)
    {
        sfxAudioSource.clip = targetAudio;
        sfxAudioSource.loop = true;
        sfxAudioSource.Play();
    }
}
