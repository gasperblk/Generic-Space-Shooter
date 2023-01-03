using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUp : MonoBehaviour
{
    private Player player;
    private Transform shield;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag is "Player")
        {
            player = other.gameObject.GetComponent<Player>();
            if (!player.isShielded)
            {
                SoundManager.PlaySound("powerUpSound");
                player.isShielded = true;
                shield = player.transform.Find("Shield");
                shield.gameObject.GetComponent<Shield>().ShieldOn();
                Destroy(this.gameObject);
            }
        }
    }    
}
