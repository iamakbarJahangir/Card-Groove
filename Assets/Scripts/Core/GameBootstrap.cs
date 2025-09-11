using System.Collections.Generic;
using UnityEngine;
using GrooveCards.Rhythm.Contracts;
using GrooveCards.Rhythm.Impl;
using GrooveCards.Rhythm.Data;

namespace GrooveCards.Core
{
    /// <summary>
    /// Purpose: Scene bootstrapper to wire backend at runtime.
    /// Drop this on an empty GameObject and assign references in the Inspector.
    /// </summary>
    public class GameBootstrap : MonoBehaviour
    {
        [Header("Core Flow")]
        [SerializeField] private GameDirector _director;             // controls card flow
        [SerializeField] private RhythmGameController _rhythm;       // rhythm loop
        [SerializeField] private CharacterAnimator _dancer;          // character anims

        [Header("Services (assign in scene)")]
        [SerializeField] private AudioSync _audioSync;
        [SerializeField] private BeatmapStatic _beatmap;
        [SerializeField] private Judge _judge;
        [SerializeField] private KeyboardInputManager _input;

        // NOTE: hud is optional for now; pass a MonoBehaviour implementing IHUD when front-end is ready
        [SerializeField] private MonoBehaviour _hud; // must implement IHUD

        private void Start()
        {
            // 1) Inject services into Rhythm system
            _rhythm.Initialize(_audioSync, _beatmap, _judge, _hud as IHUD, _input);

            // 2) Prepare a simple deck to demonstrate flow
            List<GameCard> deck = new List<GameCard>{
                new GameCard{ type=CardType.Dance, songId="song01"},
                new GameCard{ type=CardType.Other },
                new GameCard{ type=CardType.Dance, songId="song02"},
            };

            // 3) Initialize the director and start
            _director.Initialize(deck, _rhythm, _dancer);
            _director.StartRound();
        }
    }
}
