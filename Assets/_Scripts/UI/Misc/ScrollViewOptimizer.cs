using Scripts.UI.Items;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Scripts.UI.Misc
{
    public class ScrollViewOptimizer : MonoBehaviour
    {
        //Components
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private RectTransform scrollViewport;
        [SerializeField] private RectTransform scrollContent;

        //Events
        [SerializeField] private UnityEvent onReachEnd;

        public void Optimize()
        {
            Rect rect = scrollViewport.rect;
            float leftSide = scrollViewport.position.x - rect.width*1.5f;
            float rightSide = scrollViewport.position.x + rect.width*1.5f;
            float topSide = scrollViewport.position.y + rect.height*1.5f;
            float bottomSide = scrollViewport.position.y - rect.height*1.5f;            

            //Hide
            foreach (Transform child in scrollContent)
            {
                Vector2 point = child.position;
                ContactItem item = child.GetComponent<ContactItem>();

                if (point.x >= leftSide && point.x <= rightSide && point.y >= bottomSide && point.y <= topSide)
                {
                    item.Show();
                }
                else
                {
                    item.Hide();
                }
            }            

            //Create
            if (scrollRect.verticalNormalizedPosition < 0.1)
            {                
                onReachEnd?.Invoke();                
            }
        }
    }
}

