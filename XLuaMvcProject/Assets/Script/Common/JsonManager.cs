using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonManager : SingletonMono<JsonManager> {

    private int totalSize;
    public int TotalSize
    {
        get
        {
            return totalSize / 1024;
        }
        private set { totalSize = value; }
    }
    public bool isStartDownload
    {
        get;
        private set;
    }
    public int TotalCount
    {
        get;
        private set;
    }
    public int TotalCompleteCount
    {
        get;
        private set;
    }

    public int DownloadSize { get { return (m_DownloadSize + m_CurrDownloadSiz) / 1024; } }
    private int m_CurrDownloadSiz;//当前文件已经下载大小
    private int m_DownloadSize;//已下载好的文件的总大小


    protected override void OnAwake()
    {
        base.OnAwake();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
    }

}
