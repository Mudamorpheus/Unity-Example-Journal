using System;
using System.Collections.Generic;
using System.Text;

using UnityEngine;

using Scripts.Architucture.ScriptableObjects;
using Scripts.Data.InputOutput;

namespace Scripts.Data.Player
{
    //==========================
    //=====JSON
    //==========================

    public class PicturesData : PlayerData<Texture2D>
    {
        //==========================
        //=====CONSTRUCTOR
        //==========================

        public PicturesData(string file, string pregen, int count) : base(file, pregen) 
        {
            picturesCount = count;
        }

        //==========================
        //=====API
        //==========================

        private int picturesCount;

        public override void SaveData()
        {
            for(int i = 0; i < dataList.data.Count; i++)
            {
                var data = base.dataList.data[i];
                string name = StaticKeys.Instance.PicturesFile + i.ToString();
                FileIO.SaveImage(name, "png", data);
            }
        }

        public override void LoadData()
        {
            dataList.data = new List<Texture2D>();
            for (int i = 0; i < picturesCount; i++)
            {
                string name = StaticKeys.Instance.PicturesPregen + i.ToString();
                try
                {
                    Texture2D texture = FileIO.LoadImage(name, "png");
                    dataList.data.Add(texture);
                }
                catch(Exception e)
                {
                    var sb = new StringBuilder();
                    sb.Append(GetType().Name);
                    sb.Append(": ");
                    sb.Append(e.Message);
                    sb.Append(".");
                    Debug.Log(sb);

                    break;
                }
            }
        }

        public Texture2D GetRandomPicture()
        {
            if(dataList.data.Count > 0)
            {
                int rnd = UnityEngine.Random.Range(0, dataList.data.Count - 1);
                return dataList.data[rnd];
            }            
            else
            {
                return null;
            }
        }
    }
}
