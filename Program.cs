using System;
using System.Text.Json;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;


namespace modul7_1302204080
{
    public class program
    {

        public static void Main(string[] args)
        {
            BankTransferConfiguration config = new BankTransferConfiguration();
            string langChoose = Console.ReadLine();
            if (langChoose == "en")
            {
                Console.WriteLine("Please insert the amount of money to transfer: ");
            }
            else
            {
                Console.WriteLine("Masukkan jumlah uang yang akan di-transfer: ");
            }

            int biaya = int.Parse(Console.ReadLine());
            int total = 0;
            if(biaya <= config.konfigurasi.transfer.threshold && langChoose == "en")
            {
                total = biaya + config.konfigurasi.transfer.low_fee;
                Console.WriteLine("Transfer fee = " + config.konfigurasi.transfer.low_fee);
                Console.WriteLine("Total amount = " + total);

            }
            else if (biaya <= config.konfigurasi.transfer.threshold && langChoose == "id")
            {
                total = biaya + config.konfigurasi.transfer.low_fee;
                Console.WriteLine("Biaya transfer = " + config.konfigurasi.transfer.low_fee);
                Console.WriteLine("Total biaya = " + total);
            }
            else if (biaya > config.konfigurasi.transfer.threshold && langChoose == "en")
            {
                total = biaya + config.konfigurasi.transfer.high_fee;
                Console.WriteLine("Transfer fee = " + config.konfigurasi.transfer.high_fee);
                Console.WriteLine("Total amount = " + total);
            }
            else if(biaya > config.konfigurasi.transfer.threshold && langChoose == "id")
            {
                total = biaya + config.konfigurasi.transfer.high_fee;
                Console.WriteLine("Biaya transfer = " + config.konfigurasi.transfer.high_fee);
                Console.WriteLine("Total biaya = " + total);
            }
            if (langChoose == "en") Console.WriteLine("Select Transfer Method: ");
            else Console.WriteLine("Pilih metode transfer: ");
            int z = 1;
            foreach (var x in config.konfigurasi.methods)
            {
                Console.WriteLine(z + "." + x);
                z++;
            }
            string pilihmethode = Console.ReadLine();
            if (langChoose == "en") Console.WriteLine("Please type " + config.konfigurasi.confirmation.en + 
                " to confirm the transaction");
            else Console.WriteLine("Ketik " + config.konfigurasi.confirmation.id + " untuk mengkonfirmasi transaksi");
            string memastikan = Console.ReadLine();
            if (langChoose == "en" && memastikan == config.konfigurasi.confirmation.en)
                Console.WriteLine("The transfer is completed");
            else if (langChoose == "id" && memastikan == config.konfigurasi.confirmation.id)
                Console.WriteLine("Proses transfer berhasil");
            else if (langChoose == "en" && memastikan != config.konfigurasi.confirmation.en)
                Console.WriteLine("Transfer is cancelled");
            else
                Console.WriteLine("Transfer dibatalkan");


        }

        class BankTransferConfiguration
        {
            public BankTransferConfig konfigurasi;
            public string path = "D:\\Project Koding Kuliah\\KPL\\jurnalmod7";//Tolong Ganti baris kode ini, karena file pathnya pasti berbeda
            public string FileName = "bank_transfer_config.json";

            public BankTransferConfiguration()
            {
                try
                {
                    BacaFileKonfigurasi();
                }
                catch
                {
                    setDefault();
                    WriteNewConfigFile();
                }
            }

            private BankTransferConfig BacaFileKonfigurasi()
            {
                string jsonString = File.ReadAllText(path + "\\" + FileName);
                konfigurasi = JsonSerializer.Deserialize<BankTransferConfig>(jsonString);
                return konfigurasi;
            }

            private void setDefault()
            {
                Transfer transfer = new Transfer(2500000, 6500, 15000);
                List<string> methods = new List<string>() { "RTO(real-time)", "SKN", "BI FAST"};
                Confirmation confirmation = new Confirmation("yes", "ya");
                konfigurasi = new BankTransferConfig("en", transfer, methods, confirmation);

            }

            private void WriteNewConfigFile()
            {
                JsonSerializerOptions options = new JsonSerializerOptions()
                {
                    WriteIndented = true
                };
                string jsonString = JsonSerializer.Serialize(konfigurasi, options);
                File.WriteAllText(path + "\\" + FileName, jsonString);
            }
        }



        class BankTransferConfig
        {
            public string lang { get; set; }
            public Transfer transfer { get; set; }
            public List<string> methods { get; set; }
            public Confirmation confirmation { get; set; }

            public BankTransferConfig() { }
            public BankTransferConfig(string lang, Transfer transfer, List<string> methods, Confirmation confirmation)
            {
                this.lang = lang;
                this.transfer = transfer;
                this.methods = methods;
                this.confirmation = confirmation;
            }
        }

        class Transfer
        {
            public int threshold { get; set; }
            public int low_fee { get; set; }
            public int high_fee { get; set; }
            public Transfer() { }
            public Transfer(int threshold, int low_fee, int high_fee)
            {
                this.threshold = threshold;
                this.low_fee = low_fee;
                this.high_fee = high_fee;
            }
        }

        class Confirmation
        {
            public string en { get; set; }
            public string id { get; set; }
            public Confirmation() { }
            public Confirmation(string en, string id)
            {
                this.en = en;
                this.id = id;
            }
        }
    }
}