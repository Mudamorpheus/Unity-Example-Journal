using Scripts.Data.InputOutput;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Data.Player
{
    //==========================
    //=====JSON
    //==========================

    [Serializable]
    public struct Settings
    {
        public bool first_launch;

        public Settings(bool first)
        {
            first_launch = first;
        }
    }

    public class SettingsData
    {
        //==========================
        //=====FIELDS
        //==========================

        private Settings data;
        private string dataKey;

        //==========================
        //=====CONSTRUCTOR
        //==========================

        public SettingsData(string key)
        {
            dataKey = key;
        }

        //==========================
        //=====API
        //==========================

        public void SavePrefs()
        {
            string json = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(dataKey, json);
        }

        public void LoadPrefs()
        {
            //Looking for prefs
            string json = PlayerPrefs.GetString(dataKey);

            //Values
            Settings values;
            if (json == string.Empty)
            {
                values = new Settings(true);
            }
            else
            {
                values = JsonUtility.FromJson<Settings>(json);
            }

            //Data
            data = values;
        }

        public void SetFirstLaunch(bool state)
        {
            data.first_launch = state;
        }
        public bool IsFirstLaunch()
        {
            return data.first_launch;
        }
    }
}
