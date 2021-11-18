/*
* Filename: SoundAssets.cs
* Developer: Hunter Leppek
* Purpose: This file implements a prefab (of the same name) to contain all the assets for the sound manager.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A class that contains the assets needed to implement the sound manager.
/// </summary>
/// Member Variables
/// <list type = "bullet">
/// <item>m_psi</item>
/// <item>m_si</item>
/// </list>
public class SoundAssets : MonoBehaviour
{
    public SoundAudioClip[] SoundAudioClipArray;

    private static SoundAssets m_psi; //psi = private instance

    //si = (public) sound instance
    public static SoundAssets m_si
    {
        get
        {
            //The following line ensures that only one instance of our sound prefab (and therefore, our sound manager) can exist (singleton pattern).
            if (m_psi == null) m_psi = (Instantiate(Resources.Load("SoundAssets")) as GameObject).GetComponent<SoundAssets>(); 
            return m_psi;
        }
    }

    /// <summary>
    /// A class that bundles together a sound name and a sound file.
    /// </summary>
    /// Member Variables
    /// <list type = "bullet">
    /// <item>m_SAC_sound</item>
    /// <item>m_SAC_audio_clip</item>
    /// </list>
    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.m_sound m_SAC_sound;
        public AudioClip m_SAC_audio_clip;
    }
}
