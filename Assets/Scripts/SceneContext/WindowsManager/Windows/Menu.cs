using Infrastructure.StaticDataServiceNamespace.StaticData.LevelStaticData;
using ProjectContext;
using ProjectContext.StaticDataServiceNamespace;
using ProjectContext.StaticDataServiceNamespace.StaticData.LevelStaticData;
using ProjectContext.WindowsManager;
using SceneContext;
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

        [Inject]
        private void Construct(TimeController timeController, GameWindowsManager gameWindowsManager, StaticDataService staticDataService, WaveController waveController)
        {
            _timeController = timeController;
            _gameWindowsManager = gameWindowsManager;
            _staticDataService = staticDataService;
            _waveController = waveController;
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
            GameModelStaticData modelStaticData = _staticDataService.GetGameModelStaticData(GameModelName.GameModelTest);
            _waveController.SetWavesCount(modelStaticData.StartWavesCount);
            _timeController.Play();
            gameObject.SetActive(false);
        }
    }
}