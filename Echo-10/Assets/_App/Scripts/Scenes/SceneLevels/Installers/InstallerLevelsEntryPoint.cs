﻿using _App.Scripts.Infrastructure.GameCore.Commands.SwitchLevel;
using _App.Scripts.Infrastructure.GameCore.Controllers;
using _App.Scripts.Infrastructure.GameCore.States.LoadState;
using _App.Scripts.Infrastructure.GameCore.States;
using _App.Scripts.Infrastructure.GameCore.Systems;
using _App.Scripts.Infrastructure.LevelSelection.ViewHeader;
using _App.Scripts.Infrastructure.LevelSelection;
using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.StateMachine;
using _App.Scripts.Libs.Systems;
using System.Collections.Generic;
using _App.Scripts.Libs.ServiceLocator;
using Assets._App.Scripts.Scenes.SceneLevels.States;
using Assets._App.Scripts.Scenes.SceneLevels.Features;
using Assets._App.Scripts.Scenes.SceneLevels.States.Load;
using Assets._App.Scripts.Infrastructure.SceneManagement.Config;
using UnityEngine;
using Assets._App.Scripts.Scenes.SceneLevels.Systems;

namespace Assets._App.Scripts.Scenes.SceneLevels.Installers
{
    internal class InstallerLevelsEntryPoint : MonoInstaller
    {
        [SerializeField] private ConfigLevel _configLevel;

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
            gameStateMachine.AddState(CreateLoadLevelState(container));
            gameStateMachine.AddState(CreateRestartState(container));

            gameStateMachine.AddState(CreateProcessState(container, gameStateMachine));
            

            return gameStateMachine;
        }

        private GameState CreateLevelMenuState(ServiceContainer container)
        {
            return new StateLevelMenu((container.Get<LevelsMenuUI>()));
        }

        private GameState CreateLoadLevelState(ServiceContainer container)
        {
            var handlers = new List<IHandlerLoadLevel>
            {
                new HandlerLoadCutScene(_configLevel,
                    container.Get<Sevices.ServiceLevelSelection>(),
                    container.Get<CutSceneManager>()),
                new HandlerLoadObjects(_configLevel,
                    container.Get<Sevices.ServiceLevelSelection>())
                
            };

            var handlerStateSetup = new HandlerLoadLevelContainer(handlers);

            var stateLoadLevel = new StateLoadLevel(handlerStateSetup);

            return stateLoadLevel;
        }

        private GameState CreateRestartState(ServiceContainer container)
        {

            return new StateRestartLevel(container.Get<SystemHealthBarChange>(),
                    container.Get<SystemColliderRadiusChange>(),
                    container.Get<SystemPlayerMovement>(),
                    container.Get<SystemAddCoin>(),
                    _configLevel);
        }

        private GameState CreateProcessState(ServiceContainer container, GameStateMachine gameStateMachine)
        {
            var systems = new SystemsGroup();
            systems.AddSystems(container.GetServices<ISystem>());

            /*var commandSwitchLevel = new CommandSwitchLevelState<StateRestartLevel>(
                container.Get<IServiceLevelSelection>(),
                gameStateMachine);

            systems.AddSystem(new SystemProcessNextLevel(
                container.Get<ViewLevelHeader>(),
                commandSwitchLevel));*/

            var stateProcess = new StateProcessGame(systems);
            return stateProcess;
        }
    }
}
