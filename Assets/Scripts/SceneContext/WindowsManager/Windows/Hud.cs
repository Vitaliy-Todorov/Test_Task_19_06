using System;
using Cysharp.Threading.Tasks;
using Entities;
using ProjectContext;
using ProjectContext.WindowsManager;
using SceneContext;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Hud
{
    public class Hud : Window
    {
        [SerializeField, Space] private TMP_Text CounterView;
        [SerializeField] private TMP_Text WaveNumber;

        [SerializeField, Space] private Button _openMenu;
        [SerializeField] private Button _addBuilding;

        private GameWindowsManager _gameWindowsManager;
        private WaveController _waveController;
        private Counter _counter;
        private BuildingCreationArea _buildingCreationArea;

        [Inject]
        private void Construct(TimeController timeController,
            GameWindowsManager gameWindowsManager,
            WaveController waveController,
            Counter counter,
            BuildingCreationArea buildingCreationArea)
        {
            _gameWindowsManager = gameWindowsManager;
            _waveController = waveController;
            _counter = counter;
            _buildingCreationArea = buildingCreationArea;

            _counter.OnScoreChanged += ScoreChanged;
            _waveController.OnWaveStart += WaveStart;
            _waveController.OnWavesDropped += WaveStart;
        }

        private void Awake()
        {
            _openMenu.onClick.AddListener(() => _gameWindowsManager.Open(EWindow.Menu));
            _addBuilding.onClick.AddListener(() => AddBuilding().Forget());
        }

        private async UniTask AddBuilding()
        {
            if (!_buildingCreationArea.AddBuilding())
            {
                _addBuilding.image.color = Color.red;
                
                await UniTask.Delay(TimeSpan.FromSeconds(.5));
                
                _addBuilding.image.color = Color.white;
            }
        }

        private void OnDestroy() => 
            _counter.OnScoreChanged -= ScoreChanged;

        public override void Open() => 
            gameObject.SetActive(true);

        public override void Close()=> 
            gameObject.SetActive(false);

        private void WaveStart(int waveNumber) => 
            WaveNumber.text = waveNumber.ToString();

        private void ScoreChanged(int score) => 
            CounterView.text = score.ToString();
    }
}
