using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NätverksProgramServer;

public static class Användare
{
    private static TcpClient klient = new TcpClient();
    public static TcpClient Klient
    {
        get { return klient; }
        set { klient = value; }
    }

    public static string AnvändarNamn { get; set; }
    public static string Meddelande { get; set; }
    public static string NamnMeddelande { get; set; }
    public static int TypTal { get; set; }
    public static int FilStorlek { get; set; }
    public static string FilNamn { get; set; }

    static List<AnvändarKlienter> klienter = new List<AnvändarKlienter>();

    public static async void Connect(string adress, int port, string namn)
    {
        AnvändarNamn = namn;
        byte[] _namn = Encoding.Unicode.GetBytes(AnvändarNamn);
        IPAddress Adress = IPAddress.Parse(adress);
        try
        {
            await Klient.ConnectAsync(Adress, port);
            await Klient.GetStream().WriteAsync(_namn, 0, 8);
            
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
            MessageBox.Show(error.Message);
        }
    }

    public static async void Lyssna()
    {
        try
        {
            byte[] talBesk = new byte[4];
            await Klient.GetStream().ReadAsync(talBesk, 0, 4);
            TypTal = BitConverter.ToInt32(talBesk, 0);

           
        }
        
        catch
        { }
    }
   
    public static async void TaEmotMeddelande()
    {
        try
        {
            byte[] meddelande = new byte[200];
            byte[] namnMeddelande = new byte[4];
            await Klient.GetStream().ReadAsync(namnMeddelande, 0, 4);
            await Klient.GetStream().ReadAsync(meddelande, 0, 200);
            Meddelande = Encoding.Unicode.GetString(meddelande);
        }
        catch (Exception error)
        { Console.WriteLine(error.Message); }
    }
    public static async void TaEmotFildata(byte[] filbuffer)
    {
        byte[] filnamn = new byte[10];
        byte[] filstorlek = new byte[4];
        byte[] svar;
        try
        {
            await Klient.GetStream().ReadAsync(filnamn, 0, 10);

            await Klient.GetStream().ReadAsync(filstorlek, 0, 4);

            FilNamn = Encoding.Unicode.GetString(filnamn);

            FilStorlek = BitConverter.ToInt32(filstorlek, 0);
            string sträng = string.Format("Namn: {0} Filstorlek: {1} Bytes.", FilNamn, FilStorlek);
            if( MessageBox.Show(sträng, "Inkommande fil", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bool boolsvar = true;
                svar = BitConverter.GetBytes(boolsvar);
                await Klient.GetStream().WriteAsync(svar, 0, 1);
                TaEmotFil(filbuffer, FilStorlek);
            }
            else
            {
                bool boolsvar = false;
                svar = BitConverter.GetBytes(boolsvar);
                await Klient.GetStream().WriteAsync(svar, 0, 1);
            }
        }
        catch(Exception error) { Console.WriteLine(error.Message); }
    }

    public static async void TaEmotFil(byte[] filbuffer, int filstorlek)
    {
        try
        {
            int antalMottgnaByte = 0;
            filbuffer = new byte[filstorlek];

            while (filstorlek > antalMottgnaByte)
            {
                antalMottgnaByte += await Klient.GetStream().ReadAsync(filbuffer, antalMottgnaByte, filstorlek);
            }

            
        }
        catch(Exception error)
        { Console.WriteLine(error.Message); }
    }
    public static async void Skickameddelande(string meddelande)
    {
        try
        {
            //gör om talet 2 till bytes och skickar det
            byte[] tal = BitConverter.GetBytes(2);
            await Klient.GetStream().WriteAsync(tal, 0, 4);
            //gör om meddelandet till bytes och skickar det
            byte[] medel = Encoding.Unicode.GetBytes(meddelande);
            int längdMedelande = medel.Length;
            await Klient.GetStream().WriteAsync(BitConverter.GetBytes(längdMedelande), 0, 4);
            await Klient.GetStream().WriteAsync(medel, 0, medel.Length);

        }
        catch (Exception error)//blir det fel
        {
            Console.WriteLine(error.Message);
            return;
        }
    }
    /// <summary>
    /// Skickar en fil till servern
    /// </summar>
    /// <param name="namn">namnet på filen i bytes.</param>
    /// <param name="storlek">Storlek i bytes på filen</param>
    /// <param name="fildata">Sjäva filens data i byte</param>
    public static async void SkickaFil( byte[] namn, byte[] storlek, byte[] fildata)
    {
        
        if(Klient.Connected)
        {
            byte[] tal = BitConverter.GetBytes(1);
            int namnL = namn.Length;
            try
            {
                await Klient.GetStream().WriteAsync(tal, 0, 4);

                await klient.GetStream().WriteAsync(BitConverter.GetBytes(namnL), 0, 4);

                await Klient.GetStream().WriteAsync(namn, 0, namnL);

                await Klient.GetStream().WriteAsync(storlek, 0, 4);

                await Klient.GetStream().WriteAsync(fildata, 0, BitConverter.ToInt32(storlek, 0));
            }
            catch(Exception error) { Console.WriteLine(error.Message); }
        }
    }

    public static async void NyAnvändare( )
    {
        int namnLängd;
        byte[] namnbyteLängd = new byte[4];
        byte[] bID = new byte[4];
        await Klient.GetStream().ReadAsync(namnbyteLängd, 0, 4);
        namnLängd = BitConverter.ToInt32(namnbyteLängd, 0);
        byte[] bnamn = new byte[namnLängd];
        await Klient.GetStream().ReadAsync(bnamn, 0, namnLängd);
        await Klient.GetStream().ReadAsync(bID, 0, 4);

        AnvändarKlienter nyklient = new AnvändarKlienter(BitConverter.ToInt32(bID, 0));
        nyklient.Namn = Encoding.Unicode.GetString(bnamn);
        klienter.Add(nyklient);
    }

    public static string[] FåAnvändarNamn(ref int id)
    {
        id = klienter.Count;
        string[] namn = new string[id];
        int i = 0;
        foreach(AnvändarKlienter k in klienter)
        {
            namn[i] = k.Namn;
            i++;
        }
        return namn;
    }
}

