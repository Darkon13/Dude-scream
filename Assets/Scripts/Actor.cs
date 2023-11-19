using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class Actor : MonoBehaviour
{
    [SerializeField] protected float Speed = 5;

    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    protected void MoveLeft() => Move(Vector2.left);

    protected void MoveRight() => Move(Vector2.left);

    private void Move(Vector2 direction)
    {
        _rigidbody2D.velocity = _rigidbody2D.velocity + new Vector2(direction.x * Speed * Time.deltaTime, 0);
    }
}
