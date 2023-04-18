using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSprite : MonoBehaviour
{
    public Sprite projectileSprite;
    public float projectileSpeed = 10f;
    public float playerPushback = 10f;
    public bool isBoosted = false;
    private const float boostMultiplier = 2f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = transform.position.z - Camera.main.transform.position.z;
            Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 direction = (target - player.transform.position).normalized;

            GameObject projectile = new GameObject("Projectile");
            SpriteRenderer renderer = projectile.AddComponent<SpriteRenderer>();
            renderer.sprite = projectileSprite;
            renderer.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
            projectile.transform.position = player.transform.position;

            Rigidbody2D rb = projectile.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.velocity = direction * projectileSpeed;

            // Push the player object in the opposite direction of the projectile
            float pushbackForce = playerPushback * (isBoosted ? boostMultiplier : 1f);
            player.GetComponent<Rigidbody2D>().AddForce(-direction * pushbackForce, ForceMode2D.Impulse);

            Destroy(projectile, 2f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
