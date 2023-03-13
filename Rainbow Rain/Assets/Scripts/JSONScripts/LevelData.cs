using System;
using System.Collections.Generic;

[Serializable]
public class LevelData : GameData
{
    public bool LevelIsEndless;
    public List<int> LevelPatterns;
}