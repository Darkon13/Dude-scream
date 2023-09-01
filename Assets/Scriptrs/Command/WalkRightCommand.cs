using UnityEngine;

public class WalkRightCommand : ICommand
{
    public void Run(IMoveable moveable)
    {
        moveable.MoveRight();
    }
}
