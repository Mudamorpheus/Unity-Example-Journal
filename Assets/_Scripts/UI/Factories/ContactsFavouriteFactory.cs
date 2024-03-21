using System;
using System.Collections.Generic;

using UnityEngine;

using Scripts.Data.Player;
using Scripts.Architucture.Cores;
using Scripts.UI.Items;

namespace Scripts.UI.Factories
{
    public class ContactsFavouriteFactory : ContactsFactory
    {
        //==========================
        //=====CONSTRUCTOR
        //==========================

        public ContactsFavouriteFactory(GameObject factoryPrefab, GameObject factoryParent) : base(factoryPrefab, factoryParent)
        {
        }

        //==========================
        //=====FACTORY
        //==========================

        private ContactsFactory mainFactory;

        public override GameObject Create(Contact data)
        {
            var product = base.Create(data);

            var item = product.GetComponent<ContactItem>();
            item.SetData(data);
            item.SetLinkedFactories(this, mainFactory, this);

            return product;
        }

        public void Init(int count)
        {
            var profile = PlayerProfile.GetInstance();
            for (int i = 0; i < count; i++)
            {
                var contact = profile.Contacts.DataList[i];
                if(contact.favourite)
                {
                    Create(contact);
                }                
            }
        }

        public void LinkFactory(ContactsFactory mainFactory)
        {
            this.mainFactory = mainFactory;
        }
    }
}