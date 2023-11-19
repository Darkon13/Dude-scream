using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class RoutedEnemy : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _tickPerSeconds;
    [SerializeField] private float _minDistance;

    private IReadOnlyNode _currentNode;
    private Transform _transform;
    private SpriteRenderer _spriteRenderer;
    private Coroutine _coroutine;
    private bool _inited = false;

    public Color Color { get; private set; }

    public event UnityAction NodeReached;

    private void OnDisable()
    {
        if(_inited == true)
        {
            NodeReached -= OnNodeReached;

            if(_coroutine != null)
                StopCoroutine(_coroutine);
        }
    }

    private void OnEnable()
    {
        if (_inited == true)
        {
            NodeReached += OnNodeReached;

            _coroutine = StartCoroutine(nameof(MoveTowards));
        }
    }

    public void Init(IReadOnlyNode node, Color color)
    {
        if(_inited == false)
        {
            _transform = GetComponent<Transform>();
            _spriteRenderer = GetComponent<SpriteRenderer>();

            _currentNode = node;
            Color = color;

            _spriteRenderer.color = Color;

            _coroutine = StartCoroutine(nameof(MoveTowards));
            NodeReached += OnNodeReached;

            _inited = true;
        }
    }

    private void OnNodeReached()
    {
        _currentNode = _currentNode.NextNode;

        _coroutine = StartCoroutine(nameof(MoveTowards));
    }

    private IEnumerator MoveTowards()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(_duration / _tickPerSeconds);
        Vector2 startPosition = _transform.position;
        Vector2 endPosition = new Vector2(_currentNode.NextNode.X, _currentNode.NextNode.Y);
        float delta = 1 / (_duration * _tickPerSeconds);
        float currentT = 0;

        while(Vector2.Distance(_transform.position, endPosition) > _minDistance)
        {
            currentT += delta;
            _transform.position = Vector2.Lerp(startPosition, endPosition, currentT);

            yield return waitForSeconds;
        }

        NodeReached?.Invoke();
    }
}
