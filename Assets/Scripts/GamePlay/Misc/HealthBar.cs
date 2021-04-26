using UnityEngine;

public class HealthBar : MonoBehaviour
{
    #region Fields

    #region Serialized Fields

    [SerializeField] private Animator healthAnimator;
    [SerializeField] private string healthAnimatorName;
    [SerializeField] private StationaryCombat healthObj;

    #endregion

    #region Private Fields

    private float _startHealth;

    #endregion

    #endregion

    #region Methods

    #region Unity Methods

    void Start()
    {
        _startHealth = healthObj.GetStartHP();
    }

    void Update()
    {
        healthAnimator.Play(healthAnimatorName, 0, healthObj.GetHP()/(_startHealth+0.02f));
    }

    #endregion

    #endregion
}