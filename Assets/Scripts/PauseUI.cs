using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    public Button resumeBtn;
    public Button saveBtn;
    public Button quitBtn;
    public UIController UICtrl;
    private void Awake()
    {
        AddListener();
      
    }
    private void AddListener()
    {
        resumeBtn.onClick.AddListener(Resume);
        saveBtn.onClick.AddListener(Save);
        quitBtn.onClick.AddListener(Quit);
      
    }

    private void Resume()
    {
        UICtrl.ToggleByKey(UICtrl.pauseUI);     
    }
    private void Save()
    {
        GameDataManager.instance.saveGame.Save();
    }
    private void Quit()
    {
       Application.Quit();
    }
}
