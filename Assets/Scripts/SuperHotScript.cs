using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zinnia.Action;

public class SuperHotScript : MonoBehaviour
{
    [Header("Actions")] public FloatAction vertical;
    public BooleanAction trigger;
    public BooleanAction grip;
    public Transform bulletSpawner;

    public static SuperHotScript Instance;

    public bool canShoot = true;
    public bool action;

    [Header("Weapon")] public WeaponScript weapon;
    public Transform weaponHolder;
    public LayerMask weaponLayer;


    [Space] [Header("Prefabs")] public GameObject hitParticlePrefab;
    public GameObject bulletPrefab;


    private void Awake()
    {
        Instance = this;
        if (weaponHolder.GetComponentInChildren<WeaponScript>() != null)
            weapon = weaponHolder.GetComponentInChildren<WeaponScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Escape))
        // {
        //     UnityEngine.SceneManagement.SceneManager
        //         .LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        // }

        if (canShoot && trigger.Value)
        {
            StopCoroutine(ActionE(.03f));
            StartCoroutine(ActionE(.03f));
            if (weapon != null)
                weapon.Shoot(bulletSpawner.position, bulletSpawner.rotation, false);
        }

        if (grip.Value)
        {
            StopCoroutine(ActionE(.4f));
            StartCoroutine(ActionE(.4f));

            if (weapon != null)
            {
                weapon.Throw();
                weapon = null;
            }
        }

        if (weapon == null && trigger.Value &&
            Physics.Raycast(weaponHolder.position, weaponHolder.forward, out var hit, 3, weaponLayer))
        {
            hit.transform.GetComponent<WeaponScript>().Pickup();
        }

        float y = vertical.Value;

        float time = (y != 0) ? 1f : .03f;
        float lerpTime = (y != 0) ? .05f : .5f;

        time = action ? 1 : time;
        lerpTime = action ? .1f : lerpTime;

        Time.timeScale = Mathf.Lerp(Time.timeScale, time, lerpTime);
    }

    IEnumerator ActionE(float time)
    {
        action = true;
        yield return new WaitForSecondsRealtime(.06f);
        action = false;
    }

    public void ReloadUI(float time)
    {
    }
}