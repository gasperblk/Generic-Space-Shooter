using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.gameObject.GetComponent<Player>();
        ShieldOff();
    }

    public void ShieldOn()
    {
        player.isShielded = true;
        gameObject.SetActive(true);
    }

    public void ShieldOff()
    {
        player.isShielded = false;
        gameObject.SetActive(false);

    }
}
