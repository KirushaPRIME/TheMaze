using UnityEngine;

namespace gameProcess
{
    public class LigherBehaviour : MonoBehaviour
    {
        [SerializeField] private Vector3 DifferencePosition;

        void Start()
        {
            GetComponent<FollowTheObject>().TriggerGO = GameObject.FindGameObjectWithTag("MainCamera");
            GetComponent<FollowTheObject>().ConstDifference = DifferencePosition;
        }
    }
}