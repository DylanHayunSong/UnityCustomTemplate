using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace _4by4.Communicate
{
    public class UDPCommunicator
    {
        public UDPCommunicator (int[] sendPorts, int[] receivePorts)
        {
            sendPort = sendPorts;
            receivePort = receivePorts;

            Init();
        }

        private int[] sendPort;
        private int[] receivePort;
        private int receivePortNum;

        private string remoteIP = "255.255.255.255";

        private Dictionary<int, UdpClient> sendClientDic = new Dictionary<int, UdpClient>();
        private UdpClient[] receiveClient;
        private IPEndPoint remoteEndPoint;

        public Action<string> OnMessageReceived;

        public void Init ()
        {
            if(receivePort != null)
            {
                receiveClient = new UdpClient[receivePort.Length];
                for(int i = 0; i < receivePort.Length; i++)
                {
                    receiveClient[i] = new UdpClient(receivePort[i]);
                    receiveClient[i].BeginReceive(OnReceive, receiveClient[i]);
                }
            }

            if(sendPort != null)
            {
                foreach(int portNum in sendPort)
                {
                    sendClientDic.Add(portNum, new UdpClient());
                }
            }
        }

        public void Send (int portNum, string msg)
        {
            remoteEndPoint = new IPEndPoint(IPAddress.Parse(remoteIP), portNum);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(msg);
            sendClientDic[portNum].Send(data, data.Length, remoteEndPoint);
        }

        private void OnReceive (IAsyncResult ar)
        {
            try
            {
                IPEndPoint ipEndPoint = null;
                for(int i = 0; i < receiveClient.Length; i++)
                {
                    if(receiveClient[i] == ar.AsyncState)
                    {
                        byte[] data = receiveClient[i].EndReceive(ar, ref ipEndPoint);
                        receivePortNum = i;

                        if(OnMessageReceived != null)
                        {
                            OnMessageReceived.Invoke(System.Text.Encoding.Default.GetString(data));
                        }

                        break;
                    }
                }
            }
            catch(SocketException ex)
            {
                Debug.Log(ex);
                return;
            }

            receiveClient[receivePortNum].BeginReceive(OnReceive, receiveClient[receivePortNum]);
        }

        public void ShutDown ()
        {
            if(receivePort != null)
            {
                for(int i = 0; i < receivePort.Length; i++)
                {
                    if(receiveClient[i] != null)
                    {
                        receiveClient[i].Close();
                    }
                    receiveClient[i] = null;
                }
            }

            foreach(UdpClient client in sendClientDic.Values)
            {
                client.Close();
            }
            sendClientDic.Clear();
        }
    }
}