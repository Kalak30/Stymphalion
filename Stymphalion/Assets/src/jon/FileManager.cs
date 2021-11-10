/*
 * Filename: FileManager.cs
 * Developer:
 * Purpose: Provide a way to save persist way to save game data.
 * File was found on-line at the following github repo. I, nor any of my teammate, made this.
 * https://github.com/UnityTechnologies/UniteNow20-Persistent-Data/blob/main/FileManager.cs
 *
 * Jon Kopf 10/08/2021
 *
 * I added a function to save a string to a text file in the Resources folder.
 * Jon Kopf 11/09/2021
 */

using System;
using System.IO;
using UnityEngine;

/// <summary>
/// Provides functionality to save persistent game data
/// </summary>
public static class FileManager
{
    /// <summary>
    /// Loads data from a file in the persistentDataPath into a string. Defaults to a location in AppData/Local
    /// </summary>
    /// <param name="a_FileName">Name of file, including extension, in the persistentDataPath</param>
    /// <param name="result">The resulting string. LoadFromFile changes the value </param>
    /// <returns>
    /// <list type="bullet">
    ///     <item><see langword="true"/> if load is successful </item>
    ///     <item><see langword="false"/> if load is not successful</item>
    /// </list>
    /// </returns>
    public static bool LoadFromFile(string a_FileName, out string result)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);

        try
        {
            result = File.ReadAllText(fullPath);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read from {fullPath} with exception {e}");
            result = "";
            return false;
        }
    }

    /// <summary>
    /// Writes a string to a file at the persistentDataPath. Defaults to a location in AppData/Local
    /// </summary>
    /// <param name="a_FileName">Name of file, including extension, in the persistentDataPath</param>
    /// <param name="a_FileContents">Contents to be written</param>
    /// <returns>
    /// <list type="bullet">
    ///     <item><see langword="true"/> if write is successful </item>
    ///     <item><see langword="false"/> if write is not successful</item>
    /// </list>
    /// </returns>
    public static bool WriteToFile(string a_FileName, string a_FileContents)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);

        try
        {
            File.WriteAllText(fullPath, a_FileContents);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to write to {fullPath} with exception {e}");
            return false;
        }
    }
}