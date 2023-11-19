using UnityEngine;

public class DirectionalThrower : MonoBehaviour
{
    private int _directionCount = 4;
    private int _maxDegree = 360;
    private bool _inited = false;
    private Transform _transform;
    private ObjectPool _pool;

    public void Init(ObjectPool pool)
    {
        if(_inited == false)
        {
            _transform = GetComponent<Transform>();
            _pool = pool;

            _inited = true;
        }
    }

    public void Spawn()
    {
        if(_pool.TryGetObject(out Transform transform))
        {
            int directionIndex = Random.Range(0, _directionCount) + 1;
            Vector2 direction = new Vector2(Mathf.Cos((_maxDegree / _directionCount) * directionIndex * Mathf.Deg2Rad), 
                                            Mathf.Sin((_maxDegree / _directionCount) * directionIndex * Mathf.Deg2Rad));
            
            transform.position = _transform.position;

            if(transform.TryGetComponent(out DirectedEnemy enemy))
            {
                enemy.gameObject.SetActive(true);
                enemy.Init(_transform.position, direction);
            }
        }
    }
}
