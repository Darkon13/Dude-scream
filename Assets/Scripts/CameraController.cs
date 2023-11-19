using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private float _scalePower = 2f;

    private float _minSize = 4f;
    private float _maxSize = 10f;
    private Transform _playerTransform;
    private Camera _camera;
    private float _currentSize;
    private float _delta;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _currentSize = _camera.orthographicSize;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        _delta = (_currentSize - _minSize) / (_maxSize - _minSize);
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            _delta = Mathf.Max(Mathf.Min(_delta + Time.deltaTime * -Input.mouseScrollDelta.y * _scalePower, 1), 0);
            _currentSize = Mathf.Lerp(_minSize, _maxSize, _delta);
            _camera.orthographicSize = _currentSize;
        }

        gameObject.transform.position = new Vector3(_playerTransform.position.x, _playerTransform.position.y, gameObject.transform.position.z);
    }
}
