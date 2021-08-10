using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_References : MonoBehaviour
{
    public string playerTag;
    public static string _playerTag;

    public static GameObject _player;

    private void OnEnable()
    {
        if(playerTag == "")
        {
            Debug.LogWarning("Type the string of playertag in GameManager_References");
        }

        _playerTag = playerTag;

        _player = GameObject.FindGameObjectWithTag(_playerTag);
    }
}
