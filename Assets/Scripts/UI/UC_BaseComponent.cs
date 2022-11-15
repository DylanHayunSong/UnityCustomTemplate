using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UC_BaseComponent : MonoBehaviour
{
    private UP_BasePage parentPage;

    public abstract void Init ();

    public void SetParent(UP_BasePage page)
    {
        parentPage = page;
    }
}
