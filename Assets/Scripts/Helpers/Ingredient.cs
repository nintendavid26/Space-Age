using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum IngredientUnit { Spoon, Cup, Bowl, Piece }

// Custom serializable class
[System.Serializable]
public class Ingredient
{
    [SerializeField]
    public string[] ListTest=new string[] { "a","b","c"};

    public string name;
    public int amount = 1;
    public IngredientUnit unit;
    
}