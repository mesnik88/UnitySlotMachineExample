using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Lean.Pool;

public class DataHandler : PersistentSingleton<DataHandler>
{
    public delegate void OnInit();
    public OnInit onInit;
    private ElementData elementData;
    public GameObject GetElement
    {
        get
        {
            return elementData.ElementPrefab;
        }
    }
    public Sprite GetElementSprite(int type)
    {
        return elementData.ElementSprite.FirstOrDefault(sptr => sptr.Key == type.ToString()).Value;
    }
    public Sprite GetMoveElementSprite(int type)
    {
        string _type = type + "_" + type;
        return elementData.ElementSprite.FirstOrDefault(sptr => sptr.Key == _type).Value;

    }
    public CombinationDraw GetDrawCombination()
    {
        return LeanPool.Spawn(elementData.DrawCombinationPrefab, Vector3.zero, Quaternion.identity).GetComponent<CombinationDraw>();
    }
    public void ReturnDrawCombinatio(CombinationDraw combinationDraw)
    {
        LeanPool.Despawn(combinationDraw.gameObject);
    }
    protected override void Awake()
    {
        base.Awake();
        elementData = Resources.Load("ElementData") as ElementData;
        if (elementData != null)
        {
            if (onInit != null) onInit.Invoke();
            elementData.Init();
            GameController.Instance.Init();
        }
    }
}
