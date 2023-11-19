using System.Collections;
using UnityEngine;

public class HealthBarSmooth : MonoBehaviour
{
    [SerializeField] private RectTransform _redBar;
    [SerializeField] private RectTransform _yellowBar;
    [SerializeField] private float _delay;
    [SerializeField] private float _duration;
    [SerializeField] private float _tickPerSecond;
    //[SerializeField, Range(0f, 1f)] private float delta;

    private bool _inited = false;
    private int _prevHealth;
    private Player _player;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        if (_inited == true)
        {
            _player.HealthChanged += OnHealthChanged;
        }
    }

    private void OnDisable()
    {
        if (_inited == true)
        {
            _player.HealthChanged -= OnHealthChanged;

            if(_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
        }
    }

    public void Init(Player player)
    {
        if (_inited == false)
        {
            _player = player;
            _prevHealth = _player.Health;

            SetLenght(_player.Health);

            _player.HealthChanged += OnHealthChanged;

            _inited = true;
        }
    }

    private void SetLenght(int health)
    {
        _redBar.localScale = new Vector3(1f / _player.MaxHealth * health, _redBar.localScale.y, _redBar.localScale.z);
        _yellowBar.localPosition = _redBar.localPosition + _redBar.rect.width * _redBar.localScale.x * Vector3.right;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        if (_redBar.localScale.x > 0)
        {
            if(health < _prevHealth)
            {
                _yellowBar.localScale = new Vector3(_yellowBar.localScale.x + 1f / _player.MaxHealth * (_prevHealth - health), 
                                                    _yellowBar.localScale.y, 
                                                    _yellowBar.localScale.z);
            }
            else
            {  
                _yellowBar.localScale = new Vector3(Mathf.Max(_yellowBar.localScale.x - 1f / _player.MaxHealth * (health - _prevHealth), 0), 
                                                    _yellowBar.localScale.y, 
                                                    _yellowBar.localScale.z);
            }

            if(_yellowBar.localScale.x > 0)
            {
                _coroutine = StartCoroutine(nameof(LenghtToZero));
            }
        }
        else
        {
            _yellowBar.localScale = new Vector3(0, _yellowBar.localScale.y, _yellowBar.localScale.z);
        }

        _prevHealth = health;
    }

    private IEnumerator LenghtToZero()
    {
        yield return new WaitForSeconds(_delay);

        WaitForSeconds waitForSeconds = new WaitForSeconds(_duration / _tickPerSecond);
        float delta = 1f / (_duration *_tickPerSecond);
        float currentT = 0;

        while (_yellowBar.localScale.x != 0)
        {
            currentT += delta;
            _yellowBar.localScale = new Vector3(Mathf.Lerp(_yellowBar.localScale.x, 0, currentT), _yellowBar.localScale.y, _yellowBar.localScale.z);

            yield return waitForSeconds;
        }
    }

    private void OnHealthChanged(int health) => SetLenght(health);
}
