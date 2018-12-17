using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Row : Base {

    public string LevelDifficulty {
        get; set;
    }

    public Text GetText(string rowName,string cell) {
       return FindChild(rowName,cell).GetComponent<Text>();
    }















}
