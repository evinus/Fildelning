using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NätverksProgramServer
{
    public class AnvändarKlienter
    {
        private TcpClient klient;
        public  TcpClient Klient
        {
            get { return klient; }
            set { klient = value; }
        }

        public string Namn { get; set; }
        public int ID { get; private set; }
        

        public AnvändarKlienter()
        {
            klient = new TcpClient();
        }

        public AnvändarKlienter(int id)
        {
            ID = id;

        }
        public void ÄndraID(int id)
        {
            ID = id;
        }
    }
}
