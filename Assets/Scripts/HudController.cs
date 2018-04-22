using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class HudController : MonoBehaviour
    {
        public GameObject Player;

        public Image TurnBasedCircle;
        public Text TurnBasedCountText;
        public Slider Health;
        public GameObject Aim;

        public GameObject HealthBarContainer;
        public GameObject HealthBarPrefab;

        public GameObject EnemyTurnNotification;
        public GameObject SkipTurnNotification;

        private TurnBasedActor _actor;

        public static HudController Instance;

        private void Awake()
        {
            Instance = this;
        }
        
        private void Start()
        {
            _actor = Player.GetComponent<TurnBasedActor>();
            _actor.PointsChanged += UpdateUi;
            _actor.TurnStarted += UpdateUi;
            _actor.TurnEnded += UpdateUi;

            foreach (var turnBasedActor in GameManager.Instance.Actors)
            {
                var enemy = turnBasedActor.GetComponent<EnemyDamageReceiver>();
                if (enemy != null)
                {
                    var go = Instantiate(HealthBarPrefab);
                    go.transform.parent = HealthBarContainer.transform;
                    go.GetComponent<HealthBar>().DamageReceiver = enemy;
                }
            }
        }

        private void OnDestroy()
        {
            _actor.PointsChanged -= UpdateUi;
            _actor.TurnStarted -= UpdateUi;
            _actor.TurnEnded -= UpdateUi;
        }

        private void Update()
        {
            var damageReceiver = Player.GetComponent<PlayerDamageReceiver>();
            Health.value = damageReceiver.Health / (float) damageReceiver.MaxHealth;
            
            
        }

        private void UpdateUi()
        {
            TurnBasedCountText.text = _actor.CurrentActionPoints.ToString("#.#");
            TurnBasedCircle.fillAmount = _actor.CurrentActionPoints / _actor.MaxActionPoints;
            SkipTurnNotification.SetActive(_actor.YourTurn && _actor.CurrentActionPoints <= 0);          
            EnemyTurnNotification.SetActive(!_actor.YourTurn);
            Aim.SetActive(_actor.YourTurn);  
        }

        public void OnNextTurnButtonClicked()
        {
            _actor.EndTurn();
        }
    }
}