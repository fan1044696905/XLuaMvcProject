using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

/// <summary>
/// 全局初始化
/// </summary>
public class GlobalInit : MonoBehaviour
{

    /// <summary>
    /// 单例
    /// </summary>
    public static GlobalInit Instance;

    /// <summary>
    /// UI动画曲线
    /// </summary>
    public AnimationCurve UIAnimationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));

    /// <summary>
    /// 当前渠道初始化配置
    /// </summary>
    public ChannelInitConfig CurrChannelInitConfig;

    /// <summary>
    /// 账号服务器地址
    /// </summary>
    public string WebAccountUrl;

    /// <summary>
    /// 渠道号
    /// </summary>
    public int ChannelId;

    /// <summary>
    /// 内部版本号
    /// </summary>
    public int InnerVersion;


    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debuger.DebugMode = AppConst.DebugMode;
    }

    void Start()
    {
        CurrChannelInitConfig = new ChannelInitConfig();
        InitChannelConfig(ref WebAccountUrl, ref ChannelId, ref InnerVersion);
        //Debug.Log("WebAccountUrl=" + WebAccountUrl);
        //Debug.Log("ChannelId=" + ChannelId);
        //Debug.Log("InnerVersion=" + InnerVersion);

        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic["ChannelId"] = ChannelId;
        dic["InnerVersion"] = InnerVersion;

        //初始化请求服务器时间,请求服务器下发的资源地址
        //TODO...

    }

    /// <summary>
    /// 初始化渠道配置
    /// </summary>
    /// <param name="webAccountUrl">账号服务器地址</param>
    /// <param name="channelId">渠道Id</param>
    /// <param name="innerVersion">内部版本号</param>
    void InitChannelConfig(ref string webAccountUrl, ref int channelId, ref int innerVersion)
    {
        TextAsset asst = Resources.Load("Config/ChannelConfig") as TextAsset;
        XDocument xDoc = XDocument.Parse(asst.text);
        XElement root = xDoc.Root;
        webAccountUrl = root.Element("WedAccountUrl").Attribute("Value").Value;
        channelId = root.Element("ChannelId").Attribute("Value").Value.ToInt();
        innerVersion = root.Element("InnerVersion").Attribute("Value").Value.ToInt();
    }








}

