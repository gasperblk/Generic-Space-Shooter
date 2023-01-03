using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip powerUpSound;
    public static AudioClip laserSound;
    public static AudioClip hitSound;
    public static AudioClip explosionSound;
    public static AudioClip asteroidExplosionSound;
    public static AudioClip uuf;
    private static AudioSource src;

    // Start is called before the first frame update
    void Start()
    {
        powerUpSound = Resources.Load<AudioClip>("powerUpSound");
        laserSound = Resources.Load<AudioClip>("laserSound");
        hitSound = Resources.Load<AudioClip>("hitSound");
        explosionSound = Resources.Load<AudioClip>("explosionSound");
        asteroidExplosionSound = Resources.Load<AudioClip>("asteroidExplosionSound");
        uuf = Resources.Load<AudioClip>("uuf");
        src = GetComponent<AudioSource>();
        src.volume = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string soundName)
    {
        switch (soundName)
        {
            case "asteroidExplosionSound":
                src.PlayOneShot(asteroidExplosionSound);
                break;
            case "explosionSound":
                src.PlayOneShot(explosionSound);
                break;
            case "hitSound":
                src.PlayOneShot(hitSound);
                break;
            case "laserSound":
                src.PlayOneShot(laserSound);
                break;
            case "powerUpSound":
                src.PlayOneShot(powerUpSound);
                break;
            case "uuf":
                src.PlayOneShot(uuf);
                break;
            default:
                break;
        }
    }
}
