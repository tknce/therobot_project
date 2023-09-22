using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : Singleton<SoundMgr>
{


    [Header("#BGM")]
    public AudioClip BgmClip;
    public float BgmVolume;
    AudioSource BgmPlayer;

    [Header("#SFX")]
    public AudioClip[] SfxClip;
    public float SfxVolume;
    public int Channel;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx // ���� ���������� �����صα�
    {

    }

    protected override void Awake()
    {
        base.Awake();
        Init();
    }
    void Init()
    {
        // ����� �÷��̾� �ʱ�ȭ
        GameObject BgmObject = new GameObject("BgmPlayer");
        BgmObject.transform.parent = transform;
        BgmPlayer = BgmObject.AddComponent<AudioSource>();
        BgmPlayer.playOnAwake = false;
        BgmPlayer.loop = true;
        BgmPlayer.volume = BgmVolume;
        BgmPlayer.clip = BgmClip;

        // ȿ���� �÷��̾� �ʱ�ȭ
        GameObject SfxObject = new GameObject("SfxPlayer");
        SfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[Channel];
        
        for(int i = 0; i < sfxPlayers.Length; ++i)
        {
            sfxPlayers[i] = SfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;            
            sfxPlayers[i].volume = SfxVolume;
        }
    }

    public void PlaySfx(Sfx sfx)
    {
        for(int i = 0; i < sfxPlayers.Length; ++i)
        {
            int loopindex =(i + channelIndex) % sfxPlayers.Length;
            if (sfxPlayers[loopindex].isPlaying)
                continue;

            channelIndex = loopindex;
            sfxPlayers[loopindex].clip = SfxClip[(int)sfx];
            sfxPlayers[loopindex].Play();
            break;
        }
    }
}
