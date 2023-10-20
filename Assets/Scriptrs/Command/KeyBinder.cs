using System.Collections.Generic;
using UnityEngine;

public class KeyBinder
{
    private Dictionary<KeyCode, ICommand> _commands = new Dictionary<KeyCode, ICommand>();
    public IReadOnlyCollection<KeyCode> Keys => _commands.Keys;
    
    public void AddCommand(KeyCode key, ICommand command)
    {
        _commands.Add(key, command);
    }

    public ICommand GetCommand(KeyCode key)
    {
        if (_commands.ContainsKey(key))
        {
            return _commands[key];
        }

        return null;
    }
}
