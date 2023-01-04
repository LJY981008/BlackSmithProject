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
        user = new User(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp));
        IPEndPoint _ip = new IPEndPoint(IPAddress.Parse(ip), port);
        user.sock.Connect(_ip);
        
    }
    private void Update()
    {
        if (user.isConnect)
        {
            delTmp += Log;
            delTmp();
        }
    }

    public void Log()
    {
        Debug.Log("�α�");
        user.isConnect = false;
        Debug.Log(user.uid + " : " + user.name);
        delTmp -= Log;
    }
    private void OnDestroy()
    {
        user.isInterrupt = true;
        user.sock.Shutdown(SocketShutdown.Both);
        user.sock.Close();
        Debug.Log("��");
    }
}