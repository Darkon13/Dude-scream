using System.Collections.Generic;
using UnityEngine;

public class SawThrowerContoller : MonoBehaviour
{
    [SerializeField] private Timer _timer;

    private List<SawThrower> _sawThrowers;
    private bool _inited = false;

    private void OnEnable()
    {
        if(_inited == true)
        {
            _timer.TimerEnded += ThrowSaw;
        }
    }

    private void OnDisable()
    {
        if (_inited == true)
        {
            _timer.TimerEnded -= ThrowSaw;
        }
    }

    public void Init(List<SawThrower> sawThrowers)
    {
        if(_inited == false)
        {
            _sawThrowers = sawThrowers;

            _timer.TimerEnded += ThrowSaw;

            _inited = true;
        }
    }

    private void ThrowSaw()
    {
        if (_inited == true)
        {
            _sawThrowers[Random.Range(0, _sawThrowers.Count)].Throw();
        }
    }
}
