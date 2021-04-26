using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NonStationaryCombat : StationaryCombat
{
    #region Fields

    #region Serialize Field

    [Header("NonStationaryCombat")]
    [SerializeField] protected float speed;

    #endregion

    #endregion

    #region Methods

    #region Protected Methods

    //Движ-Париж
    protected abstract void Move();
    
    #endregion

    #endregion
}