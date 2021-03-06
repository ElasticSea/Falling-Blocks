﻿using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private StackBuilder builder;
        [SerializeField] private int expandThreshold;

        private bool gameover;
        public float Record { get; private set; }

        private void Awake()
        {
            builder.OnBlockPlaced += result =>
            {
                switch (result)
                {
                    case StackBuilder.PlacementResult.Partial:
                        Record = 0;
                        break;
                    case StackBuilder.PlacementResult.Placed:
                        Record++;
                        break;
                    case StackBuilder.PlacementResult.Miss:
                        gameover = true;
                        break;
                }

                if (Record >= expandThreshold)
                {
                    builder.Extend();
                }
            };

            StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            yield return new WaitForEndOfFrame();
            builder.SpawnBlock();
        }

        public void NextBlock()
        {
            builder.PlaceBlock();

            if (gameover == false)
            {
                builder.SpawnBlock();
            }
        }
    }
}