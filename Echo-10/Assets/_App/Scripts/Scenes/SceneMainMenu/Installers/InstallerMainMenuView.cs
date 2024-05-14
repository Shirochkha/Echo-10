using _App.Scripts.Infrastructure.GameCore.Controllers;
using _App.Scripts.Infrastructure.GameCore.States.LoadState;
using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.ServiceLocator;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _App.Scripts.Scenes.SceneMainMenu.Installers
{
    public class InstallerMainMenuView : MonoInstaller
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonExit;

        [SerializeField] private Image _imageToChange;
        [SerializeField] private Sprite _newSpriteOnHover;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            var buttonImageChange = new ButtonImageChange(_imageToChange, _newSpriteOnHover);

            _buttonStart.gameObject.AddComponent<EventTrigger>();
            _buttonStart.gameObject.GetComponent<EventTrigger>().triggers.Add(CreateEventTriggerEntry(EventTriggerType.PointerEnter, () => buttonImageChange.OnPointerEnter()));
            _buttonStart.gameObject.GetComponent<EventTrigger>().triggers.Add(CreateEventTriggerEntry(EventTriggerType.PointerExit, () => buttonImageChange.OnPointerExit()));

            _buttonExit.gameObject.AddComponent<EventTrigger>();
            _buttonExit.gameObject.GetComponent<EventTrigger>().triggers.Add(CreateEventTriggerEntry(EventTriggerType.PointerEnter, () => buttonImageChange.OnPointerEnter()));
            _buttonExit.gameObject.GetComponent<EventTrigger>().triggers.Add(CreateEventTriggerEntry(EventTriggerType.PointerExit, () => buttonImageChange.OnPointerExit()));

            serviceContainer.SetServiceSelf(buttonImageChange);
        }


        //необходимо для срабатывания смены спрайта при наведении
        private EventTrigger.Entry CreateEventTriggerEntry(EventTriggerType type, UnityEngine.Events.UnityAction action)
        {
            var entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback.AddListener((eventData) => action());
            return entry;
        }
    }
}
