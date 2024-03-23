using UnityEngine;
using UnityEngine.UI;

using Scripts.UI.Factories;
using Scripts.Data.Player;
using System.Text;

using TMPro;

using Scripts.UI.Items;
using Scripts.UI.Misc;

namespace Scripts.Architucture.Cores
{
    public class JournalCore : MonoBehaviour
    {
        //==========================
        //=====SINGLETON
        //==========================

        private static JournalCore instance;
        public static JournalCore Instance { get { return instance; } }

        //==========================
        //=====INSPECTOR
        //==========================

        [Header("UI")]
        [SerializeField] private Canvas[] viewsArray;
        [SerializeField] private Canvas viewDefault;

        [Header("Contacts")]
        [SerializeField] private GameObject contactPrefab;
        [SerializeField] private GameObject contactsMainParent;
        [SerializeField] private GameObject contactsFavouriteParent;

        [Header("Profile")]
        [SerializeField] private Canvas profileView;
        [SerializeField] private Image profileImageAvatar;
        [SerializeField] private Image profileImageFavourite;
        [SerializeField] private TMP_Text profileTextName;
        [SerializeField] private TMP_Text profileTextGender;
        [SerializeField] private TMP_Text profileTextEmail;
        [SerializeField] private TMP_Text profileTextIP;

        [Header("Params")]
        [SerializeField] private int contactFactoryLimit = 10;
        [SerializeField] private int contactFactoryStep = 5;

        //==========================
        //=====SERVICES
        //==========================

        private ViewsSwitcherJournal viewsSwitcher;
        private ContactsMainFactory contactsMainFactory;
        private ContactsFavouriteFactory contactsFavouriteFactory;

        //==========================
        //=====MONOBEHAVIOUR
        //==========================

        private void Awake()
        {
            //Singleton
            if(instance != null)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }

            //Player
            var profile = PlayerProfile.GetInstance();

            //ViewsSwitcher
            viewsSwitcher = new ViewsSwitcherJournal(viewsArray);
            viewsSwitcher.InitializeProfile(profileView, profileImageAvatar, profileImageFavourite, profileTextName, profileTextGender, profileTextEmail, profileTextIP);
            viewsSwitcher.Switch(viewDefault);

            //Factories
            contactsMainFactory = new ContactsMainFactory(contactPrefab, contactsMainParent);
            contactsFavouriteFactory = new ContactsFavouriteFactory(contactPrefab, contactsFavouriteParent);

            contactsMainFactory.Link(contactsFavouriteFactory);
            contactsFavouriteFactory.Link(contactsMainFactory);

            contactsMainFactory.Fill(contactFactoryLimit);
            contactsFavouriteFactory.Fill(contactFactoryLimit);
        }

        private void OnApplicationQuit()
        {
            var profile = PlayerProfile.GetInstance();            
            profile.SaveData();
        }

        //==========================
        //=====API
        //==========================

        public void SwitchView(Canvas view)
        {
            viewsSwitcher.Switch(view);
        }

        public void SwitchBack()
        {
            viewsSwitcher.Back();
        }

        public void SwitchProfile(ContactItem item)
        {
            viewsSwitcher.SwitchProfile(item);
        }

        public void SwitchProfileFavourite()
        {
            viewsSwitcher.SwitchProfileFavourite();
        }

        public void NextMainFactory()
        {
            contactsMainFactory.Next();
        }
        public void NextFavouriteFactory()
        {
            contactsFavouriteFactory.Next();
        }
    }
}

