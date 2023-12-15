using Sirenix.OdinInspector.Editor.GettingStarted;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Components.MonoBehaviourComponents
{
    public class ComicsMonoComponent : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Button button;

        public void SetSprite(Sprite sprite)
        {
            image.sprite = sprite;
        }
        
        public event UnityAction OnClick
        {
            add => button.onClick.AddListener(value);
            remove => button.onClick.RemoveListener(value);
        }


    }
}