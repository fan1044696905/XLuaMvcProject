using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test : MonoBehaviour {

    public Image bgImage;

    private Image[] images;
    private Button[] btns;
    private int[] strs = {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,17,16,15,14,13,12,11,10,9,8,7,6,5 };

    private Transform transParent;
	void Start () {
        images = transform.GetComponentsInChildren<Image>();
        for (int i = 0; i < strs.Length; i++)
        {
            //images[i].SetAtlasSprite("fishsss", strs[i].ToString());
            //images[i].SetAtlasSprite("fishsss", "fishsss");
            //images[i].SetBgSprite("12");
        }
        //transParent = GameObject.Find("CubeParent").transform;
        //AudioManager.Instance.go = gameObject;

        //GameObject go = Resources.Load("Prefab/Cube") as GameObject;
        //PoolManager.Instance.Register("Cube", go, 10);

        //btns = transform.GetComponentsInChildren<Button>();
        //AudioManager.Instance.AddAudioSource();
        //btns[0].onClick.AddListener(delegate 
        //{
        //    //AudioManager.Instance.PlayBgmAudio("login");
        //    //PoolManager.Instance.Get("Cube", transParent);
        //    FrameTimerManager.Instance.Add("Test", 1f, OnUpdate,5);
        //    //StartCoroutine(DestroyTimer());
        //});
        //btns[1].onClick.AddListener(delegate
        //{
        //    //AudioManager.Instance.PlayBgmAudio("jieyangshan");
        //    //PoolManager.Instance.RecycleAll("Cube");
        //});
        //btns[2].onClick.AddListener(delegate
        //{
        //    //AudioManager.Instance.PlayBgmAudio("juejun");
        //});
        //btns[3].onClick.AddListener(delegate
        //{
        //    AudioManager.Instance.StopAllEffect();
        //});
        //btns[4].onClick.AddListener(delegate
        //{
        //    AudioManager.Instance.PlayEffectAudio("2");
        //});
        //btns[5].onClick.AddListener(delegate
        //{
        //    AudioManager.Instance.PlayEffectAudio("3");
        //});

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debuger.Log("当前网络:不可用");
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            Debuger.Log("当前网络:3G/4G");
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            Debuger.Log("当前网络:Wifi");
        }

        StartCoroutine(LoadBgSprite());
    }

    private void FixedUpdate()
    {
        FrameTimerManager.Instance.FrameHandle();
    }

    private void OnOver()
    {
        Debuger.Log("onOver");
        FrameTimerManager.Instance.RemoveCallback("Tes");
    }

    private void OnUpdate()
    {
        Debuger.Log("百世经纶一页书");
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(2);
        FrameTimerManager.Instance.RemoveAll();
    }


    IEnumerator LoadBgSprite()
    {
        string url = "http://192.168.1.105:8080/Web/Images/common/bg.jpg";
        WWW www = new WWW(url);
        yield return www;
        if (www!=null && string.IsNullOrEmpty(www.error))
        {
            Texture2D texture = www.texture;
            Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            bgImage.sprite = sp;
        }
        else
        {
            Debuger.LogError("The url is error:  "+url);
        }
    }
}
