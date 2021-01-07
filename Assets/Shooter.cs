using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    public float gunDamage = 10f;
    public float gunRange = 50f;
    public float impactForce = 3000f;
    public float gunFireRate = 3f;

    public Camera playerCamera;
    public ParticleSystem flash;
    public ParticleSystem hitObjectEffect;

    private float nextTimeToFire = 0f;
    public int ammo = 15;
    public Text gunText;
    private readonly char[] numArray = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'};

    // Start is called before the first frame update
    void Start()
    {
        int indexOfFirstNumber = gunText.text.IndexOfAny(numArray);
        // Change ammo text UI
        gunText.text = gunText.text.Substring(0, indexOfFirstNumber) + ammo;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire && GUIUtility.hotControl == 0)
        {
            nextTimeToFire = Time.time + 1f / gunFireRate;
            Fire();
        }
    }

    // Use raycasting to fire a bullet
    void Fire()
    {
        int indexOfFirstNumber = gunText.text.IndexOfAny(numArray);
        ammo = int.Parse(gunText.text.Substring(indexOfFirstNumber));

        if (ammo > 0)
        {

            float distance = -.2f;
            GameObject gunFlashGameObject = Instantiate(flash.gameObject, transform.position + transform.forward * distance, transform.rotation);
            Destroy(gunFlashGameObject, 2f);

            // Subtract one from ammo
            ammo -= 1;

            // Change ammo text UI
            gunText.text = gunText.text.Substring(0, indexOfFirstNumber) + ammo;

            RaycastHit hitInfo;

            LayerMask layerToIgnore = (1 << 9);
            layerToIgnore = ~layerToIgnore;

            // If raycast hits something, then put info into hitInfo and proceed
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, gunRange, layerToIgnore))
            {
                // Find enemy component in parents
                Enemy enemy = hitInfo.transform.GetComponentInParent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(gunDamage);
                }

                //// Push back on enemy if they have a rigidbody
                //if (hitInfo.rigidbody)
                //{
                //    print("adding force");
                //    hitInfo.rigidbody.AddForce(-hitInfo.normal * impactForce);
                //}

                GameObject hitObjectEffectGameObject = Instantiate(hitObjectEffect.gameObject, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(hitObjectEffectGameObject, 2f);
            }
        }
    }

    public void AddMoreAmmo(int ammoToAdd)
    {
        ammo += ammoToAdd;

        int indexOfFirstNumber = gunText.text.IndexOfAny(numArray);
        // Change ammo text UI
        gunText.text = gunText.text.Substring(0, indexOfFirstNumber) + ammo;
    }
}
