using UnityEngine.Events;

public interface IEnemyController
{
    public event UnityAction Died;

    public void Update();
}
