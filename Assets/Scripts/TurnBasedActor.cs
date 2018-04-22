using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace DefaultNamespace
{
    public class TurnBasedActor : MonoBehaviour
    {
        public float MaxActionPoints;
        public bool AutoEndTurn;
        
        
        public event Action TurnEnded;
        public event Action PointsChanged;
        public event Action TurnStarted;
        
        private float _currentActionPoints;
        private bool _yourTurn;
        
        public float CurrentActionPoints
        {
            get
            {
                return _currentActionPoints; 
                
            }
        }

        public bool YourTurn
        {
            get
            {
                return _yourTurn;
            }
        }
        
        public void StartTurn()
        {
            _currentActionPoints = MaxActionPoints;
            _yourTurn = true;
            if (TurnStarted != null)
            {
                TurnStarted.Invoke();
            }
        }
        
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        public bool TryUsePoints(float pointCount, bool strict)
        {
            if (_currentActionPoints > 0f && (!strict || _currentActionPoints >= pointCount))
            {
                _currentActionPoints -= pointCount;

                if (PointsChanged != null)
                {
                    PointsChanged.Invoke();
                }
                
                if (_currentActionPoints <= 0f)
                {
                    if (AutoEndTurn)
                    {
                        EndTurn();
                    }

                    return false;
                }

                return true;
            }
            

            Debug.LogFormat("Not enough points: require {0} exists {1}", pointCount, _currentActionPoints);
            return false;
        }

        public void EndTurn()
        {
            _yourTurn = false;
            if (TurnEnded != null)
            {
                TurnEnded.Invoke();
            }
        }
       
    }
}