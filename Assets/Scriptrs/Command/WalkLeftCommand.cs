using UnityEngine;

public class WalkLeftCommand : ICommand
{
    public void Run(IMoveable moveable)
    {
        moveable.MoveLeft(); 
    }
}
