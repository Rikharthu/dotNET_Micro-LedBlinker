using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using GHIElectronics.NETMF.FEZ;
using System.Threading;

namespace ButtonClicker
{
    public class Program
    {

        public static void Main()
        {
            // Izveido ieejas portu uz norādītās CPU kājas 
            InputPort button = new InputPort(
                (Cpu.Pin)FEZ_Pin.Digital.LDR,
                false, // Trokšņu filtrs: false -> izslēgts, true -> ieslēgts 
                Port.ResistorMode.PullUp); // Uzstādīt kāju uz high, kad nekas nav pievienots 

            OutputPort led = new OutputPort(
                (Cpu.Pin)FEZ_Pin.Digital.LED, 
                false);

            bool oldButton=false;

            while (true)
            {
                // Nolasīt kājas stāvokli : -true, high, false -> low Kad LDR poga ir nospiesta,
                // tā savieno LDR kāju ar zemi, iestatot to uz low. 
                bool buttonPressed = !button.Read();
                bool buttonToggled = buttonPressed && buttonPressed != oldButton;
                if (buttonToggled)
                {
                    led.Write(!led.Read());
                    Debug.Print("Button Toggled");
                }
                //if (buttonPressed)
                //{
                //    // toggle led
                //    led.Write(!led.Read());
                //    Debug.Print("Button pressed");
                //    //Thread.Sleep(50);
                //}
                Thread.Sleep(100);
                Debug.Print("buttonPressed: " + buttonPressed);
                oldButton = buttonPressed;
            }
        }
    }
}
