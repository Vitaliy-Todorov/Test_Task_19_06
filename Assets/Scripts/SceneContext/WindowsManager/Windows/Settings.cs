using System;
using Infrastructure.DataServiceNamespace;
using Infrastructure.StaticDataServiceNamespace.StaticData.LevelStaticData;
using ProjectContext.StaticDataServiceNamespace;
using ProjectContext.StaticDataServiceNamespace.StaticData.LevelStaticData;
using SceneContext;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Hud
{
    public class Settings : MonoBehaviour
    {
        [SerializeField, Space] private Toggle _realTime;
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

            _realTime.isOn = _dataService.IsRealTime;
            _currentTime.value = _dataService.TimeInMinutes() / _modelStaticData.MaxTime;

            _currentLevel.maxValue = _dataService.MaxLevel;
            _currentLevelText.text = _waveController.WavesCount.ToString();
            _waveController.OnWaveStart += SetLevel;
            
            _boostToggle.isOn = _dataService.Boost;
        }

        private void Awake()
        {
            _realTime.onValueChanged.AddListener(value => _dataService.IsRealTime = value);
            _currentTime.onValueChanged.AddListener(value => _dataService.CurrentTime = value * _modelStaticData.MaxTime);
            
            _currentLevel.onValueChanged.AddListener(value => _currentLevelText.text = value.ToString());
            _updeteLevel.onClick.AddListener(UpdateLevel);
            
            _boostToggle.onValueChanged.AddListener(Boost);
        }

        private void Update()
        {
            _currentTimeText.text = (_dataService.TimeInMinutes() / 60).ToString();
            _currentTime.value = _dataService.TimeInMinutes() / _modelStaticData.MaxTime;
        }

        private void SetLevel(int value)
        {
            _currentLevel.value = value;
            _currentLevelText.text = value.ToString();
        }

        private void Boost(bool value)
        {
            _dataService.Boost = value;
            _currentLevel.maxValue = _dataService.MaxLevel;
        }

        private void UpdateLevel()
        {
            _enemiesSpawner.ClearAllEnemies();
            _waveController.SetWavesCount((int) _currentLevel.value);
        }
    }
}