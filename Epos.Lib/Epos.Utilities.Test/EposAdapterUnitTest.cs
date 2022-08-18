using Epos.Utilities.Adapter;
using Epos.Utilities.Interfaces;
using Epos.Utilities.Printers;
using System;
using System.Collections.Generic;
using Xunit;
using Epos.Utilities.Adapter.Model;
using Epos.Utilities.Adapter.Infrastructure;
using System.Collections;
using Epos.Utilities.Test.Model;

namespace Epos.Utilities.Test
{

    public class EposAdapterUnitTest
    {
        const string _actionMethods = "Create_And_Check_Result";

        [Theory]
        [PrintTestEmbeddedResource("Data/PrintTestData.json")]
        [Trait(_actionMethods, "Create_Multiple_Printer_And_Check_Outputs")]
        public void Adapter_Print_Test(IEnumerable<PrintTestModel> factModel)
        {
            foreach(var model in factModel)
            {
                var adpter = new PrintAdapter();
                var config = new PrinterConfig()
                {
                    ModelName = model.ModelName,
                    BrandName = model.BrandName,
                    IpAddress = model.IpAddress,
                    PrinterName = model.PrinterName,
                    Port = model.Port,
                    TimeOut = model.TimeOut
                };

                adpter.SetPrinterConfig(config);

                adpter.StatusChanged += delegate (object sender, EventArgs e)
                {
                    var printerStatus = (PrinterStatusEventArgs)e;
                    var actual = printerStatus.PrintData;
                    var excepted = model.Expected;

                    excepted = excepted.Replace(@"'", @"""");
                    actual = actual.Replace("\r\n", string.Empty);

                    System.Diagnostics.Debug.WriteLine($"{printerStatus.PrintData}");
                    System.Diagnostics.Debug.WriteLine("===========================");

                    Assert.Equal(actual, excepted);
                };

                var document = PrintDocumentBuilder.Start()
                        .PrintLine(model.TextToPrint)
                        .SetStyle(x => { x.Height = "80px"; x.Width = "100px"; });

                adpter.SetPrintDocument(document);
                adpter.SendPrintRequest();

            }
          
        }

        [Fact]
        public void Epos_Adapter_Test()
        {
            var epsonAdpter = new PrintAdapter();
            var epsonConfig = new PrinterConfig()
            {
                ModelName = "TM-L90-i",
                BrandName = "EPSON",
                IpAddress = "192.168.7.12",
                PrinterName = "Ep-test",
                Port = 80,
                TimeOut = 120
            };

            epsonAdpter.SetPrinterConfig(epsonConfig);
            epsonAdpter.StatusChanged += StatusChanged;

            var epsonDocument = PrintDocumentBuilder.Start()

                    .PrintLine("Its a Test Print!")
                    .PrintLine("Print Hello World !!!");
                  

            epsonAdpter.SetPrintDocument(epsonDocument);
            epsonAdpter.SendPrintRequest();

            //Define New Printer
            ////////////////////////////////
            ///
            var cannonAdpter = new PrintAdapter();
            var cannonConfig = new PrinterConfig()
            {
                ModelName = "CP-100",
                BrandName = "Cannon",
                IpAddress = "192.168.158.1",
                PrinterName = "Mahmoodreza_Printer",
                Port = 80,
                SerialPort = 11,
                TimeOut = 120
            };

            cannonAdpter.SetPrinterConfig(cannonConfig);
            cannonAdpter.StatusChanged += StatusChanged;

            var cannonDocument = PrintDocumentBuilder.Start().PrintLine("Say Hello Cannon!!");

            cannonAdpter.SetPrintDocument(cannonDocument);
            cannonAdpter.SendPrintRequest();

        }


        [Fact]
        public void Fluent_Print_Document_Builder()
        {

            var cannonDocument = PrintDocumentBuilder.Start()
                .PrintLine("Say Hello Cannon!!")
                   .SetStyle(x => { x.Height = "80px"; x.Width = "100px"; })
                        .PrintLine("Say Hello Cannon-1-1!!")
                                .PrintLine("Say Hello Cannon-1-2!!")
                .SetStyle(x =>  { x.Height = "500px"; x.Width = "100px"; })
                .PrintLine("Say Hello Epson!!")
                .Build();

        }

        private static void StatusChanged(object sender, EventArgs ps)
        {
            var printerStatus = (PrinterStatusEventArgs)ps;
            System.Diagnostics.Debug.WriteLine($"{printerStatus.PrintData}");
            System.Diagnostics.Debug.WriteLine("============================================");
        }
    }
}
