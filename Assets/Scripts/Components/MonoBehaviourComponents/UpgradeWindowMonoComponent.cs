using UnityEngine;
using UnityEngine.UI;

namespace Components.MonoBehaviourComponents
{
    public class UpgradeWindowMonoComponent : MonoBehaviour
    {
        public Button CloseButton;
        public UpgradeSlotUIMonoComponent UpgradeSlotPrefab;
        public Transform ContentParent;
    }
}