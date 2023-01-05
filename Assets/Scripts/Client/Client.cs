using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConnectClient.User;
using System.Net.Sockets;
using System.Net;
delegate void delegateTmp();
public class Client : MonoBehaviour
{
    private User user;
    private delegateTmp delTmp;
    private string ip;
    private int port;
    private void Awake()
    {
        ip = "172.30.1.25";
        port = 8082;
    }
    private void Start()
    {
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

    private void OnDestroy()
    {
        try
        {
            user.isInterrupt = true;
            user.sock.Shutdown(SocketShutdown.Both);
            user.sock.Close();
        }
        catch (SocketException e)
        {
            Debug.Log(e.Message);
        }
        Debug.Log("³¡");
    }
}
