using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedUp : MonoBehaviour
{

    public float moveSpeedIncrease = 1.0f;
    public float moveSpeedLimit = 15.0f;
    private Player player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag is "Player")
        {
            player = other.gameObject.GetComponent<Player>();
            if (player.speed < moveSpeedLimit)
            {
                SoundManager.PlaySound("powerUpSound");
                player.speed += moveSpeedIncrease;
                Destroy(this.gameObject);
            }
        }
    }
}
