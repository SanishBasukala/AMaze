using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public SpriteRenderer playerSr;
    public Player player;

    [SerializeField]
    private AudioSource hurtAudio;
    private void Update()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            // make hearts visible
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public void TakeDamage(float damageAmount)
    {
        hurtAudio.Play();
        StartCoroutine(DamageIndicator());
        health -= damageAmount;

        if (health <= 0)
        {
            playerSr.enabled = false;
            player.enabled = false;
        }
    }
    public void GainHealth()
    {
        health += 1;
        if (health >= numOfHearts)
        {
            health = numOfHearts;
        }
    }

    IEnumerator DamageIndicator()
    {
        playerSr.material.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        playerSr.material.color = new Color(255, 255, 254);
        yield return new WaitForSeconds(.05f);
        playerSr.material.color = Color.white;
    }
}
