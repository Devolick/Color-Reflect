using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class SaveType {

    List<SaveData> saveList;


    public List<SaveData> SaveList {
        get { return saveList; }
    }
    public SaveData this[string difficulty] {
        get {
            if (saveList.Count > 0) {
                foreach (SaveData save in saveList) {
                    if (save.Difficulty == difficulty) {
                        return save;
                    }
                }
            }
            return null;
        }
    }

    public SaveType() {
        saveList = new List<SaveData>();
    }




















}
