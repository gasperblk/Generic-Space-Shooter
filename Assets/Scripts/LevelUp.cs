using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{

    Animator playerAnimator;
    Player player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag is "Player")
        {
            player = other.gameObject.GetComponent<Player>();
            playerAnimator = other.gameObject.GetComponent<Animator>();
            int playerLevel = playerAnimator.GetInteger("playerLevel");
            switch (playerLevel)
            {
                case 1:
                    SoundManager.PlaySound("powerUpSound");
                    playerLevel = 2;
                    playerAnimator.SetInteger("playerLevel", playerLevel);
                    player.LevelUp(playerLevel);
                    Destroy(this.gameObject);
                    break;
                case 2:
                    SoundManager.PlaySound("powerUpSound");
                    playerLevel = 3;
                    playerAnimator.SetInteger("playerLevel", playerLevel);
                    player.LevelUp(playerLevel);
                    player.laserEnabled = true;
                    Destroy(this.gameObject);
                    break;
                default:
                    break;
            }
        }
    }
}
