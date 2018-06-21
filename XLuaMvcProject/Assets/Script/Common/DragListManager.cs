using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;
[LuaCallCSharp]
public class DragListManager:Singleton<DragListManager> {

    /// <summary>
    /// 创建DragList
    /// 若需要分页 则要设置NeedCount(需要加载的总个数)
    /// </summary>
    /// <param name="parent">父对象</param>
    /// <param name="type">类型(0:左右滑动;1:上下滑动;2:左右滑动)</param>
    /// <param name="posAndSize">DragList初始位置及大小</param>
    /// <returns></returns>
    public DragList Create(Transform parent, int type, Vector4 posAndSize)
    {
        DragList dragListItem = new DragList(parent, type, posAndSize);
        return dragListItem;
    }
}
[LuaCallCSharp]
public class DragList
{
    private string m_ContentPath = "Viewport/Content";//Content路径
    private string m_DragListPath = "Prefab/DragList";//DragListPrefab路径
    private GameObject m_DragList;//DragList对象
    private ScrollRect m_ScrollRect;//滑动列表
    private Image m_BgImage;//滑动列表背景图片
    private Transform m_ContentTrans;//Content Transform
    private GridLayoutGroup1 m_Grid;//网格布局
    private RectTransform m_DragListRect;// DragList RectTransform
    
    private RectTransform m_ContentRect;//Content RectTransform

    private int m_Type = 1;//滑动类型 1 Vertical 2/0 Horizontal

    private float m_GridCellSizeX;//每个item的宽度
    private float m_GridCellSizeY;//每个item的高度
    private float m_DragSizeX;//draglist 宽度
    private float m_DragSizeY;//draglist 高度

    private int m_OnePageCount = 0;//一页有多少个item
    private int m_OneLineCount = 0;//每一行(垂直布局)或者一列(水平布局)有多少个item
    

    /// <summary>
    /// 加载下一页事件(分页加载必须设置该事件)
    /// 在定义的事件中调用AddNewData添加新的item
    /// </summary>
    public Action OnLoadNextPage;//加载下一页事件
    private Image BgImage
    {
        get
        {
            if (m_BgImage == null)
            {
                m_BgImage = m_DragList.GetComponent<Image>();
            }
            return m_BgImage;
        }   
    }
    private Transform ContentTrans
    {
        get
        {
            if (m_ContentTrans == null)
            {
                m_ContentTrans = m_DragList.transform.Find(m_ContentPath);
            }
            return m_ContentTrans;
        }
    }

    private RectTransform ContentRect
    {
        get
        {
            if (m_ContentRect == null)
            {
                m_ContentRect = ContentTrans.GetComponent<RectTransform>();
            }
            return m_ContentRect;
        }
    }

    private Vector2 ContentSizeDelta
    {
        get
        {
            return ContentRect.sizeDelta;
        }
        set
        {
            ContentRect.sizeDelta = value;
        }
    }
    private GridLayoutGroup1 Grid
    {
        get
        {
            if (m_Grid == null)
            {
                m_Grid = ContentTrans.GetComponent<GridLayoutGroup1>();
            }
            return m_Grid;
        }
    }

    /// <summary>
    /// 需要加载的item总个数
    /// </summary>
    public int NeedCount
    {
        get; set;
    }
    public int NowCount
    {
        get
        {
            return ContentTrans.childCount;
        }
    }

    public int OnePageCount
    {
        get
        {
            if (m_OnePageCount == 0)
            {
                if (m_Type == 1)
                {
                    m_OnePageCount = Mathf.FloorToInt(m_DragSizeX / m_GridCellSizeX) * Mathf.CeilToInt(m_DragSizeY / m_GridCellSizeY);
                }
                else
                {
                    m_OnePageCount = Mathf.CeilToInt(m_DragSizeX / m_GridCellSizeX) * Mathf.FloorToInt(m_DragSizeY / m_GridCellSizeY);
                }
            }
            return m_OnePageCount;
        }
        set
        {
            m_OnePageCount = value;
        }
    }

