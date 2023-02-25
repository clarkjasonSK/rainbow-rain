using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorDictionary
{
    private static List<Color> _color_list;

    public static Color _cyan = new Color(.5f, 1, 1, 1);
    public static Color _magenta = new Color(1, .5f, 1, 1);
    public static Color _yellow = new Color(1, 1, .5f, 1);

    public static void InitializeColors()
    {
        _color_list = new List<Color>();
        _color_list.Add(_cyan);
        _color_list.Add(_magenta);
        _color_list.Add(_yellow);
        //Debug.Log("Color population: " + _color_list.Count);
    }

    public static Color getSpecifiedColor(string color)
    {
        switch (color)
        {
            case ColorNames.CYAN:
                return _cyan;
            case ColorNames.MAGENTA:
                return _magenta;
            case ColorNames.YELLOW:
                return _yellow;
        }
        return new Color(0, 0, 0, 0);
    }

    public static Color getRandomColor()
    {
        return _color_list[Random.Range(0, _color_list.Count)];
    }
    public static Color getRandomColor(Color excemptColor)
    {
        List<Color> tempList = new List<Color>(_color_list);
        tempList.Remove(excemptColor);
        return tempList[Random.Range(0, tempList.Count)];
    }
}

public static class ColorNames
{
    public const string CYAN = " CYAN";
    public const string MAGENTA = " MAGENTA";
    public const string YELLOW = " YELLOW";
}