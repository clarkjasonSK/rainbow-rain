using System;
using System.Collections.Generic;

[Serializable]
public class LevelData
{
    public int LevelID;
    public bool LevelIsEndless;
    public List<int> LevelPatterns;
}