    private int OneLineCount
    {
        get
        {
            if (m_OneLineCount == 0)
            {
                if (m_Type == 1)
                {
                    m_OneLineCount = Mathf.FloorToInt(m_DragSizeX / m_GridCellSizeX);
                }
                else
                {
                    m_OneLineCount = Mathf.FloorToInt(m_DragSizeY / m_GridCellSizeY);
                }
            }
            return m_OneLineCount;
        }
    }
    public string Name
    {
        get
        {
            return m_DragList.name;
        }
        set
        {
            m_DragList.name = value;
        }
    }

    /// <summary>
    /// MoveToIndex速度默认为2计算公式为(item.height/value)
    /// 设置的值越小速度越快
    /// </summary>
    public float MoveSpped
    {
        get;set;
    }

    /// <summary>
    /// 实例化Draglist
    /// </summary>
    /// <param name="parent">父对象</param>
    /// <param name="type">类型(0:上下左右滑动；1:上下滑动； 0/2:左右滑动)</param>
    /// <param name="posAndSize">位置以及初始大小</param>
    public DragList(Transform parent,int type,Vector4 posAndSize)
    {
        if (type>2 || type<0)
        {
            Debuger.LogError("type is error,type can be 1 or 0 or 2");
            return;
        }
        MoveSpped = 2;


        m_Type = type;
        m_DragSizeX = posAndSize.z;
        m_DragSizeY = posAndSize.w;
        
        m_DragList = GameObject.Instantiate(Resources.Load(m_DragListPath) as GameObject,parent);
        m_DragList.name = "DragList";
        m_DragListRect = m_DragList.GetComponent<RectTransform>();
        m_DragListRect.sizeDelta = new Vector2(posAndSize.z, posAndSize.w);
        m_DragList.transform.localPosition = new Vector2(posAndSize.x, posAndSize.y);


        m_ScrollRect = m_DragList.transform.GetComponent<ScrollRect>();
        m_ScrollRect.vertical = m_Type == 1;
        m_ScrollRect.horizontal = m_Type != 1;
        if (m_Type == 0)
        {
            Grid.startAxis = GridLayoutGroup1.Axis.Vertical;
        }
        if (m_Type == 0 || m_Type == 2)
        {
            ContentRect.anchorMin = new Vector2(0, 0);
            ContentRect.anchorMax = new Vector2(0, 1);
            ContentRect.pivot = new Vector2(0, 1);
            ContentSizeDelta = new Vector2(posAndSize.z, 0);
        }
        else
        {
            ContentSizeDelta = new Vector2(0, posAndSize.w);
        }
        AddListener();
    }

    /// <summary>
    /// 设置DragList背景图片
    /// </summary>
    /// <param name="bgName">背景图片名字</param>
    public void SetListBg(string bgName)
    {
        BgImage.SetBgSprite(bgName);
    }

    /// <summary>
    /// 设置DragList局部位置
    /// </summary>
    /// <param name="x">x</param>
    /// <param name="y">y</param>
    public void SetLoacation(float x,float y)
    {
        m_DragList.transform.localPosition = new Vector2(x,y);
    }

    /// <summary>
    /// 设置滑动类型
    /// 0 - Unrestricted - 无限制
    /// 1 - Elastic - 弹性
    /// 2 - Clamped - 夹紧
    /// </summary>
    /// <param name="type"></param>
    public void SetMovementType(int type)
    {
        m_ScrollRect.movementType = (ScrollRect.MovementType)type;
    }

    /// <summary>
    /// 设置每个item的大小
    /// </summary>
    /// <param name="cellSizeX">item 宽度</param>
    /// <param name="cellSizeY">item 高度</param>
    public void SetCellSize(float cellSizeX,float cellSizeY)
    {
        this.m_GridCellSizeX = cellSizeX;
        this.m_GridCellSizeY = cellSizeY;
        Grid.cellSize = new Vector2(cellSizeX,cellSizeY);
    }

    /// <summary>
    /// 设置Content宽高度(初次加载item/添加item时调用)
    /// </summary>
    private void SetContentHeiht()
    {
        //上下滑动
        if (m_Type == 1)
        {
            int lineCount = Mathf.FloorToInt(ContentTrans.childCount / Mathf.FloorToInt(m_DragSizeX/m_GridCellSizeX));//一共有多少行
            ContentSizeDelta = new Vector2(ContentSizeDelta.x, lineCount * m_GridCellSizeY);//设置Content高度
        }
        else
        {
            int lineCount = Mathf.FloorToInt(ContentTrans.childCount / Mathf.FloorToInt(m_DragSizeY / m_GridCellSizeY));//一共有多少列
            ContentSizeDelta = new Vector2(lineCount * m_GridCellSizeX, ContentSizeDelta.y);//设置Content宽度
        }
    }

