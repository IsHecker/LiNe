using System;
using System.Collections;
using UnityEngine;

public class AudioManager : SingletonPresistent<AudioManager>
{
    [SerializeField] private AudioSource _musicSource, _effectSource;

    public void PlaySound(AudioClip audioClip) => _effectSource.PlayOneShot(audioClip);
    public void PlaySound(AudioClipsHolder audioHolder, string clipName)
    {
        AudioClip audioClip = audioHolder.audioClips.Find<Audio>(value => value.ClipName == clipName).AudioClip;
        _effectSource.PlayOneShot(audioClip);
    }
    public void ChangeMasterVolume(float value) => AudioListener.volume = value;
    public void ChangeMusicSourceVolume(float Value) => _musicSource.volume = Value;
    public void ChangeEffectSourceVolume(float Value) => _effectSource.volume = Value;
    public void GameOverEffect() => LeanTween.value(_musicSource.pitch, 0, 2f).setOnUpdate(value => _musicSource.pitch = value).setOnComplete(_musicSource.Pause);
    public void RestartEffect() { _musicSource.UnPause(); LeanTween.value(_musicSource.pitch, 1, 1.2f).setIgnoreTimeScale(true).setOnUpdate(value => _musicSource.pitch = value); }
    
    //public void RestartEffect() 
    //{
    //    _musicSource.UnPause();
    //    StartCoroutine(animaiton());
    //    IEnumerator animaiton()
    //    {
    //        while (true)
    //        {
    //            yield return new WaitForSecondsRealtime(0.001f);
    //            _musicSource.pitch = Mathf.MoveTowards(_musicSource.pitch, 1, 0.8f * Time.unscaledDeltaTime);
    //            if (_musicSource.pitch == 1) yield break;
    //            Console.WriteLine("oh");
    //        }
    //    }
    //}
}
