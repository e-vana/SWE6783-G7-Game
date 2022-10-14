using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveToJson : MonoBehaviour
{
    public stageManager stageManagement;

    public void SaveStageManagementToJson()
    {
        string json = JsonUtility.ToJson(stageManagement, true);
        File.WriteAllText(Application.dataPath + "/" + "playSession" + ".json", json);

    }
    private void OnApplicationQuit()
    {
        Debug.Log("fired on app quit");
        SaveStageManagementToJson();
    }
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

    }

}
