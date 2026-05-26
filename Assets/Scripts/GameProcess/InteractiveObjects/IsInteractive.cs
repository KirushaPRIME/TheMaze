using UnityEngine;

public class IsInteractive : MonoBehaviour
{
    public bool IsActive = true;
    public KeyCode keyForInt = KeyCode.E;
    public string HintMassage;
    public delegate void Interactive();
    public event Interactive OnInteractive;

    public void StartInteractive()
    {
        if (IsActive && OnInteractive != null)
            OnInteractive();
    }

    public void ResetEvent()
    {
        OnInteractive = null;
    }
}
