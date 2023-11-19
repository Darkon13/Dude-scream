using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private HealthBarDigit _healthBarDigit;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private HealthBarSmooth _healthBarSmooth;

    public void Init(Player player)
    {
        _healthBarDigit.Init(player);
        _healthBar.Init(player);
        _healthBarSmooth.Init(player);
    }
}
