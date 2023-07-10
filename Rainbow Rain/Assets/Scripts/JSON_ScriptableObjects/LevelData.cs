using System;
using System.Collections.Generic;

[Serializable]
public class LevelData : JSONData
{
    public bool LevelIsEndless;
    public List<int> LevelPatterns;
}