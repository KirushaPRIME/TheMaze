using Unity.Burst.CompilerServices;
using UnityEngine;

public class InteractiveRay : MonoBehaviour
{
    private Camera camera;
    bool HintHasAdd = false;
    string Hint;


    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        Vector3 point = new Vector3(
                camera.pixelWidth / 2,
                camera.pixelHeight / 2,
                0);
        Ray ray = camera.ScreenPointToRay(point);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 2))
        {
            GameObject hitObject = hit.transform.gameObject;
            IsInteractive target = hitObject.GetComponent<IsInteractive>();
            if (target != null && target.IsActive)
            {
                if (!HintHasAdd)
                {
                    HintHasAdd = true;
                    Hint = target.HintMassage;
                    HintBehaviour.AddHint(Hint);
                }
                if (Input.GetKeyDown(target.keyForInt))
                {
                    target.StartInteractive();
                }
            }else if (HintHasAdd)
            {
                HintHasAdd = false;
                HintBehaviour.RemoveHint(Hint);
            }
        } else if (HintHasAdd)
        {
            HintHasAdd = false;
            HintBehaviour.RemoveHint(Hint);
        }
    }
}
