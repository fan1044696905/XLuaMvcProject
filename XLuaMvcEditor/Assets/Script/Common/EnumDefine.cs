
using UnityEngine;
using System.Collections;


/// <summary>
/// 枚举定义
/// </summary>
public class EnumDefine
{
}


#region Language 语言

/// <summary>
/// 语言
/// </summary>
public enum E_Language
{
    CN,
    EN
}

#endregion

#region PlayType 玩法类型

/// <summary>
/// 玩法类型
/// </summary>
public enum E_PlayType
{
    /// <summary>
    /// 单人副本
    /// </summary>
    PVE,
    /// <summary>
    /// 多人混战
    /// </summary>
    PVP
}

#endregion

#region SceneType 场景类型
/// <summary>
/// 场景类型
/// </summary>
public enum E_SceneType
{
    /// <summary>
    /// 登陆场景
    /// </summary>
    LogOn=0,

    /// <summary>
    /// 选择角色
    /// </summary>
    SelectRole,

    /// <summary>
    /// 世界地图
    /// </summary>
    WorldMap,

    /// <summary>
    /// 游戏关卡
    /// </summary>
    GameLevel,
}
#endregion

#region MessageViewType 消息类型

/// <summary>
/// 消息类型
/// </summary>
public enum E_MessageViewType
{
    /// <summary>
    /// 确定
    /// </summary>
    Ok,

    /// <summary>
    /// 确定和取消
    /// </summary>
    OkAndCancel
}

#endregion

#region WindowUIType 窗口类型
/// <summary>
/// 窗口类型
/// </summary>
public enum E_WindowUIType
{
    /// <summary>
    /// 未设置
    /// </summary>
    None=0,
    /// <summary>
    /// 登录窗口
    /// </summary>
    LogOn,
    /// <summary>
    /// 注册窗口
    /// </summary>
    Reg,
    /// <summary>
    /// 进入区服
    /// </summary>
    GameServerEnter,
    /// <summary>
    /// 区服选择
    /// </summary>
    GameServerSelect,
    /// <summary>
    /// 角色信息
    /// </summary>
    RoleInfo,
    /// <summary>
    /// 游戏关卡地图
    /// </summary>
    GameLevelMap,
    /// <summary>
    /// 游戏关卡详情
    /// </summary>
    GameLevelDetail,
    /// <summary>
    /// 游戏关卡胜利
    /// </summary>
    GameLevelVictory,
    /// <summary>
    /// 游戏关卡失败
    /// </summary>
    GameLevelFail,
    /// <summary>
    /// 世界地图
    /// </summary>
    WorldMap,
    /// <summary>
    /// 世界地图失败
    /// </summary>
    WorldMapFail
}
#endregion

#region WindowUIContainerType UI容器类型
/// <summary>
/// UI容器类型
/// </summary>
public enum E_WindowOpenType
{
    /// <summary>
    /// 左上
    /// </summary>
    TopLeft=0,
    /// <summary>
    /// 右上
    /// </summary>
    TopRight,
    /// <summary>
    /// 左下
    /// </summary>
    BottomLeft,
    /// <summary>
    /// 右下
    /// </summary>
    BottomRight,
    /// <summary>
    /// 居中
    /// </summary>
    Center
}
#endregion

#region WindowShowStyle 窗口打开方式
/// <summary>
/// 窗口打开方式
/// </summary>
public enum E_WindowShowStyle
{
    /// <summary>
    /// 正常打开
    /// </summary>
    Normal=0,
    /// <summary>
    /// 从中间放大
    /// </summary>
    CenterToBig,
    /// <summary>
    /// 从上往下
    /// </summary>
    FromTop,
    /// <summary>
    /// 从下往上
    /// </summary>
    FromDown,
    /// <summary>
    /// 从左向右
    /// </summary>
    FromLeft,
    /// <summary>
    /// 从右向左
    /// </summary>
    FromRight
}
#endregion

#region RoleType 角色类型
/// <summary>
/// 角色类型
/// </summary>
public enum E_RoleType
{
    /// <summary>
    /// 未设置
    /// </summary>
    None = 0,
    /// <summary>
    /// 当前玩家
    /// </summary>
    MainPlayer = 1,
    /// <summary>
    /// 怪
    /// </summary>
    Monster = 2,
    /// <summary>
    /// 其他玩家
    /// </summary>
    OTherRole = 3
}
#endregion

#region RoleState 角色状态

