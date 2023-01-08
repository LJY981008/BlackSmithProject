using Game.Packet;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 유저소켓 정보
/// </summary>
namespace ConnectClient.User
{
    public class User:IDisposable
    {
        public Socket sock;
        public byte[] sBuff;
        public byte[] rBuff;
        public Queue<byte[]> packetQueue;
        public bool isConnect;
        public bool isInterrupt;
        public int uid;
        public Dictionary<string, string> myInfo;
        private Thread thread;
        public User(Socket _sock, IPEndPoint _ip)
        {
            sock = _sock;
            sBuff = new byte[256];
            rBuff = new byte[256];
            isConnect = false;
            isInterrupt = false;
            myInfo = new Dictionary<string, string>();
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
            try
            {
                byte[] data = new byte[256];
                Array.Copy(rBuff, data, rBuff.Length);
                Array.Clear(rBuff, 0, rBuff.Length);
                packetQueue.Enqueue(data);
                sock.BeginReceive(rBuff, 0, rBuff.Length, SocketFlags.None, ReceiveCallBack, sock);
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        public void SendCallBack(IAsyncResult ar)
        {
            byte[] data = new byte[256];
            Array.Copy(sBuff, 0, data, 0, sBuff.Length);
            Array.Clear(sBuff, 0, sBuff.Length);
            if(!isConnect)
                Dispose();
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
                        case (int)ePACKETTYPE.LOGININFO:
                            {
                                byte[] _isSuccess = new byte[4];
                                Array.Copy(data, 2, _isSuccess, 0, _isSuccess.Length);
                                int success = BitConverter.ToInt32(_isSuccess);
                                if(success == 0)
                                {
                                    byte[] _name = new byte[20];
                                    byte[] _id = new byte[30];
                                    byte[] _pw = new byte[40];
                                    byte[] _email = new byte[60];
                                    Array.Copy(data, 6, _id, 0, _id.Length);
                                    Array.Copy(data, 36, _pw, 0, _pw.Length);
                                    Array.Copy(data, 76, _name, 0, _name.Length);
                                    Array.Copy(data, 96, _email, 0, _email.Length);
                                    myInfo.Add("Name", Encoding.UTF8.GetString(_name).Trim('\0'));
                                    myInfo.Add("ID", Encoding.UTF8.GetString(_id).Trim('\0'));
                                    myInfo.Add("PW", Encoding.UTF8.GetString(_pw).Trim('\0'));
                                    myInfo.Add("Email", Encoding.UTF8.GetString(_email).Trim('\0'));
                                    ClientManager.Instance.triggerLogin = 0;
                                }
                                else if(success == 1)
                                {
                                    ClientManager.Instance.triggerLogin = 1;
                                }
                                else if(success == 2)
                                {
                                    ClientManager.Instance.triggerLogin = 2;
                                }
                                else
                                {
                                    ClientManager.Instance.triggerLogin = 3;
                                }
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
            byte[] _packetType = new byte[2];
            byte[] _name = new byte[10];
            byte[] _id = new byte[30];
            byte[] _pw = new byte[40];
            byte[] _email = new byte[60];
            _packetType = BitConverter.GetBytes((short)registInfo.ePacketType);
            _name = Encoding.UTF8.GetBytes(registInfo.name);
            _id = Encoding.UTF8.GetBytes(registInfo.id);
            _pw = Encoding.UTF8.GetBytes(registInfo.pw);
            _email = Encoding.UTF8.GetBytes(registInfo.email);
            Array.Copy(_packetType, 0, sBuff, 0, _packetType.Length);
            Array.Copy(_name, 0, sBuff, 2, _name.Length);
            Array.Copy(_id, 0, sBuff, 12, _id.Length);
            Array.Copy(_pw, 0, sBuff, 42, _pw.Length);
            Array.Copy(_email, 0, sBuff, 82, _email.Length);
            Send();
        }
        public void MakeLoginPacket(List<string> info)
        {
            LOGININFO login;
            login.ePacketType = ePACKETTYPE.LOGININFO;
            login.id = info[0];
            login.pw = info[1];
            login.name = null;
            login.isSuccess = false;
            byte[] _packetType = new byte[2];
            byte[] _id = new byte[30];
            byte[] _pw = new byte[40];
            _packetType = BitConverter.GetBytes((short) login.ePacketType);
            _id = Encoding.UTF8.GetBytes(login.id);
            _pw = Encoding.UTF8.GetBytes(login.pw);
            Array.Copy(_packetType, 0, sBuff, 0, _packetType.Length);
            Array.Copy(_id, 0, sBuff, 2, _id.Length);
            Array.Copy(_pw, 0, sBuff, 32, _pw.Length);
            Send();
        }
        public void MakeExitPacket()
        {
            EXIT exit;
            exit.ePacketType = ePACKETTYPE.EXIT;
            byte[] _packetType = new byte[2];
            _packetType = BitConverter.GetBytes((short)exit.ePacketType);
            Array.Copy(_packetType, 0, sBuff, 0, _packetType.Length);
            Debug.Log((short)exit.ePacketType);
            isConnect = false;
            Send();
        }

        public void Dispose()
        {
            try
            {
                Debug.Log("종료");
                isInterrupt = true;
                sock.Close();
                GC.SuppressFinalize(this);
                if (EditorApplication.isPlaying)
                    EditorApplication.isPlaying = false;
                else
                    Application.Quit();
            }
            catch (SocketException e)
            {
                Debug.Log(e.Message);
            }
        }

    }
}
