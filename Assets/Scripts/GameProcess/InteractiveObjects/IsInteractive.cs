using UnityEngine;

[RequireComponent(typeof(Collider))]
public class IsInteractive : MonoBehaviour
{
    public bool IsActive = true;
    public KeyCode keyForInt = KeyCode.E;
    public string HintMassage;
    public delegate void Interactive(GameObject WhoInter);
    public event Interactive OnInteractive;

    public void StartInteractive(GameObject WhoInter)
    {
        if (IsActive && OnInteractive != null)
            OnInteractive(WhoInter);
    }

    public void ResetEvent()
    {
        OnInteractive = null;
    }
}
