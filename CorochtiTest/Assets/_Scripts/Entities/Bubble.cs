using System;
using Platformer.Mechanics;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    #region Defines

    private const string PLAYER_TAG = "Player";

    #endregion
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag(PLAYER_TAG))
        {
            return;
        }

        Player player = other.GetComponent<Player>();
        if (player == null)
        {
            return;
        }
        
        player.EnterFlyState();
        gameObject.SetActive(false);
    }
}
