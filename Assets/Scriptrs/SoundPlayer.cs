using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AlarmTrigger _alarmTrigger;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private float _tickPerSecond = 1f;
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _minVolume = 0;
    IEnumerator _coroutine;
    private float _delta;
    AudioSource _audioSource;

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
        _delta = (_maxVolume - _minVolume) / _duration / _tickPerSecond;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = _minVolume;
    }

    private IEnumerator ChangeVolume(float delta)
    {
        //Debug.Log("111");
        float remainingTime = _duration - (_audioSource.volume - _minVolume) / (_maxVolume - _minVolume) / _delta / _tickPerSecond;
        float deltaTime = _duration / _tickPerSecond;
        WaitForSecondsRealtime waitForSeconds = new WaitForSecondsRealtime(deltaTime);
        Debug.Log(remainingTime);
        Debug.Log(deltaTime);

        if (delta > 0)
        {
            _audioSource.Play();
        }

        while(remainingTime > 0)
        {
            remainingTime -= deltaTime;
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

        _coroutine = ChangeVolume(_delta);
        StartCoroutine(_coroutine);
    }

    private void StopSound()
    {
        Debug.Log("Stop");
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = ChangeVolume(-_delta);
        StartCoroutine(_coroutine);
    }
}
