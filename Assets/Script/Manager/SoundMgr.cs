using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMgr : Singleton<SoundMgr>
{
    public AudioMixer MasterMixer;

    [Header("#BGM")]
    public AudioClip BgmClip;
    public float BgmVolume;
    public AudioSource BgmPlayer;

    [Header("#SFX")]
    public AudioClip[] SfxClip;
    public float SfxVolume;
    public int Channel;
    AudioSource[] sfxPlayers;
    int channelIndex;

    

    public enum Sfx // 사운드 열거형으로 설정해두기
    {

    }

    protected override void Awake()
    {
        base.Awake();
        Init();
    }
    void Init()
    {     
        // 배경음 플레이어 초기화
        GameObject BgmObject = new GameObject("BgmPlayer");
        BgmObject.transform.parent = transform;
        BgmPlayer = BgmObject.AddComponent<AudioSource>();
        BgmPlayer.outputAudioMixerGroup = MasterMixer.FindMatchingGroups("BGM")[0];

        BgmPlayer.playOnAwake = false;
        BgmPlayer.loop = true;
        //BgmPlayer.volume = BgmVolume;
        BgmPlayer.clip = BgmClip;

        // 효과음 플레이어 초기화
        GameObject SfxObject = new GameObject("SfxPlayer");
        SfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[Channel];
        
        for(int i = 0; i < sfxPlayers.Length; ++i)
        {
            sfxPlayers[i] = SfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;            
            // sfxPlayers[i].volume = SfxVolume;
            sfxPlayers[i].outputAudioMixerGroup = MasterMixer.FindMatchingGroups("SFX")[0];
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

    public void PlayBGM()
    {
        //BgmPlayer.Play();
    }

    public void SetVolume(float _BGM, float _SFX)
    {
        BgmVolume = _BGM;
        SfxVolume = _SFX;
        BgmPlayer.volume = BgmVolume;
        for (int i = 0; i < sfxPlayers.Length; ++i)
        {        
           sfxPlayers[i].volume = SfxVolume;
        }
        
    }
    public void SetVolume_BGM(float _BGM)
    {
        BgmVolume = _BGM;
        BgmPlayer.volume = BgmVolume;


    }
    public void SetVolume_SFX( float _SFX)
    {
        SfxVolume = _SFX;
        for (int i = 0; i < sfxPlayers.Length; ++i)
        {
            sfxPlayers[i].volume = SfxVolume;
        }

    }
}
