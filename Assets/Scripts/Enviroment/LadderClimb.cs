using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    private PlayerController _player;
    private bool _playerPresent;
    public Transform _startPos;

    private void Update()
    {
        if (_playerPresent)
        {
            _player.onLadder = true;
            _playerPresent = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player = other.GetComponent<PlayerController>();
            if (_player != null && !_player.onLadder)
            {
                _playerPresent = true;
            }
        }
    }
}
