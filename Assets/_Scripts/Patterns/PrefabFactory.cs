using UnityEngine;

namespace Scripts.Patterns
{
    public abstract class PrefabFactory
    {
        //==========================
        //=====FIELDS
        //==========================

        protected GameObject factoryPrefab;
        protected GameObject factoryParent;
        public GameObject FactoryParent { get { return factoryParent; } }

        //==========================
        //=====CONSTRUCTOR
        //==========================

        public PrefabFactory(GameObject factoryPrefab, GameObject factoryParent)
        {
            this.factoryPrefab = factoryPrefab;
            this.factoryParent = factoryParent;
        }

        //==========================
        //=====FACTORY
        //==========================

        public virtual GameObject Create()
        {
            var product = GameObject.Instantiate(factoryPrefab, factoryParent.transform);

            return product;
        }
    }
}
