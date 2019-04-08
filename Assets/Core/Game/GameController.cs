using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using System.Linq;

public class GameController : PersistentSingleton<GameController>
{
    public delegate void OnSpin(bool forcePos);
    public event OnSpin _spin;
    public delegate void OnEndMath(int score);
    public event OnEndMath _OnEndMath;
    public delegate void OnFindMathc();
    public event OnFindMathc _findMatch;
    public List<TileView> AllElement = new List<TileView>();
    public List<Line> AllLine = new List<Line>();
    private GameObject ElementPrefab;
    private float gridstartx;
    private float gridstarty;
    private void Start()
    {
        DataHandler.Instance.onInit += Init;
    }
    public void Init()
    {
        ElementPrefab = DataHandler.Instance.GetElement;
        CreateGrig();
    }
    private void CreateGrig()
    {
        gridstartx = gameObject.GetComponent<Transform>().position.x - 3.5f;
        gridstarty = gameObject.GetComponent<Transform>().position.y + 3f;
        for (int y = 0; y< Constant.YGrid;y++)
        {
            Line line = new Line();
            for (int x = 0; x < Constant.XGrid; x++)
            {              
                GameObject newslot = LeanPool.Spawn(ElementPrefab);
                TileView tileView = newslot.GetComponent<TileView>();
                tileView.Init(new Point(x, y));
                newslot.transform.position = new Vector3(gridstartx + x*1.7f, gridstarty - y*2, 1);
                newslot.name = "slot_" + x.ToString() + "_" + y.ToString();
                newslot.transform.parent = gameObject.transform;
                AllElement.Add(tileView);
                line.AddElement(tileView);
            }
            AllLine.Add(line);
            line.Init();
        }
    }
    public void Spin()
    {
        UIManager.Instance.EnableFillButton(false);
        for (int i = 0; i < AllLine.Count; i++)
        {
            StartCoroutine(RepeatAction(0.1f, 8 + i * 3, () =>
            {
                if (_spin != null) _spin.Invoke(false);
            }, () =>
            {
            }, () =>
            {
                if (_spin != null) _spin.Invoke(false);
            }));
        }
        StartCoroutine(DelayAction(2.2f, () =>
        {
            UIManager.Instance.EnableFillButton(true);
            FindMatch();
        }));
    }
    IEnumerator DelayAction(float dTime, System.Action callback)
    {
        yield return new WaitForSeconds(dTime);
        callback();
    }
    IEnumerator RepeatAction(float dTime, int count, System.Action callback1, System.Action callback2, System.Action callback3)
    {
        if (count > 1) callback1();
        else callback3();
        if (--count > 0)
        {
            if (count == 1) callback2();
            yield return new WaitForSeconds(dTime);
            StartCoroutine(RepeatAction(dTime, count, callback1, callback2, callback3));
        }
    }
    public TileView GetElement(Point point)
    {
       return AllElement.FirstOrDefault(tile => tile.Cell == point);
    }
    public void FindMatch()
    {
        if (_findMatch != null) _findMatch.Invoke();
    }
    public void IsMatch(List<TileView> elements)
    {
        UIManager.Instance.EnableFillButton(false);
        CombinationDraw combinationDraw = DataHandler.Instance.GetDrawCombination();
        combinationDraw.DrawComBination(elements);
        StartCoroutine(DelayAction(3f, () =>
        {
            EndMatch(combinationDraw);
        }));
    }
    private void EndMatch(CombinationDraw combination)
    {
        UIManager.Instance.EnableFillButton(true);
        DataHandler.Instance.ReturnDrawCombinatio(combination);
        if (_OnEndMath != null) _OnEndMath.Invoke(20);
    }
}
