using PaintAstic.Global;
using PaintAstic.Module.Message;
using PaintAstic.Module.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Scene.Gameplay.Items
{
    public class ItemCollectPoint : BaseItem
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerController player = other.gameObject.GetComponent<PlayerController>();
                int targetPlayerIndex = player.playerIndex;
                bool isDouble = player.isDoublePoint;
                OnCollided(targetPlayerIndex, isDouble);
            }
        }

        public void OnCollided(int playerIndex, bool isDouble)
        {
            EventManager.TriggerEvent("CollectPointMessage", new CollectPointMessage(playerIndex,isDouble));
            EventManager.TriggerEvent("CollectPointParticleMessage", transform.position);
            EventManager.TriggerEvent("CollectedMessage");
            EventManager.TriggerEvent("ResetLastCollectPointMessage", playerIndex);
            gameObject.SetActive(false);
        }
    }
}

