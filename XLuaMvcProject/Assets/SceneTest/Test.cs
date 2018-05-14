using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test : MonoBehaviour {

    private Image image;
	void Start () {
        image = gameObject.GetComponent<Image>("Image2");
        EventTriggerListener.Get(image.gameObject).onClick += onClick;
    }

    private void onClick(GameObject go)
    {
        Debug.Log(go.name);
    }

    private void OnClick(GameObject _listener, object _args, object[] _params)
    {
        Debug.Log("ssssssssssss");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
