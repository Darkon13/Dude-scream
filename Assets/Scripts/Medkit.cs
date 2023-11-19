using UnityEngine;

[CreateAssetMenu(fileName = "New Medkit", menuName = "Objects/Create Medkit", order = 51)]
public class Medkit : CollectableObject
{
    [SerializeField] private int _health;

    private Player _player;

    public void Init(Player player)
    {
        _player = player;
    }

    public override void Action()
    {
        if (_player != null)
        {
            _player.AddHealth(_health);
        }
    }
}
