using UnityEngine;

public class Soldier : Enemy
{
    #region Fields

    #region Serialize Fields

    [SerializeField] private float BulletSpeed = 100f;
    [SerializeField] private Transform Bullet;
    [SerializeField] private Transform BulletSpawner;
    [SerializeField] AudioClip SoldierAudio;

    #endregion

    #endregion

    #region Methods

    #region Protected Methods

    protected override void Attack()
    {
        GetComponent<AudioSource>().spatialBlend = 1f;
        GetComponent<AudioSource>().volume = 1f;
        GetComponent<AudioSource>().maxDistance = 100f;
        GetComponent<AudioSource>().PlayOneShot(SoldierAudio);
        Transform bull = Instantiate(Bullet, BulletSpawner.position, Quaternion.identity);
        bull.GetComponent<Rigidbody>().AddForce(transform.forward * BulletSpeed, ForceMode.Impulse);

        bull.GetComponent<Bullet>().OnCreate(Damage);

        bull.name = "Enemy Bullet";
    }

    #endregion

    #endregion
}