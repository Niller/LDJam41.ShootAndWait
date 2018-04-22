using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class TurnState : MonoBehaviour
    {
        private readonly Stack<TurnFrameState> _states = new Stack<TurnFrameState>();
        private bool _isReplay;
        
        public bool IsReplay
        {
            get
            {
                return _isReplay;
            }
        }

        public void AddState()
        {
            _states.Push(new TurnFrameState(transform.position, transform.rotation));
        }

        public void Clear()
        {
            _states.Clear();
        }

        public void Replay()
        {
            _isReplay = true;
        }

        private void Update()
        {
            if (_isReplay)
            {
                if (_states.Count > 0)
                {
                    var currentState = _states.Pop();
                    currentState.Apply(transform);
                }
                else
                {
                    _isReplay = false;
                    GetComponent<TurnBasedActor>().StartTurn();
                }
            }
        }
    }
}