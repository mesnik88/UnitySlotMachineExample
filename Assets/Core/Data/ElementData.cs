using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementData", menuName = "SlotMachine/CreateElementData", order = 1)]
public class ElementData : ScriptableObject
{
    public Dictionary<string, Sprite> ElementSprite = new Dictionary<string, Sprite>();
    public GameObject ElementPrefab,DrawCombinationPrefab;
    public void Init()
    {
        ElementPrefab = Resources.Load("SlotPrefab") as GameObject;
        DrawCombinationPrefab = Resources.Load("DrawCombination") as GameObject;
        var elemntsprite = Resources.LoadAll<Sprite>("ElementImage");
        for(int i = 0; i < elemntsprite.Length; i++)
        {
            ElementSprite.Add(elemntsprite[i].name, elemntsprite[i] as Sprite);
        }
    }
}
