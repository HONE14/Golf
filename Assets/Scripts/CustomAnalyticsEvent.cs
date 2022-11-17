using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services;
using Unity.Services.Core.Environments;
using Unity.Services.Analytics;

public class CustomAnalyticsEvent : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] string level;
    [SerializeField] int score;
    [SerializeField] bool isAlive;
    async void Start()
    {
        InitializationOptions options = new InitializationOptions();
        options.SetEnvironmentName("development");
        await UnityServices.InitializeAsync(options);
        await AnalyticsService.Instance.CheckForRequiredConsents();
        Debug.Log("User id: " + AnalyticsService.Instance.GetAnalyticsUserID());
    }
    [SerializeField] float timer=0;
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > 60)
        {
            var parameters = new Dictionary<string, object>();
            parameters["Level"] = level;
            parameters["Health"] = health;
            parameters["Score"] = score;
            parameters["Alive"] = isAlive;
            AnalyticsService.Instance.CustomData("PlayerStats", parameters);
            health -= 1;
            score += 1;
            isAlive = !isAlive;
            timer = 0;
        }
    }
}
