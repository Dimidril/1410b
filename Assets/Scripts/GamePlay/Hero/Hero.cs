using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class Hero : NonStationaryCombat
{
    #region Fields

    #region Serialize Fields

    [Header("Hero")]
    [SerializeField] private float RotationSpeed = 0.2f;
    [SerializeField] private Gun gun;
    [SerializeField] private JoysticController MoveController;
    [SerializeField] private JoysticController FireController;
    [SerializeField] [Range(0.1f, 0.9f)] private float FireBorderInController = 0.5f;
    [SerializeField] private SpawnItemController spawnPointItem;
    [SerializeField] private Animator _animator;
    [SerializeField] private Text RegenerationTimeText;
    [SerializeField] private int BaseRegenerationTime = 10;
    [SerializeField] private int DeltaRegenerationTime = 5;
    [SerializeField] private AudioSource _deathsound;
    
    #endregion

    #region Private Fields

    private float _GravityFarce;
    private CharacterController _controller;
    private Vector3 _MoveVector;
    private Vector3 _LookVector;
    private IEnumerator _fireCor;
    private bool _isLookFireController;
    private Transform respawnPoint;
    private int _regenerationTime;
    private bool _isGameOver = false;

    #endregion

    #region Public Fields

    public static Hero instance { get; private set; }
    public bool isDeath { get; private set; } = false;

    #endregion

    #endregion

    #region Methods

    #region Unity Methods

    new private void Awake()
    {
        base.Awake();
        #region Singleton Implementation

        instance = this;

        #endregion

        RegenerationTimeText.gameObject.SetActive(false);
        _regenerationTime = BaseRegenerationTime;

        LevelManager.instance.changePoint += changeRespawnPoint;
        LevelManager.instance.RetreatTime += changeRespawnPoint;
        LevelManager.instance.gameOver += GameOver;
        respawnPoint = LevelManager.instance.ActivePoint.RespawnPoint;

        _controller = GetComponent<CharacterController>();
        _fireCor = gun.GetComponent<Gun>().FireBurst();
        _isLookFireController = false;
    }

    private void FixedUpdate()
    {
        Gravity();
        if (!_isGameOver)
        {
            Move();
            LookAndFire();
        }
    }

    #endregion

    #region Public Method

    public SpawnItemController GetSpawnPointItem()
    {
        return spawnPointItem;
    }

    //Перезарядка
    public void Recharge()
    {
        gun.Recharge();
    }

    //Добавление патронов
    public void AddBullets(int count)
    {
        gun.AddBul(count);
    }

    public override void ApplyDamage(float damage)
    {
        if (!isDeath)
        {
            base.ApplyDamage(damage);
        }
    }

    #endregion

    #region Protected Methods

    protected override void Attack()
    {
        if (_LookVector.magnitude > FireBorderInController && !_isFire && !isDeath)
        {
            _isFire = true;
            StartCoroutine(_fireCor);
        }
        else if ((_LookVector.magnitude < FireBorderInController && _isFire) || isDeath)
        {
            _isFire = false;
            StopCoroutine(_fireCor);
        }
    }

    protected override void Move()
    {
        _MoveVector = Vector3.zero;

        if (!isDeath)
        {

            _MoveVector.x = MoveController.InputVector.x * speed * Time.fixedDeltaTime;
            _MoveVector.z = MoveController.InputVector.y * speed * Time.fixedDeltaTime;

            _animator.SetFloat("SpeedWagon", _MoveVector.magnitude);

            if (!_isLookFireController)
            {
                Look(_MoveVector);
            }

            _MoveVector.y = _GravityFarce;

            _controller.Move(_MoveVector);
        }
    }

    protected void LookAndFire()
    {
        _LookVector = Vector3.zero;

        _LookVector.x = FireController.InputVector.x;
        _LookVector.z = FireController.InputVector.y;

        if (_LookVector.magnitude > 0.01f)
        {
            _isLookFireController = true;
        }
        else
        {
            _isLookFireController = false;
        }

        if (_isLookFireController)
        {
            Look(_LookVector);
        }

        Attack();
    }

    protected override void Die()
    {
        _deathsound.Play();
        _animator.SetTrigger("Die");
        isDeath = true;
        StartCoroutine(RespawnCorutine());
    }

    #endregion

    #region Private Methods

    private void Look(Vector3 vector)
    {
        if ((Vector3.Angle(Vector3.forward, vector) > 1f || Vector3.Angle(Vector3.forward, vector) == 0) && !isDeath)
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, vector, RotationSpeed, 0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }
    }

    private void Gravity()
    {
        if (!_controller.isGrounded)
        {
            _GravityFarce -= 10 * Time.fixedDeltaTime;
        }
        else
        {
            _GravityFarce = -1f;
        }
    }

    private void changeRespawnPoint(Point point)
    {
        respawnPoint = point.RespawnPoint;
    }

    private IEnumerator RespawnCorutine()
    {

        RegenerationTimeText.gameObject.SetActive(true);
        _controller.enabled = false;
        for (int i = (int)_regenerationTime; i > 0; i--)
        {
            RegenerationTimeText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        _regenerationTime += DeltaRegenerationTime;
        RegenerationTimeText.gameObject.SetActive(false);
        Respawn();
    }

    private void Respawn()
    {
        _animator.SetTrigger("Alife");
        isDeath = false;
        transform.position = respawnPoint.position;
        HP = _startHealth;
        _controller.enabled = true;
    }

    private void GameOver()
    {
        _isGameOver = true;
        StopAllCoroutines();
    }

    #endregion

    #endregion
}