using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TableScore : Table {
 
   protected override void ShowAll() {
        for(int i =0; i < rows.Count; ++i) {
            RowData(rows[i],i);
        }
    }
    protected override void RowData(Row row,int index) {
        SaveData save = GetSaveByIndex(index);
        row.GetText("Level", "lvl").text = ""+save.Level;
        row.GetText("Level", "LevelDifficulty").text = " Level - "+save.Difficulty;

        //row.GetText("Balls", "BallsText").text = " Date " + save.Date.Date;
        row.GetText("Balls", "CatchText").text = "" + save.BallsCatch;
        row.GetText("Balls", "LostText").text = "" + save.BallsLost;
    }








}
