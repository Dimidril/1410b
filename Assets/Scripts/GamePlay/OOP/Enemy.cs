using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : NonStationaryCombat
{
    #region Fields

    #region Serialize Fields

    [Header("Enemy")]
    [SerializeField] protected float Shield;
    [SerializeField] protected Transform Coin;
    [SerializeField] protected Transform CoinSpawner;
    [SerializeField] protected Animator _animator;

    #endregion

    #region Protected Fields

    protected NavMeshAgent nav;
    protected float MainDist;
    protected float ExtraDist;
    protected float MoveDist;
    protected Transform MainTarget;
    protected Transform ExtraTarget;
    protected Transform MoveTarget;

    protected bool _stop = false;
    protected bool _end = false;

    #endregion

    #endregion

    #region Methods

    #region Unity Methods

    new private void Start()
    {
        base.Start();
        MainTarget = LevelManager.instance.ActivePoint.transform;

        LevelManager.instance.changePoint += ChangeMainTarget;
        LevelManager.instance.RetreatTime += Retreat;
        LevelManager.instance.gameOver += GameOver;

        MoveTarget = EnemyMoveManager.instance.ActivePoint.transform;

        EnemyMoveManager.instance.changePoint += ChangeMoveTarget;

        ExtraTarget = Hero.instance.gameObject.transform;
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Проверка на смерть
        if (Hero.instance.isDeath && MainTarget == null) {
            StopCoroutine(_attackCoroutine);
            return;
        }
        //Debug.Log(_stop);

        //Проверка на конец игры
        if (!_end)
        {
            //Назначение целей фрага
            MoveDist = Vector3.Distance(MoveTarget.position, transform.position);
            if (MainTarget != null) MainDist = Vector3.Distance(MainTarget.position, transform.position);
            ExtraDist = Vector3.Distance(ExtraTarget.position, transform.position);
        
            if ((MainTarget==null || ExtraDist <= Scope) && ExtraTarget!=null && !Hero.instance.isDeath)
            {
                if (ExtraDist <= 20)
                {
                    _animator.SetBool("isMoving", false);
                }
                else if (ExtraDist > 20)
            {
                _animator.SetBool("isMoving", true);
            }
                ExtraAttantion();
            }
            else if (Hero.instance.isDeath || ExtraDist > Scope)
            {
                if ((MainDist <= MoveDist || MainDist <= Scope) && MainTarget!=null)
                { 
                    if (MainDist <= 20)
                    {
                        _animator.SetBool("isMoving", false);
                    }
                    else if (MainDist > 20)
                    {
                        _animator.SetBool("isMoving", true);
                    }
                    MainAttantion();
                }
                else if (MainDist > Scope || MainTarget==null)
                {
                    InvisibleAttantion();
                }
            }
        }
    }

    #endregion

    #region Public Methods

    public void ApplyDamageShield(int shieldDamage)
    {
        Shield -= shieldDamage;
    }

    #endregion

    #region Protected Methods

    //Смена основной цели
    protected void ChangeMainTarget(Point point)
    {
        MainTarget = point.gameObject.transform;
    }

    //Смена цели движения
    protected void ChangeMoveTarget(InvisiblePoint point)
    {
        MoveTarget = point.gameObject.transform;
    }

    protected override void Move()
    {
        nav.SetDestination(MainTarget.position);
    }

    //Двигаться к игроку
    protected void MovetoHero()
    {
        nav.SetDestination(ExtraTarget.position);
    }

    protected virtual void InvisibleAttantion()
    {
        Move();
        Look(MoveTarget);
    }

    //Назначение основной цели
    protected virtual void MainAttantion()
    {
        Move();
        Look(MainTarget);
        if (MainDist > Scope && _isFire)
        {
            StopCoroutine(_attackCoroutine);
            _isFire = false;
        }

        if (MainDist <= Scope && !_isFire)
        {
            _isFire = true;
            StartCoroutine(_attackCoroutine);
        }
    }

    //Назначение дополнительной цели
    protected virtual void ExtraAttantion()
    {
        MovetoHero();
        Look(ExtraTarget);
        if (ExtraDist > Scope && _isFire)
        {
            StopCoroutine(_attackCoroutine);
            _isFire = false;
        }

        if (ExtraDist <= Scope && !_isFire)
        {
            _isFire = true;
            StartCoroutine(_attackCoroutine);
        }
    }

    protected void Look(Transform _targ)
    {
        //nav.SetDestination(_targ.position);
        Vector3 direct = new Vector3(_targ.position.x, transform.position.y, _targ.position.z);
        transform.LookAt(direct);
    }

    protected override void Die()
    {
        Transform monet = (Transform)Instantiate(Coin, CoinSpawner.position, Quaternion.identity);
        monet.name = "Coin";
        Destroy(gameObject);
    }

    #endregion

    #region Private Methods
    
    private void Retreat(Point next)
    {
        MainTarget = null;
    }

    private void GameOver()
    {
        _end = true;
    }
    
    #endregion

    #endregion
}