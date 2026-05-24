using UnityEngine;


namespace gameProcess
{
    public class UseMause : MonoBehaviour
    {
        [SerializeField] private float speedX;
        [SerializeField] private float speedY;
        public static float multiplier = 0;

        [SerializeField] private float AngleX;
        [SerializeField] private float AngleY;

        [SerializeField] private float MaxAngle;

        enum OperatingMode { RotateX, RotateY, RotateXY };

        [SerializeField] private OperatingMode OM;

        private void Start()
        {

        }

        void FixedUpdate()
        {
            switch (OM)
            {
                case OperatingMode.RotateX:
                    AngleX += Input.GetAxis("Mouse X") * speedX * multiplier;
                    this.transform.localEulerAngles = new Vector3(this.transform.localRotation.y, AngleX, 0);

                    if (AngleX > 360)
                        AngleX = AngleX - 360;
                    else if (AngleX < -360)
                        AngleX = AngleX + 360;

                    break;
                case OperatingMode.RotateY:
                    AngleY -= Input.GetAxis("Mouse Y") * speedY * multiplier;
                    AngleY = Mathf.Abs(AngleY) > MaxAngle ?
                        (AngleY > 0) ? MaxAngle : -MaxAngle :
                        AngleY;
                    this.transform.localEulerAngles = new Vector3(AngleY, this.transform.localRotation.x, 0);
                    break;
                case OperatingMode.RotateXY:
                    AngleY -= Input.GetAxis("Mouse Y") * speedY * multiplier;
                    AngleY = Mathf.Abs(AngleY) > MaxAngle ?
                        (AngleY > 0) ? MaxAngle : -MaxAngle :
                        AngleY;
                    AngleX += Input.GetAxis("Mouse X") * speedX * multiplier;
                    this.transform.localEulerAngles = new Vector3(AngleY, AngleX, 0);

                    if (AngleX > 360)
                        AngleX = AngleX - 360;
                    else if (AngleX < -360)
                        AngleX = AngleX + 360;

                    break;
            }
        }
    }
}