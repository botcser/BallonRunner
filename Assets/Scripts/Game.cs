using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using Assets.Scripts.Data;
using Assets.Scripts.Interface;
using UnityEngine;
using UnityEngine.UIElements;

public class Game : MonoBehaviour
{
    public RectTransform Canvas;
    public StartTimer StartTimer;
    public BalloonController Balloon;
    public PositionController BackgroundPositionController;

    public static Game Instance;

    private Vector2 _backgroundFromY;
    private float _lineStep;
    private List<ItemProto> _staticItemPrototypes = new();
    private bool _playing;

    public void Start()
    {
        Instance = this;
        _lineStep = Canvas.rect.width / 4;
        _staticItemPrototypes = Resources.LoadAll<ItemProto>("StaticItems").ToList();
        _backgroundFromY = BackgroundPositionController.From;
        Play();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && Balloon.transform.localPosition.x + _lineStep < Canvas.rect.width / 2 - 10)
        {
            Balloon.Move(_lineStep);
        }
        else if (Input.GetKeyDown(KeyCode.A) && Balloon.transform.localPosition.x - _lineStep > -Canvas.rect.width / 2 + 10)
        {
            Balloon.Move(-_lineStep);
        }

        if (_playing && Math.Abs(BackgroundPositionController.From.y - BackgroundPositionController.To.y) < 0.02f)
        {
           EndGame("YOU WIN!.\nRETRY?");
        }
    }

    public void Play()
    {
        _playing = true;
        EnemySpawner.Instance.Reset();
        BackgroundPositionController.From = _backgroundFromY;
        Balloon.Reset();

        BackgroundPositionController.enabled = true;
        EnemySpawner.Instance.Init(_lineStep);
        StartCoroutine(EnemySpawner.Instance.SpawnStaticItems(_staticItemPrototypes, 5f));

        //StartTimer.Run(() =>
        //{
        //    BackgroundPositionController.enabled = true;
        //    EnemySpawner.Instance.Init(_lineStep);
        //    StartCoroutine(EnemySpawner.Instance.SpawnStaticItems(_staticItemPrototypes, 5f));
        //});
    }

    public void GameOver()
    {
        EndGame("YOU WIN!.\nRETRY?");
    }

    private void EndGame(string message)
    {
        _playing = false;
        Balloon.Position.enabled = false;
        BackgroundPositionController.enabled = false;
        EnemySpawner.Instance.CancellationTokenSource.Cancel();
        PopupSpin.Instance.RequestConfirmation(message, confirm: Play);
    }
}
