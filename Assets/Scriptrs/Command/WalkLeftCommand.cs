using UnityEngine;

public class WalkLeftCommand : ICommand
{
    public void Run(IMoveable moveable)
    {
        moveable.MoveToDirection(new Vector3(-1, 0, 0)); 
    }
}
