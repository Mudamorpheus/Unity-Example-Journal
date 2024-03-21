using System;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.Networking;

namespace Scripts.Data.InputOutput
{
    public static class FileIO
    {
        //==========================
        //=====FILEIO
        //==========================

        private static string dataPath = Application.persistentDataPath;

        public static void SaveFile(string file, string extension, string data)
        {
            string path = string.Format("{0}/{1}.{2}", dataPath, file, extension);
            System.IO.File.WriteAllText(path, data);            
        }

        public static string LoadFile(string file, string extension)
        {
            string path = string.Format("{0}/{1}.{2}", dataPath, file, extension);
            return System.IO.File.ReadAllText(path);
        }

        public static void SaveImage(string file, string extension, Texture2D data)
        {
            string path = string.Format("{0}/{1}.{2}", dataPath, file, extension);
            System.IO.File.WriteAllBytes(path, data.EncodeToPNG());
        }

        public static Texture2D LoadImage(string file, string extension)
        {
            string path = string.Format("{0}/{1}.{2}", dataPath, file, extension);
            var bytes = System.IO.File.ReadAllBytes(path);
            var texture = new Texture2D(20, 200);
            texture.LoadImage(bytes);
            return texture;
        }

        //==========================
        //=====NETWORK
        //==========================

        public static async Task DownloadUrl(string url, string file)
        {
            using var request = UnityWebRequest.Get(url);
            var operation = request.SendWebRequest();

            while(!operation.isDone)
            {
                await Task.Yield();
            }

            if(request.result == UnityWebRequest.Result.Success)
            {
                //Debug
                Debug.Log($"FileManager: Succeed to download file.");

                //Save
                SaveFile(file, "json", request.downloadHandler.text);
            }
            else
            {
                //Debug
                Debug.Log($"FileManager: " + request.error + ".");
            }
        }

        public static async Task DownloadImages(string url, string file, int count)
        {
            for(int i = 0; i < count; i++)
            {
                using var request = UnityWebRequestTexture.GetTexture(url, false);
                var operation = request.SendWebRequest();

                while (!operation.isDone)
                {
                    await Task.Yield();
                }

                if (request.result == UnityWebRequest.Result.Success)
                {
                    //Debug
                    Debug.Log($"FileManager: Succeed to download image.");

                    //Image
                    Texture2D texture = DownloadHandlerTexture.GetContent(request);

                    //Save
                    string name = file + i.ToString();
                    SaveImage(name, "png", texture);
                }
                else
                {
                    //Debug
                    Debug.Log($"FileManager: " + request.error + ".");
                }
            }
        }
    }
}

