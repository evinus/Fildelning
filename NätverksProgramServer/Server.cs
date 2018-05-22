using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NätverksProgramServer
{
    public partial class Server : Form
    {
        #region Fält
        List<AnvändarKlienter> klienter = new List<AnvändarKlienter>();
        TcpListener lyssnare;
        IPAddress hostAdress;
        List<int> tal = new List<int>();
        List<byte> filData = new List<byte>();
        #endregion
        public Server()
        {
            InitializeComponent();
            string hostNamn = Dns.GetHostName();
            lyssnare = new TcpListener(IPAddress.Any, 12345);
            lyssnare.Start();
            StartaLyssna();
        }
        public async void StartaLyssna()
        {
            try
            {
                // lyssnar efter inkommande klienter och sen starta lyssna funktioner på den klienten.
                AnvändarKlienter nyklient = new AnvändarKlienter();
                nyklient.Klient = await lyssnare.AcceptTcpClientAsync();
                nyklient.Klient.NoDelay = false;


                byte[] byteNamn = new byte[8];
                await nyklient.Klient.GetStream().ReadAsync(byteNamn, 0, 8);
                nyklient.Namn = Encoding.Unicode.GetString(byteNamn);
                nyklient.ÄndraID(klienter.Count);

                klienter.Add(nyklient);
                tbxLogg.AppendText("\r\n"+ DateTime.Now.ToString("h:mm:ss tt") + ": " + (nyklient.Klient.Client.RemoteEndPoint as IPEndPoint).Address.ToString() + " Har anslutet");
                Lyssna(nyklient);
            }
            catch(Exception error)
            {
                MessageBox.Show( "\r\n"+ error.Message +" kunde inte lysssna", this.Text );
                return;
            }
            StartaLyssna();
        }
        public async void Lyssna(AnvändarKlienter nyklient)
        {
            byte[] buffer;
            byte[] bytefilstorlek = new byte[4];
            byte[] namn;
            byte[] tal = new byte[4];
            
            try
            {
                //ser vilket tal som har skickats.   
                await nyklient.Klient.GetStream().ReadAsync(tal, 0, 4);
                int Tal = BitConverter.ToInt32(tal, 0);

                if (Tal == 1)
                {
                    int namnTalLängd;
                    byte[] namnbytelängd = new byte[4];
                    await nyklient.Klient.GetStream().ReadAsync(namnbytelängd, 0, 4);
                    namnTalLängd = BitConverter.ToInt32(namnbytelängd, 0);
                    namn = new byte[namnTalLängd];

                    await nyklient.Klient.GetStream().ReadAsync(namn, 0, namnTalLängd);

                    //läser in storleken på filen.
                    await nyklient.Klient.GetStream().ReadAsync(bytefilstorlek, 0, 4);

                    int TalfilStorlek = BitConverter.ToInt32(bytefilstorlek, 0);
                    tbxLogg.AppendText("\r\n har fått storleken " + TalfilStorlek.ToString());

                    buffer = new byte[TalfilStorlek];
                    int a = 0;
                    //Läser in filen
                    while (TalfilStorlek > a)
                    {
                        a += await nyklient.Klient.GetStream().ReadAsync(buffer, a, TalfilStorlek);
                    }
                    tbxLogg.AppendText("\r\n Ska börja skicka filen");

                    //SKickar ut filen till alla klienter
                    for (int i = 0; i < klienter.Count; i++)
                    {
                        SkickaFil(buffer, bytefilstorlek, namn, i);
                        
                        //if (k == client) continue;
                        //await klienter[i].Klient.GetStream().WriteAsync(tal, 0, 4);
                        //await klienter[i].Klient.GetStream().WriteAsync(filstorlek, 0, 4);
                        //await klienter[i].Klient.GetStream().WriteAsync(buffer, 0, filStorlek);

                    }
                }
                if (Tal == 2) // tar emot meddelandet och skickar vidare det.
                {
                    
                    byte[] medelandelängdByte = new byte[4];
                    await nyklient.Klient.GetStream().ReadAsync(medelandelängdByte, 0, 4);

                    int längdMedelade = BitConverter.ToInt32(medelandelängdByte, 0);
                    byte[] Meddelande = new byte[längdMedelade];
                    await nyklient.Klient.GetStream().ReadAsync(Meddelande, 0, längdMedelade);

                    for (int i = 0; i < klienter.Count; i++)
                    {
                        await klienter[i].Klient.GetStream().WriteAsync(tal, 0, 4);
                        await klienter[i].Klient.GetStream().WriteAsync(medelandelängdByte, 0, 4);
                        await klienter[i].Klient.GetStream().WriteAsync(Meddelande, 0,längdMedelade);
                    }
                }
                if(Tal == 3)
                {

                }

            }
            catch (Exception error) // Om det blir fel tas klienten borts.
            {
                tbxLogg.AppendText("\r\n" + DateTime.Now.ToString("h:mm:ss tt") + error.Message);
                if (nyklient.Klient.Connected == false) { klienter.Remove(nyklient); nyklient.Klient.Close(); return; }
            }
            Lyssna(nyklient);
        }

        private async void SkickaFil(byte[] buffer,byte[] filstorlek, byte[] namn, int ID)
        {
            byte[] tal = BitConverter.GetBytes(1);
            int namnL = namn.Length;
            try
            {
                byte[] svar = new byte[1];
                await klienter[ID].Klient.GetStream().WriteAsync(tal, 0, 4);

                await klienter[ID].Klient.GetStream().WriteAsync(BitConverter.GetBytes(namnL), 0, 4);

                await klienter[ID].Klient.GetStream().WriteAsync(namn, 0, namnL);

                await klienter[ID].Klient.GetStream().WriteAsync(filstorlek, 0, 4);

                await klienter[ID].Klient.GetStream().ReadAsync(svar, 0, 1);
                bool bsvar = BitConverter.ToBoolean(svar, 0);
                if (bsvar)
                {
                    int filtal = BitConverter.ToInt32(filstorlek, 0);
                    await klienter[ID].Klient.GetStream().WriteAsync(buffer, 0, filtal);
                }
            }
            catch(Exception error) { tbxLogg.AppendText("\r\n" + error.Message); }
        }
        private async void SkickaNyAnvändare(string namn, int id)
        {
            byte[] bnamn =Encoding.Unicode.GetBytes(namn);  
            //int namntal = Encoding.Unicode.GetByteCount(namn);
            //byte[] btal = BitConverter.GetBytes(namntal);
            byte[] definitionsTal = BitConverter.GetBytes(3);
            byte[] bID = BitConverter.GetBytes(id);
            int namnLängd = bnamn.Length;
            try
            {
                for (int i = 0; i < klienter.Count; i++)
                {
                    await klienter[i].Klient.GetStream().WriteAsync(definitionsTal, 0, 4);
                    //await klienter[i].Klient.GetStream().WriteAsync(btal, 0, 4);
                    await klienter[i].Klient.GetStream().WriteAsync(BitConverter.GetBytes(namnLängd),0, 4);
                    await klienter[i].Klient.GetStream().WriteAsync(bnamn, 0, namnLängd);
                    await klienter[i].Klient.GetStream().WriteAsync(bID, 0, 4);
                }
            }
            catch(Exception error) { tbxLogg.AppendText("\r\n" + error.Message); }
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            lyssnare.Stop();
            foreach (AnvändarKlienter k in klienter)
            {
                k.Klient.Close();
            }
        } 
    }
}
