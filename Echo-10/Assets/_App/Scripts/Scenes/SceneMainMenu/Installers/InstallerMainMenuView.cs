using _App.Scripts.Libs.Installer;
using _App.Scripts.Libs.ServiceLocator;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _App.Scripts.Scenes.SceneMainMenu.Installers
{
    public class InstallerMainMenuView : MonoInstaller
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonExit;

        [SerializeField] private Image _imageToChange;
        [SerializeField] private Sprite _newSpriteOnHover;

        public override void InstallBindings(ServiceContainer serviceContainer)
        {
            var buttonImageChange = new ButtonImageChange(_imageToChange, _newSpriteOnHover);

            AddEventTrigger(_buttonStart.gameObject, buttonImageChange);
            AddEventTrigger(_buttonSettings.gameObject, buttonImageChange);
            AddEventTrigger(_buttonExit.gameObject, buttonImageChange);

            serviceContainer.SetServiceSelf(buttonImageChange);
        }


        //необходимо для срабатывания смены спрайта при наведении

        private void AddEventTrigger(GameObject button, ButtonImageChange buttonImageChange)
        {
            var eventTrigger = button.AddComponent<EventTrigger>();
            eventTrigger.triggers.Add(CreateEventTriggerEntry(EventTriggerType.PointerEnter, () => buttonImageChange.OnPointerEnter()));
            eventTrigger.triggers.Add(CreateEventTriggerEntry(EventTriggerType.PointerExit, () => buttonImageChange.OnPointerExit()));
        }
        private EventTrigger.Entry CreateEventTriggerEntry(EventTriggerType type, UnityEngine.Events.UnityAction action)
        {
            var entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback.AddListener((eventData) => action());
            return entry;
        }
    }
}
