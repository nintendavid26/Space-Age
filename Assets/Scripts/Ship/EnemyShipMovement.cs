using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions.Collections;
using Battle;

namespace Overworld
{
    [RequireComponent(typeof(EnemyShip))]
    public class EnemyShipMovement : ShipMovement
    {
        public EnemyShipMovementType mov;

        void Start()
        {
            ship = GetComponent<Ship>();

            mov = GetComponent<EnemyShipMovementType>();

        }

        void Update()
        {
            if (!BattleController.Controller.InBattle)
            {
                mov.Move(this);
            }
        }

        public EnemyShip[] CastTeam()
        {
            return new EnemyShip[] { (EnemyShip)ship,(EnemyShip)ship.Allies[1],(EnemyShip)ship.Allies[2]};
        }

        void OnTriggerEnter(Collider other)
        {
            if (!enabled) { return; }
            if (other.CompareTag("Bullet"))
            {
                BattleController.Controller.StartBattle(CastTeam(), BattleController.Initiative.Player);
            }
            else if (other.CompareTag("Player"))
            {
                BattleController.Controller.StartBattle(CastTeam(), BattleController.Initiative.Neutral);
            }
        }
    }
}