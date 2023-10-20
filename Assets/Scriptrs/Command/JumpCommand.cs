public class JumpCommand : ICommand
{
    public void Run(IMoveable moveable)
    {
        moveable.Jump();
    }
}
