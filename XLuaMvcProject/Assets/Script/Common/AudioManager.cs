using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    public void StopBgm()
    {
        audioList[0].Stop();
    }
    public void StopEffect(string effectName)
    {
        for (int i = 1; i < audioList.Count; i++)
        {
            if (audioList[i].audioClipName.Equals(effectName))
            {
                audioList[i].Stop();
                break;
            }
        }
    }
    public void StopAllEffect()
    {
        for (int i = 1; i < audioList.Count; i++)
        {
            audioList[i].Stop();
        }
    }
    public void PauseBgm()
    {
        audioList[0].Pause();
    }
}

public class AudioItem
{
    private AudioSource audioSource;
    private float duration = 0.2f;
    public string audioClipName = "";
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
        audioClipName = bgmName;
        AudioClip ac = AssetBundleMgr.Instance.LoadAudio(string.Format(AppConst.AudioBgmPath, bgmName), bgmName);
        audioSource.clip = ac;
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
        audioClipName = effectName;
        AudioClip ac = AssetBundleMgr.Instance.LoadAudio(string.Format(AppConst.AudioEffectPath,effectName), effectName);
        audioSource.loop = isLoop;
        audioSource.clip = ac;
        audioSource.volume = volume;
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void Pause()
    {
        audioSource.Pause();
    }
}
