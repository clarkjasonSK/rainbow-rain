public interface IScriptableObject
{
    void InstantiateData<T>(T gameData) where T : GameData;
}
