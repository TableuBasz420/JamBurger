using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip playerHitSound;
    public static AudioClip playerJumpSound;
    public static AudioClip enemyDieSound;
    static AudioSource audiosrc;
    // Start is called before the first frame update
    void Start()
    {
        playerHitSound = Resources.Load<AudioClip>("playerHit");
        playerJumpSound = Resources.Load<AudioClip>("playerJump");
        enemyDieSound = Resources.Load<AudioClip>("enemyDie");

        audiosrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public static void playSound (string clip)
    {
        switch (clip)
        {
            case "playerHit":
                audiosrc.PlayOneShot(playerHitSound);
                break;
            case "playerJump":
                audiosrc.PlayOneShot(playerJumpSound);
                break;
            case "enemyDie":
                audiosrc.PlayOneShot(enemyDieSound);
                break;
        }
    }
}
