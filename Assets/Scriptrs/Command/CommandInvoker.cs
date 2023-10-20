using UnityEngine;

public abstract class CommandInvoker : MonoBehaviour
{
    protected IMoveable Moveable;
    protected KeyBinder Binder;

    protected void Process()
    {
        foreach(KeyCode key in Binder.Keys)
        {
            if (Input.GetKey(key))
            {
                Invoke(key);
            }
        }
    }

    private void Invoke(KeyCode key)
    {
        ICommand command = Binder.GetCommand(key);

        if (command != null)
        {
            command.Run(Moveable);
        }
    }
}
