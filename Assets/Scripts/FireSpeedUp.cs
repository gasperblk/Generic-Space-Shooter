using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpeedUp : MonoBehaviour
{

    [Range(0.0f, 1.0f)]
    public float fireCooldownDecrease = 0.1f;
    public float fireCooldownLimit = 0.1f;
    private Player player;
    private Laser laser;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag is "Player")
        {

            player = other.gameObject.GetComponent<Player>();
            laser = other.transform.Find("Fire Point 2/Laser").GetComponent<Laser>();
            if (player.fireCooldown > fireCooldownLimit)
            {   
                SoundManager.PlaySound("powerUpSound");
                laser.fireCooldown -= fireCooldownDecrease;
                player.fireCooldown -= fireCooldownDecrease;
                Destroy(this.gameObject);
            }
        }
    }
}
