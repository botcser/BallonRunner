using Assets.ECS.Components;
using Assets.ECS.Systems;
using Assets.Scripts.Data;
using Assets.Scripts.Interface;
using Leopotam.Ecs;
using System.Collections.Generic;
using System.Linq;
using Assets.ECS.Data;
using Assets.Scripts;
using Assets.Scripts.Interface.Elements;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Assets.ECS
{
    public class EcsGame : MonoBehaviour
    {
        public StartTimer StartTimer;
        public RectTransform BackgroundRectTransform;
        public RectTransform BalloonRectTransform;
        public RectTransform Canvas;
        public float LineStep;

        public static EcsGame Instance;

        private int _entitiesCount;
        private GameState _gameState;
        private EcsWorld _ecsWorld;
        private EcsSystems _ecsSystems;
        private Vector2 _playerStartPosition;
        private Vector2 _backgroundStartPosition;
        private List<ItemProto> _staticItemPrototypes = new();


        public void Awake()
        {
            Instance = this;
        }

        public void Update()
        {
            if (_gameState.Playing) _ecsSystems?.Run();
        }

        public void Start()
        {
            _staticItemPrototypes = Resources.LoadAll<ItemProto>("StaticItems").ToList();
            LineStep = Canvas.rect.width / 4;
            _playerStartPosition = BalloonRectTransform.anchoredPosition;
            _backgroundStartPosition = BackgroundRectTransform.anchoredPosition;
            _gameState = new GameState();
            _ecsWorld = new EcsWorld();
            _ecsSystems = new EcsSystems(_ecsWorld);


#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_ecsWorld);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_ecsSystems);
#endif

            _ecsSystems
                .Inject(_entitiesCount)
                .Inject(_gameState)
                .Add(new PlayerInit())
                .Add(new BackgroundInit())
                .Add(new Swipe())
                .Add(new Moving())
                .Init();

            Play();
        }

        public void Play()
        {
            Reset();

            StartTimer.Run(() =>
            {
                _gameState.Playing = true;
                EnemySpawner.Instance.Init(LineStep);
                StartCoroutine(EnemySpawner.Instance.SpawnStaticItems(_staticItemPrototypes, 5f));
            });
        }

        private void Reset()
        {
            Blackout.Instance.Show(() =>
            {
                Blackout.Instance.Hide();

                BackgroundRectTransform.anchoredPosition = _backgroundStartPosition;
                BalloonRectTransform.anchoredPosition = _playerStartPosition;

                var entities = new EcsEntity[_entitiesCount];

                _ecsWorld.GetAllEntities(ref entities);

                foreach (var ecsEntity in entities)
                {
                    ref var entity = ref ecsEntity.Get<EntityData>();
                    entity.Reset();
                }

                EnemySpawner.Instance.Reset();
            });
        }

        public void GameSuccess()
        {
            EndGame("YOU WIN!\nRETRY?");
        }

        public void GameOver()
        {
            EndGame("YOU Lose!\nRETRY?");
        }

        private void EndGame(string message)
        {
            _gameState.Playing = false;
            EnemySpawner.Instance.CancellationTokenSource.Cancel();
            PopupSpin.Instance.RequestConfirmation(message, confirm: Play);
        }

        public void OnDestroy()
        {
            if (_ecsSystems == null) return;

            _ecsSystems.Destroy();
            _ecsSystems = null;
            _ecsWorld.Destroy();
            _ecsWorld = null;
        }
    }
}
