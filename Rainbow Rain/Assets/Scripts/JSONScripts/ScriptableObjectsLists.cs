using System;
using System.Collections.Generic;

[Serializable]
public class ScriptableObjectsLists //SO == Scriptable Objects
{
    public List<LevelKey> LevelKeyList;
    public List<PatternKey> PatternKeyList;
    public List<ProjectileKey> ProjectileKeyList;

}

public abstract class DataKey{
    public int SOID;
    public string SOFileName;
}

[Serializable]
public class LevelKey : DataKey
{

}

[Serializable]
public class PatternKey : DataKey
{

}

[Serializable]
public class ProjectileKey : DataKey
{

}

