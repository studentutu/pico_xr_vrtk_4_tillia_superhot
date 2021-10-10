using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody))]
[SelectionBase]
public class WeaponScript : MonoBehaviour
{
    [Header("Bools")] public bool reloading;

    private Rigidbody rb;
    private Collider _collider;
    private Renderer _renderer;

    [Space] [Header("Weapon Settings")] public float reloadTime = .3f;
    public int bulletAmount = 6;


    IEnumerator Start()
    {
        rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();

        while (SuperHotScript.Instance == null)
        {
            yield return null;
        }

        ChangeSettings();
    }

    void ChangeSettings()
    {
        if (transform.parent != null)
            return;

        rb.isKinematic = (SuperHotScript.Instance.weapon == this) ? true : false;
        rb.interpolation = (SuperHotScript.Instance.weapon == this)
            ? RigidbodyInterpolation.None
            : RigidbodyInterpolation.Interpolate;
        _collider.isTrigger = (SuperHotScript.Instance.weapon == this);
    }

    public void Shoot(Vector3 pos, Quaternion rot, bool isEnemy)
    {
        if (SuperHotScript.Instance == null)
        {
            return;
        }

        if (Camera.main == null)
        {
            return;
        }

        if (reloading)
            return;

        if (bulletAmount <= 0)
            return;

        if (!SuperHotScript.Instance.weapon == this)
            bulletAmount--;

        GameObject bullet = Instantiate(SuperHotScript.Instance.bulletPrefab, pos, rot);

        if (GetComponentInChildren<ParticleSystem>() != null)
            GetComponentInChildren<ParticleSystem>().Play();

        if (SuperHotScript.Instance.weapon == this)
            StartCoroutine(Reload());

        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, .01f, 10, 90, false, true).SetUpdate(true);

        if (SuperHotScript.Instance.weapon == this)
            transform.DOLocalMoveZ(-.1f, .05f).OnComplete(() => transform.DOLocalMoveZ(0, .2f));
    }

    public void Throw()
    {
        if (Camera.main == null)
        {
            return;
        }

        Sequence s = DOTween.Sequence();
        Transform mainTransform = Camera.main.transform;
        s.Append(transform.DOMove(transform.position - transform.forward, .01f)).SetUpdate(true);
        s.AppendCallback(() => transform.parent = null);
        s.AppendCallback(
            () => { transform.position = mainTransform.position + (mainTransform.right * .1f); });
        s.AppendCallback(() => ChangeSettings());
        s.AppendCallback(() => rb.AddForce(mainTransform.forward * 10, ForceMode.Impulse));
        s.AppendCallback(() =>
            rb.AddTorque(transform.transform.right + transform.up * 20, ForceMode.Impulse));
    }

    public void Pickup()
    {
        if (SuperHotScript.Instance == null)
            return;

        SuperHotScript.Instance.weapon = this;
        ChangeSettings();

        transform.parent = SuperHotScript.Instance.weaponHolder;

        transform.DOLocalMove(Vector3.zero, .25f).SetEase(Ease.OutBack).SetUpdate(true);
        transform.DOLocalRotate(Vector3.zero, .25f).SetUpdate(true);
    }

    public void Release()
    {
        if (Camera.main == null)
        {
            return;
        }

        transform.parent = null;
        rb.isKinematic = false;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        _collider.isTrigger = false;

        rb.AddForce((Camera.main.transform.position - transform.position) * 2, ForceMode.Impulse);
        rb.AddForce(Vector3.up * 2, ForceMode.Impulse);
    }

    IEnumerator Reload()
    {
        if (SuperHotScript.Instance == null)
        {
            yield break;
        }

        if (SuperHotScript.Instance.weapon != this)
            yield break;
        SuperHotScript.Instance.ReloadUI(reloadTime);
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (SuperHotScript.Instance == null)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Enemy") && collision.relativeVelocity.magnitude < 15)
        {
            BodyPartScript bp = collision.gameObject.GetComponent<BodyPartScript>();

            if (!bp.enemy.dead)
                Instantiate(SuperHotScript.Instance.hitParticlePrefab, transform.position, transform.rotation);

            bp.HidePartAndReplace();
            bp.enemy.Ragdoll();
        }
    }
}