using UnityEngine;

namespace Components.MonoBehaviourComponents
{
    public class MakeRotationIdentity : MonoBehaviour
    {
        
        private void Update()
        {
            transform.rotation = Quaternion.identity;
        }
    }
}