using UnityEngine;

public abstract class Actor : MonoBehaviour, IMoveable
{
    protected float Speed;
    protected Transform Transform;

    public void MoveToDirection(Vector3 direction)
    {
        Transform.position += direction * Speed * Time.deltaTime;
        Transform.rotation = new Quaternion(0, 90 + -90 * direction.x, 0, 0);
    }
}
