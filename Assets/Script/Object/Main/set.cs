using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using UnityEngine.Audio;
using UnityEngine.UI;

public class set : MonoBehaviour
{
    public GameObject _setting;
    bool _settingChanged = false;

    public AudioMixer MasterMixer;
    public Slider AudioSlider_Master;
    public Slider AudioSlider_Bgm;
    public Slider AudioSlider_SFX;
    public void OnoffObj()
    {
        if(_settingChanged)
        {
            _settingChanged = false;
            _setting.SetActive(_settingChanged);
            return;
        }
        _settingChanged= true;
        _setting.SetActive(_settingChanged);
    }
    // 0 1100 원래 자리 오른쪽 자리
    public void OnObj()
    {
        
    }
    public void OffObj()
    {

    }
    public void AndioControl_Master()
    {
        float sound = AudioSlider_Master.value;

        if (sound == -40f)
        {
            MasterMixer.SetFloat("Master", -80);
            MasterMixer.SetFloat("BGM", -80);
            MasterMixer.SetFloat("SFX", -80);

            AudioSlider_Bgm.value = sound;
            AudioSlider_SFX.value = sound;
            SoundMgr.Inst.SetVolume(-80, -80);
        }
        else 
        { 
            MasterMixer.SetFloat("Master", sound);
            MasterMixer.SetFloat("BGM", sound);
            MasterMixer.SetFloat("SFX", sound);

            AudioSlider_Bgm.value = sound;
            AudioSlider_SFX.value = sound;
            SoundMgr.Inst.SetVolume(sound, sound);
        }
        
    }
    public void AndioControl_BGM()
    {
        float sound = AudioSlider_Bgm.value;

        if(sound > AudioSlider_Master.value)
        {
            sound = AudioSlider_Master.value;
            AudioSlider_Bgm.value = sound;
        }            

        if (sound == -40f)
        {
            MasterMixer.SetFloat("BGM", -80);            
            SoundMgr.Inst.SetVolume_BGM(0);
        }
        else
        {
            MasterMixer.SetFloat("BGM", sound);
            SoundMgr.Inst.SetVolume_BGM(sound);
        }
    }
    public void AndioControl_SFX()
    {
        float sound = AudioSlider_SFX.value;
        if (sound > AudioSlider_Master.value)
        {
            sound = AudioSlider_Master.value;
            AudioSlider_SFX.value = sound;
        }
        if (sound == -40f)
        {
            MasterMixer.SetFloat("SFX", -80);
            SoundMgr.Inst.SetVolume_SFX(-80);
        }
        else
        {
            MasterMixer.SetFloat("SFX", sound);
            SoundMgr.Inst.SetVolume_SFX(sound);
        }
    }
}
