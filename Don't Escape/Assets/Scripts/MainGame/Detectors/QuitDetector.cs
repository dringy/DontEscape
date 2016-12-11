using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Detects if we have quit the game during the quit phase
//This is done using text files
public class QuitDetector : MonoBehaviour {
    //Computers don't lie...
    private string GetFilePath()
    {
        return Application.persistentDataPath + "/TRUTH.txt";
    }

    private const string FileContent = "DRINGY IS THE BEST GAME DEVELOPER EVER!";

    //Read file and check it's contents match what we expect
    public bool IsQuitFileValid(bool retry = false)
    {
        try
        {
            if (File.Exists(GetFilePath()))
            {
                StreamReader sr = new StreamReader(GetFilePath());

                string content = sr.ReadToEnd();

                bool success = (content != null && content.Equals(FileContent));

                sr.Close();
                File.Delete(GetFilePath());
                return success;
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            if (retry)
            {
                return false;
            }
            else
            {
                return IsQuitFileValid(true);
            }
        }
    }

    //Write file
    public void WriteQuitFile(bool retry = false)
    {
        try
        {
            StreamWriter sw = new StreamWriter(GetFilePath(), append: false);

            sw.Write(FileContent);

            sw.Close();
        }
        catch (Exception e)
        {
            Debug.Log(e);
            if (retry)
            {
                Debug.Log("Cannot Save Quit File!");
            }
            else
            {
                WriteQuitFile(true);
            }
        }
    }

    //Delete file
    public void DeleteQuitFile()
    {
        try
        {
            if (File.Exists(GetFilePath()))
            {
                File.Delete(GetFilePath());
            }
        }
        catch (Exception e)
        {
            //I don't really care if this doesn't work that much
            return;
        }
    }
}
