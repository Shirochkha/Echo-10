using System.Collections;
using System.Collections.Generic;
using _App.Scripts.Libs.ServiceLocator;
using UnityEngine;

namespace _App.Scripts.Libs.Installer
{
    public class MonoContext : MonoBehaviour
    {
        public List<MonoInstaller> installers = new();

        private readonly List<IAwakeable> _awakeables = new();
        private readonly List<IInitializable> _initializables = new();
        private readonly List<IUpdatable> _updatables = new();

        private void Awake()
        {
            foreach (var awakeables in _awakeables) awakeables.Awake();
        }

        private void Start()
        {
            Setup();

            StartCoroutine(DelayStart());
        }

        private void Update()
        {
            foreach (var updatable in _updatables) updatable.Update();
        }

        private IEnumerator DelayStart()
        {
            yield return null;
            Init();
        }

        private void Setup()
        {
            var container = BuildContainer();
            _awakeables.AddRange(container.GetServices<IAwakeable>());
            _initializables.AddRange(container.GetServices<IInitializable>());
            _updatables.AddRange(container.GetServices<IUpdatable>());
        }

        private ServiceContainer BuildContainer()
        {
            var container = new ServiceContainer();
            foreach (var installer in installers) installer.InstallBindings(container);

            return container;
        }

        private void Init()
        {
            foreach (var initializable in _initializables) initializable.Init();
        }
    }
}