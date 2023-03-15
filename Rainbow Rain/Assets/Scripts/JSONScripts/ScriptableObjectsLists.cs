using System;
using System.Collections.Generic;

[Serializable]
public class ScriptableObjectsLists //SO == Scriptable Objects
{
    public List<LevelSO> LevelSOList;
    public List<PatternSO> PatternSOList;
    public List<ProjectileSO> ProjectileSOList;

}

public abstract class ScriptableObjectKey{
    public int SOID;
    public string SOFileName;
}

[Serializable]
public class LevelSO : ScriptableObjectKey
{

}

[Serializable]
public class PatternSO : ScriptableObjectKey
{

}

[Serializable]
public class ProjectileSO : ScriptableObjectKey
{

}

