using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveAndroid : SaveMode
{
    [SerializeField]
    string saveName = "data";

    public void FileExists(BinaryFormatter bf)
    {
        if (!File.Exists(Application.persistentDataPath+"/"+ saveName)) {
            FileStream st = File.Create(Application.persistentDataPath + "/" + saveName);
            SaveType data = new SaveType();
            bf.Serialize(st, data);
            st.Close();
        }
    }
    public override SaveData GetLevel()
    {
        return LevelExist(Difficulty);
    }

    SaveData LevelExist(string difficulty) {
        SaveType data = Open();
        if (LoadFrom == "Save")
        {
            foreach (SaveData s in data.SaveList)
            {
                if (s.Difficulty == difficulty)
                {
                    return s;
                }
            }
        }
        SaveData save = new SaveData();
        save.Difficulty = difficulty;
        save.Date = DateTime.Now;
        return save;
    }
    public override SaveType Open()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileExists(bf);
        FileStream st = File.Open(Application.persistentDataPath + "/" + saveName, FileMode.Open, FileAccess.Read);
        SaveType data = bf.Deserialize(st) as SaveType;
        st.Close();
        return data;
    }
    public override SaveData Open(string difficulty)
    {
        return LevelExist(difficulty);
    }
    public override void Save(SaveData save)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileExists(bf);
        FileStream st = File.Open(Application.persistentDataPath + "/" + saveName, FileMode.Open, FileAccess.Read);
        SaveType data = bf.Deserialize(st) as SaveType;
        st.Close();
        data = FilterData(data, save);
        st = File.OpenWrite(Application.persistentDataPath + "/" + saveName);
        bf.Serialize(st, data);
        st.Close();
    }
    SaveType FilterData(SaveType data,SaveData save) {
        int index = 0;
        bool overlap = false;
        for(int i=0; i < data.SaveList.Count; ++i)
        {
            if (data.SaveList[i].Difficulty == save.Difficulty) {
                overlap = true;
                index = i;
                break;
            }
        }
        if (overlap)
        {
            data.SaveList[index] = save;
        }
        else {
            data.SaveList.Add(save);
        }
        return data;
    }

















}
