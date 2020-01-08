using BestHTTP.SocketIO;
using BestHTTP.SocketIO.Events;
using System;
using UnityEngine;

public class Sockets : MonoBehaviour
{
    #region INSPECTOR_HIDDEN
    static Socket _socket;
    SocketManager socketManager;
    public delegate void SocketStatus(bool status);
    public static event SocketStatus OnSocketConnected;
    public delegate void SocketError(SocketIOErrors error);
    public static event SocketError OnSocketError;
    public static bool isSocketConnected;
    #endregion

    #region INSPECTOR_VISIBLE
    #endregion

    void Start()
    {
        CreateSocket();
    }

    public static void ListenToThis(string eventName, SocketIOCallback callback)
    {
        _socket.On(eventName, callback);
    }

    public static void EmitEvent(string eventName, object data, SocketIOAckCallback callback)
    {
        _socket.Emit(eventName, callback, data);
    }

    #region PRIVATE_METHOD
    void CreateSocket()
    {
        Debug.Log("Creating socket");
        try
        {
            TimeSpan miliSecForReconnect = TimeSpan.FromMilliseconds(1000);
            SocketOptions options = new SocketOptions();
            options.ReconnectionAttempts = 1;
            options.AutoConnect = true;
            options.ReconnectionDelay = miliSecForReconnect;
            socketManager = new SocketManager(new Uri(APIs.SocketBaseURL), options);
            _socket = socketManager.Socket;

            _socket.On(SocketIOEventTypes.Connect, OnServerConnected);
            _socket.On(SocketIOEventTypes.Disconnect, OnServerDisconnected);
            _socket.On(SocketIOEventTypes.Error, OnError);
        }
        catch (Exception e)
        {
            Debug.Log("Throwing error " + e.Message);
            throw;
        }
    }

    void OnError(Socket socket, Packet packet, object[] args)
    {
        Error error = args[0] as Error;
        switch (error.Code)
        {
            case SocketIOErrors.User:
                Debug.Log("Exception in an event handler!");
                break;
            case SocketIOErrors.Internal:
                Debug.Log("Internal error! " + error.Message);
                break;
            default:
                Debug.Log("Error " + error.Message);
                break;
        }
    }

    void OnServerDisconnected(Socket socket, Packet packet, object[] args)
    {
        isSocketConnected = false;
        Debug.Log("Disconnected " + packet.Payload);
        OnSocketConnected?.Invoke(false);
        _socket.Off();
        ShowDisconnectedDialog();
    }

    void ShowDisconnectedDialog()
    {
        //DialogBoxData tempData = new DialogBoxData();
        //tempData.button1Label = GameData.networkButtons[0];
        //tempData.button2Label = GameData.networkButtons[1];
        //tempData.description = GameData.networkDescription;
        //tempData.title = GameData.networkTitle;
        //tempData.btn1Callback = OnBtnCancel;
        //tempData.btn2Callback = OnBtnConnect;
        //UIDialogBox.ShowDialog(tempData);
    }

    void OnServerConnected(Socket socket, Packet packet, object[] args)
    {
        isSocketConnected = true;
        Debug.Log("Connected " + packet.Payload);
        OnSocketConnected?.Invoke(true);
        System.Collections.Generic.Dictionary<string, string> pairs = new System.Collections.Generic.Dictionary<string, string>();
        pairs.Add("token", APIs.TokenSocket);

        _socket.Emit("authenticate", pairs);

        //_socket.On("chatMessage", OnChatMsgReceived);
        //_socket.On("invite-message", OnNotificationRecieved);
    }

    /// <summary>
    /// Chat message Recieved.
    /// </summary>
    void OnChatMsgReceived(Socket socket, Packet packet, object[] args)
    {
        //chatHandler.MessageRecieved(packet.ToString());
    }

    /// <summary>
    /// Notification Recieved.
    /// </summary>
    void OnNotificationRecieved(Socket socket, Packet packet, object[] args)
    {
        //notificationHandler.RecieveNotification(packet.ToString());
    }
    #endregion
}