using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DirectedEnemy : MonoBehaviour
{
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _speed;

    private bool _inited = false;
    private Vector2 _direction;
    private Vector2 _pointFrom;
    private Transform _transform;
    private Coroutine _coroutine;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void OnDisable()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _inited = false;
    }

    public void Init(Vector2 from, Vector2 direction)
    {
        if(_inited == false)
        {
            _pointFrom = from;
            _direction = direction;
            _transform.rotation = Quaternion.LookRotation(Vector3.forward, _direction);
            _coroutine = StartCoroutine(nameof(MoveToDirection));

            _inited = true;
        }
    }

    private IEnumerator MoveToDirection()
    {
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

        while(Vector2.Distance(_pointFrom, _transform.position) < _maxDistance)
        {
            _transform.position = (Vector2)_transform.position + _direction * _speed * Time.deltaTime;

            yield return waitForFixedUpdate;
        }

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
    }
}
