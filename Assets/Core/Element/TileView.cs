using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileView : MonoBehaviour, Initable
{
    #region PrivateFields
    [SerializeField] private Point cell = Point.Zero;
    [SerializeField] private int type;
    private SpriteRenderer tileRenderer;
    #endregion
    public delegate void OnCompleteMove();
    public OnCompleteMove onCompleteMove;
    [SerializeField] private Sprite ElementSprite, ElementSpriteMove;
    public Point Cell { get => cell; set => cell = value; }
    public int Type { get => type; set => type = value; }
    public void OnInit()
    {
    }
    public void DeInit()
    {

    }
    public void Init(Point point)
    {
        tileRenderer = GetComponent<SpriteRenderer>();
        Cell = point;
        Init();
    }
    public void Init()
    {
        int somerandom = Random.Range(1, Constant.ElementCount) % Constant.ElementCount;
        type = somerandom;
        ElementSprite = DataHandler.Instance.GetElementSprite(type);
        ElementSpriteMove = DataHandler.Instance.GetMoveElementSprite(type);
        tileRenderer.sprite = ElementSprite;
    }
    void Start()
    {

    }
    void Update()
    {

    }
    #region Move
    public void MoveTo()
    {
        tileRenderer.sprite = ElementSpriteMove;
        if (Cell.y < Constant.YGrid - 1)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 2, transform.localPosition.z);
            Cell.y++;
        }
        else if (Cell.y == Constant.YGrid - 1)
        {
            Cell.y = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, 3, transform.localPosition.z);
        }
        Init();
        //Cell.y = Cell.y == Constant.YGrid - 1 ? 0: Cell.y++;

    }
    //Move end
    private void Complete()
    {
        if (onCompleteMove != null)
            onCompleteMove.Invoke();
    }
    #endregion
}
