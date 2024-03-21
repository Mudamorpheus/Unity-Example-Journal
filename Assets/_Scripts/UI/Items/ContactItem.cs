using System.Text;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Scripts.Data.Player;
using Scripts.UI.Factories;
using Scripts.Architucture.Cores;

namespace Scripts.UI.Items
{
    public class ContactItem : MonoBehaviour
    {
        //==========================
        //=====INSPECTOR
        //==========================

        [Header("UI")]
        [SerializeField] private Image uiImageBorder;
        [SerializeField] private Color uiColorBorder;

        [Header("Avatar")]
        [SerializeField] private Image uiImageAvatar;

        [Header("Info")]
        [SerializeField] private TMP_Text uiTextName;
        [SerializeField] private TMP_Text uiTextEmail;
        [SerializeField] private TMP_Text uiTextIP;

        [Header("Buttons")]
        [SerializeField] private Image uiImageFavourite;
        [SerializeField] private Image uiImageOpen;

        [SerializeField] private Button uiButtonFavourite;                
        [SerializeField] private Button uiButtonOpen;
        public Button ButtonFavourite { get { return uiButtonFavourite; } }
        public Button ButtonOpen { get { return uiButtonOpen; } }

        //==========================
        //=====MONOBEHAVIOUR
        //==========================

        private void OnDestroy()
        {
            uiButtonFavourite.onClick.RemoveAllListeners();
            uiButtonOpen.onClick.RemoveAllListeners();
        }

        //==========================
        //=====ITEM
        //==========================

        private Contact contactData;
        public Contact Data { get { return contactData; } }

        public void SetData(Contact data)
        {
            //Data
            this.contactData = data;

            //Params
            ResetBorderColor();
            ResetAvatar();
            ResetInfo();
            ResetFavouriteSprite();
        }

        //==========================
        //=====FAVOURITE FACTORY
        //==========================

        private ContactsFactory parentFactory;
        private ContactsFactory mainFactory;
        private ContactsFactory favouriteFactory;

        public void SetLinkedFactories(ContactsFactory parentFactory, ContactsFactory mainFactory, ContactsFactory favouriteFactory)
        {
            this.parentFactory = parentFactory;
            this.mainFactory = mainFactory;
            this.favouriteFactory = favouriteFactory;
        }

        public void SwitchFavourite()
        {
            //Change state
            contactData.favourite = !contactData.favourite;
            ResetFavouriteSprite();

            //Add copy to factory
            if (contactData.favourite)
            {
                favouriteFactory.Create(contactData);
            }
            //Remove copy from factory
            else
            {
                if(parentFactory == favouriteFactory)
                {
                    foreach (Transform child in mainFactory.FactoryParent.transform)
                    {
                        var item = child.GetComponent<ContactItem>();
                        if (item.Data.id == contactData.id)
                        {
                            item.Data.favourite = false;
                            item.ResetFavouriteSprite();
                        }
                    }
                    Destroy(gameObject);
                }
                if(parentFactory == mainFactory)
                {
                    foreach (Transform child in favouriteFactory.FactoryParent.transform)
                    {
                        var item = child.GetComponent<ContactItem>();
                        if (item.Data.id == contactData.id)
                        {
                            Destroy(item.gameObject);
                        }
                    }
                }                
            }

            //Save changes
            var profile = PlayerProfile.GetInstance();
            profile.Contacts.SaveData();
        }

        public void SwitchProfile()
        {
            JournalCore.Instance.SwitchProfile(contactData);
        }

        //==========================
        //=====UI
        //==========================

        private void ResetBorderColor()
        {
            if (contactData.id % 2 == 1)
            {
                uiImageBorder.color = uiColorBorder;
            }
        }

        private void ResetAvatar()
        {
            var profile = PlayerProfile.GetInstance();
            int max = profile.Pictures.DataList.Count;
            Texture2D avatarTexture = profile.Pictures.DataList[contactData.id % max];
            if (avatarTexture != null)
            {
                var avatarSprite = Sprite.Create(avatarTexture, new Rect(0, 0, avatarTexture.width, avatarTexture.height), Vector2.zero);
                uiImageAvatar.sprite = avatarSprite;
            }
        }

        private void ResetInfo()
        {
            var sb = new StringBuilder();
            sb.Append(contactData.first_name);
            sb.Append(" ");
            sb.Append(contactData.last_name);
            uiTextName.text = contactData.first_name + " " + contactData.last_name;
            uiTextEmail.text = contactData.email;
            uiTextIP.text = contactData.ip_address;
        }

        private void ResetFavouriteSprite()
        {
            uiImageFavourite.sprite = contactData.favourite ? JournalCore.Instance.SpriteFavouriteOn : JournalCore.Instance.SpriteFavouriteOff;
        }
    }
}


