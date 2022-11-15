using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using System;

public class PageController : MonoBehaviour
{
    [ReadOnly]
    [SerializeField]
    private PageType curPage;
    private PageType? prevPage;

    [SerializeField]
    private PageBase pages;

    public PageType CurPage { get { return curPage; } }
    public PageType? PrevPage { get { return prevPage; } }

    private void Awake()
    {
        GameManager.inst.SetPageController(this);

        foreach (var page in pages)
        {
            page.Value.Init();

            if (page.Key == PageType.MAIN)
            {
                page.Value.gameObject.SetActive(true);
            }
            else
            {
                page.Value.gameObject.SetActive(false);
            }
        }
    }
    public void ChangePage(PageType type)
    {
        if (curPage != type)
        {
            foreach (var page in pages)
            {
                if (type == page.Key)
                {
                    page.Value.gameObject.SetActive(true);
                }
                else
                {
                    page.Value.gameObject.SetActive(false);
                }
            }

            if (GameManager.inst.OnPageChangedAction != null)
            {
                GameManager.inst.OnPageChangedAction.Invoke(type);
            }

            prevPage = curPage;
            curPage = type;
        }
    }


    [Serializable]
    private class PageBase : SerializableDictionaryBase<PageType, UP_BasePage> { }
}
