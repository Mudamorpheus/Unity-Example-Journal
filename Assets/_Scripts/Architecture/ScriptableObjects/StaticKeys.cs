using UnityEngine;

using Scripts.Patterns;

namespace Scripts.Architucture.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Static", menuName = "ScriptableObjects/StaticKeys", order = 1)]
    public class StaticKeys : SingletonScriptableObject<StaticKeys>
    {
        //==========================
        //=====SCRIPTABLEOBJECT
        //==========================

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void FirstInitialize()
        {

        }

        //==========================
        //=====KEYS
        //==========================

        public string Settings = "Settings";
        public string ContactsFile = "ContactsFile";
        public string ContactsPregen = "ContactsPregen";
        public string PicturesFile = "PicturesFile";
        public string PicturesPregen = "PicturesPregen";
    }
}
