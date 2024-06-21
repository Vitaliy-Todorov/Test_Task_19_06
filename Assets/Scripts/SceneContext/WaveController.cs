using System;
using Cysharp.Threading.Tasks;
using ProjectContext;
using ProjectContext.StaticDataServiceNamespace;
using ProjectContext.StaticDataServiceNamespace.StaticData.LevelStaticData;

namespace SceneContext
{
    public class WaveController
    {
        public Action OnWaveStart;
        private StaticDataService _staticDataService;
        private TimeController _timeController;
        private float _lastWaveStartTime;

        private WaveController(StaticDataService staticDataService, TimeController timeController)
        {
            _staticDataService = staticDataService;
            _timeController = timeController;

            WaveStart().Forget();
        }

         async UniTask WaveStart()
        {
            LevelStaticData levelStaticData = _staticDataService.GetLevelStaticData(LevelName.LevelTest);
            int wavesCount = levelStaticData.WavesCount;
            _lastWaveStartTime = _timeController.CurrentTime;
            await UniTask.WaitUntil(() => _timeController.CurrentTime - _lastWaveStartTime >= levelStaticData.TimeStartWaves);
            
            while (wavesCount > 0)
            {
                wavesCount--;
                OnWaveStart?.Invoke();
                
                _lastWaveStartTime = _timeController.CurrentTime;
                await UniTask.WaitUntil(() => _timeController.CurrentTime - _lastWaveStartTime >= levelStaticData.TimeBetweenWaves);
            }
        }
    }
}