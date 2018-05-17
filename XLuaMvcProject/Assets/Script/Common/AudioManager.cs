using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum E_AudioType
{
    Bgm,
    Effect
}
public class AudioManager : Singleton<AudioManager> {


    public GameObject go;
    private List<AudioItem> audioList = new List<AudioItem>();

    private AudioClip ac;
    public void AddAudioSource()
    {
        if (audioList.Count<AppConst.AudioSourceCount)
        {
            int count = AppConst.AudioSourceCount - audioList.Count;
            for (int i = 0; i < count; i++)
            {
                AudioSource audioSource = go.AddComponent<AudioSource>();
                AudioItem audioItem = new AudioItem(audioSource);
                if (audioList.Count==0)
                    audioItem.AudioType = E_AudioType.Bgm;
                else
                    audioItem.AudioType = E_AudioType.Effect;
                audioList.Add(audioItem);
            }
        }
    }

    public void PlayBgmAudio(string bgmName, float volume = 0.8f, bool isLoop = true)
    {
        audioList[0].PlayBgmAudio(bgmName, volume, isLoop);   
    }

    public void PlayEffectAudio(string effectName, float volume = 0.8f, bool isLoop = false)
    {
        for (int i = 1 ; i < audioList.Count; i++)
        {
            if (audioList[i].IsPlay)
            {
                continue;
            }
            audioList[i].PlayEffectAudio(effectName, volume, isLoop);
            return;
        }
    }
}

public class AudioItem
{
    private AudioSource audioSource;
    public E_AudioType AudioType;
    private float duration = 0.2f;
    
    public bool IsPlay
    {
        get
        {
            return audioSource.isPlaying;
        }
    }
    
    public AudioItem(AudioSource audioSource)
    {
        this.audioSource = audioSource;
    }
    public void PlayBgmAudio(string bgmName, float volume = 0.8f, bool isLoop = true)
    {
        AudioClip clip = Resources.Load("Audio/AudioBgm/" + bgmName) as AudioClip;
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = isLoop;
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.Play();
    }
    public void PlayEffectAudio(string effectName, float volume = 0.8f, bool isLoop = false)
    {
         AudioClip clip = Resources.Load("Audio/AudioEffect/" + effectName) as AudioClip;
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.loop = isLoop;
        audioSource.Play();
        //audioSource.PlayOneShot(clip, volume);
    }
}
