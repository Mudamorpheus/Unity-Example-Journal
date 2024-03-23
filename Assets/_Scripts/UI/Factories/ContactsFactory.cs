using UnityEngine;

using Scripts.Data.Player;
using Scripts.UI.Items;
using Scripts.Patterns;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using UnityEngine.Profiling;

namespace Scripts.UI.Factories
{
    public class ContactsFactory : PrefabFactory
    {
        //==========================
        //=====CONSTRUCTOR
        //==========================
        public ContactsFactory(GameObject factoryPrefab, GameObject factoryParent) : base(factoryPrefab, factoryParent) 
        {
        }

        //==========================
        //=====FACTORY
        //==========================

        protected List<GameObject> factoryList = new List<GameObject>();
        protected int factoryLastId;        

        public virtual GameObject Create(Contact data)
        {
            var product = base.Create();
            var item = product.GetComponent<ContactItem>();
            item.SetData(data);
            factoryList.Add(product);

            return product;
        }
        public virtual void Remove(GameObject product)
        {
            factoryList.Remove(product);
            Object.Destroy(product);
        }

        public virtual void Fill(int count)
        {
            var profile = PlayerProfile.GetInstance();
            int size = profile.Contacts.DataList.Count;
            for (int i = 0; i < size; i++)
            {
                //Create
                var contact = profile.Contacts.DataList[i];
                Create(contact);

                //Counters                
                factoryLastId = i;

                //Limit
                if (factoryList.Count >= count)
                {
                    break;
                }
            }            
        }

        public virtual void Next()
        {
            var profile = PlayerProfile.GetInstance();
            int size = profile.Contacts.DataList.Count;

            //Create new         
            if (factoryLastId < size)
            {                
                var contact = profile.Contacts.DataList[factoryLastId+1];
                Create(contact);
                factoryLastId++;
            }

            //Remove old
            //Remove(factoryList.First());
        }
    }
}