    /// <summary>
    /// 初次添加item对象,初始化至少要保证大于1页
    /// 如:5行 5列 一页有25个item 则设置30个或者更多
    /// 即大于等于一页加一行item
    /// </summary>
    /// <param name="gameObjects"></param>
    public void SetArrayData(GameObject[] gameObjects)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetParent(ContentTrans);
            gameObjects[i].transform.SetAsLastSibling();
            gameObjects[i].SetActive(true);
        }
        SetContentHeiht();
    }

    /// <summary>
    /// 添加下一页item(无个数限制 最好是刚好一页)
    /// 需要在自定义事件中调用
    /// </summary>
    /// <param name="gameObjects">item数组</param>
    public void AddNewData(GameObject[] gameObjects)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetParent(ContentTrans);
            gameObjects[i].transform.SetAsLastSibling();
            gameObjects[i].SetActive(false);
        }
        SetContentHeiht();
    }

    /// <summary>
    /// 添加滑动事件
    /// </summary>
    private void AddListener()
    {
        m_ScrollRect.onValueChanged.AddListener((Vector2 v2)=> 
        {
            if (ContentTrans.childCount<NeedCount)
            {
                //说明滑到了底部 需要加载新的item
                if (m_Type == 1)
                {
                    if (v2.y<=0)
                    {
                        OnLoadNextPage();
                        return;
                    }
                }
                else
                {
                    if (v2.x>=1)
                    {
                        OnLoadNextPage();
                        return;
                    }
                }
            }
            HideOutItem();
        });
    }

    float m_OutValue = 0;
    int m_OutLine = -1;

    float m_PosY = 0;
    float m_PosX = 0;
    /// <summary>
    /// 隐藏无需显示的item
    /// </summary>
    private void HideOutItem()
    {
        if (m_Type == 1)
        {
            //向上滑动
            if (ContentTrans.localPosition.y > m_PosY)
            {
                if (ContentTrans.localPosition.y < 0 || ContentTrans.localPosition.y > ContentSizeDelta.y - m_DragSizeY)
                {
                    return;
                }
                m_PosY = ContentTrans.localPosition.y;
                if (m_PosY > m_OutValue + m_GridCellSizeY)
                {
                    m_OutValue += m_GridCellSizeY;
                    m_OutLine++;
                    for (int i = 0; i < OneLineCount; i++)
                    {
                        GameObject obj = ContentTrans.GetChild(i + m_OutLine * OneLineCount).gameObject;
                        obj.SetActive(false);

                        GameObject obj1 = ContentTrans.GetChild(i + (m_OutLine+1) * OneLineCount+OnePageCount).gameObject;
                        obj1.SetActive(true);
                    }
                }
            }
            //向下滑动
            else if (ContentTrans.localPosition.y < m_PosY)
            {
                if (ContentTrans.localPosition.y < 0 || ContentTrans.localPosition.y > ContentSizeDelta.y - m_DragSizeY)
                {
                    return;
                }
                m_PosY = ContentTrans.localPosition.y;
                if (m_PosY < m_OutValue)
                {
                    m_OutValue -= m_GridCellSizeY;
                    for (int i = 0; i < OneLineCount; i++)
                    {
                        GameObject obj = ContentTrans.GetChild(i + m_OutLine * OneLineCount).gameObject;
                        obj.SetActive(true);

                        GameObject obj1 = ContentTrans.GetChild(i + (m_OutLine + 1) * OneLineCount+OnePageCount).gameObject;
                        obj1.SetActive(false);
                    }
                    m_OutLine--;
                }
            }
        }
        else
        {
            //向左滑动 ContentTrans.localPosition.x 减小
            if (ContentTrans.localPosition.x < m_PosX)
            {
                if (ContentTrans.localPosition.x > 0 || Mathf.Abs(ContentTrans.localPosition.x) > ContentSizeDelta.x - m_DragSizeX)
                {
                    return;
                }
                m_PosX = ContentTrans.localPosition.x;
                if (m_PosX < m_OutValue - m_GridCellSizeX)
                {
                    m_OutValue -= m_GridCellSizeX;
                    m_OutLine++;
                    for (int i = 0; i < OneLineCount; i++)
                    {
                        GameObject obj = ContentTrans.GetChild(i + m_OutLine * OneLineCount).gameObject;
                        obj.SetActive(false);

                        GameObject obj1 = ContentTrans.GetChild(i + (m_OutLine + 1) * OneLineCount+OnePageCount).gameObject;
                        obj1.SetActive(true);
                    }
                }
            }
            //向右滑动
            else if (ContentTrans.localPosition.x > m_PosX)
            {
                if (ContentTrans.localPosition.x > 0 || Mathf.Abs(ContentTrans.localPosition.x) > ContentSizeDelta.x - m_DragSizeX)
                {
                    return;
                }
                m_PosX = ContentTrans.localPosition.x;
                if (m_PosX>0)
                {
                    return;
                }
                if (m_PosX > m_OutValue)
                {
                    m_OutValue += m_GridCellSizeX;
                    for (int i = 0; i < OneLineCount; i++)
                    {
                        GameObject obj = ContentTrans.GetChild(i + m_OutLine * OneLineCount).gameObject;
                        obj.SetActive(true);

                        GameObject obj1 = ContentTrans.GetChild(i + (m_OutLine + 1) * OneLineCount+OnePageCount).gameObject;
                        obj1.SetActive(false);
                    }
                    m_OutLine--;
                }
            }
        }
    }

    /// <summary>
    /// 设置选中索引
    /// </summary>
    /// <param name="index"></param>
    public void SetSelected(int index)
    {

    }

    /// <summary>
    /// 移动到指定索引
    /// </summary>
    /// <param name="index">索引(从0开始)</param>
    public void MoveToIndex(int index)
    {
        if (m_Type == 1)
        {
            float posY = ContentTrans.localPosition.y;
            float indexPosY = m_GridCellSizeY * (index == 0 ? 0 : Mathf.CeilToInt(index / (float)OneLineCount) - 1);
            GlobalInit.Instance.StartCoroutine(StartMoveToIndexX(posY,indexPosY));
        }
        else
        {
            float posX = ContentTrans.localPosition.x;
            float indexPosX = m_GridCellSizeX * (index == 0 ? 0 : Mathf.CeilToInt(index / (float)OneLineCount) - 1)*(-1);
            GlobalInit.Instance.StartCoroutine(StartMoveToIndexY(posX, indexPosX));
        }
    }


    private IEnumerator StartMoveToIndexX(float posY, float indexPosY)
    {
        float remainder = posY % m_GridCellSizeY;
        if (remainder != 0)
        {
            posY -= remainder;
            ContentTrans.localPosition = new Vector2(ContentTrans.localPosition.x, posY);
        }
        if (posY>indexPosY)
        {
            while (posY>indexPosY)
            {
                posY -= (m_GridCellSizeY / MoveSpped);
                ContentTrans.localPosition = new Vector2(ContentTrans.localPosition.x, posY);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (posY < indexPosY)
            {
                posY += (m_GridCellSizeY / MoveSpped);
                ContentTrans.localPosition = new Vector2(ContentTrans.localPosition.x, posY);
                yield return new WaitForEndOfFrame();
            }
        }
    }
    private IEnumerator StartMoveToIndexY(float posX, float indexPosX)
    {
        float remainder = posX % m_GridCellSizeX;
        if (remainder != 0)
        {
            posX -= remainder;
            ContentTrans.localPosition = new Vector2(posX,ContentTrans.localPosition.y);
        }
        if (posX > indexPosX)
        {
            while (posX > indexPosX)
            {
                posX -= (m_GridCellSizeX / MoveSpped);
                ContentTrans.localPosition = new Vector2(posX, ContentTrans.localPosition.y);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (posX < indexPosX)
            {
                posX += (m_GridCellSizeX / MoveSpped);
                ContentTrans.localPosition = new Vector2(posX, ContentTrans.localPosition.y);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
