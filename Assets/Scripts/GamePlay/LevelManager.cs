using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    #region Fields

    #region Serialize Fields 

    [SerializeField] private ProgressLoader.LevelManagerSavesStruct LevelSaveData;
    [SerializeField] private int BegineCoinCount = 0;
    [SerializeField] private Point[] pointsQueueInic;
    [SerializeField] private Text CountdownText;
    [SerializeField] private Text CoinCountText;
    [SerializeField] private Text RetreatTimeText;

    [SerializeField] private Text LoseUI;
    [SerializeField] private AudioSource LoseAudio;
    [SerializeField] private Text WinUI;
    [SerializeField] private Button healRetreatBut;

    #endregion

    #region Private Fields

    private Queue<Point> points;
    private Point activePoint;
    private int _nowReward;

    #endregion

    #region Public Fields

    public delegate void NewPoint(Point newPoint);
    public delegate void Retreat(Point nextPoint);
    public delegate void GameEnd();

    public event NewPoint changePoint;
    public event GameEnd gameOver;
    public event Retreat RetreatTime;

    public static LevelManager instance { get; private set; }

    #endregion

    #endregion

    #region Properties

    public Point ActivePoint 
    {
        get { return activePoint; }
        set
        {
            activePoint = value;
            changePoint?.Invoke(activePoint);
        }
    }
    public int DefendedPoint { get; private set; }
    public int CoinCount { get; private set; }

    #endregion

    #region Unity Methods

    private void Awake()
    {
        instance = this;
        CoinCount = BegineCoinCount;

        WinUI.gameObject.SetActive(false);

        LoseUI.gameObject.SetActive(false);

        healRetreatBut.gameObject.SetActive(false);

        WinUI.transform.Find("Stars").transform.GetChild(0).gameObject.SetActive(false);
        WinUI.transform.Find("Stars").transform.GetChild(1).gameObject.SetActive(false);
        WinUI.transform.Find("Stars").transform.GetChild(2).gameObject.SetActive(false);

        if (CoinCountText!=null) CoinCountText.text = CoinCount.ToString();
        DefendedPoint = 0;
        points = new Queue<Point>();
        Time.timeScale = 1;
        for (int i = 0; i < pointsQueueInic.Length; i++)
        {
            //pointsQueueInic[i].abyss += PointProtected;
            pointsQueueInic[i].die += AbusePoint;
            points.Enqueue(pointsQueueInic[i]);
        }

        ActivePoint = points.Dequeue();
        StartCoroutine(DefendTimer());
        _nowReward = activePoint.GetReward();
        RetreatTimeText.gameObject.SetActive(false);
    }

    #endregion

    #region Public Methods

    //Изменение кол-ва монеток на +count
    public void changeCoinCount(int count)
    {
        CoinCount += count;
        if (CoinCount < 0) CoinCount = 0;   
        if (CoinCountText != null) CoinCountText.text = CoinCount.ToString();
    }

    #endregion

    #region Private Fields

    //Вызываеться если точка была защищена 
    private void PointProtected()
    {
        DefendedPoint++;
        changeCoinCount(_nowReward);
        Destroy(activePoint.gameObject);
        AbusePoint();
    }

    //Покидание точки
    private void AbusePoint()
    {
        StopAllCoroutines();
        if (points.Count > 0)
            StartCoroutine(RetreatTimer());
        else
            GameOver(); 
    }

    //отсчитывет время отступления
    private IEnumerator RetreatTimer()
    {
        RetreatTime?.Invoke(points.Peek());
        if (healRetreatBut) healRetreatBut.gameObject.SetActive(true); 
        RetreatTimeText.gameObject.SetActive(true);
        for (int i = (int)activePoint.GetRetreatTime(); i >= 0; i--)
        {
            RetreatTimeText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        if (healRetreatBut) healRetreatBut.gameObject.SetActive(false);
        RetreatTimeText.gameObject.SetActive(false);
        NextPoint();
    }

    //отсчитывает время защиты точки
    private IEnumerator DefendTimer()
    {
        for (int i = (int)activePoint.GetDefTime(); i >= 0; i--)
        {
            string nol = (i % 60 < 10) ? "0" : "";
            CountdownText.text = (i / 60).ToString() + ':' + nol + (i % 60).ToString();
            yield return new WaitForSeconds(1);
        }
        PointProtected();
    }

    //меняет активную точку
    private void NextPoint()
    {
        ActivePoint = points.Dequeue();
        StartCoroutine(DefendTimer());
        if (ActivePoint == null)
        {
            GameOver();
            return;
        }
    }

    //вызываеться когда не осталось точек
    private void GameOver()
    {
        gameOver?.Invoke();

        if (DefendedPoint <= 0)
        {
            LoseUI.gameObject.SetActive(true);
            LoseAudio.Play();
            StopAllCoroutines();
        }
        else
        {
            WinUI.gameObject.SetActive(true);
            StartCoroutine (Win(DefendedPoint));
        }
    }

    //Поочерёдное появление звёздочек и сохранение прогресса при победе
    private IEnumerator Win(int count)
    {
        Transform star1 = WinUI.transform.Find("Stars").transform.GetChild(0);
        Transform star2 = WinUI.transform.Find("Stars").transform.GetChild(1);
        Transform star3 = WinUI.transform.Find("Stars").transform.GetChild(2);

        Debug.Log(count);

        if(LevelSaveData._needSave)
        {
            ProgressLoader.SetLevelStars(LevelSaveData._level, count);
            ProgressLoader.SetUnlockLevelState(LevelSaveData._level + 1);
        }

        yield return new WaitForSeconds(1);
        star1.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        if(count>1) star2.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        if (count>2) star3.gameObject.SetActive(true);

        StopAllCoroutines();
    }

    #endregion
}