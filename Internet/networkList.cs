using EugenPechanec.NativeWifi;
using EugenPechanec.NativeWifi.Wlan;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Internet
{
    public partial class networkList : Form
    {
        [DllImport("wlanapi.dll")]
        public static extern int WlanGetAvailableNetworkList(
        IntPtr hClientHandle,
        ref Guid pInterfaceGuid,
        int dwFlags,
        IntPtr pReserved,
        out IntPtr ppAvailableNetworkList);
        [DllImport("wlanapi.dll")]
        public static extern int WlanOpenHandle(
    uint dwClientVersion,
    IntPtr pReserved,
    out uint pdwNegotiatedVersion,
    out IntPtr phClientHandle
);

        [DllImport("wlanapi.dll")]
        public static extern void WlanCloseHandle(
            IntPtr hClientHandle,
            IntPtr pReserved
        );
        private IntPtr GetClientHandle()
        {
            IntPtr clientHandle = IntPtr.Zero;
            uint negotiatedVersion;

            // Llamar a WlanOpenHandle para obtener el ClientHandle
            int result = WlanOpenHandle(
                2, // Versión del cliente (2 es común para Windows Vista y superior)
                IntPtr.Zero,
                out negotiatedVersion,
                out clientHandle
            );

            if (result != 0)
            {
                throw new Win32Exception(result, "Error al abrir el controlador WLAN");
            }

            return clientHandle;
        }

        private void ReleaseClientHandle(IntPtr clientHandle)
        {
            // Liberar el ClientHandle cuando ya no sea necesario
            WlanCloseHandle(clientHandle, IntPtr.Zero);
        }

        public networkList()
        {
            InitializeComponent();
        }

        private void networkList_Load(object sender, EventArgs e)
        {
            LoadNetworks();
        }

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {/* 
                    ConectarWiFi(comboBox1.SelectedItem.ToString()!);
                    EugenPechanec.NativeWifi.Wlan.WlanClient client = EugenPechanec.NativeWifi.Wlan.WlanClient.CreateClient();
                    WlanInterfaceInfo info;
                    info.
                    //crear interfaz de wifi con EugenPechanec.NativeWifi
                    EugenPechanec.NativeWifi.Wlan.WlanInterface wlanInterface = EugenPechanec.NativeWifi.Wlan.WlanInterface.CreateInterface(client, )*/
            }
        }
        private void ConectarWiFi(string ssid)
        {
            input input1 = new input();
            input.ssid = ssid;
            input1.ShowDialog();
            CrearPerfil(ssid, input1.contraseña);

            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "netsh",
                    Arguments = $"wlan connect name=\"{ssid}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };


            process.Start();
            process.WaitForExit();
            MessageBox.Show($"Intentando conectar a {ssid}");
        }

        void CrearPerfil(string ssid, string contraseña)
        {
            string password = contraseña;

            string xmlProfile = $@"
    <WLANProfile xmlns='http://www.microsoft.com/networking/WLAN/profile/v1'>
        <name>{ssid}</name>
        <SSIDConfig>
            <SSID>
                <name>{ssid}</name>
            </SSID>
        </SSIDConfig>
        <connectionType>ESS</connectionType>
        <connectionMode>auto</connectionMode>
        <authentication>WPA2PSK</authentication>
        <encryption>AES</encryption>
        <MSM>
            <security>
                <sharedKey>
                    <keyType>passPhrase</keyType>
                    <protected>false</protected>
                    <keyMaterial>{password}</keyMaterial>
                </sharedKey>
            </security>
        </MSM>
    </WLANProfile>";
            string path = Path.Combine(Path.GetTempPath(), "perfil.xml");
            File.WriteAllText(path, xmlProfile);
            Process.Start("netsh", $"wlan add profile filename=\"{path}\"").WaitForExit();
            File.Delete(path);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ButtonConnect_Click(sender, e);
        }
    

private void LoadNetworks()
        {/*
            IntPtr clientHandle = IntPtr.Zero;

            try
            {
                clientHandle = GetClientHandle();

                // Obtener la lista de interfaces de red  
                Guid interfaceGuid = Guid.Empty; // Aquí deberías obtener el GUID de la interfaz Wi-Fi que deseas usar  
                IntPtr availableNetworkListPtr;

                int result = WlanGetAvailableNetworkList(
                    clientHandle,
                    ref interfaceGuid,
                    (int)WlanGetAvailableNetworkFlags.IncludeAllAdhocProfiles,
                    IntPtr.Zero,
                    out availableNetworkListPtr
                );

                if (result != 0)
                {
                    throw new Win32Exception(result, "Error al obtener la lista de redes disponibles");
                }

                // Procesar la lista de redes disponibles  
                var availableNetworkList = Marshal.PtrToStructure<WlanAvailableNetworkList>(availableNetworkListPtr);

                // Convertir el puntero a una lista de redes  
                IntPtr currentNetworkPtr = availableNetworkList.Networks;
                for (int i = 0; i < availableNetworkList.NumberOfItems; i++)
                {
                    var network = Marshal.PtrToStructure<WlanAvailableNetwork>(currentNetworkPtr);

                    // Extraer el SSID de cada red  
                    string ssid = Encoding.UTF8.GetString(network.Ssid).TrimEnd('\0');
                    if (!string.IsNullOrEmpty(ssid))
                    {
                        comboBox1.Items.Add(ssid); // Añadir el SSID al comboBox1  
                    }

                    // Avanzar al siguiente elemento en la lista  
                    currentNetworkPtr = IntPtr.Add(currentNetworkPtr, Marshal.SizeOf<WlanAvailableNetwork>());
                }

                // Liberar memoria asignada por la API nativa  
                WlanFreeMemory(availableNetworkListPtr);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar redes Wi-Fi: {ex.Message}");
            }
            finally
            {
                if (clientHandle != IntPtr.Zero)
                {
                    ReleaseClientHandle(clientHandle);
                }
            }
            */
        }

[StructLayout(LayoutKind.Sequential)]
    internal struct WlanAvailableNetwork
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] Ssid;
        // Otros campos relevantes pueden ser añadidos aquí si es necesario  
    }
}
