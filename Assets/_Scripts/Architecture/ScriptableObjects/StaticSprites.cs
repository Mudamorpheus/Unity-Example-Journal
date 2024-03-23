using UnityEngine;

using Scripts.Patterns;

namespace Scripts.Architucture.ScriptableObjects
{
    [CreateAssetMenu(menuName = "ScriptableObjects/StaticSprites", order = 1)]
    public class StaticSprites : SingletonScriptableObject<StaticSprites>
    {
        //==========================
        //=====SCRIPTABLEOBJECT
        //==========================

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void FirstInitialize()
        {

        }

        //==========================
        //=====SPRITES
        //==========================

        public Sprite SpriteFavouriteOn;
        public Sprite SpriteFavouriteOff;
    }
}
