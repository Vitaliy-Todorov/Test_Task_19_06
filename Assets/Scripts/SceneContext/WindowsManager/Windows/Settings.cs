using Entities;
using Infrastructure.DataServiceNamespace;
using Infrastructure.StaticDataServiceNamespace.StaticData.LevelStaticData;
using ProjectContext.StaticDataServiceNamespace;
using ProjectContext.StaticDataServiceNamespace.StaticData.LevelStaticData;
using ProjectContext.WindowsManager;
using SceneContext;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace UI.Hud
{
    public class Settings : MonoBehaviour
    {
        [SerializeField, Space] private Slider _currentTime;
        [SerializeField] private TMP_Text _currentTimeText;
        
        [SerializeField, Space] private Slider _currentLevel;
        [SerializeField] private TMP_Text _currentLevelText;
        [SerializeField] private Button _updeteLevel;

        [SerializeField, Space] private Toggle _boostToggle;
        
        private DataService _dataService;
        private GameModelStaticData _modelStaticData;
        private WaveController _waveController;
        private EnemiesSpawner _enemiesSpawner;

        [Inject]
        private void Construct(StaticDataService staticDataService, DataService dataService, WaveController waveController, EnemiesSpawner enemiesSpawner)
        {
            _dataService = dataService;
            _waveController = waveController;
            _enemiesSpawner = enemiesSpawner;

            _modelStaticData = staticDataService.GetGameModelStaticData(GameModelName.GameModelTest);

            _currentTime.value = _dataService.CurrentTime / _modelStaticData.MaxTime;
            _currentTimeText.text = _dataService.CurrentTime.ToString();

            _currentLevel.value = _waveController.WavesCount / _dataService.MaxLevel;
            _currentLevel.maxValue = _dataService.MaxLevel;
            _currentLevelText.text = _waveController.WavesCount.ToString();
            
            _boostToggle.isOn = _dataService.Boost;
        }

        private void Awake()
        {
            _currentTime.onValueChanged.AddListener(ChangeTime);
            
            _currentLevel.onValueChanged.AddListener(value => Test(value));
            _updeteLevel.onClick.AddListener(UpdateLevel);
            
            _boostToggle.onValueChanged.AddListener(Boost);
        }

        private void Test(float value)
        {
            _currentLevelText.text = (value * _waveController.WavesCount).ToString();
        }

        private void Boost(bool value)
        {
            _dataService.Boost = value;
            _currentLevel.maxValue = _dataService.MaxLevel;
        }

        private void ChangeTime(float value)
        {
            _currentTimeText.text = (value * _modelStaticData.MaxTime / 60).ToString();
            _dataService.CurrentTime = value * _modelStaticData.MaxTime;
        }

        private void UpdateLevel()
        {
            _enemiesSpawner.ClearAllEnemies();
            _waveController.SetWavesCount((int) _currentLevel.value);
        }
    }
}