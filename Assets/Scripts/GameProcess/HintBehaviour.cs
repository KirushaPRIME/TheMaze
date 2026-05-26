using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintBehaviour : MonoBehaviour
{
    static TextMeshProUGUI Text;
    static List<string> Hints;

    void Start()
    {
        Text = GetComponent<TextMeshProUGUI>();
        Hints = new List<string>();
    }

    static public void AddHint(string hint)
    {
        Hints.Add(hint);
        Text.text = "";
        foreach (var H in Hints)
        {
            Text.text += " [" + H + "] ";
        }
    }

    static public void RemoveHint(string hint)
    {
        Hints.Remove(hint);
        Text.text = "";
        foreach (var H in Hints)
        {
            Text.text += " [" + H + "] ";
        }
    }

    static public void ResetHints()
    {
        Text.text = "";
    }
}
