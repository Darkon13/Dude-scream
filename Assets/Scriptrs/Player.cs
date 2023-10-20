using UnityEngine;

public class Player : Actor
{
    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Collider = GetComponent<BoxCollider2D>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (Normal != null && VelocityDirection != null)
        {
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + Normal);
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + VelocityDirection.normalized);
        }
    }
}
