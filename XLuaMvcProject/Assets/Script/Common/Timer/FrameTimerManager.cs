using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class FrameTimerManager:Singleton<FrameTimerManager>
{
	public FrameTimerManager()
	{
	}

    private List<TimeFrameItem> list = new List<TimeFrameItem>();
		
	public void Clear(){
        list.Clear();
	}

    /// <summary>
    /// 延迟回调
    /// </summary>
    /// <param name="delay"></param>
    /// <param name="callback"></param>
    public void DelayCall(float delay, TimeFrameItem.OnTick0 callback)
    {
        Add("DelayCall", delay, callback, 1);
    }

    /// <summary>
    /// 添加计时器
    /// </summary>
    /// <param name="name">计时器名称</param>
    /// <param name=" ">多少秒执行一次</param>
    /// <param name="callback">回调函数</param>
    /// <param name="repeat">执行次数，-1为无限次</param>
    /// <param name="onOver">结束函数</param>
    /// <param name="priority">优先级</param>
    public void Add(string name, float delay, TimeFrameItem.OnTick0 callback, int repeat = -1, TimeFrameItem.OnTick0 onOver = null, int priority = 10)
    {
        //if (HasFunction(callback) == -1)
        //{
        //    TimeFrameItem fti = new TimeFrameItem(name, delay, repeat, callback, onOver, priority);
        //    list.Add(fti);
        //}
        if (HasFunction(name) == -1)
        {
            TimeFrameItem fti = new TimeFrameItem(name, delay, repeat, callback, onOver, priority);
            list.Add(fti);
        }
        else
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].callback == callback)
                {
                    list[i].delay = delay;
                    list[i].repeat = repeat;
                    list[i].SetEnd = false;
                }
            }
        }
    }

    /// <summary>
    /// 移除计时器
    /// </summary>
    /// <param name="callback">计时器</param>
    public void RemoveCallback(TimeFrameItem.OnTick0 callback)
    {
        int index = HasFunction(callback);
        if (index != -1)
        {
            list[index].SetEnd = true;
        }
    }

    private int HasFunction(TimeFrameItem.OnTick0 callback)
    {
        int i = 0;
        for (i = 0; i < list.Count; i++)
        {
			if (list[i].callback == callback && list[i].IsEnd == false)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 移除计时器
    /// </summary>
    /// <param name="name">事件名</param>
    public void RemoveCallback(string name)
    {
        int index = HasFunction(name);
        if (index != -1)
        {
            list[index].SetEnd = true;
        }
    }

    private int HasFunction(string name)
    {
        int i = 0;
        for (i = 0; i < list.Count; i++)
        {
            if (list[i].name == name && list[i].IsEnd == false)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// 移除所有计时器 切换场景调用
    /// </summary>
    public void RemoveAll()
    {
        int count = list.Count;
        for (int i = 0; i < count; i++)
        {
            list.RemoveAt(i);
        }
    }

    /// <summary>
    /// 计时器入口
    /// 检测计时器 需要在初始化场景中调用(并且DontDestroyOnLoad)
    /// </summary>
	public void FrameHandle()
	{
        int i = 0;
        float nowTime = Time.time;
        for (i = 0; i < list.Count; i++ )
        {
            if (list[i].IsEnd == false)
                list[i].Execute(nowTime);
        }
        for (i = 0; i < list.Count; i++)
        {
            if (list[i].IsEnd == true)
            {
                Debuger.Log("移除计时器："+list[i].name);
                list.RemoveAt(i);
                i--;
            }
        }
	}
}
