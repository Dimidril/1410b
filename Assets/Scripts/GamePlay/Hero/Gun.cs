using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Gun : MonoBehaviour
{
    #region Fields

    #region Serialize Fields

    [SerializeField] private float Damage;
    [SerializeField] private float BulletSpeed = 100f;
    [SerializeField] private float DeltaFireTime = 0.5f;
    [SerializeField] private float DeltaRechargeTime = 4f;
    [SerializeField] private int MaxBulletAmount;
    [SerializeField] private int MaxBulletInMagazine;
    [SerializeField] private Transform Bullet;
    [SerializeField] private Transform BulletSpawner;
    [SerializeField] private Text BulletPanel;
    [SerializeField] GameObject Bang;
    [SerializeField] AudioClip Shot;
    [SerializeField] AudioClip Reload;
    [SerializeField] Animator DubovAnim;

    #endregion

    #region Private Fields

    private bool _isRecharge = false;

    #endregion

    #endregion

    #region Properties

    public int BulletRemains { get; private set; }
    public int BulletMagazineRemains { get; private set; }

    #endregion

    #region Methods

    #region Unity Methods

    private void Awake()
    {
        BulletRemains = MaxBulletAmount;
        BulletMagazineRemains = MaxBulletInMagazine;
        BulletPanel.text = BulletMagazineRemains.ToString() + "/" + BulletRemains.ToString();
    }

    private void Update()
    {
        Bang.SetActive(false);
        BulletPanel.text = BulletMagazineRemains.ToString() + "/" + BulletRemains.ToString();
        if (BulletMagazineRemains == 0 && !_isRecharge)
        {
            Recharge();
        }
    }

    #endregion

    #region Public Methods

    public IEnumerator FireBurst()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(DeltaFireTime); 
        }
    }

    public IEnumerator RechargeCor()
    {
        if (BulletRemains > 0)
        {
            _isRecharge = true;
            yield return new WaitForSeconds(DeltaRechargeTime);

            int bullReachargeCount = MaxBulletInMagazine - BulletMagazineRemains;
            if (BulletRemains < bullReachargeCount) bullReachargeCount = BulletRemains;
            BulletRemains -= bullReachargeCount;
            BulletMagazineRemains += bullReachargeCount;

            _isRecharge = false;
        }
    }

    public void Recharge()
    {
        if (!_isRecharge && BulletMagazineRemains < MaxBulletInMagazine && BulletRemains > 0)
        {
            DubovAnim.SetTrigger("Recharge");
            GetComponent<AudioSource>().volume = 1f;
            GetComponent<AudioSource>().PlayOneShot(Reload);
            StartCoroutine(RechargeCor());
        }
    }
        

    public void AddBul(int count)
    {
        BulletRemains += count;
    }

    #endregion

    #region Private Methods

    private void Fire()
    {
        if (BulletMagazineRemains>0 && !_isRecharge)
        {
            Transform bull = Instantiate(Bullet, BulletSpawner.position, BulletSpawner.rotation);
            bull.GetComponent<Rigidbody>().AddForce(transform.forward * BulletSpeed, ForceMode.Impulse);
            Bang.SetActive(true);
            GetComponent<AudioSource>().volume = 0.2f;
            GetComponent<AudioSource>().PlayOneShot(Shot);
            
            bull.GetComponent<Bullet>().OnCreate(Damage);
            BulletMagazineRemains--;

            bull.name = "Bullet";
        }
    }

    #endregion

    #endregion
}