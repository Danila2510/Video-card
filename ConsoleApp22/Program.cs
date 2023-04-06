
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp22
{
    internal class Program
    {
        public class FasadeComputer
        {
            protected VideoCard videoCard;
            protected RAM ram;
            protected HDD hdd;
            protected ODD odd;
            protected PSU psu;
            protected Sensor sensor;
            public FasadeComputer(VideoCard videoCard, RAM ram, HDD hdd, ODD odd, PSU psu, Sensor sensor)
            {
                this.videoCard = videoCard;
                this.ram = ram;
                this.hdd = hdd;
                this.odd = odd;
                this.psu = psu;
                this.sensor = sensor;
            }
            public void Start()
            {
                while (true)
                {
                    if (!psu.ElectricitySupply())
                    {
                        Console.WriteLine("An error has occurred in the power supply");
                        break;
                    }
                    Console.WriteLine("Voltage is applied");

                    if (!sensor.VoltageTest(true))
                    {
                        Console.WriteLine("Voltage unstable");
                        break;
                    }
                    Console.WriteLine("Voltage is OK");

                    if (!sensor.CheckPowerSupplyTemp(psu))
                    {
                        Console.WriteLine("PSU temperature is unstable");
                        break;
                    }
                    Console.WriteLine("The temperature of the power supply is normal");

                    if (!sensor.CheckVideoCardTemp(videoCard))
                    {
                        Console.WriteLine("Graphics card temperature is unstable");
                        break;
                    }
                    Console.WriteLine("The temperature of the video card is normal");

                    if (!psu.CheckPowerSupplyToVideoCard())
                    {
                        Console.WriteLine("An error has occurred in the power supply");
                        break;
                    }
                    Console.WriteLine("Power is being supplied to the video card");

                    if (!videoCard.VideoCardLaunch())
                    {
                        Console.WriteLine("An error has occurred in the video card");
                        break;
                    }
                    Console.WriteLine("Video card starts up");

                    if (!videoCard.MonitorCheck())
                    {
                        Console.WriteLine("An error has occurred in the video card, there is no access to the monitor");
                        break;
                    }
                    Console.WriteLine("Communicating with the monitor");

                    if (!sensor.CheckRAM(ram))
                    {
                        Console.WriteLine("RAM temperature is unstable");
                        break;
                    }
                    Console.WriteLine("RAM temperature normal");

                    if (!psu.PowerSupplyRAM())
                    {
                        Console.WriteLine("An error has occurred in the power supply");
                        break;
                    }
                    Console.WriteLine("Power supply to RAM");

                    if (!ram.LaunchDevices())
                    {
                        Console.WriteLine("Power supply to RAM");
                        break;
                    }
                    Console.WriteLine("Devices start up");

                    if (!ram.DataAnalysis())
                    {
                        Console.WriteLine("Error in RAM");
                        break;
                    }
                    Console.WriteLine("Memory analysis in progress");

                    Console.WriteLine($"Information about RAM:\n{videoCard.ShowRAM(ram)}");

                    if (!psu.SupplyingPowerODR())
                    {
                        Console.WriteLine("An error has occurred in the power supply");
                        break;
                    }
                    Console.WriteLine("Applying Power to the Disc Reader");

                    if (!odd.StartingOpticalDiscReader())
                    {
                        Console.WriteLine("Error starting reading optical discs");
                        break;
                    }
                    Console.WriteLine("Starting the optical disc reader");

                    if (!odd.CheckingDiskPresence(true))
                    {
                        Console.WriteLine("Error! Missing optical disc");
                        break;
                    }
                    Console.WriteLine("The presence of an optical disc");

                    Console.WriteLine($"Information about optical discs:\n{videoCard.ShowODD(odd)}");

                    if (!psu.PowerHDD())
                    {
                        Console.WriteLine("An error has occurred in the power supply");
                        break;
                    }
                    Console.WriteLine("Power is supplied to the hard drive");

                    if (!hdd.HDDStartup(true))
                    {
                        Console.WriteLine("Error! hard drive startup");
                    }
                    Console.WriteLine("Starting the hard drive");

                    if (!hdd.BootDiskCheck(true))
                    {
                        Console.WriteLine("Boot disk is corrupted");
                    }
                    Console.WriteLine("Boot disk is ok");

                    Console.WriteLine($"Hard disk information:\n{videoCard.ShowHDD(hdd)}");

                    break;
                }
            }
            public void Shutdown()
            {
                while (true)
                {
                    if (!hdd.DeviceShutdown())
                    {
                        Console.WriteLine("Error!");
                        break;
                    }
                    Console.WriteLine("Stopping the hard drive");

                    if (!ram.ClearingMemory())
                    {
                        Console.WriteLine("Memory clearing error");
                        break;
                    }
                    Console.WriteLine("Clearing RAM");

                    if (!odd.StartingPosition())
                    {
                        Console.WriteLine("Error! optical disc");
                        break;
                    }
                    Console.WriteLine("Return of optical discs");

                    if (!psu.PowerOutage())
                    {
                        Console.WriteLine("Error! Power supply");
                        break;
                    }
                    Console.WriteLine("Power off all devices");

                    if (sensor.VoltageTest(false))
                    {
                        Console.WriteLine("Error!");
                    }
                    Console.WriteLine("No voltage");
                    Console.WriteLine("Turning off the power supply");
                    break;

                }
            }
        }
        public class VideoCard
        {
            public double Temperature { get; set; }
            public VideoCard(double temperature)
            {
                Temperature = temperature;
            }
            public bool VideoCardLaunch()
            {
                return true;
            }
            public bool MonitorCheck()
            {
                return true;
            }
            public string ShowRAM(RAM ram)
            {
                return $"^Memory size: {ram.Memory}\n^Memory type:{ram.TypeRAM}\n^Used memory:{ram.MemoryUsage}\n^Temperature: {ram.Temperature}";
            }
            public string ShowODD(ODD odd)
            {
                return $"^Disk type {odd.TypeODD} - {odd.UseROM}";
            }
            public string ShowHDD(HDD hdd)
            {
                return $"Hard disk capacity:{hdd.Capacity}\nBuffer size {hdd.BufferSize}";
            }
        }
        public class RAM
        {
            public double Temperature { get; set; }
            public string TypeRAM { get; set; }
            public int Memory { get; set; }
            public double MemoryUsage { get; set; }
            public RAM(double temperature, string typeRAM, int memory, double memoryUsage)
            {
                Temperature = temperature;
                TypeRAM = typeRAM;
                Memory = memory;
                MemoryUsage = memoryUsage;
            }
            public bool LaunchDevices()
            {
                return true;
            }
            public bool DataAnalysis()
            {
                return true;
            }
            public bool ClearingMemory()
            {
                MemoryUsage = 0;

                return true;
            }

        }
        public class HDD
        {
            public string Capacity { get; set; }
            public int BufferSize { get; set; }
            public HDD(string capacity, int bufferSize)
            {
                Capacity = capacity;
                BufferSize = bufferSize;
            }

            public bool HDDStartup(bool test)
            {
                if (test)
                    return true;
                return false;
            }
            public bool BootDiskCheck(bool test)
            {
                if (test)
                    return true;
                return false;
            }

            public bool DeviceShutdown()
            {
                return true;
            }
        }
        public class ODD
        {
            public string TypeODD { get; set; }
            public string UseROM { get; set; }

            public ODD(string typeODD, string useROM)
            {
                TypeODD = typeODD;
                UseROM = useROM;
            }

            public bool StartingOpticalDiscReader()
            {
                return true;
            }
            public bool CheckingDiskPresence(bool test)
            {
                if (test)
                    return true;
                return false;
            }
            public bool StartingPosition()
            {
                return true;
            }
        }
        public class PSU
        {
            public double Temperature { get; set; }
            public PSU(double temperature)
            {
                Temperature = temperature;
            }

            public bool ElectricitySupply()
            {
                return true;
            }
            public bool CheckPowerSupplyToVideoCard()
            {
                return true;
            }
            public bool PowerSupplyRAM()
            {
                return true;
            }
            public bool SupplyingPowerODR()
            {
                return true;
            }
            public bool PowerHDD()
            {
                return true;
            }

            public bool PowerOutage()
            {
                return true;
            }
        }
        public class Sensor
        {
            public bool VoltageTest(bool test)
            {
                if (test)
                    return true;
                return false;
            }
            public bool CheckPowerSupplyTemp(PSU psu)
            {
                if (psu.Temperature > 50 || psu.Temperature < 30)
                    return false;
                return true;
            }
            public bool CheckVideoCardTemp(VideoCard videoCard)
            {
                if (videoCard.Temperature > 80 || videoCard.Temperature < 60)
                    return false;
                return true;
            }
            public bool CheckRAM(RAM ram)
            {
                if (ram.Temperature > 45 || ram.Temperature < 25)
                    return false;
                return true;
            }
        }
        static void Main(string[] args)
        {
            RAM ram = new RAM(30, "DDR5", 16, 15);
            VideoCard videoCard = new VideoCard(70);
            HDD hDD = new HDD("10 ТB", 500);
            ODD oDD = new ODD("DVD", "DVD-ROM");
            PSU psu = new PSU(50);
            Sensor sensor = new Sensor();
            FasadeComputer comp = new FasadeComputer(videoCard, ram, hDD, oDD, psu, sensor);
            comp.Start();
            comp.Shutdown();
        }
    }
}