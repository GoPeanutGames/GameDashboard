using PeanutDashboard.Server;
using PeanutDashboard.Server.Data;
using PeanutDashboard.Shared.Logging;
using System;
using UnityEngine;

public class GameplaySession
{
    private string currentSessionId = string.Empty;
    private string gameName = string.Empty;
    private string mode = string.Empty;

    private AuthenticationData authenticationData;

    public GameplaySession(AuthenticationData _authenticationData, string _gameName, string _mode)
    {
        authenticationData = _authenticationData;
        gameName = _gameName;
        mode = _mode;
    }

    public void StartSession()
    {
        string timezone = "GMT+" + TimeZoneInfo.Local.GetUtcOffset(DateTime.Now).ToString("hh\\:mm");
        string startTime = DateTime.Now.ToString("ddd MMM dd yyyy HH:mm:ss");

        currentSessionId = string.Empty;
        SessionStartData formData = new()
        {
            signature = authenticationData.signature,
            address = authenticationData.address,
            mode = mode,
            timezone = timezone,
            startTime = startTime,
            gameName = gameName,
        };

        ServerService.PostDataToServer<SessionApi>(SessionApi.Start, JsonUtility.ToJson(formData), 
            (resp) =>
        {
            SessionResponseData sessionData = JsonUtility.FromJson<SessionResponseData>(resp);
            LoggerService.LogInfo($"{nameof(GameplaySession)}::{nameof(StartSession)} - Start Session, session Id: " + sessionData.sessionId);

            if (sessionData != null && !string.IsNullOrEmpty(sessionData.sessionId))
            {
                currentSessionId = sessionData.sessionId;
            }
            else
            {
                LoggerService.LogInfo($"{nameof(GameplaySession)}::{nameof(StartSession)} - Start Session, Invalid session id!");
            }
        }, 
            (str) =>
        {
            LoggerService.LogInfo($"{nameof(GameplaySession)}::{nameof(StartSession)} - Start Session failed " + str.ToString());
        });
    }

    public void UpdateSession(int _score)
    {
        SessionUpdateData formData = new()
        {
            signature = authenticationData.signature,
            address = authenticationData.address,
            sessionId = currentSessionId,
            score = _score,
        };

        ServerService.PostDataToServer<SessionApi>(SessionApi.Update, JsonUtility.ToJson(formData), 
            (resp) =>
        {
            LoggerService.LogInfo($"{nameof(GameplaySession)}::{nameof(UpdateSession)} - Update Session: " + resp);
        }, 
            (str) =>
        {
            LoggerService.LogInfo($"{nameof(GameplaySession)}::{nameof(UpdateSession)} - Update Session failed: " + str);
        });
    }

    public void EndSession(bool _hasWon, int _score)
    {
        string endTime = DateTime.Now.ToString("ddd MMM dd yyyy HH:mm:ss");

        SessionEndData formData = new()
        {
            signature = authenticationData.signature,
            address = authenticationData.address,
            sessionId = currentSessionId,
            score = _score,
            status = _hasWon ? "WON" : "LOSE",
            endTime = endTime,
        };

        ServerService.PostDataToServer<SessionApi>(SessionApi.End, JsonUtility.ToJson(formData), 
            (resp) =>
        {
            LoggerService.LogInfo($"{nameof(GameplaySession)}::{nameof(EndSession)} - End Session: " + resp);
        }, 
            (str) =>
        {
            LoggerService.LogInfo($"{nameof(GameplaySession)}::{nameof(EndSession)} - End Session failed: " + str);
        });
    }
}