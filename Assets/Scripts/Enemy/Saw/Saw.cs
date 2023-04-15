using UnityEngine;

public class Saw : MonoBehaviour
{
	public int baseAttack;
	public PlayerHealth playerHealth;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			playerHealth.TakeDamage(baseAttack);
		}
	}
}
