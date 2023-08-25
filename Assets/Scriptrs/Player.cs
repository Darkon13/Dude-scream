using UnityEngine;

public class Player : Actor
{ 
    private Animator _animator;
    private PlayerCommandInvoker _invoker;

    private void Start()
    {
        //_invoker = new PlayerCommandInvoker(this);
        Speed = 5;
        Transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
    }
}
