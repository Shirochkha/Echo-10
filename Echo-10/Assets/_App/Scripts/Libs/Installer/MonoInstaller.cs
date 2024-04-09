using _App.Scripts.Libs.ServiceLocator;
using UnityEngine;

namespace _App.Scripts.Libs.Installer
{
    public abstract class MonoInstaller : MonoBehaviour
    {
        public abstract void InstallBindings(ServiceContainer serviceContainer);
    }
}