using Unity.Collections;
using UnityEditor;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PhisicsBodyBehaviour : MonoBehaviour
{
    public Vector3 Impuls;
    public Vector3 Force;
    public Vector3 Velocity;
    public Vector3 TransmittedSpeed;
    public float Mass = 1;

    private bool WasTransport;
    private Vector3 LastPosition;
    private Vector3 NewPosition;
    Vector3 Movement;

    public delegate void IsFallDelegate(Vector3 Velocity);
    public event IsFallDelegate IsFall;

    public bool IsGrounded {  get; private set; }

    [SerializeField] LayerMask groundLayer;
    private float distanceToGround = 0.1f;
    private Collider _col;

    static private float GravityForce = -9.8f;

    private CharacterController CC;

    void Start()
    {
        CC = GetComponent<CharacterController>();
        Force.y = GravityForce;
        _col = GetComponent<Collider>();
    }

    void FixedUpdate()
    {
        if (WasTransport)
        {
            WasTransport = false;
            return;
        }
        Velocity = (LastPosition - NewPosition) / Time.fixedDeltaTime;
        Impuls = Velocity * Mass;
        LastPosition = NewPosition;
        TransmittedSpeed += Force / Mass * Time.fixedDeltaTime / 2;
        CC.Move(
            Movement +
            TransmittedSpeed * Time.fixedDeltaTime
            );
        //Debug.Log(IsGrounded);
        if (f_IsGrounded() && TransmittedSpeed.y < 0)
        {
            Force = Vector3.zero;
        }
        else
        {
            Force = new Vector3(0,GravityForce,0);
        }
        NewPosition = transform.position;
    }

    public void Move(Vector3 Movement)
    {
        this.Movement = Movement;
    }

    public void AddImpuls(Vector3 Impuls)
    {
        TransmittedSpeed += Impuls / Mass;
    }

    private bool f_IsGrounded()
    {
        IsGrounded = Physics.CheckCapsule(
            _col.bounds.center,
            new UnityEngine.Vector3(
                _col.bounds.center.x,
                _col.bounds.min.y,
                _col.bounds.center.z
                ),
            distanceToGround,
            groundLayer,
            QueryTriggerInteraction.Ignore
            );
        if (IsGrounded && IsFall != null)
            IsFall(Velocity);
        return IsGrounded;
    }
    public void TransformObject(Vector3 position)
    {
        WasTransport = true;
        GetComponent<Transform>().position = position;
    }

}
