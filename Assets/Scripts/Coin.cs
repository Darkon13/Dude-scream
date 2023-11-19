using UnityEngine;

[CreateAssetMenu(fileName = "New Coin", menuName = "Objects/Create Coin", order = 51)]
public class Coin : CollectableObject
{
    [SerializeField] private int _value;

    private Score _score;

    public void Init(Score score)
    {
        _score = score;
    }

    public override void Action()
    {
        if(_score != null)
        {
            _score.AddPoints(_value);
        }
    }
}
