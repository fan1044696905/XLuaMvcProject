using UnityEngine;

public class TimeFrameItem
{
    public delegate void OnTick0();

    public string name;
    /** 速度，等待多少秒执行一次 */
    public float delay;
    /** 重复次数，-1为无限次 */
    public int repeat;
    /** 回调函数 */
    public OnTick0 callback;
    /** 结束函数 **/
    public OnTick0 onOver;
    /** 上次调用时间 */
    private float lastTime = 0;
    private bool _setEnd = false;
    public int priority;
    public TimeFrameItem(string name, float delay, int repeat, OnTick0 callBack, OnTick0 onOver = null, int p = 10)
    {
        this.name = name;
        this.delay = delay;
        this.repeat = repeat;
        this.callback = callBack;
        this.onOver = onOver;
        this.priority = p;
        this.lastTime = Time.time;
    }
    public bool SetEnd
    {
        set
        {
            _setEnd = value;
        }
    }
    public bool IsEnd
    {
        get
        {
            return _setEnd == true || repeat == 0;
        }
    }
    /// <summary>
    /// 执行函数
    /// </summary>
    /// <param name="nowTime">当前时间</param>
    public void Execute(float nowTime)
    {
        float dt = nowTime - lastTime;
        if (lastTime == -1 || dt > delay)
        {
            lastTime = nowTime;
			callback();
            if (repeat > 0)
            {
                repeat--;
                if (repeat==0)
                {
                    if (onOver != null)
                    {
                        onOver();
                    }
                }
            }
        }
    }
}
