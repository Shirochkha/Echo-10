/*using _App.Scripts.Infrastructure.GameCore.States.SetupState;
using _App.Scripts.Infrastructure.LevelSelection;
using System.Threading.Tasks;

namespace Assets._App.Scripts.Scenes.SceneLevels.States.Setup
{
    public class HandlerSetupLevel : IHandlerSetupLevel
    {
        private readonly IProviderFillwordLevel _providerFillwordLevel;  // заменить на SO
        private readonly IServiceLevelSelection _serviceLevelSelection;

        public HandlerSetupFillwords(IProviderFillwordLevel providerFillwordLevel,
            IServiceLevelSelection serviceLevelSelection,
            ViewGridLetters viewGridLetters, ContainerGrid containerGrid)
        {
            _providerFillwordLevel = providerFillwordLevel;
            _serviceLevelSelection = serviceLevelSelection;
            _viewGridLetters = viewGridLetters;
            _containerGrid = containerGrid;
        }

        private int _lastLevelIndex;
        public Task Process()
        {
            GridFillWords model = _providerFillwordLevel.LoadModel(_serviceLevelSelection.CurrentLevelIndex);

            while (model == null)
            {
                int bias = _lastLevelIndex < _serviceLevelSelection.CurrentLevelIndex ? 1 : -1;
                _serviceLevelSelection.UpdateSelectedLevel(_serviceLevelSelection.CurrentLevelIndex + bias);
                model = _providerFillwordLevel.LoadModel(_serviceLevelSelection.CurrentLevelIndex);
            }

            _viewGridLetters.UpdateItems(model);
            _containerGrid.SetupGrid(model, _serviceLevelSelection.CurrentLevelIndex);
            _lastLevelIndex = _serviceLevelSelection.CurrentLevelIndex;
            return Task.CompletedTask;
        }
    }
}*/