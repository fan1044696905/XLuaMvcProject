using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragListTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DragList draglist = DragListManager.Instance.Create(transform, 0, new Vector4(0, 0, 500, 500));
        DragListManager.Instance.Create(transform, 1, new Vector4(-450, 0, 400, 400));
        draglist.SetCellSize(null, 100, 100);

        //Debuger.Log(XMLManager.Instance.GetString("Xml/Test",2, "des"));
        //Debuger.Log(XMLManager.Instance.GetStringArray("Xml/Item2", 6, "items4")[0]);
        //Debuger.Log(XMLManager.Instance.GetStringArray("Xml/Item2", 6, "items4")[0]);

        //Debuger.Log(XMLManager.Instance.GetString("Item3", 3,"des"));

        //StartCoroutine(XMLManager.Instance.LoadXml("Test"));
        //XMLManager.Instance.GetString("Test", 1, "des");
        //StartCoroutine(LoadXML());
        //StartCoroutine(XMLManager.Instance.ReadXmlTxt());
        //StartCoroutine(XMLManager.Instance.SplitXmlTxt());
    }
    
	
	
}
