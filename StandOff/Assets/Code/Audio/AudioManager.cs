using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : WwiseMaster
{
    [Header("Audio Banks")]
    [Tooltip("Link to the core Wwise sound bank")]
    [SerializeField]
    private AK.Wwise.Bank SoundBank;

    [Header("SFX")]
    [Header("Wise Events")]
    [Tooltip("Link to the Gunshot event")]
    [SerializeField]
    private AK.Wwise.Event GunShot;

    private void Awake()
    {
        LoadBank(SoundBank.Name);

        //PostEvent(GunShot.Name, this.gameObject);
    }

}
