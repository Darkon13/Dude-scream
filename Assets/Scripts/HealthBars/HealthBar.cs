using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private RectTransform _bar;

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
        if(_inited == true)
        {
            _player.HealthChanged -= OnHealthChanged;
        }
    }

    public void Init(Player player)
    {
        if(_inited == false)
        {
            _player = player;
            SetLenght(_player.Health);

            _player.HealthChanged += OnHealthChanged;

            _inited = true;

            Debug.Log(Color.black);
        }
    }


    private void SetLenght(int health) => _bar.localScale = new Vector3(1f / _player.MaxHealth * health, _bar.localScale.y, _bar.localScale.z);

    private void OnHealthChanged(int health) => SetLenght(health);
}
