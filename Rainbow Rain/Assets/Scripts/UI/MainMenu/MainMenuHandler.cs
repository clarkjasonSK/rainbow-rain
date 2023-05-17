using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuHandler: Handler
{
    [SerializeField] private MainMenuRefs _main_menu_refs;

    public override void Initialize()
    {
        if (_main_menu_refs is null)
            _main_menu_refs = GetComponent<MainMenuRefs>();

        _main_menu_refs.TitlePanel.transform.position = _main_menu_refs.MidPanel.transform.position;
        _main_menu_refs.LevelPanel.transform.position = _main_menu_refs.TopPanel.transform.position;

        //_level_panel.SetActive(false);

        AddEventObservers();
        //DontDestroyOnLoad(this.transform);
    }
    public override void AddEventObservers()
    {
        //EventBroadcaster.Instance.AddObserver(EventKeys.PLAY_PRESSED, OnPlayPressed);
        //EventBroadcaster.Instance.AddObserver(EventKeys.LEVEL_BACK_PRESSED, OnBackLevelPressed);
    }


    /*
    public void ToggleVisibility()
    {
        _main_canvas.SetActive( _main_canvas.activeInHierarchy == true ? false : true);
    }*/


}
