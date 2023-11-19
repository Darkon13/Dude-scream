using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private UIController _UIController;

    private void Awake()
    {
        _player.Init();
        _UIController.Init(_player);
    }
}
