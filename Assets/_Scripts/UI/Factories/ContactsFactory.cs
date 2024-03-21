using UnityEngine;

using Scripts.Data.Player;
using Scripts.UI.Items;

namespace Scripts.UI.Factories
{
    public class ContactsFactory : PrefabFactory
    {
        //==========================
        //=====CONSTRUCTOR
        //==========================
        public ContactsFactory(GameObject factoryPrefab, GameObject factoryParent) : base(factoryPrefab, factoryParent) { }

        //==========================
        //=====FACTORY
        //==========================

        public virtual GameObject Create(Contact data)
        {
            var product = base.Create();
            var item = product.GetComponent<ContactItem>();
            item.SetData(data);

            return product;
        }
    }
}

