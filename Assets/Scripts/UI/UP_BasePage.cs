using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UP_BasePage : MonoBehaviour
{
    private UC_BaseComponent[] childComponents;

    protected void Awake ()
    {
        childComponents = GetComponentsInChildren<UC_BaseComponent>(true);

        foreach(UC_BaseComponent component in childComponents)
        {
            component.SetParent(this);
            component.Init();
        }

        BindDelegates();
    }

    protected abstract void BindDelegates ();
    public abstract void Init ();
    protected abstract void ResetPage ();

    protected virtual void OnEnable()
    {
        ResetPage();
    }
}
