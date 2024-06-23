using System;
using ProjectContext;
using ProjectContext.WindowsManager;
using SceneContext;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace UI.Hud
{
    public class Hud : Window
    {
        [SerializeField] private Button AddBuilding;
        [SerializeField, Space] private TMP_Text CounterView;
        [SerializeField] private TMP_Text WaveNumber;
        
        [SerializeField, Space] private Button _openMenu;
        // [SerializeField] private Button _play;


        private TimeController _timeController;
        private GameWindowsManager _gameWindowsManager;
        private WaveController _waveController;
        private Counter _counter;

        [Inject]
        private void Construct(TimeController timeController,
            GameWindowsManager gameWindowsManager,
            WaveController waveController,
            Counter counter)
        {
            _timeController = timeController;
            _gameWindowsManager = gameWindowsManager;
            _waveController = waveController;
            _counter = counter;

            _counter.OnScoreChanged += ScoreChanged;
            _waveController.OnWaveStart += WaveStart;
        }

        private void Awake()
        {
            // _pause.onClick.AddListener(_timeController.Pause);
            // _play.onClick.AddListener(_timeController.Play);
            
            _openMenu.onClick.AddListener(() => _gameWindowsManager.Open(EWindow.Menu));
            // _play.onClick.AddListener(() => _gameWindowsManager.Close());
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
