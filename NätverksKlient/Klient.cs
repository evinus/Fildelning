using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using NätverksProgramServer;

namespace NätverksKlient
{
    public partial class Klient : Form
    {
        //TcpClient klient = new TcpClient();
        

        public Klient()
        {
            InitializeComponent();
            
            btnSendFile.Enabled = false;
            btnTaEmot.Enabled = false;
            Användare.Klient.NoDelay = false;
        }
       
        public void Connect()
        {
            //IPAddress adress = IPAddress.Parse(tbxIP.Text);
            
            try
            {
                Användare.Connect(tbxIP.Text, int.Parse(tbxport.Text), tbxNamn.Text);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Kunde inte ansluta"); return;
            }
            startalyssna();
            
            btnConnect.Enabled = false;
            btnSendFile.Enabled = true;
            btnTaEmot.Enabled = true;
        }
        private void startalyssna()
        {
            Användare.Lyssna();
            if(Användare.TypTal ==1)
            {
                byte[] buffer = null;
                Användare.TaEmotFildata(buffer);


                if (buffer != null)
                {
                    saveFileDialog1.FileName = Användare.FilNamn;
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(saveFileDialog1.FileName, buffer);
                    }
                }
            }
            if(Användare.TypTal == 2)
            {
                Användare.TaEmotMeddelande();

            }
            if(Användare.TypTal == 3)
            {
                Användare.NyAnvändare();
                UppdatareAnvändare();
            }
            startalyssna();
        }

        /* public async void StartSending ( )
        {
            if ( klient.Connected )
            { 
                  
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    byte[] filData = System.IO.File.ReadAllBytes(openFileDialog1.FileName);
                    byte[] tal = BitConverter.GetBytes(1);
                    byte[] nr = BitConverter.GetBytes(filData.Length);
                
                try
                    {
                        await klient.GetStream().WriteAsync(tal, 0, 4);    

                        await klient.GetStream().WriteAsync(nr, 0, 4);
                                            
                        await klient.GetStream().WriteAsync(filData, 0, filData.Length);
                          
                    }
                    catch (Exception error) { MessageBox.Show(error.Message, "Klientfel"); return; }
                }
            }
        }*/
        /*    public async void Lyssna()
         {
             
             byte[] bufferTemp = new byte[1024];
             byte[] buffer = null;
             byte[] nr = new byte[4];
             byte[] meddelande = new byte[200];
             List<byte> filen = new List<byte>();
             int antalMottagnaByte = 0;
             int filstorlek = 0;
             int tal;
             try
             { 
                 // tar emot ett tal som har skckats för att om det är en fil eller ett meddelande
                 byte[] talb = new byte[4];
                 await klient.GetStream().ReadAsync(talb, 0, 4);
                 tal = BitConverter.ToInt32(talb, 0);
                 if (tal == 1) // om det är en fil
                 {
                     //tar emot filstorleken
                     int n = await klient.GetStream().ReadAsync(nr, 0, 4);
                     filstorlek = BitConverter.ToInt32(nr, 0);
                     buffer = new byte[filstorlek];

                     //tar emot filen, är den stor, gör vi det i flera steg.
                     while (klient.Available > 0)
                     {
                         antalMottagnaByte += await klient.GetStream().ReadAsync(buffer, antalMottagnaByte, klient.Available);
                     }

                     DialogResult resultat = MessageBox.Show("Inkommande fil, Vill du ladda ner det?", "Ladda ner?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                     if (resultat == DialogResult.Yes)
                     {
                         if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                         {
                             //spara filen
                             File.WriteAllBytes(saveFileDialog1.FileName, buffer);
                         }
                     }
                 }
                 if (tal == 2) //Är det ett meddelande skriver vi ut den.
                 {
                     await klient.GetStream().ReadAsync(meddelande, 0, 200);
                     string meddeland = Encoding.Unicode.GetString(meddelande);
                     tbxLogg.AppendText("\r\n" + meddeland);
                 }
             }
             catch (Exception error) // Blir det fel skriv ut saker om felet.
             {
                 string bufferinfo = "Buffert: " + ((buffer == null) ? "Buffer ej skapad" : buffer.Length.ToString());
                 MessageBox.Show(error.Message + " Antalmotagnabytes " + antalMottagnaByte.ToString() + ", bufferstorleken " + bufferinfo + ", filstorlek: " + filstorlek.ToString());
                 return;
             }
             Lyssna();
         }
         */
        /*  public async void Skickameddelande (string meddelande)
        {
            try
            {
                //gör om talet 2 till bytes och skickar det
                byte[] tal = BitConverter.GetBytes(2);
                await klient.GetStream().WriteAsync(tal, 0, 4);
                //gör om meddelandet till bytes och skickar det
                byte[] medel = Encoding.Unicode.GetBytes(meddelande);
                await klient.GetStream().WriteAsync(medel, 0, medel.Length);

            }
            catch(Exception error)//blir det fel
            {
                MessageBox.Show(error.Message);
                return;
            }
        }
        */

        private void UppdatareAnvändare()
        {
            lbxAnvändare.Items.Clear();
            int id =0;
            string[] namn = Användare.FåAnvändarNamn( ref id);

            for (int i = 0; i < id; i++)
            {
               lbxAnvändare.Items.Add( namn[i]);
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!Användare.Klient.Connected) //Användare.Connect(tbxIP.Text, int.Parse(tbxport.Text), tbxNamn.Text);
                Connect();
        }

        private void btnSendFile_Click(object sender, EventArgs e)
        {
            // StartSending();

            

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                byte[] filData =File.ReadAllBytes(openFileDialog1.FileName);
                byte[] tal = BitConverter.GetBytes(1);
                byte[] storlek = BitConverter.GetBytes(filData.Length);
                byte[] namn = Encoding.Unicode.GetBytes(openFileDialog1.FileName);
                Användare.SkickaFil(namn, storlek, filData);
                    

            }
            
        }

        private void Klient_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult resultat = MessageBox.Show("Vill du avsluta", "stänger fönstret", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultat == DialogResult.No) e.Cancel = true; 
           // if (klient != null) klient.Close();
        }

        private void btnTaEmot_Click(object sender, EventArgs e)
        {
            btnTaEmot.Enabled = false;
            Användare.Lyssna();
            //Lyssna(); 
        }

        private void btnSkicka_Click(object sender, EventArgs e)
        {
            //Skickameddelande(tbxMeddelande.Text);
            Användare.Skickameddelande(tbxMeddelande.Text);
            tbxMeddelande.Text = "";
        }

        private void Klient_KeyUp(object sender, KeyEventArgs e)
        {
            
            if(e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = false;
                // Skickameddelande(tbxMeddelande.Text);
                Användare.Skickameddelande(tbxMeddelande.Text);
                tbxMeddelande.Text = "";
            }
        }

        private void lbxAnvändare_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
