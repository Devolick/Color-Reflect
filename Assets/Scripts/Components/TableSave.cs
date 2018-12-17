using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TableSave : Table {
    UIMenuSaveControl menuController;

    public RowSave RowSelect
    {
        get;
        protected set;
    }

    public override void AskYou(Base sender)
    {
        base.AskYou(sender);
        if (sender.name == "Save")
        {
            menuController = sender as UIMenuSaveControl;
            menuController.Reconnect(this);
        }
    }
    protected override void FindRows()
    {
        rows.AddRange(FindChilds<Row>());
        foreach (RowSave r in rows) {
            r.MyTable = this;
        }
    }
    protected override void ShowAll() {
        for(int i =0; i < rows.Count; ++i) {
            RowData(rows[i],i);
        }
    }
    protected override void RowData(Row row,int index) {
        SaveData save = GetSaveByIndex(index);
        row.LevelDifficulty = save.Difficulty;       
        row.GetText("Level", "lvl").text = ""+save.Level;
        row.GetText("Level", "LevelDifficulty").text = " Level - "+save.Difficulty;
        row.GetText("Date", "Text").text = " Date "+save.Date;
    }
    public void SwapSelect(RowSave row) {
        if (RowSelect != null)
        {
            RowSelect.Select = false;
            RowSelect = row;
        }
        else {
            RowSelect = row;
        }
    }







}
