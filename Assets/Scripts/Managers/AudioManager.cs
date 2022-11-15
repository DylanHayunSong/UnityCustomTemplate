using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : SingletonBehaviour<AudioManager>
{
    private AudioSource bgmSource = null;

    private GameObject sfxSourceObject = null;
    private List<AudioSource> sfxSources = new List<AudioSource>();

    private int firstlyPlayedSfxNum = 0;
    private const int MAX_SFX_NUM = 10;

    protected override void Init ()
    {
        bgmSource = GetComponent<AudioSource>();
        bgmSource.playOnAwake = false;
        bgmSource.loop = true;

        sfxSourceObject = new GameObject("SfxSources");
        sfxSourceObject.transform.SetParent(transform);
    }

    public void PlayBgm (BgmType type)
    {
        bgmSource.Stop();
        bgmSource.clip = null;

        bgmSource.clip = ResourceCacheManager.inst.GetBgmClip(type);
        bgmSource.Play();
    }

    public void PlaySfx (SfxType type)
    {
        for(int i = 0; i < sfxSources.Count; i++)
        {
            if(sfxSources[i].isPlaying == false)
            {
                sfxSources[i].clip = ResourceCacheManager.inst.GetSfxClip(type);
                sfxSources[i].Play();
                return;
            }
        }

        if(sfxSources.Count < MAX_SFX_NUM)
        {
            AudioSource newSource = sfxSourceObject.AddComponent<AudioSource>();
            sfxSources.Add(newSource);

            newSource.playOnAwake = false;
            newSource.loop = false;
            newSource.clip = ResourceCacheManager.inst.GetSfxClip(type);
            newSource.Play();
        }
        else
        {
            for(int i = 0; i < sfxSources.Count - 1; i++)
            {
                firstlyPlayedSfxNum = sfxSources[i].time < sfxSources[i].time ? i : i + 1;
            }

            sfxSources[firstlyPlayedSfxNum].Stop();
            sfxSources[firstlyPlayedSfxNum].clip = ResourceCacheManager.inst.GetSfxClip(type);
            sfxSources[firstlyPlayedSfxNum].Play();
        }
    }


}
