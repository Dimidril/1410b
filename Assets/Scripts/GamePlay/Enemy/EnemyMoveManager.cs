using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveManager : MonoBehaviour
{
    #region Fields

    #region Serialize Fields 

    [SerializeField] private InvisiblePoint[] pointsQueueInic;

    #endregion

    #region Private Fields

    private Queue<InvisiblePoint> points;
    private InvisiblePoint activePoint;

    #endregion

    #region Public Fields

    public delegate void NewPoint(InvisiblePoint newPoint);
    public event NewPoint changePoint;
    public static EnemyMoveManager instance { get; private set; }

    #endregion

    #endregion

    #region Properties

    public InvisiblePoint ActivePoint
    {
        get { return activePoint; }
        set
        {
            activePoint = value;
            changePoint?.Invoke(activePoint);
        }
    }

    #endregion

    #region Unity Methods

    private void Awake()
    {
        instance = this;
        points = new Queue<InvisiblePoint>();
        for (int i = 0; i < pointsQueueInic.Length; i++)
        {
            pointsQueueInic[i].die += nextPoint;
            points.Enqueue(pointsQueueInic[i]);
        }
        ActivePoint = points.Dequeue();
    }

    #endregion

    #region Private Methods

    #endregion

    #region Private Fields

    private void nextPoint()
    {
        ActivePoint = points?.Dequeue();
         if (ActivePoint == null)
         {
             return;
        }
    }

    #endregion
}