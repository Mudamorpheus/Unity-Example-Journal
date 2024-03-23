using UnityEngine;
using UnityEngine.SceneManagement;

using Scripts.Architucture.ScriptableObjects;
using Scripts.Data.InputOutput;
using Scripts.Data.Player;

namespace Scripts.Architucture.Cores
{
    public class BootstrapperCore : MonoBehaviour
    {
        //==========================
        //=====INSPECTOR
        //==========================

        [SerializeField][TextArea(1, 1)] private string nextScene;
        [SerializeField][TextArea(4, 4)] private string presetContactsJsonUrl;
        [SerializeField][TextArea(4, 4)] private string presetPicturesUrl;
        [SerializeField] private int presetPicturesCount;

        //==========================
        //=====MONOBEHAVIOUR
        //==========================

        private async void Awake()
        {
            //Player profile
            PlayerProfile profile = PlayerProfile.GetInstance();
            profile.Initialize("Username", StaticKeys.Instance.Settings, StaticKeys.Instance.ContactsFile, StaticKeys.Instance.ContactsPregen, StaticKeys.Instance.PicturesFile, StaticKeys.Instance.PicturesPregen, presetPicturesCount);

            //First Launch
            if (profile.Settings.IsFirstLaunch())
            {
                await FileIO.DownloadUrl(presetContactsJsonUrl, StaticKeys.Instance.ContactsPregen);
                await FileIO.DownloadImages(presetPicturesUrl, StaticKeys.Instance.PicturesPregen, presetPicturesCount);
            }               
                             
            //Load or pregen data
            profile.LoadData();

            //First launch  
            if (profile.Settings.IsFirstLaunch())
            {
                profile.Settings.SetFirstLaunch(false);
                profile.SaveData();                
            }

            //Launch
            Bootstrap();
        }

        //==========================
        //=====BOOTSTRAPPER
        //==========================

        private void Bootstrap()
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}

