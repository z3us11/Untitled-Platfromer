using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] AudioSource BG;
    [SerializeField] AudioSource[] vfx;


    public void VfxPlay(SoundType type)
    {
        switch (type)
        {
            case SoundType.Jump:vfx[(int)type].PlayOneShot(clips[(int)type]);
                break;
            case SoundType.PickUp: vfx[(int)type].PlayOneShot(clips[(int)type]);
                break;
            case SoundType.Hit: vfx[(int)type].PlayOneShot(clips[(int)type]);
                break;
            case SoundType.RepairOnGoing: vfx[(int)type].PlayOneShot(clips[(int)type]);
                break;
            case SoundType.RepairFinsh:
                vfx[(int)type].PlayOneShot(clips[(int)type]);
                break;
            case SoundType.Shoot: vfx[(int)type].PlayOneShot(clips[(int)type]);
                break;
            case SoundType.ButtonSfx: vfx[(int)type].PlayOneShot(clips[(int)type]);
                break;
        }
    }
    public void BgMusic(bool play)
    {
        if (BG.clip != null)
        {
            BG.mute = play;
        }
        else
        {
            Debug.LogError("Please add BG Music");
        }
    }
    public void VfxOnOff(bool play)
    {
        for(int i = 0; i <= clips.Length; i++)
        {
            vfx[i].mute = play;
        }
    }
}
