using _App.Scripts.Infrastructure.GameCore.Commands.SwitchLevel;
using _App.Scripts.Infrastructure.GameCore.Controllers;
using _App.Scripts.Infrastructure.GameCore.States.SetupState;
using _App.Scripts.Infrastructure.GameCore.States;
using _App.Scripts.Infrastructure.GameCore.Systems;
using _App.Scripts.Infrastructure.LevelSelection.ViewHeader;
using _App.Scripts.Infrastructure.LevelSelection;
using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.StateMachine;
using _App.Scripts.Libs.Systems;
using System;
using System.Collections.Generic;
using _App.Scripts.Libs.ServiceLocator;
using Assets._App.Scripts.Scenes.SceneLevels.States;
using Assets._App.Scripts.Scenes.SceneLevels.Systems;
using Assets._App.Scripts.Scenes.SceneLevels.Features;

namespace Assets._App.Scripts.Scenes.SceneLevels.Installers
{
    internal class InstallerLevelsEntryPoint : MonoInstaller
    {
        public override void InstallBindings(ServiceContainer container)
        {
            var gameStateMachine = BuildStateMachine(container);
            var controllerEntryPoint = new ControllerEntryPoint<StateLevelMenu>(gameStateMachine);

            container.SetService<IInitializable, ControllerEntryPoint<StateLevelMenu>>(controllerEntryPoint);
            container.SetService<IUpdatable, ControllerEntryPoint<StateLevelMenu>>(controllerEntryPoint);
        }

        private GameStateMachine BuildStateMachine(ServiceContainer container)
        {
            var gameStateMachine = new GameStateMachine();

            gameStateMachine.AddState(CreateLevelMenuState(container));

            //gameStateMachine.AddState(CreateStateSetupLevel(container));
            gameStateMachine.AddState(CreateProcessState(container, gameStateMachine));
            // gameStateMachine.AddState(CreateRestartState(container, gameStateMachine));

            return gameStateMachine;
        }

        private GameState CreateLevelMenuState(ServiceContainer container)
        {
            return new StateLevelMenu((container.Get<LevelsMenuUI>()));
        }

        /*private GameState CreateRestartState(ServiceContainer container, GameStateMachine gameStateMachine)
        {

            return new StateRestartLevel(container.Get<SystemHealthBarChange>());
            //return new StateRestartLevel(container.Get<ViewGridLetters>());
        }*/

        /*private GameState CreateStateSetupLevel(ServiceContainer container)
        {
            var handlers = new List<IHandlerSetupLevel>
            {
                *//*new HandlerSetupLevel(container.Get<IProviderFillwordLevel>(),
                    container.Get<IServiceLevelSelection>(),
                    container.Get<ViewGridLetters>(),
                    container.Get<ContainerGrid>()),*//*
            };

            var handlerStateSetup = new HandlerSetupLevelContainer(handlers);

            var stateSetupLevel = new StateSetupLevel(handlerStateSetup);

            return stateSetupLevel;
        }*/

        private GameState CreateProcessState(ServiceContainer container, GameStateMachine gameStateMachine)
        {
            var systems = new SystemsGroup();
            systems.AddSystems(container.GetServices<ISystem>());

            var commandSwitchLevel = new CommandSwitchLevelState<StateRestartLevel>(
                container.Get<IServiceLevelSelection>(),
                gameStateMachine);

            systems.AddSystem(new SystemProcessNextLevel(
                container.Get<ViewLevelHeader>(),
                commandSwitchLevel));

            var stateProcess = new StateProcessGame(systems);
            return stateProcess;
        }
    }
}
