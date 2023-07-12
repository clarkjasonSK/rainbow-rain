using System;
using System.Collections.Generic;

[Serializable]
public class LevlJSONData : JSONData
{
    public bool LevelIsEndless;
    public List<int> LevelPatterns;
}