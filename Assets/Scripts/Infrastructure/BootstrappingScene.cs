using System;
using Infrastructure.DataServiceNamespace;
using ProjectContext.WindowsManager;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class BootstrappingScene : MonoBehaviour
    {
        private GameWindowsManager _gameWindowsManager;
        private DataService _dataService;

        [Inject]
        private void Construct(GameWindowsManager gameWindowsManager, DataService dataService)
        {
            _gameWindowsManager = gameWindowsManager;
            _dataService = dataService;
        }

        private void Awake()
        {
            _gameWindowsManager.Awake();
            _gameWindowsManager.OpenHud();
            _gameWindowsManager.Open(EWindow.Menu);
        }

        private void OnDestroy() => 
            _dataService.Save();
    }
}