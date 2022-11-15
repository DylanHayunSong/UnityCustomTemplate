using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

public class DataTableManager : SingletonBehaviour<DataTableManager>
{
    [SerializeField]
    private DataTableBase dataTables;

    protected override void Init ()
    {
    }

    [Serializable]
    private class DataTableBase : SerializableDictionaryBase<DataTableType, BaseDataTable> { }
}
