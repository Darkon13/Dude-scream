using UnityEngine;
using UnityEngine.Events;

public class AlarmTrigger : MonoBehaviour
{
    [HideInInspector] public event UnityAction OnEnter;
    [HideInInspector] public event UnityAction OnExit;

    [SerializeField] private float _duration = 5f;
    private float _delta = 0;
    private int _deltaFactor = -1;
    private AudioSource _source;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter?.Invoke();
        //_deltaFactor = 1;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnExit?.Invoke();
        //_deltaFactor = -1;
    }

    private void Update()
    {
        _delta = Mathf.Max(Mathf.Min(_delta + Time.deltaTime / _duration * _deltaFactor, 1), 0);
        _source.volume = _delta;
    }
}
