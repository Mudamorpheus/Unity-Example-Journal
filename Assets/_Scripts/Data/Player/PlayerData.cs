using System;
using System.Text;
using System.Collections.Generic;

using UnityEngine;

using Scripts.Data.InputOutput;

namespace Scripts.Data.Player
{
    public abstract class PlayerData<T> : IPlayerData
    {
        //==========================
        //=====JSON
        //==========================

        public struct Data
        {
            public List<T> data;

            public Data(List<T> list)
            {
                data = list;
            }
        }

        //==========================
        //=====FIELDS
        //==========================

        protected Data dataList;
        public List<T> DataList { get { return dataList.data; } }

        protected string dataFile;
        protected string dataPregen;

        //==========================
        //=====CONSTRUCTOR
        //==========================

        public PlayerData(string file, string pregen)
        {
            dataFile = file;
            dataPregen = pregen;
        }

        //==========================
        //=====API
        //==========================

        public virtual void SaveData()
        {
            string json = JsonUtility.ToJson(dataList);
            FileIO.SaveFile(dataFile, "json", json);
        }

        public virtual void LoadData()
        {
            //Looking for files
            string json = string.Empty;
            try
            {
                json = FileIO.LoadFile(dataFile, "json");                
            }
            catch
            {                
                try
                {
                    json = FileIO.LoadFile(dataPregen, "json");
                }
                catch(Exception e)
                {
                    var sb = new StringBuilder();
                    sb.Append(GetType().Name);
                    sb.Append(": ");
                    sb.Append(e.Message);
                    sb.Append(".");
                    Debug.Log(sb);
                }
            }

            //Data
            if(json != string.Empty)
            {
                dataList = JsonUtility.FromJson<Data>(json);
            }
            else
            {
                var list = new List<T>();
                dataList = new Data(list);                
            }            
        }
    }
}
