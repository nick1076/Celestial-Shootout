using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    //Focus variable
    [HideInInspector] public bool focused;

    [Header("Gun Objects")]
    //Muzzle origin - for muzzle flash effects
    public Transform gunMuzzleOrigin;

    //Location to fire tracers from
    public Transform gunTracerOrigin;

    //Tracer effect prefab
    public GameObject gunEffect_tracer;

    //Muzzle flash effect prefab
    public GameObject gunEffect_muzzleFlash;

    //Animator for gun
    public Animator gunAnimator;

    //List of all physical bullets - for display purposes
    public List<GameObject> gunBulletObjects = new List<GameObject>();

    //Manager for combos to increase or reset on kills
    public ComboManager comboMan;

    [Header("Gun Settings")]

    //Knockback power of gun
    public float gun_KnockbackForce = 5.0f;

    //Total bullets per magazine
    public int gun_totalMagCapacity = 8;

    //Animator timer - fire effect delay (time delay after animation begins to actually fire)
    public float gunAnimator_delayAfterAnimationStartsToDoAction_fireAnimation = 0.1f;

    //Animation timer - reload effect delay (time delay after animation begins to actually reload)
    public float gunAnimator_delayAfterAnimationStartsToDoAction_reloadAnimation = 0.2f;

    //Cooldown after reload action queued to allow the player to do another action
    public float actionCooldown_reloadAction = 1;

    //Cooldown after fire action queued to allow the player to do another action
    public float actionCooldown_fireCooldown = 0.25f;

    [Header("Player Components")]

    //Player's rigidbody, used to apply gun_KnockbackForce to player after shooting
    public Rigidbody player_physics;

    //Player camera object
    public Transform player_camera;

    //Hidden

    //Elapsed timer used for both fire and reload, used in tandem with reloads
    float timeElapsed_sinceLastAction = 0f;

    //Elapsed timer that begins counting after the gun is reloaded 
    float timeElapsed_sinceAnimationBegun_gunReload = 0f;

    //Elapsed timer that begins counting after the gun is fired 
    float timeElapsed_sinceAnimationBegun_gunFire = 0f;

    //Tells if an action has been qued - gun firing action
    bool actionQueued_fireGun = false;

    //Tells if an action has been qued - gun reloading action
    bool actionQueued_reloadGun = false;

    //Tells if the watch is currently up
    bool actionActive_watchIsUp = false;

    //Count of current bullets in weapon
    int gun_currentBullets = 8;

    void Update()
    {
        timeElapsed_sinceLastAction += Time.deltaTime;
        timeElapsed_sinceAnimationBegun_gunReload += Time.deltaTime;
        timeElapsed_sinceAnimationBegun_gunFire += Time.deltaTime;

        if (focused && !actionActive_watchIsUp && Input.GetKeyDown(KeyCode.Mouse0) && gun_currentBullets > 0 && timeElapsed_sinceAnimationBegun_gunReload >= actionCooldown_reloadAction && !actionQueued_fireGun && timeElapsed_sinceAnimationBegun_gunFire >= actionCooldown_fireCooldown)
        {
            Fire();
        }
        else if (focused && !actionActive_watchIsUp && Input.GetKeyDown(KeyCode.Mouse0) && gun_currentBullets == 0 && timeElapsed_sinceAnimationBegun_gunReload >= actionCooldown_reloadAction && !actionQueued_fireGun && timeElapsed_sinceAnimationBegun_gunFire >= actionCooldown_fireCooldown)
        {
            Reload();
        }

        if (focused && !actionActive_watchIsUp && Input.GetKeyDown(KeyCode.Mouse1) && gun_currentBullets < gun_totalMagCapacity && !actionQueued_fireGun && !actionQueued_reloadGun)
        {
            Reload();
        }

        if (focused && !actionActive_watchIsUp && Input.GetAxis("Mouse ScrollWheel") > 0f && !actionQueued_fireGun && !actionQueued_reloadGun)
        {
            //Pull up watch
            actionActive_watchIsUp = true;
            gunAnimator.SetTrigger("Watch_Open");
        }

        if (focused && actionActive_watchIsUp && Input.GetAxis("Mouse ScrollWheel") < 0f && !actionQueued_fireGun && !actionQueued_reloadGun)
        {
            //Pull up watch
            actionActive_watchIsUp = false;
            gunAnimator.SetTrigger("Watch_Close");
        }

        if (actionQueued_fireGun && timeElapsed_sinceLastAction >= gunAnimator_delayAfterAnimationStartsToDoAction_fireAnimation)
        {
            timeElapsed_sinceLastAction = 0;
            actionQueued_fireGun = false;

            gun_currentBullets--;

            for (int i = gunBulletObjects.Count - 1; i > gun_currentBullets - 1; i--)
            {
                if (gunBulletObjects[i] != null)
                {
                    gunBulletObjects[i].SetActive(false);
                }
            }

            Instantiate(gunEffect_muzzleFlash, gunMuzzleOrigin.transform.position, gunMuzzleOrigin.transform.rotation, gunMuzzleOrigin.transform);
            GameObject tracer = Instantiate(gunEffect_tracer, gunTracerOrigin.transform.position, gunTracerOrigin.transform.rotation);

            Vector3 fwd = player_camera.transform.TransformDirection(Vector3.forward);

            RaycastHit objectHit;

            if (Physics.Raycast(player_camera.transform.position, fwd, out objectHit, 50))
            {
                //do something if hit object ie
                if (objectHit.collider.gameObject.tag == "Enemy")
                {
                    comboMan.IncrimentCombo();
                    Destroy(tracer);
                    objectHit.collider.gameObject.GetComponent<EnemyController>().OnDeath();
                }
                else
                {
                    comboMan.ResetCombo();
                }
            }
            player_physics.AddForce(player_camera.transform.forward * -1 * gun_KnockbackForce);
        }
        
        if (actionQueued_reloadGun && timeElapsed_sinceLastAction >= gunAnimator_delayAfterAnimationStartsToDoAction_reloadAnimation)
        {
            timeElapsed_sinceLastAction = 0;
            actionQueued_reloadGun = false;

            gun_currentBullets = gun_totalMagCapacity;

            for (int i = 0; i < gunBulletObjects.Count; i++)
            {
                if (gunBulletObjects[i] != null)
                {
                    gunBulletObjects[i].SetActive(true);
                }
            }
        }
    }

    public void Reload()
    {
        timeElapsed_sinceLastAction = 0;
        timeElapsed_sinceAnimationBegun_gunReload = 0;
        if (gunAnimator != null)
        {
            gunAnimator.SetTrigger("Reload");
        }
        actionQueued_reloadGun = true;
    }

    public void Fire()
    {
        timeElapsed_sinceLastAction = 0;
        timeElapsed_sinceAnimationBegun_gunFire = 0;
        if (gunAnimator != null)
        {
            gunAnimator.SetTrigger("Fire");
        }
        actionQueued_fireGun = true;
    }
}
