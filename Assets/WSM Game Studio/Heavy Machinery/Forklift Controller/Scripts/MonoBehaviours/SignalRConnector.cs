using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http;


public class SignalRConnector
{
    public Action<Message> OnMessageReceived;
    private HubConnection connection;
    public async Task InitAsync()
    {
        Debug.Log("Init");
        connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7036/chatHub")
                .Build();
        connection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            OnMessageReceived?.Invoke(new Message
            {
                UserName = user,
                Text = message,
            });
        });
        await StartConnectionAsync();
    }
    public async Task SendMessageAsync(Message message)
    {
        Debug.Log("SignalRConnector sendMessageAsync");
        try
        {
            await connection.InvokeAsync("SendMessage",
                message.UserName, message.Text);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"Error {ex.Message}");
        }
    }
    private async Task StartConnectionAsync()
    {
        Debug.Log("SignalRConnector startConnectionAsync");
        try
        {
            await connection.StartAsync();

        }
        catch (HttpRequestException ex)
        {
            UnityEngine.Debug.LogError($"Error {ex.Message}");
        }
    }
}