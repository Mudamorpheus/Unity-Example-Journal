using System;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

using Scripts.Architucture.ScriptableObjects;
using Scripts.Data.Player;
using Scripts.UI.Items;
using Scripts.UI.Misc;

using TMPro;

namespace Scripts.UI.Misc
{
    public class ViewsSwitcherJournal : ViewsSwitcher
    {
        //==========================
        //=====CONSTRUCTOR
        //==========================

        public ViewsSwitcherJournal(params Canvas[] views) : base(views)
        {

        }

        //==========================
        //=====PROFILE
        //==========================

        private ContactItem selectedProfileItem;
        private Canvas profileView;
        private Image profileImageAvatar;
        private Image profileImageFavourite;
        private TMP_Text profileTextName;
        private TMP_Text profileTextGender;
        private TMP_Text profileTextEmail;
        private TMP_Text profileTextIP;

        public void InitializeProfile(Canvas view, Image avatar, Image favourite, TMP_Text name, TMP_Text gender, TMP_Text email, TMP_Text ip)
        {
            profileView = view;
            profileImageAvatar = avatar;
            profileImageFavourite = favourite;
            profileTextName = name;
            profileTextGender = gender;
            profileTextEmail = email;
            profileTextIP = ip;
        }

        public void SwitchProfile(ContactItem item)
        {
            //Item
            selectedProfileItem = item;

            //Player
            var profile = PlayerProfile.GetInstance();
            var data = item.Data;

            //Avatar
            int max = profile.Pictures.DataList.Count;
            Texture2D avatarTexture = profile.Pictures.DataList[data.id % max];
            if (avatarTexture != null)
            {
                var avatarSprite = Sprite.Create(avatarTexture, new Rect(0, 0, avatarTexture.width, avatarTexture.height), Vector2.zero);
                profileImageAvatar.sprite = avatarSprite;
            }

            //Favourite        
            profileImageFavourite.sprite = data.favourite ? StaticSprites.Instance.SpriteFavouriteOn : StaticSprites.Instance.SpriteFavouriteOff;

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
            Switch(profileView);
        }

        public void SwitchProfileFavourite()
        {
            selectedProfileItem.SwitchFavourite();
            profileImageFavourite.sprite = selectedProfileItem.Data.favourite ? StaticSprites.Instance.SpriteFavouriteOn : StaticSprites.Instance.SpriteFavouriteOff;
        }
    }
}