/// <summary>
/// 角色状态
/// </summary>
public enum E_RoleState
{
    /// <summary>
    /// 未设置
    /// </summary>
    None = 0,
    /// <summary>
    /// 待机
    /// </summary>
    Idle = 1,
    /// <summary>
    /// 跑了
    /// </summary>
    Run = 2,
    /// <summary>
    /// 攻击
    /// </summary>
    Attack = 3,
    /// <summary>
    /// 受伤
    /// </summary>
    Hurt = 4,
    /// <summary>
    /// 死亡
    /// </summary>
    Die = 5,
    /// <summary>
    /// 选择
    /// </summary>
    Select = 11
}

#endregion

#region ValueChangeType 数值变化类型

/// <summary>
/// 数值变化类型
/// </summary>
public enum E_ValueChangeType
{
    /// <summary>
    /// 增加
    /// </summary>
    Add = 0,
    /// <summary>
    /// 减少
    /// </summary>
    Subtrack = 1
}

#endregion

#region RoleAnimatorState 角色动画状态

/// <summary>
/// 角色动画状态
/// </summary>
public enum E_RoleAnimatorState
{
    /// <summary>
    /// 
    /// </summary>
    Idle_Normal = 1,
    Idle_Fight = 2,
    Run = 3,
    Hurt = 4,
    Die = 5,
    Select = 6,
    XiuXian = 7,
    Died = 8,
    PhyAttack1 = 11,
    PhyAttack2 = 12,
    PhyAttack3 = 13,
    Skill1 = 14,
    Skill2 = 15,
    Skill3 = 16,
    Skill4 = 17,
    Skill5 = 18,
    Skill6 = 19,
}

#endregion

#region RoleIdleState 角色待机状态

/// <summary>
/// 角色待机状态
/// </summary>
public enum E_RoleIdleState
{
    /// <summary>
    /// 普通待机
    /// </summary>
    ToIdleNormal,

    /// <summary>
    /// 战斗待机
    /// </summary>
    ToIdleFight
}

#endregion

#region ToAnimatorCondition 动画条件？

/// <summary>
/// 动画条件名称
/// </summary>
public enum E_ToAnimatorCondition
{
    ToIdleNormal,
    ToIdleFight,
    ToRun,
    ToHurt,
    ToDie,
    ToDied,
    ToPhyAttack,
    ToSkill,
    ToSelect,
    ToXiuXian,
    CurrState
}

#endregion

#region RoleAttackType 角色攻击类型

/// <summary>
/// 角色攻击类型
/// </summary>
public enum E_RoleAttackType
{
    /// <summary>
    /// 物理攻击
    /// </summary>
    PhyAttack,
    /// <summary>
    /// 技能攻击
    /// </summary>
    SkillAttack
}

#endregion

#region GameLevelGrade

/// <summary>
/// 游戏关卡难度等级
/// </summary>
public enum E_GameLevelGrade
{
    /// <summary>
    /// 普通
    /// </summary>
    Normal = 0,
    /// <summary>
    /// 困难
    /// </summary>
    Hard = 1,
    /// <summary>
    /// 地狱
    /// </summary>
    Hell = 2
}

#endregion

#region GoodsType 物品类型

/// <summary>
/// 物品类型
/// </summary>
public enum E_GoodsType
{
    /// <summary>
    /// 装备
    /// </summary>
    Equip = 0,
    /// <summary>
    /// 道具
    /// </summary>
    Item = 1,
    /// <summary>
    /// 材料
    /// </summary>
    Material = 2
}

#endregion

#region SpriteSourceType 图片资源类型

/// <summary>
/// 图片资源类型
/// </summary>
public enum E_SpriteSourceType
{
    /// <summary>
    /// 剧情关卡图标
    /// </summary>
    GameLevelIco = 0,
    /// <summary>
    /// 剧情关卡详情图片
    /// </summary>
    GameLevelDetail = 1,
    /// <summary>
    /// 世界地图图标
    /// </summary>
    WorldMapIco = 2,
    /// <summary>
    /// 世界地图小地图
    /// </summary>
    WorldMapSmall = 3
}

#endregion

#region UIAudioEffectType UI音频效果类型

/// <summary>
/// UI音频效果类型
/// </summary>
public enum E_UIAudioEffectType
{
    /// <summary>
    /// 按钮点击
    /// </summary>
    ButtonClick = 0,
    /// <summary>
    /// UI关闭
    /// </summary>
    UIClose = 1
}

#endregion
