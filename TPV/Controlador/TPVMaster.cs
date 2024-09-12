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

        // Clave compartida entre el cliente y el servidor:
        private byte[] _claveCompartida = Encoding.UTF8.GetBytes(DateTime.Now.ToString("ddMMyyyy"));

        public TPVMaster()
        {
            _isRunning = false;
            Iniciar();
        }

        public bool Iniciar()
        {
            if (!_isRunning)
            {
                try
                {
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
            if (_isRunning)
            {
                _isRunning = false;
                MessageBox.Show("Cerrando procesos master");
            }
        }

        private int Listener()
        {
            this._isRunning = true;
            int estado = 1;

            _tcpListener = new TcpListener(IPAddress.Any, 12700);
            _tcpListener.Start();
            MessageBox.Show("A la escucha de llamadas");

            while (_isRunning)
            {
                TcpClient? _cliente = null;
                try
                {
                    _cliente = _tcpListener.AcceptTcpClient();
                    MessageBox.Show("Cliente conectado");

                    // Procesar el cliente
                    _hiloCliente = new Thread(async () => await ProcesarCliente(_cliente));
                    _hiloCliente.Start();
                }
                catch (InvalidOperationException) { return -2; }
                catch (Exception)
                {
                    return -1;
                }
                finally
                {
                    _cliente!.Close();
                }
            }

            _tcpListener!.Stop();
            estado = 0;
            return estado;
        }

        private async Task<bool> ProcesarCliente(TcpClient cliente)
        {
            try
            {
                NetworkStream stream = cliente.GetStream();
                await _bloqueo.WaitAsync();
                try
                {
                    // Cifrar el número de ticket más alto antes de enviarlo
                    string lastTicketNum = ControladorComun.BD!.SelectMAXTicketT(2024,1);
                    byte[] data = Encoding.UTF8.GetBytes(lastTicketNum);
                    byte[] encryptedData = EncryptData(data, _claveCompartida); //Aquí encriptamos los datos

                    // Enviar los datos cifrados al cliente
                    await stream.WriteAsync(encryptedData, 0, encryptedData.Length);
                }
                finally
                {
                    _bloqueo.Release();
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

        // Método para cifrar los datos
        private byte[] EncryptData(byte[] data, byte[] key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.Mode = CipherMode.ECB;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(data, 0, data.Length);
                        csEncrypt.FlushFinalBlock();
                        return msEncrypt.ToArray();
                    }
                }
            }
        }
    }
}
