using TMPro;
using UnityEngine;

public class HealthBarDigit : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private Player _player;
    private bool _inited = false;

    private void OnEnable()
    {
        if(_inited == true)
        {
            _player.HealthChanged += OnHealthChanged;
        }
    }

    private void OnDisable()
    {
        if (_inited == true)
        {
            _player.HealthChanged -= OnHealthChanged;
        }
    }

    public void Init(Player player)
    {
        if(_inited == false)
        {
            _player = player;

            SetText(player.Health);

            _player.HealthChanged += OnHealthChanged;

            _inited = true;
        }
    }

    private void SetText(int health)
    {
        _text.SetText($"{health}/{_player.MaxHealth}");
    }

    private void OnHealthChanged(int health) => SetText(health);
}
