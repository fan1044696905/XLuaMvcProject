using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test : MonoBehaviour {

    private Image[] images;
    private int[] strs = {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,17,16,15,14,13,12,11,10,9,8,7,6,5,4,3,2,1 };
	void Start () {
        images = transform.GetComponentsInChildren<Image>();
        for (int i = 0; i < images.Length; i++)
        {
            //images[i].SetSprite("Atlas/fish", strs[i].ToString());
            images[i].SetSprite("Atlas/fishsss", "fishsss");
        }
    }
}
