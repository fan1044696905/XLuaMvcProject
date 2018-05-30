using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 创建的AudioSource对象有添加AudioListener
/// 如果场景中有其他AudioListener，需要将之移除
/// </summary>
public class AudioManager : Singleton<AudioManager> {


    private GameObject go;
    private List<AudioItem> audioList = new List<AudioItem>();

    private AudioClip ac;
    public override void Init()
    {
        base.Init();
        if (go == null)
        {
            go = new GameObject("AudioSource");
            go.AddComponent<AudioListener>();
            MonoBehaviour.DontDestroyOnLoad(go);
        }
        if (audioList.Count == 0)
        {
            AddAudioSource();
        }
    }
    public void AddAudioSource()
    {
        if (audioList.Count < AppConst.AudioSourceCount)
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

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="bgmName">音乐名</param>
    /// <param name="volume">声音大小,不设置默认为0.8f</param>
    /// <param name="isLoop">是否循环,默认为循环</param>
    public void PlayBgmAudio(string bgmName, float volume = 0.8f, bool isLoop = true)
    {
        audioList[0].PlayBgmAudio(bgmName, volume, isLoop);
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="effectName">音效名</param>
    /// <param name="volume">声音大小,默认0.8f</param>
    /// <param name="isLoop">是否循环,默认不循环</param>
    public void PlayEffectAudio(string effectName, float volume = 0.8f, bool isLoop = false)
    {
        for (int i = 1; i < audioList.Count; i++)
        {
            if (audioList[i].IsPlay)
            {
                continue;
            }
            audioList[i].PlayEffectAudio(effectName, volume, isLoop);
            return;
        }
    }
    /// <summary>
    /// 停止播放背景音乐
    /// </summary>
    public void StopBgm()
    {
        audioList[0].Stop();
    }

    /// <summary>
    /// 停止播放音效
    /// </summary>
    /// <param name="effectName">音效名</param>
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
    /// <summary>
    /// 停止播放所有音效
    /// </summary>
    public void StopAllEffect()
    {
        for (int i = 1; i < audioList.Count; i++)
        {
            audioList[i].Stop();
        }
    }
    /// <summary>
    /// 暂停背景音乐
    /// </summary>
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
