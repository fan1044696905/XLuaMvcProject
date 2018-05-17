using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoLoadTexture : MonoBehaviour
{
    public string ImgName;
    public string ImgPath;

    void Start()
    {
        Image img = GetComponent<Image>();
        if (img != null && !string.IsNullOrEmpty(ImgPath))
        {
            AssetBundleMgr.Instance.LoadOrDownload<Texture2D>(ImgPath, ImgName, (Texture2D obj) =>
            {
                if (obj == null) return;

                var iconRect = new Rect(0, 0, obj.width, obj.height);
                var iconSprite = Sprite.Create(obj, iconRect, new Vector2(0.5f, 0.5f));

                img.overrideSprite = iconSprite;
                img.SetNativeSize();

            }, type: 1);
        }
    }
}
