using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject[] Zombies;
    public Player player;
    public float radiusForStarting = 25.0f;
    private bool hasStarted = false;
    public float zombieIntervalTime = 3.0f;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, 3);

        if (hasStarted == false)
        {
            // Only do this if user is within distance
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

            if (distanceToPlayer <= radiusForStarting)
            {
                hasStarted = true;
                InvokeRepeating("SpawnZombie", 1.0f, zombieIntervalTime);

            }
        }
    }

    public void SpawnZombie()
    {
        int index = Random.Range(0, Zombies.Length);
        GameObject Zombie = Zombies[index];

        // For each Zombie, instantiate at different heights
        if (index == 0)
        {
            Instantiate(Zombie, new Vector3(transform.position.x, 2.77f , transform.position.z), Quaternion.identity);

        }
        else if (index == 1)
        {
            Instantiate(Zombie, new Vector3(transform.position.x, 1.99f, transform.position.z), Quaternion.identity);
        }
        else if (index == 2)
        {
            Instantiate(Zombie, new Vector3(transform.position.x, 2.77f, transform.position.z), Quaternion.identity);
        }

    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusForStarting);
    }
}
