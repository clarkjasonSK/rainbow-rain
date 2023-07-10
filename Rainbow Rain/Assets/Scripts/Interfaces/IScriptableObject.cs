public interface IScriptableObject
{
    void InstantiateData<T>(T JSONData) where T : JSONData;
}
