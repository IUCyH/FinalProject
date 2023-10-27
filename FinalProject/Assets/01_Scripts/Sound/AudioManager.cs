using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton_DontDestroy<SoundManager>
{
    public float masterVolumeSFX = 1f;
    public float masterVolumeBGM = 1f;

    AudioSource sfxPlayer;
    AudioSource bgmPlayer;

    public void SetVolumeSFX(float a_volume)
    {
        masterVolumeSFX = a_volume;
    }

    public void SetVolumeBGM(float a_volume)
    {
        masterVolumeBGM = a_volume;
        bgmPlayer.volume = masterVolumeBGM;
    }
}
