/*
* Filename: SoundManager.cs
* Developer: Hunter Leppek
* Purpose: This file implements a sound manager for the game using a Singleton pattern.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A class that manages the sound effects in the game.
/// </summary>
/// Member Variables
/// <list type = "bullet">
/// <item>m_sound_timer_dictionary</item>
/// </list>
public static class SoundManager
{
    private static Dictionary<m_sound, float> m_sound_timer_dicitonary; //A dictionary to manage the timing of certain sounds

    public enum m_sound //An enum to list the possible sounds
    {
        bow,
        interact,
        pickup,
        player_hurt,
        player_step,
        sword_swing,
    }

    /// <summary>
    /// A function to initiailze the sound timer dictionary
    /// </summary>
    public static void Initialize() 
    {
        m_sound_timer_dicitonary = new Dictionary<m_sound, float>();
        m_sound_timer_dicitonary[m_sound.player_step] = 0f;
    }

    /// <summary>
    /// A function to play a sound. Can be invoked by: SoundManager.PlaySound(SoundManager.m_sound.SoundName), where SoundName is a sound in the m_sound enum.
    /// </summary>
    /// <param name="sound"></param>
    public static void PlaySound(m_sound sound) 
    {
        GameObject sound_game_object = new GameObject("Sound");
        AudioSource audio_source = sound_game_object.AddComponent<AudioSource>();
        audio_source.PlayOneShot(GetAudioClip(sound));
    }

    /// <summary>
    /// A function to check if a sound can be played. This is used in conjunction with the sound timer dicitonary to ensure that sounds are not overplayed.
    /// </summary>
    /// <param name="sound"></param>
    /// <returns> bool </returns>
    private static bool CanPlaySound(m_sound sound)
    {
        switch (sound)
        {
        default:
                return true;

        case m_sound.player_step:
            if (m_sound_timer_dicitonary.ContainsKey(sound))
            {
                float LastTimePlayed = m_sound_timer_dicitonary[sound];
                float PlayerMoveTimerMax = .1f;
                if (LastTimePlayed + PlayerMoveTimerMax < Time.time)
                {
                     m_sound_timer_dicitonary[sound] = Time.time;
                    return true;
                }
                 else
                {
                    return false;
                }
            }
            else
            {
                    return true;
                }
        }
    }

    /// <summary>
    /// A function to retrieve an audio clip from the audio clip array.
    /// </summary>
    /// <param name="sound"></param>
    /// <returns> AudioClip </returns>
    private static AudioClip GetAudioClip(m_sound sound)
    {
        foreach (SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.m_si.SoundAudioClipArray)
        {
            if (soundAudioClip.m_SAC_sound == sound)
            {
                return soundAudioClip.m_SAC_audio_clip;
            }
        }
        Debug.LogError("Sound" + sound + " could not be found!");
        return null;
    }
}


