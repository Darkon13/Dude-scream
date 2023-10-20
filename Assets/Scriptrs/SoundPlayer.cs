using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AlarmTrigger _alarmTrigger;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private float _tickPerSecond = 1f;
    private float _maxVolume = 1f;
    private float _minVolume = 0;

    private IEnumerator _coroutine;
    private AudioSource _audioSource;

    private void OnEnable()
    {
        if (_alarmTrigger != null)
        {
            _alarmTrigger.OnEnter += StartSound;
            _alarmTrigger.OnExit += StopSound;
        }
    }

    private void OnDisable()
    {
        if (_alarmTrigger != null)
        {
            _alarmTrigger.OnEnter -= StartSound;
            _alarmTrigger.OnExit -= StopSound;
        }
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minVolume;
    }

    private IEnumerator ChangeVolume(float target)
    {
        float delta = (_maxVolume - _minVolume) / _duration / _tickPerSecond;
        float deltaTime = _duration / _tickPerSecond;
        WaitForSecondsRealtime waitForSeconds = new WaitForSecondsRealtime(deltaTime);

        if (target == _maxVolume)
        {
            _audioSource.Play();
        }
        else
        {
            delta *= -1;
        }

        while(_audioSource.volume != target)
        {
            _audioSource.volume = Mathf.Clamp(_audioSource.volume + delta, _minVolume, _maxVolume);

            yield return waitForSeconds;
        }

        if(_audioSource.volume == _minVolume)
        {
            _audioSource.Stop();
        }
    }

    private void StartSound()
    {
        Debug.Log("Start");
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = ChangeVolume(_maxVolume);
        StartCoroutine(_coroutine);
    }

    private void StopSound()
    {
        Debug.Log("Stop");
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = ChangeVolume(_minVolume);
        StartCoroutine(_coroutine);
    }
}
