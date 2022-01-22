﻿using System;
using Sources.Runtime.Models.Abilities;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Runtime.Models.CharactersStateMachine
{
    public class AbilityCastState : State
    {
        public Ability Ability { get; set; }

        private readonly NavMeshAgent _navMeshAgent;
        
        public AbilityCastState(NavMeshAgent navMeshAgent, Func<dynamic> getTarget,
            Transformable characterTransformable, float attackDistance, StateMachine stateMachine) 
            : base(getTarget, characterTransformable, attackDistance, stateMachine)
        {
            _navMeshAgent = navMeshAgent;
        }

        public override void Enter()
        {
            _navMeshAgent.isStopped = !Ability.Mobility;
        }

        public override void Exit()
        {
            if (Ability.Mobility)
                _navMeshAgent.isStopped = true;
        }

        public override void LogicUpdate()
        {
            
        }

        public override void Update(float deltaTime)//TODO: Remove repeat from MoveState
        {
            dynamic target = _getTarget.Invoke();
            if (target is Transformable targetTransformable)
            {
                _navMeshAgent.SetDestination(targetTransformable.Position);
            }
            else if(target is Vector3 targetPoint)
            {
                _navMeshAgent.SetDestination(targetPoint); ;
            }
            _characterTransformable.MoveTo(_navMeshAgent.nextPosition);
        }
    }
}