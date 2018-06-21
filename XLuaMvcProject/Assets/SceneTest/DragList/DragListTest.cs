using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DragListTest : MonoBehaviour {

    private Button Btn0;
    private Button Btn1;
    private Button Btn2;
    private DragList dragList1;
    // Use this for initialization
	void Start () {

        Btn0 = transform.Find("Btn0").GetComponent<Button>();
        Btn1 = transform.Find("Btn1").GetComponent<Button>();
        Btn2 = transform.Find("Btn2").GetComponent<Button>();


        

        Btn0.onClick.AddListener(()=> 
        {
            DragList draglist = DragListManager.Instance.Create(transform, 0, new Vector4(0, 0, 500, 500));
            draglist.SetCellSize(100, 100);
            draglist.NeedCount = 300;
            dragList1 = draglist;
            draglist.OnLoadNextPage = OnLoadNextPage;
            draglist.SetArrayData(CreateOnePage(30));
        });
        Btn1.onClick.AddListener(() =>
        {
            DragList draglist = DragListManager.Instance.Create(transform,1, new Vector4(0, 0, 500, 500));
            draglist.SetCellSize(100, 100);
            draglist.NeedCount = 300;
            dragList1 = draglist;
            draglist.OnLoadNextPage = OnLoadNextPage;
            draglist.SetArrayData(CreateOnePage(30));
        });
        Btn2.onClick.AddListener(() =>
        {
            dragList1.MoveToIndex(20);
        });
    }

    private void OnLoadNextPage()
    {
        dragList1.AddNewData(CreateOnePage(30));
    }
    public GameObject[] CreateOnePage(int count)
    {
        int nowCount = dragList1.NowCount;
        GameObject[] gameObjects = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            int index = i + 1 + nowCount;
            GameObject obj = GameObject.Instantiate(Resources.Load("Prefab/Image") as GameObject);
            obj.name = "GameObject" + index;
            obj.transform.GetComponent<Text>("Text").text = index.ToString();
            obj.GetComponent<Button>().onClick.AddListener(()=> 
            {
                Debuger.Log(obj.name);
            });
            gameObjects[i] = obj;
        }
        return gameObjects;
    }
}
