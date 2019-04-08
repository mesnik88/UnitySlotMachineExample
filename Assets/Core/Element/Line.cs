using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[System.Serializable]
public class Line
{
    [SerializeField] private List<TileView> Elements = new List<TileView>();
    [SerializeField] private List<TileView> MatchElemt = new List<TileView>();
    [SerializeField] private int comb;
    private int Y;
    public delegate void HaveMatch(List<TileView> elements);
    public HaveMatch haveMatch;
    public void Init()
    {
        GameController.Instance._spin += Spin;
        GameController.Instance._findMatch += FindMatches;
        haveMatch += GameController.Instance.IsMatch;
        Y = Elements[0].Cell.y;
    }
    public void FindMatches()
    {
        MatchElemt.Clear();
        TileView prew = Elements[0];
        MatchElemt.Add(prew);
        for (int i = 1; i < Elements.Count; i++)
        {
            if(Elements[i].Type == prew.Type)
            {
                MatchElemt.Add(Elements[i]);
                prew = Elements[i];
                if (MatchElemt.Count > 2) i = Elements.Count;
            }
            else
            {
                MatchElemt.Clear();
                prew = Elements[i];
                MatchElemt.Add(prew);
            }
        }
        if (MatchElemt.Count > 2) if (haveMatch != null) haveMatch.Invoke(MatchElemt);
    }
    private void Spin(bool forcePos)
    {
        int MaxElement = Constant.MaxElement;
        for (int i = 0; i < Elements.Count; i++)
        {
            Elements[i].MoveTo();
        }
    }
    public void AddElement(TileView tile)
    {
        Elements.Add(tile);
    }
    public void RemoveElement(TileView tile)
    {
        Elements.Remove(tile);
    }
    public void RemoveElement(Point point)
    {
        Elements.Remove(Elements.FirstOrDefault(tile => tile.Cell == point));
    }
}
