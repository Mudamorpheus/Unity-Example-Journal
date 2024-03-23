using System;
using System.Collections.Generic;

using UnityEngine;

using Scripts.Data.Player;
using Scripts.Architucture.Cores;
using Scripts.UI.Items;
using System.Linq;

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

        private ContactsFactory factoryMain;

        public override GameObject Create(Contact data)
        {
            var product = base.Create(data);

            var item = product.GetComponent<ContactItem>();
            item.SetData(data);
            item.SetLinkedFactories(this, factoryMain, this);

            return product;
        }

        public override void Fill(int count)
        {
            var profile = PlayerProfile.GetInstance();
            int size = profile.Contacts.DataList.Count;
            for (int i = 0; i < size; i++)
            {
                var contact = profile.Contacts.DataList[i];
                if(contact.favourite)
                {
                    //Create
                    Create(contact);
                    factoryLastId = i;

                    //Limit
                    if (factoryList.Count >= count)
                    {
                        break;
                    }
                }                
            }            
        }

        public override void Next()
        {
            var profile = PlayerProfile.GetInstance();
            int size = profile.Contacts.DataList.Count;

            //End of scroll
            for(int i = factoryLastId+1; i < size; i++)
            {                
                if (factoryLastId < size)
                {
                    var contact = profile.Contacts.DataList[i];
                    if (contact.favourite)
                    {
                        Create(contact);
                        factoryLastId = i;
                        break;
                    }
                }
            }

        }

        public void Link(ContactsFactory mainFactory)
        {
            factoryMain = mainFactory;
        }
    }
}