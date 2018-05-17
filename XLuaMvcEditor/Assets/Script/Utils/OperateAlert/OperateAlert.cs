using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class OperateAlert : MonoBehaviour {

    private Transform trans;
    private CanvasGroup cg;
    private Text text;

    private Tweener transTweener;
    private Tweener hideTweener;
    private float duration = 0.5f;

    [HideInInspector]
    public bool isRun = false;

    private void Awake()
    {
        trans = this.transform;
        cg = trans.GetOrAddComponent<CanvasGroup>();
        text = trans.GetComponent<Text>("Text");
        transTweener = trans.DOLocalMoveY(trans.localPosition.y + 100, duration).SetAutoKill(false).Pause().OnStepComplete(delegate 
        {
            isRun = false;
            gameObject.SetActive(false);
        });
        hideTweener = cg.DOFade(0.6f, duration).SetAutoKill(false).Pause();
    }

    public void Show(string msg)
    {
        text.text = msg;
        isRun = true;
        gameObject.SetActive(true);
        trans.SetAsLastSibling();
        transTweener.Restart();
        hideTweener.Restart();
    }
}
