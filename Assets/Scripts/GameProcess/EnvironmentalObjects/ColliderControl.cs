using UnityEngine;

public class ColliderControl : MonoBehaviour
{
    [SerializeField] private static Transform TriggerObject;
    [SerializeField] private float triggerDistance;

    private delegate void DoInUpdate();
    private event DoInUpdate DoInUpdateEvent;

    bool ColliderActive = false;
    private void Start()
    {
        
        if (GetComponent<Collider>() == null)
        {
            GetComponent<ColliderControl>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }
        
        DoInUpdateEvent += FindTriggerObject;
    }
    private void FixedUpdate()
    {
        if (DoInUpdateEvent != null)
            DoInUpdateEvent();
    }
    void SetActive()
    {
        if (ColliderActive && (TriggerObject.position - transform.position).magnitude > triggerDistance)
        {
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            ColliderActive = false;
        }
        else if (!ColliderActive && (TriggerObject.position - transform.position).magnitude < triggerDistance)
        {
            GetComponent<Collider>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
            ColliderActive = true;
        }
    }
    void FindTriggerObject()
    {
        if (TriggerObject == null)
        {
            if (GameObject.Find("Player") != null)
            {
                TriggerObject = GameObject.Find("Player").GetComponent<Transform>();
                DoInUpdateEvent -= FindTriggerObject;
                DoInUpdateEvent += SetActive;
            }
        }
        else
        {
            DoInUpdateEvent -= FindTriggerObject;
            DoInUpdateEvent += SetActive;
        }
    }
}
