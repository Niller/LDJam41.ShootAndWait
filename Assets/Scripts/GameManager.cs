using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
    public class GameManager : MonoBehaviour
    {
        public List<TurnBasedActor> Actors = new List<TurnBasedActor>();
        public int CurrentActorIndex;
        public bool EndGame;
        public bool IsPause;

        public static GameManager Instance;

        public void Awake()
        {
            Instance = this;
        }
        
        public void Start()
        {
            foreach (var actor in Actors)
            {
                actor.GetComponent<BaseDamageReceiver>().Died += (damageReceiver) =>
                {
                    Actors.Remove(damageReceiver.GetComponent<TurnBasedActor>());
                };
            }
            StartTurn();
        }
        
        private void StartTurn()
        {
            Actors[CurrentActorIndex].TurnEnded += OnTurnEnded;
            Actors[CurrentActorIndex].StartTurn();
        }

        private void OnTurnEnded()
        {
            Actors[CurrentActorIndex].TurnEnded -= OnTurnEnded;
            CurrentActorIndex = ++CurrentActorIndex % Actors.Count;
            StartTurn();
        }

        private void Update()
        {
            if (Actors.Count == 1)
            {
                if (Actors.First().GetComponent<PlayerController>())
                {
                    EndGame = true;
                    UiController.Instance.OpenWinDialog();
                }
            }
        }
    }
}