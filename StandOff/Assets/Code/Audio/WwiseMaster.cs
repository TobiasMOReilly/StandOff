using System.Collections;
using UnityEngine;
using System;

public class WwiseMaster : MonoBehaviour
{
    uint bankID;
    //System.IntPtr pointer;

     
    #region Wwise Setup and managment
    /// <summary>
    /// Loads a Wwise soundbank that contains all audio events for a project
    /// </summary>
    /// <param name="bankName">Name of the soundBank</param>
    public void LoadBank(string bankName)
    {
        AkSoundEngine.LoadBank(bankName, out bankID);

        //AkSoundEngine.LoadBank(bankName, AkSoundEngine.AK_DEFAULT_POOL_ID, out bankID);
    }

    /// <summary>
    /// NOT SURE MUST ASK!!!!!
    /// </summary>
    public void AudioRendering()
    {
        AkSoundEngine.RenderAudio();
    }

    /// <summary>
    /// NOT SURE MUST ASK!!!!!
    /// </summary>
    public void SetRTPCValue(string rTpcName, float value)
    {
        AkSoundEngine.SetRTPCValue(rTpcName, value);
    }
    #endregion

    #region Post / switch events & Change State
    /// <summary>
    /// Used to play a specific audio event (sample / music)
    /// </summary>
    /// <param name="eventName"> Name of the event</param>
    /// <param name="gameObject">the game object the the sample will originate from</param>
    public void PostEvent(string eventName, GameObject gameObject)
    {
        AkSoundEngine.PostEvent(eventName, gameObject);
        AudioRendering();
    }
    /// <summary>
    /// Used to play a specific audio event (sample / music)
    /// </summary>
    /// <param name="eventID"> ID of the event</param>
    /// <param name="gameObject">the game object the the sample will originate from</param>
    public void PostEvent(uint eventID, GameObject gameObject)
    {
        AkSoundEngine.PostEvent(eventID, gameObject);
        AudioRendering();
    }

    /// <summary>
    /// Switches from one audio event to another. EG from footStepDirt to FootStepGrass
    /// </summary>
    /// <param name="fromSound">Switch Group name</param>
    /// <param name="toSound">Switch ID name</param>
    public void ChangeSwitch(string switchGroup, string switchName, GameObject gameObject)
    {
        AkSoundEngine.SetSwitch(switchGroup, switchName, gameObject);
        AudioRendering();
    }
    /// <summary>
    /// Switches from one audio event to another. EG from footStepDirt to FootStepGrass
    /// Override to accept IDs 
    /// </summary>
    /// <param name="fromSound">Switch Group ID</param>
    /// <param name="toSound">Switch ID</param>
    public void ChangeSwitch(uint switchGroup, uint switchID, GameObject gameObject)
    {
        AkSoundEngine.SetSwitch(switchGroup, switchID, gameObject);
        AudioRendering();
    }

    /// <summary>
    /// NOT SURE MUST ASK!!!!!
    /// </summary>
    public void ChangeState(string stateContainerName, string stateName)
    {
        AkSoundEngine.SetState(stateContainerName, stateName);
    }
    #endregion

    #region Play / Pause / Resume
    /// <summary>
    /// Used to pause an audio event. includes a fade out.
    /// </summary>
    /// <param name="eventName">Name opf the event to be paused</param>
    /// <param name="fadeOut"> Fadeout time</param>
    public void PauseMX(string eventName, float fadeOut)
    {
        uint eventID;
        eventID = AkSoundEngine.GetIDFromString(eventName);

        AkSoundEngine.ExecuteActionOnEvent(eventID, AkActionOnEventType.AkActionOnEventType_Pause, gameObject, (int)(fadeOut * 1000), AkCurveInterpolation.AkCurveInterpolation_Sine);
    }

    /// <summary>
    /// Used to stop an audio event. includes a fade out.
    /// </summary>
    /// <param name="eventName">Name opf the event to be stopped</param>
    /// <param name="fadeOut"> Fadeout time</param>
    public void StopMX(string eventName, float fadeOut)
    {
        uint eventID;
        eventID = AkSoundEngine.GetIDFromString(eventName);
        AkSoundEngine.ExecuteActionOnEvent(eventID, AkActionOnEventType.AkActionOnEventType_Stop, gameObject, (int)(fadeOut * 1000), AkCurveInterpolation.AkCurveInterpolation_Sine);
    }

    /// <summary>
    /// Used to resume an audio event. includes a fade in.
    /// </summary>
    /// <param name="eventName">Name opf the event to be resumed</param>
    /// <param name="fadeOut"> Fade in time</param>
    public void Resume(string eventName, float fadeIn)
    {
        uint eventID;
        eventID = AkSoundEngine.GetIDFromString(eventName);
        AkSoundEngine.ExecuteActionOnEvent(eventID, AkActionOnEventType.AkActionOnEventType_Resume, gameObject, (int)(fadeIn * 1000), AkCurveInterpolation.AkCurveInterpolation_Sine);
    }
    #endregion


    //https://www.audiokinetic.com/library/edge/?source=Unity&id=class_ak_event.html
}