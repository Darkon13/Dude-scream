using UnityEngine;

public class PlayerCommandInvoker : CommandInvoker
{
    [SerializeField] Player _player;

    private void Start()
    {
        Moveable = _player;
        Binder = new KeyBinder();

        Binder.AddCommand(KeyCode.A, new WalkLeftCommand());
        Binder.AddCommand(KeyCode.D, new WalkRightCommand());
        Binder.AddCommand(KeyCode.Space, new JumpCommand());
    }

    private void Update()
    {
        Process();
    }
}
