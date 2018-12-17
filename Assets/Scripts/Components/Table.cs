using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public abstract class Table : Base {

    [SerializeField]
    protected List<Row> rows;
    protected override void _Awake()
    {
        base._Awake();
        GameInstance.Save.LoadFrom = "Save";
        rows = new List<Row>();
        FindRows();
    }
    protected override void _Start()
    {
        base._Start();
        ShowAll();
    }
   protected virtual void FindRows() {
        rows.AddRange(FindChilds<Row>());
    }
    protected abstract void ShowAll();
    protected abstract void RowData(Row row, int index);
    protected SaveData GetSaveByIndex(int index) {
        SaveData save = null;
        switch (index) {
            case 0:
                {
                    save = GameInstance.Save.Open("Easy");
                    break;
                }
            case 1:
                {
                    save = GameInstance.Save.Open("Normal");
                    break;
                }
            case 2:
                {
                    save = GameInstance.Save.Open("Hard");
                    break;
                }
        }
        return save;
    }







}
