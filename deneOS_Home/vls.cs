using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace deneOS_Home
{
    internal class vls
    {
        public static int GetSystemVolume()
        {
            var enumerator = new MMDeviceEnumerator();
            var device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            return (int)(device.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
        }


        public static void SetSystemVolume(float level) // de 0.0f a 1.0f
        {
            var deviceEnumerator = new MMDeviceEnumerator();
            var device = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            device.AudioEndpointVolume.MasterVolumeLevelScalar = level;
        }

        public void ChangeVolume(float delta)
        {
            var deviceEnumerator = new MMDeviceEnumerator();
            var device = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

#pragma warning disable CS0436 // El tipo entra en conflicto con un tipo importado
            float newVol = Math.Clamp(device.AudioEndpointVolume.MasterVolumeLevelScalar + delta, 0f, 1f);
#pragma warning restore CS0436 // El tipo entra en conflicto con un tipo importado
            device.AudioEndpointVolume.MasterVolumeLevelScalar = newVol;
        }

    }
}
