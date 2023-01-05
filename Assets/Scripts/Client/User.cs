using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using Game.Packet;
/// <summary>
/// 유저소켓 정보
/// </summary>
namespace ConnectClient.User {
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
            Debug.Log("ㅎㅇ");
            sock = _sock;
            sBuff = new byte[128];
            rBuff = new byte[128];
            name = string.Empty;
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
            sock.BeginReceive(rBuff, 0, rBuff.Length, SocketFlags.None, ReceiveCallBack, sock);
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
                                Array.Copy(data, 2, _uid, 0, _uid.Length);
                                uid = BitConverter.ToInt32(_uid, 0);
                                isConnect = true;
                                Debug.Log("커넥트 UID = " + uid);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        public void MakeRegistPacket(List<string> info)
        {
            REGISTINFO registInfo;
            registInfo.ePacketType = ePACKETTYPE.REGISTINFO;
            registInfo.name = info[0];
            registInfo.id = info[1];
            registInfo.pw = info[2];
            registInfo.email = info[3];
            byte[] _packetType = BitConverter.GetBytes((short)registInfo.ePacketType);
            byte[] _name = new byte[10];
            byte[] _id = new byte[30];
            byte[] _pw = new byte[40];
            byte[] _email = new byte[60];
            _name = Encoding.UTF8.GetBytes(registInfo.name);
            _id = Encoding.UTF8.GetBytes(registInfo.id);
            _pw = Encoding.UTF8.GetBytes(registInfo.pw);
            _email = Encoding.UTF8.GetBytes(registInfo.email);
            Array.Copy(_packetType, 0, sBuff, 0, _packetType.Length);
            Array.Copy(_name, 0, sBuff, 2, _name.Length);
            Array.Copy(_id, 0, sBuff, 12, _id.Length);
            Array.Copy(_pw, 0, sBuff, 32, _pw.Length);
            Array.Copy(_email, 0, sBuff, 72, _email.Length);
            Send();
        }
    }
}
