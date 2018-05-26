using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragListManager:Singleton<DragListManager> {

    public DragList Create(Transform parent, int type, Vector4 posAndSize)
    {
        DragList dragListItem = new DragList(parent, type, posAndSize);
        return dragListItem;
    }
}

public class DragList
{
    private GameObject dragList;
    private Image bgImage;
    private Transform content;
    private GridLayoutGroup grid;
    private RectTransform dragListRect;
    private float cellSizeX;
    private float cellSizeY;
    private Object dragListItem;
    private Image BgImage
    {
        get
        {
            if (bgImage == null)
            {
                bgImage = dragList.GetComponent<Image>();
            }
            return bgImage;
        }   
    }
    private Transform Content
    {
        get
        {
            if (content == null)
            {
                content = dragList.transform.Find("Viewport/Content");
            }
            return content;
        }
    }
    private GridLayoutGroup Grid
    {
        get
        {
            if (grid == null)
            {
                grid = Content.transform.GetComponent<GridLayoutGroup>();
            }
            return grid;
        }
    }


    /// <summary>
    /// 实例化Draglist
    /// </summary>
    /// <param name="parent">父对象</param>
    /// <param name="type">类型(0:上下左右滑动；1:上下滑动； 2:左右滑动)</param>
    /// <param name="posAndSize">位置以及初始大小</param>
    public DragList(Transform parent,int type,Vector4 posAndSize)
    {
        dragList = GameObject.Instantiate(Resources.Load("Prefab/DragList") as GameObject,parent);
        dragList.name = "DragList";
        dragListRect = dragList.GetComponent<RectTransform>();
        dragListRect.sizeDelta = new Vector2(posAndSize.z, posAndSize.w);
        dragList.transform.localPosition = new Vector2(posAndSize.x, posAndSize.y);
    }

    public void SetListBg(string bgName)
    {
        BgImage.SetBgSprite(bgName);
    }

    public void SetLoacation(float x,float y)
    {
        dragList.transform.localPosition = new Vector2(x,y);
    }
    public void SetMovementType(int type)
    {
        dragList.GetComponent<ScrollRect>().movementType = (ScrollRect.MovementType)type;
    }

    public void SetCellSize(UnityEngine.Object dragListItem,float cellSizeX,float cellSizeY)
    {
        this.dragListItem = dragListItem;
        this.cellSizeX = cellSizeX;
        this.cellSizeY = cellSizeY;
        Grid.cellSize = new Vector2(cellSizeX,cellSizeY);
        SetGridHeiht();
    }

    public void SetGridHeiht()
    {
        int lineCount = Mathf.CeilToInt(Grid.transform.childCount / dragListRect.sizeDelta.x);
        RectTransform rt = Grid.transform.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, lineCount * this.cellSizeY);
    }

    public void SetArrayData()
    {

    }
    public void AddNewData()
    {

    }

    public void SetSelected(int index)
    {

    }
    public void MoveToIndex(int index)
    {

    }
}
