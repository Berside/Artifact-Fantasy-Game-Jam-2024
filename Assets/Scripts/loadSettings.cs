using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class loadSettings : MonoBehaviour
{
    public Settings gameSettings;

    // Start is called before the first frame update
    void Start()
    {
        gameSettings = new Settings();
        gameSettings = JsonUtility.FromJson<Settings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));
        AudioListener.volume = gameSettings.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
