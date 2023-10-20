using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public abstract class Actor : MonoBehaviour, IMoveable
{
    [SerializeField] protected float Speed;
    [SerializeField] protected float JumpForce;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float airAccelerationFactor;
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _hit = new RaycastHit2D[1];
    protected Rigidbody2D Rigidbody;
    protected Collider2D Collider;
    protected Vector2 Normal;
    protected Vector2 VelocityDirection;

    private void Start()
    {
        _contactFilter.layerMask = _layerMask;
    }

    public bool IsGrounded(ref RaycastHit2D[] hits, float rayLenght = 1f)
    {
        return Rigidbody.Cast(Vector2.down, _contactFilter, hits, rayLenght) > 0;
    }

    public bool IsGrounded() => IsGrounded(ref _hit);

    public void MoveLeft() => MoveToDirection(Vector2.left);

    public void MoveRight() => MoveToDirection(Vector2.right);

    public void Jump()
    {
        if (IsGrounded())
        {
            Rigidbody.AddForce(Vector2.up * JumpForce);
        }
    }

    private void MoveToDirection(Vector2 direction)
    {
        if(IsGrounded())
        {
            Normal = _hit[0].normal;
            float angleFactor = Mathf.Sin(Vector2.Angle(direction, Normal) * Mathf.Deg2Rad);
            VelocityDirection = -direction.x * angleFactor * Speed * Vector2.Perpendicular(Normal);

            Rigidbody.velocity = Vector2.ClampMagnitude(Rigidbody.velocity + VelocityDirection, _maxVelocity);
        }
        else
        {
            Rigidbody.velocity = Vector2.ClampMagnitude(Rigidbody.velocity + direction * airAccelerationFactor * Speed, _maxVelocity);
        }

        transform.rotation = new Quaternion(0, 90 + -90 * direction.x, 0, 0);
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    if (Normal != null && VelocityDirection != null)
    //    {
    //        Gizmos.DrawLine(Collider.transform.position, Normal);
    //        Gizmos.DrawLine(Collider.transform.position, VelocityDirection.normalized);
    //    }
    //}
}
