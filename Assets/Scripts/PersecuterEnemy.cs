using UnityEngine;
using UnityEngine.Events;

public class PersecuterEnemy : IEnemyController
{
    private Transform _target;
    private Transform _transform;
    private float _speed;
    private float _rangeCollision;

    public event UnityAction Died;

    public PersecuterEnemy(Transform target, Transform transform, float speed, float rangeCollision)
    {
        _target = target;
        _transform = transform;
        _speed = speed;
        _rangeCollision = rangeCollision;
    }

    public void Update()
    {
        if(Vector2.Distance(_transform.position, _target.position) > _rangeCollision)
        {
            MoveToTarget();
        }
        else
        {
            Died?.Invoke();
        }
    }

    private void MoveToTarget()
    {
        Vector2 direction = (_target.position - _transform.position).normalized;

        _transform.position = (Vector2)_transform.position + _speed * direction * Time.deltaTime;
    }
}
