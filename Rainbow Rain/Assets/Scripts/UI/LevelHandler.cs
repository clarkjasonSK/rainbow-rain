using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private GameObject _level_button_template;
    [SerializeField] private Transform _level_content_transform;

    public void Awake()
    {
        Debug.Log("levelslist count: " + GameManager.Instance.LevelsList.Count);
        CreateLevelButtons(GameManager.Instance.LevelsList);
    }
    public void CreateLevelButtons(List<LevelKey> lvlList)
    {
        foreach(LevelKey lvlkey in lvlList)
        {
            GameObject newObj = Instantiate(_level_button_template, _level_content_transform);
            newObj.SetActive(true);
            LevelKeyDataUI newLevel = newObj.GetComponent<LevelKeyDataUI>();

            newLevel.LevelID = lvlkey.SOID;
            newLevel.LevelName = lvlkey.SOFileName;

            newObj.GetComponentInChildren<TextMeshProUGUI>().text = newLevel.LevelName;

        }
    }
}
