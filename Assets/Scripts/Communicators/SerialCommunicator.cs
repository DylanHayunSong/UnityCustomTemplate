using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

// Edit > Project Settings... > Player > Configuration
// Api Compatibility Level* ==> .NET 4.x로 변경

namespace _4by4.Communicate
{
    public class SerialCommunicator
    {
        public SerialCommunicator (string portNum)
        {
            comNum = portNum;
            serialPort = new SerialPort();
            buffer = new byte[1];

            Init();
        }

        public Action<string> OnDataReceived = null;
        public string comNum;
        public bool listening { get; private set; }


        private SerialPort serialPort = null;
        private byte[] buffer = null;
        private const int rate = 9600;

        private void Init ()
        {
            serialPort.PortName = comNum;
            serialPort.BaudRate = rate;
            serialPort.Parity = Parity.None;
            serialPort.StopBits = StopBits.One;
            serialPort.DataBits = 8;

            serialPort.Open();
        }

        public void Write (string data)
        {
            if(serialPort.IsOpen == true)
            {
                serialPort.Write(Encoding.ASCII.GetBytes(data), 0, data.Length);
            }
        }
        public void Listen ()
        {
            if(!listening)
            {
                Thread t = new Thread(new ThreadStart(ThreadProc));
                t.IsBackground = true;
                t.Start();

                listening = true;
            }
        }
        public void Close ()
        {
            listening = false;
            serialPort.Close();
        }

        private void ThreadProc ()
        {
            while(listening)
            {
                int len = serialPort.Read(buffer, 0, buffer.Length);

                if(len > 0)
                {
                    if(OnDataReceived != null)
                    {
                        string data = Encoding.ASCII.GetString(buffer, 0, buffer.Length);

                        OnDataReceived.Invoke(data);
                    }
                }
            }

            serialPort.Close();
        }
    }
}