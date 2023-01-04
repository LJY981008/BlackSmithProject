using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using Game.Packet;
namespace ConnectClient.User{
    public class User
    {
        public Socket sock;
        public byte[] sBuff;
        public byte[] rBuff;
        public Queue<byte[]> packetQueue;
        public bool isConnect;
        public bool isInterrupt;
        public int uid;
        public string name;
        private Thread thread;
        public User(Socket _sock, IPEndPoint _ip)
        {
            sock = _sock;
            sBuff = new byte[128];
            rBuff = new byte[128];
            name = String.Empty;
            isConnect = false;
            isInterrupt = false;
            packetQueue = new Queue<byte[]>();
            try
            {
                sock.Connect(_ip);
                ThreadStart threadStart = new ThreadStart(NewPacket);
                thread = new Thread(threadStart);
                thread.Start();
            }
            catch (SocketException e)
            {
                Debug.Log(e.Message);
            }
        }
        public void Receive()
        {
            Debug.Log("리");
            sock.BeginReceive(rBuff, 0, rBuff.Length, SocketFlags.None, ReceiveCallBack, sock);
            Debug.Log("리시브");
        }
        public void Send()
        {
            sock.BeginSend(sBuff, 0, sBuff.Length, SocketFlags.None, SendCallBack, sock);
        }
        public void ReceiveCallBack(IAsyncResult ar)
        {
            byte[] data = new byte[128];
            Array.Copy(rBuff, data, rBuff.Length);
            Array.Clear(rBuff, 0, rBuff.Length);
            packetQueue.Enqueue(data);
            sock.BeginReceive(rBuff, 0, rBuff.Length, SocketFlags.None, ReceiveCallBack, sock);
        }
        public void SendCallBack(IAsyncResult ar)
        {
            byte[] data = new byte[128];
            Array.Copy(sBuff, 0, data, 0, sBuff.Length);
            Array.Clear(sBuff, 0, sBuff.Length);
            packetQueue.Enqueue(data);
        }
        public void NewPacket()
        {
            Receive();
            while (!isInterrupt)
            {
                
                if (packetQueue.Count > 0)
                {
                    byte[] data = packetQueue.Dequeue();
                    byte[] _packet = new byte[2];
                    Array.Copy(data, 0, _packet, 0, 2);
                    short type = BitConverter.ToInt16(_packet);
                    switch ((int)type)
                    {
                        case (int)ePACKETTYPE.USERINFO:
                            {
                                byte[] _uid = new byte[4];
                                byte[] _name = new byte[120];
                                Array.Copy(data, 2, _uid, 0, _uid.Length);
                                Array.Copy(data, 2, _name, 0, _name.Length);
                                uid = BitConverter.ToInt32(_uid, 0);
                                name = Encoding.Default.GetString(_name);
                                isConnect = true;
                                Debug.Log("커넥트");
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
