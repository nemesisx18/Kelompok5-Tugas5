using PaintAstic.Global;
using PaintAstic.Module.Message;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace PaintAstic.Module.HUD
{
    public class GameplayUI : MonoBehaviour
    {
        [Header("Winner")]
        [SerializeField] private TextMeshProUGUI winPlayerText;
        [SerializeField] private TextMeshProUGUI winPlayerPointText;

        [SerializeField] private TextMeshProUGUI[] scorePlayerText;
        [SerializeField] private int[] playerName;
        private void OnEnable()
        {
            EventManager.StartListening("UpdatePointMessage", AddPointToString);
            EventManager.StartListening("WinnerMessage", GetPlayerWinner);
        }
        private void OnDisable()
        {
            EventManager.StopListening("UpdatePointMessage", AddPointToString);
            EventManager.StopListening("WinnerMessage", GetPlayerWinner);
        }
        void AddPointToString(object pointData)
        {
            UpdatePointMessage message = (UpdatePointMessage)pointData;
            //int PlayerName = playerName[message.playerIndex] = message.playerIndex + 1;
            int PlayerName = message.playerIndex + 1;
            scorePlayerText[message.playerIndex].text = "Player " + PlayerName.ToString() + " Point : " + message.point.ToString();
            //scorePlayerText[message.playerIndex].text = "Player " + message.playerIndex + 1.ToString() + " Point : " + message.point.ToString();
        }

        void GetPlayerWinner(object winData)
        {
            WinnerMessage message = (WinnerMessage)winData;
            int WinPlayerName = message.playerIndex + 1;
            winPlayerText.text = "Player " + WinPlayerName.ToString() + "WIN";
            winPlayerPointText.text = "Your Point " + message.point.ToString();

        }
    }
}

