using PaintAstic.Global;
using PaintAstic.Module.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintAstic.Scene.Gameplay.Items
{
    public abstract class BaseItem : MonoBehaviour
    {
        [SerializeField] protected float _despawnDelay = 6f;
        protected float _despawnDelayTimer;

        protected void OnEnable()
        {
            _despawnDelayTimer = 0f;
            EventManager.StartListening("OnGamePauseMessage", OnGamePause);
            EventManager.StartListening("OnGameContinueMessage", OnGameContinue);
        }

        private void OnDisable()
        {
            EventManager.StopListening("OnGamePauseMessage", OnGamePause);
            EventManager.StopListening("OnGameContinueMessage", OnGameContinue);
        }

        protected void Update()
        {
            _despawnDelayTimer += Time.deltaTime;
            if (_despawnDelayTimer > _despawnDelay)
            {
                gameObject.SetActive(false);
                _despawnDelayTimer = 0f;
            }
        }

        

        private void OnGamePause()
        {
            Time.timeScale = 0f;
        }

        private void OnGameContinue()
        {
            Time.timeScale = 1f;
        }

    }

}
