using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperateUtil : Singleton<OperateUtil> {

    List<OperateAlert> operateList = new List<OperateAlert>();

    public void Show(string msg)
    {
        if (operateList.Count < AppConst.OperateAlertCount)
        {
            int count = AppConst.OperateAlertCount - operateList.Count;
            Object obj = Resources.Load("Prefabs/OperateAlert");
            Transform parentTrans = GameObject.Find(AppConst.OperateAlertParentPath).transform;
            for (int i = 0; i < count; i++)
            {
                GameObject go = GameObject.Instantiate(obj,parentTrans) as GameObject;
                operateList.Add(go.GetComponent<OperateAlert>());
                go.SetActive(false);
            }
        }
        for (int i = 0; i < AppConst.OperateAlertCount; i++)
        {
            if (operateList[i].isRun)
            {
                continue;
            }
            operateList[i].Show(msg);
            break;
        }
    }
}
