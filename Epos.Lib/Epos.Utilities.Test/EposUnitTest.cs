using Epos.Utilities.Adapter;
using Epos.Utilities.Interfaces;
using Epos.Utilities.Printers;
using System;
using System.Collections.Generic;
using Xunit;
using Epos.Utilities.Adapter.Model;
using Epos.Utilities.Adapter.Infrastructure;
using Epos.Utilities.Printers.Configs;
using System.Linq;

namespace Epos.Utilities.Test
{
    public class EposUnitTest
    {

        [Theory]
        [InlineData("Hello World!!!")]
        public void Create_New_Network_Printer_And_Test_Output(string textToPrint)
        {
            var config = new NetworkConfig()
            {
                IpAddress = "192.168.7.55",
                Port = 8080,
                PrinterName = "test-printer12",
                TimeOut = 1000
            };
            var networkPrinter = new NetworkPrinter(config);

            networkPrinter.StatusChanged += StatusChanged;
            networkPrinter.PrintOutput(textToPrint);

        }

        private static void StatusChanged(object sender, EventArgs ps)
        {
            var printerStatus = (PrinterStatusEventArgs)ps;
            var actual = printerStatus.PrintData;
            var excepted = "Hello World!!!";
            Assert.Equal(actual, excepted);
        }

        [Fact]
        public void Fluent_Test_Api()
        {
            var printDoc = PrintDocumentBuilder.Start()

                .PrintLine("Print Hello World !!!")
                .PrintNewLine()
                .PrintImage("/../test/print.png")
                .Build();

            var printDocArr = printDoc.ToArray();

            Assert.True(printDocArr.Count() == 3);

            Assert.True(typeof(BaseTextLineDocumentElement) == printDocArr[0].GetType());
            Assert.True(typeof(BaseNewLineDocumentElement) == printDocArr[1].GetType());
            Assert.True(typeof(BaseImageDocumentElement) == printDocArr[2].GetType());

        }



    }
}
