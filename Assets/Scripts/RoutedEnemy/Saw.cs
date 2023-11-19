using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Saw : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _minDistance;

    private Transform _target;
    private Transform _transform;
    private SpriteRenderer _spriteRenderer;
    private Coroutine _coroutine;
    private bool _inited = false;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _inited = false;
    }

    public void Init(Transform target, Color color)
    {
        if(_inited == false)
        {
            _target = target;
            _spriteRenderer.color = color;
            _coroutine = StartCoroutine(nameof(MoveToTarget));

            _inited = true;
        }
    }

    private IEnumerator MoveToTarget()
    {
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

        while(Vector2.Distance(_target.position, _transform.position) > _minDistance)
        {
            Vector2 direction = (_target.position - _transform.position).normalized;
            Quaternion rotateTo = Quaternion.LookRotation(Vector3.forward, direction);
            float deltaDegree = 10;

            _transform.position = (Vector2)_transform.position + direction * _speed * Time.deltaTime;
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, rotateTo, deltaDegree);

            yield return waitForFixedUpdate;
        }

        gameObject.SetActive(false);
    }
}
