namespace Assets._App.Scripts.Scenes.SceneLevels.Sevices
{
    public interface IPersistence<T>
    {
        T Load();
        void Save(T obj);
    }
}
