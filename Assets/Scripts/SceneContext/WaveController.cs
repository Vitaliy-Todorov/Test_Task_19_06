﻿using System;
using Cysharp.Threading.Tasks;
using Infrastructure.StaticDataServiceNamespace.StaticData.LevelStaticData;
using ProjectContext;
using ProjectContext.StaticDataServiceNamespace;
using ProjectContext.StaticDataServiceNamespace.StaticData.LevelStaticData;

namespace SceneContext
{
    public class WaveController
    {
        public Action<int> OnWaveStart;
        public Action<int> OnSetWavesCount ;
        
        private StaticDataService _staticDataService;
        private TimeController _timeController;
        private float _lastWaveStartTime;
        
        private GameModelStaticData _levelStaticData;
        public int WavesCount { get; private set; }

        private WaveController(StaticDataService staticDataService, TimeController timeController)
        {
            _staticDataService = staticDataService;
            _timeController = timeController;

            _levelStaticData = _staticDataService.GetGameModelStaticData(GameModelName.GameModelTest);
            WaveStart().Forget();
        }

        public void SetWavesCount(int startWavesCount)
        {
            WavesCount = startWavesCount;
            OnSetWavesCount?.Invoke(WavesCount);
        }

        public void DropOn(int howMuchToDrop)
        {
            WavesCount -= howMuchToDrop;
            OnSetWavesCount?.Invoke(WavesCount);
        }

        private async UniTask WaveStart()
        {
            _lastWaveStartTime = _timeController.CurrentTime;
            await UniTask.WaitUntil(() => _timeController.CurrentTime - _lastWaveStartTime >= _levelStaticData.TimeStartWaves);
            
            while (true)
            {
                WavesCount++;
                OnWaveStart?.Invoke(WavesCount);

                _lastWaveStartTime = _timeController.CurrentTime;
                await UniTask.WaitUntil(() => _timeController.CurrentTime - _lastWaveStartTime >= _levelStaticData.TimeBetweenWaves);
            }
        }
    }
}