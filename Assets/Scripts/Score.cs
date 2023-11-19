using UnityEngine;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    private int _value;

    public int Value => _value;

    public event UnityAction ValueChanged;

    public void AddPoints(int amount)
    {
        if(amount > 0)
        {
            _value += amount;

            ValueChanged?.Invoke();
        }
    }
}
