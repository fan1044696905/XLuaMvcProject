using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConfigTest : MonoBehaviour {

    private Button btn;
    private void Awake()
    {
        btn = transform.GetComponent<Button>("Button");

        XLuaManager.Instance.DoString("require'LuaManager'");
    }
    // Use this for initialization
    void Start () {
        btn.onClick.AddListener(() =>
        {
            LuaManager.Instance.SetLuaConfig("Boss");
            string ss = LuaManager.Instance.GetString(10, "At1tack");
            Debuger.Log(ss);

            string sss = LuaManager.Instance.GetString(10, "MonsterType");
            Debuger.Log(sss);

            int[] ints = LuaManager.Instance.GetIntArray(10, "Ints");
            Debuger.Log(ints[0]);

            float[] floats = LuaManager.Instance.GetFloatArray(10, "Floats");
            Debuger.Log(floats[0]);

            double[] doubles = LuaManager.Instance.GetDoubleArray(10, "Doubles");
            Debuger.Log(doubles[0]);

            string[] strings = LuaManager.Instance.GetStringArray(10, "Names");
            Debuger.Log(strings[0]);
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
