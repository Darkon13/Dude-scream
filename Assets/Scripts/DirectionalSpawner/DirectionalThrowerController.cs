using System.Collections.Generic;
using UnityEngine;

public class DirectionalThrowerController : MonoBehaviour
{
    [SerializeField] private Timer _timer;

    private List<DirectionalThrower> _throwers;
    private bool _inited;

    private void OnEnable()
    {
        if(_inited == true)
        {
            _timer.TimerEnded += Throw;
        }
    }

    private void OnDisable()
    {
        if (_inited == true)
        {
            _timer.TimerEnded -= Throw;
        }
    }

    public void Init(List<DirectionalThrower> throwers)
    {
        if(_inited == false)
        {
            _throwers = throwers;

            _timer.TimerEnded += Throw;

            _inited = true;
        }
    }

    private void Throw()
    {
        if (_inited == true)
        {
            _throwers[Random.Range(0, _throwers.Count)].Spawn();
        }
    }
}
