using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveZombie : MonoBehaviour
{
    public float radiusForChasing = 8.0f;
    public Transform targetTransform;
    public float speed = 0.5f;
    float YPosition;
    public float radiusForDamaging = 2.0f;
    public float damageToDeal = 10.0f;
    public Player player;
    public float damagePlayerRate = 0.8f;
    private float nextTimeToDamage = 0f;
    public GameObject explosionEffect;


    // Start is called before the first frame update
    void Start()
    {
        YPosition = transform.position.y;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetTransform = player.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(targetTransform.position, transform.position);

        // Following AI
        // First if loop determines if player is in range to chase 
        if (distanceToPlayer <= radiusForChasing)
        {
            transform.position = new Vector3(transform.position.x, YPosition, transform.position.z);
            Vector3 targetPostition = new Vector3(targetTransform.position.x,
                                       this.transform.position.y,
                                       targetTransform.position.z);
            this.transform.LookAt(targetPostition);
            transform.position += transform.forward * speed * Time.deltaTime;

            // If zombie is close enough to player, do damage to player
            if (distanceToPlayer <= radiusForDamaging && Time.time >= nextTimeToDamage)
            {
                nextTimeToDamage = Time.time + 1f / damagePlayerRate;
                player.TakeDamage(damageToDeal);

                // Instantiate explosion on ground
                GameObject explosion = Instantiate(explosionEffect, new Vector3(transform.position.x, 1.0f, transform.position.z), Quaternion.identity);
                Destroy(explosion, 5.0f);

                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusForChasing);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radiusForDamaging);
    }

}
