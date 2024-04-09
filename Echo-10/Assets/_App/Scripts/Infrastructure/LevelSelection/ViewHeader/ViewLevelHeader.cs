using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace _App.Scripts.Infrastructure.LevelSelection.ViewHeader
{
    public class ViewLevelHeader : MonoBehaviour
    {
        [SerializeField] private Button buttonNext;
        [SerializeField] private Button buttonPrev;

        private void Awake()
        {
            buttonNext.onClick.AddListener(() => { OnNextLevel?.Invoke(); });

            buttonPrev.onClick.AddListener(() => { OnPrevLevel?.Invoke(); });
        }

        public event Action OnNextLevel;
        public event Action OnPrevLevel;

        public void Cleanup()
        {
            OnNextLevel = null;
            OnPrevLevel = null;
        }
    }
}