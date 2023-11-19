using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _seconds;

    private Coroutine _coroutine;

    public event UnityAction TimerEnded;

    private void OnEnable()
    {
        TimerEnded += OnTimerEnded;

        _coroutine = StartCoroutine(nameof(StartTimer));
    }

    private void OnDisable()
    {
        if(_coroutine != null)
            StopCoroutine(_coroutine);

        TimerEnded -= OnTimerEnded;
    }

    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(_seconds);

        TimerEnded?.Invoke();
    }

    private void OnTimerEnded()
    {
        _coroutine = StartCoroutine(nameof(StartTimer));
    } 
}
