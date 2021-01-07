using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomieZombie : MonoBehaviour
{
    public float radiusForChasing = 15.0f;
    public Transform targetTransform;
    public float speed = 3.0f;
    float YPosition;
    public float radiusForDamaging = 2.0f;
    public float damageToDeal = 3.0f;
    public Player player;
    public float damagePlayerRate = 1.5f;
    private float nextTimeToDamage = 0f;


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
            }
        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, radiusForChasing);

    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, radiusForDamaging);
    //}

}
