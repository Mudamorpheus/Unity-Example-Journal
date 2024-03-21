using UnityEngine;

using System.Collections.Generic;

using Scripts.Data.Player;
using Scripts.UI.Items;
using System.Threading;

namespace Scripts.UI.Factories
{
    public class ContactsMainFactory : ContactsFactory
    {
        //==========================
        //=====CONSTRUCTOR
        //==========================

        public ContactsMainFactory(GameObject factoryPrefab, GameObject factoryParent) : base(factoryPrefab, factoryParent)
        {
        }

        //==========================
        //=====FACTORY
        //==========================

        private ContactsFactory favouriteFactory;

        public override GameObject Create(Contact data)
        {
            var product = base.Create(data);

            var item = product.GetComponent<ContactItem>();
            item.SetData(data);
            item.SetLinkedFactories(this, this, favouriteFactory);

            return product;
        }

        public void Init(int count)
        {
            var profile = PlayerProfile.GetInstance();
            for (int i = 0; i < count; i++)
            {
                var contact = profile.Contacts.DataList[i];
                Create(contact);
            }
        }

        public void LinkFactory(ContactsFactory favouriteFactory)
        {
            this.favouriteFactory = favouriteFactory;
        }
    }
}