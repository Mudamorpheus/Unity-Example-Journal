using UnityEngine;

using Scripts.UI.Factories;
using Scripts.Data.Player;
using UnityEngine.UI;
using TMPro;
using System.Text;

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
        [SerializeField] private Image profileImageAvatar;
        [SerializeField] private Image profileImageFavourite;
        [SerializeField] private TMP_Text profileTextName;
        [SerializeField] private TMP_Text profileTextGender;
        [SerializeField] private TMP_Text profileTextEmail;
        [SerializeField] private TMP_Text profileTextIP;
        [SerializeField] private Sprite profileSpriteFavouriteOn;
        [SerializeField] private Sprite profileSpriteFavouriteOff;
        [SerializeField] private Canvas profileProfileView;
        public Sprite SpriteFavouriteOn { get { return profileSpriteFavouriteOn; } }
        public Sprite SpriteFavouriteOff { get { return profileSpriteFavouriteOff; } }

        //==========================
        //=====SERVICES
        //==========================

        private ViewsSwitcher viewsSwitcher;
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
            viewsSwitcher = new ViewsSwitcher(viewsArray);
            viewsSwitcher.Switch(viewDefault);

            //Factories
            int count = 20;

            contactsMainFactory = new ContactsMainFactory(contactPrefab, contactsMainParent);
            contactsFavouriteFactory = new ContactsFavouriteFactory(contactPrefab, contactsFavouriteParent);

            contactsMainFactory.LinkFactory(contactsFavouriteFactory);
            contactsFavouriteFactory.LinkFactory(contactsMainFactory);

            contactsMainFactory.Init(count);
            contactsFavouriteFactory.Init(count);
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

        public void SwitchProfile(Contact data)
        {
            //Player
            var profile = PlayerProfile.GetInstance();

            //Avatar
            int max = profile.Pictures.DataList.Count;
            Texture2D avatarTexture = profile.Pictures.DataList[data.id % max];
            if (avatarTexture != null)
            {
                var avatarSprite = Sprite.Create(avatarTexture, new Rect(0, 0, avatarTexture.width, avatarTexture.height), Vector2.zero);
                profileImageAvatar.sprite = avatarSprite;
            }

            //Favourite
            profileImageFavourite.sprite = data.favourite ? SpriteFavouriteOn : SpriteFavouriteOff;

            //Info
            var sb = new StringBuilder();
            sb.Append(data.first_name);
            sb.Append(" ");
            sb.Append(data.last_name);
            profileTextName.text = data.first_name + " " + data.last_name;
            profileTextGender.text = data.gender;
            profileTextEmail.text = data.email;
            profileTextIP.text = data.ip_address;

            //Switch
            SwitchView(profileProfileView);
        }
    }
}

