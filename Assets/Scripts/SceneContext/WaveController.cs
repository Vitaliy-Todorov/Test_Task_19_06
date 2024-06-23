using System;
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
        
        private StaticDataService _staticDataService;
        private TimeController _timeController;
        private float _lastWaveStartTime;
        
        private GameModelStaticData _levelStaticData;
        private int _wavesCount;

        private WaveController(StaticDataService staticDataService, TimeController timeController)
        {
            _staticDataService = staticDataService;
            _timeController = timeController;

            _levelStaticData = _staticDataService.GetGameModelStaticData(GameModelName.GameModelTest);
            WaveStart().Forget();
        }

        private async UniTask WaveStart()
        {
            _lastWaveStartTime = _timeController.CurrentTime;
            await UniTask.WaitUntil(() => _timeController.CurrentTime - _lastWaveStartTime >= _levelStaticData.TimeStartWaves);
            
            while (true)
            {
                _wavesCount++;
                OnWaveStart?.Invoke(_wavesCount);

                _lastWaveStartTime = _timeController.CurrentTime;
                await UniTask.WaitUntil(() => _timeController.CurrentTime - _lastWaveStartTime >= _levelStaticData.TimeBetweenWaves);
            }
        }
    }
}