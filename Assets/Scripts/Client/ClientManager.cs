using ConnectClient.User;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
/// <summary>
/// 클라이언트
/// </summary>
delegate void delegateTmp();
public class ClientManager : Singleton<ClientManager>
{
    protected ClientManager() { }
    private User user;
    private delegateTmp delTmp;
    private string ip;
    private int port;
    public int triggerLogin = -1;
    private void Start()
    {
        ip = "172.30.1.25";
        port = 8082;
        IPEndPoint _ip = new IPEndPoint(IPAddress.Parse(ip), port);
        user = new User(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp), _ip);
    }
    private void Update()
    {
    }

    public void Resist(List<string> _infoList)
    {
        user.MakeRegistPacket(_infoList);
    }
    public void Login(List<string> _infoList)
    {
        user.MakeLoginPacket(_infoList);
    }
    public void Exit()
    {
        user.MakeExitPacket();
    }
    private void OnDestroy()
    {
        try
        {
            Debug.Log("종료");
            user.MakeExitPacket();
            user.Dispose();
        }
        catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}
