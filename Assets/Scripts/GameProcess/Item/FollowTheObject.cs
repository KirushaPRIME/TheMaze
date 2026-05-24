using UnityEngine;

namespace gameProcess
{
    public class FollowTheObject : MonoBehaviour
    {
        public GameObject TriggerGO;
        public Vector3 ConstDifference;

        private void Start()
        {

        }
        void Update()
        {
            if (TriggerGO != null)
            {
                GetComponent<Transform>().position = TriggerGO.GetComponent<Transform>().position + TriggerGO.GetComponent<Transform>().TransformVector(ConstDifference);
                GetComponent<Transform>().rotation = TriggerGO.transform.rotation;
            }
        }
    }
}