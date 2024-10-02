using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Windows;

namespace TPV.Controlador
{
    public class TPVMaster
    {
        private bool _isRunning;
        private Thread? _hiloCliente;
        private TcpListener? _tcpListener;
        // Semáforo para controlar la zona crítica
        private SemaphoreSlim _bloqueo = new SemaphoreSlim(1, 1);
        public bool IsRunning { get { return _isRunning; } }

        public TPVMaster()
        {
            this._isRunning = false;
            Iniciar();
        }

        public bool Iniciar()
        {
            if (!this._isRunning)
            {
                try
                {
                    //this._tcpListener = await _tcpListener.AcceptTcpClientAsync()
                    Listener();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public void Parar()
        {
            if (this._isRunning)
            {
                this._isRunning = false;
                MessageBox.Show("Cerrando procesos master");
            }
        }

        private async Task <int> Listener()
        {
            this._isRunning = true;
            int estado = 1;
            
            this._tcpListener = new TcpListener(IPAddress.Any, 12700);
            this._tcpListener.Start();

            while (this._isRunning)
            {              
                try
                {
                    TcpClient cliente = await this._tcpListener.AcceptTcpClientAsync();
                    MessageBox.Show("Cliente " + cliente.Client.RemoteEndPoint.ToString() + " conectado");

                    // Procesar el cliente
                    _ = Task.Run(() => ProcesarCliente(cliente));
                    //this._hiloCliente = new Thread(async () => await ProcesarCliente(cliente)); //Vetusto, C# no funciona bien usando Thread. En su lugar, uso Task.Run
                    //this._hiloCliente.Start();
                }
                catch (InvalidOperationException) { return -2; }
                catch (Exception)
                {
                    return -1;
                }
            }

            this._tcpListener!.Stop();
            estado = 0;
            return estado;
        }

        private async Task<bool> ProcesarCliente(TcpClient cliente)
        {
            try
            {
                NetworkStream stream = cliente.GetStream();
                await this._bloqueo.WaitAsync();
                try
                {
                    // Cifrar el número de ticket más alto antes de enviarlo
                    string lastTicketNum = ControladorComun.BD!.SelectMAXTicketT(ControladorComun.TiendaActual!.CodTienda);
                    lastTicketNum = "99999"; // Prueba
                    byte[] numTicketEnBytes = Encoding.UTF8.GetBytes(lastTicketNum);
                    byte[] iv = new byte[16] { 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0, 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0 };
                    byte[] encryptedData = EncryptData(lastTicketNum, ControladorComun.ClaveCompartida, ControladorComun.Iv); //Aquí encriptamos los datos

                    // Enviar los datos cifrados al cliente
                    await stream.WriteAsync(encryptedData.ToArray().AsMemory(0, encryptedData.Length));                  
                }
                finally
                {
                    this._bloqueo.Release();
                }

                stream.Close();
                cliente.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private byte[] EncryptData(string numTicket, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(numTicket);
                        }
                    }
                    byte[] msgCryptEnviar = msEncrypt.ToArray();
                    return msgCryptEnviar;
                }
            }
        }
    }
}
