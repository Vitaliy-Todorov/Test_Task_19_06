using Infrastructure.DataServiceNamespace;
using Infrastructure.StaticDataServiceNamespace.StaticData.LevelStaticData;
using ProjectContext;
using ProjectContext.StaticDataServiceNamespace;
using ProjectContext.StaticDataServiceNamespace.StaticData.LevelStaticData;
using ProjectContext.WindowsManager;
using SceneContext;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Hud
{
    public class Menu : Window
    {
        [SerializeField] private Button _play;
        [SerializeField] private Button _exit;

        private TimeController _timeController;
        private GameWindowsManager _gameWindowsManager;
        private WaveController _waveController;
        private StaticDataService _staticDataService;
        private DataService _dataService;
        private GameModelStaticData _modelStaticData;

        [Inject]
        private void Construct(TimeController timeController,
            GameWindowsManager gameWindowsManager,
            StaticDataService staticDataService,
            DataService dataService,
            WaveController waveController)
        {
            _timeController = timeController;
            _gameWindowsManager = gameWindowsManager;
            _staticDataService = staticDataService;
            _dataService = dataService;
            _waveController = waveController;
            
            _modelStaticData = _staticDataService.GetGameModelStaticData(GameModelName.GameModelTest);
        }
    
        private void Awake()
        {
            _play.onClick.AddListener(_gameWindowsManager.Close);
            _exit.onClick.AddListener(Application.Quit);
        }

        public override void Open()
        {
            _timeController.Pause();
            gameObject.SetActive(true);
        }

        public override void Close()
        {
            _waveController.SetWavesCount(_modelStaticData.StartWavesCount);
            _timeController.Play();
            gameObject.SetActive(false);
        }
    }
}