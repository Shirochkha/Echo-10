namespace Assets._App.Scripts.Scenes.SceneLevels.Sevices
{
    public class ServiceLevelSelection
    {
        public int? SelectedLevelId { get; private set; }

        public void SetSelectedLevel(int id)
        {
            SelectedLevelId = id;
        }
    }

}
