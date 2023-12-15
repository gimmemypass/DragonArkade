using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Components.MonoBehaviourComponents
{
    public class LevelBattleUIMonoComponent : MonoBehaviour
    {
        public TextMeshProUGUI LevelText;
        public WaveUIMonoComponent WavePrefab;
        public Transform WavesParent;
    }
}