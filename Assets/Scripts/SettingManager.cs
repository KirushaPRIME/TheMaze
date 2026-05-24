using System;
using UnityEngine;
using UnityEngine.InputSystem;

public static class SettingManager
{
    static readonly string[] SettingsList = 
    {
        "MauseSensitivity",
    };

    public static void CheakConfigurationSettings()
    {
        if (!PlayerPrefs.HasKey("MauseSensitivity"))
            PlayerPrefs.SetFloat("MauseSensitivity", 0.5f);
    }

    public static void SetSetting<T>(string Key, T value) where T : IConvertible
    {
        if (PlayerPrefs.HasKey(Key))
        {
            if (typeof(T) == typeof(int))
                PlayerPrefs.SetInt(Key, Convert.ToInt32(value));
            if (typeof(T) == typeof(float))
                PlayerPrefs.SetFloat(Key, Convert.ToSingle(value));
            if ((typeof(T) == typeof(string)))
                PlayerPrefs.SetString(Key, Convert.ToString(value));
        }
    }

    public static string GetStrSetting(string Key)
    {
        if (PlayerPrefs.HasKey(Key))
            return PlayerPrefs.GetString(Key);
        else
            throw new IndexOutOfRangeException();
    }

    public static int GetIntSetting(string Key)
    {
        if (PlayerPrefs.HasKey(Key))
            return PlayerPrefs.GetInt(Key);
        else
            throw new IndexOutOfRangeException();
    }

    public static float GetFloatSetting(string Key)
    {
        if (PlayerPrefs.HasKey(Key))
            return PlayerPrefs.GetFloat(Key);
        else
            throw new IndexOutOfRangeException();
    }
}
