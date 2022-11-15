using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

public class ResourceCacheManager : SingletonBehaviour<ResourceCacheManager>
{
    [SerializeField]
    private GameSetting gameSetting;
    [SerializeField]
    private BgmClipBase bgmClips;
    [SerializeField]
    private SfxClipBase sfxClips;

    protected override void Init()
    {

    }

    public GameSetting GameSetting { get { return gameSetting; } }

    public AudioClip GetBgmClip(BgmType type)
    {
        return bgmClips[type];
    }

    public AudioClip GetSfxClip(SfxType type)
    {
        return sfxClips[type];
    }

    [Serializable]
    private class BgmClipBase : SerializableDictionaryBase<BgmType, AudioClip> { }
    [Serializable]
    private class SfxClipBase : SerializableDictionaryBase<SfxType, AudioClip> { }
}
