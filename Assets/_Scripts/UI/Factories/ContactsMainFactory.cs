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

        private ContactsFactory factoryFavourite;

        public override GameObject Create(Contact data)
        {
            var product = base.Create(data);

            var item = product.GetComponent<ContactItem>();
            item.SetData(data);
            item.SetLinkedFactories(this, this, factoryFavourite);

            return product;
        }

        public void Link(ContactsFactory favouriteFactory)
        {
            factoryFavourite = favouriteFactory;
        }
    }
}