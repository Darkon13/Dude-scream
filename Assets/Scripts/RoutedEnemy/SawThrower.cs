using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SawThrower : MonoBehaviour
{
    private Transform _transform;
    private Transform _target;
    private SpriteRenderer _spriteRenderer;
    private ObjectPool _pool;
    private Color _color;
    private bool _inited = false;

    public void Init(Transform target, Color color, ObjectPool pool)
    {
        if(_inited == false)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _transform = GetComponent<Transform>();

            _target = target;
            _color = color;
            _spriteRenderer.color = _color;
            _pool = pool;

            _inited = true;
        }
    }

    public void Throw()
    {
        if(_inited == true)
        {
            if(_pool.TryGetObject(out Transform transform))
            {
                transform.position = _transform.position;
                transform.gameObject.SetActive(true);
                
                if(transform.TryGetComponent(out Saw saw))
                {
                    saw.Init(_target, _color);
                }
            }
        }
    }
}
