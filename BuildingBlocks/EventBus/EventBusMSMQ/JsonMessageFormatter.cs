using Experimental.System.Messaging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using Epos.Utilities.Adapter;
using Epos.Utilities.Adapter.Infrastructure;
using Epos.Utilities.Adapter.Interfaces;
using Epos.Utilities.Adapter.Model;
using Epos.Utilities.Interfaces;

namespace Epos.Service.EventBus.EventBusMSMQ
{
    public class JsonMessageFormatter : IMessageFormatter
    {
        private Encoding encoding;
        private Type _type;
        private Message _message;
        public JsonMessageFormatter(Type type, Message message)
        {
            this.encoding = Encoding.UTF8;
            _type = type;
            _message = message;
        }
        public JsonMessageFormatter()
        {
            this.encoding = Encoding.UTF8;
        }
        public JsonMessageFormatter(Encoding encoding)
        {
            this.encoding = encoding;
        }
        public bool CanRead(Message message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            var stream = message.BodyStream;
            return stream != null
            && stream.CanRead
            && stream.Length > 0;
        }
        public object Clone()
        {
            return new JsonMessageFormatter(encoding);
        }
        public object Read()
        {
            if (_message == null)
            {
                throw new ArgumentNullException("message");
            }
            using (var reader = new StreamReader(_message.BodyStream, encoding))
            {
                var json = reader.ReadToEnd();

                var settings = new JsonSerializerSettings();
           
                settings.Converters.Add(new MyConcreteConverter<IPrintAdapter, PrintAdapter>());
                settings.Converters.Add(new MyConcreteConverter<IPrinterAdapterConfig, PrinterConfig>());
                settings.Converters.Add(new MyConcreteConverter<IFluentPrintDocumentBuilder, PrintDocumentBuilder>());
                settings.Converters.Add(new MyConcreteConverter<IPrintDocumentElementAttribute, TextStyle>());
                settings.Converters.Add(new MyConcreteConverter<IPrintDocumentElementAttribute, TextAttribute>());
                settings.Converters.Add(new MyConcreteConverter<IPrintDocumentElement, BaseTextLineDocumentElement>());
                settings.Converters.Add(new MyConcreteConverter<IPrintDocumentElement, BaseImageDocumentElement>());
                settings.Converters.Add(new MyConcreteConverter<IPrintDocumentElement, BaseNewLineDocumentElement>());


                return JsonConvert.DeserializeObject(json ,_type, settings);
            }
        }
        public object Read(Message message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            using (var reader = new StreamReader(message.BodyStream, encoding))
            {
                var json = reader.ReadToEnd();

                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new MyConcreteConverter<IPrinterAdapterConfig ,PrinterConfig>());
                settings.Converters.Add(new MyConcreteConverter<IFluentPrintDocumentBuilder, PrintDocumentBuilder>());

                return JsonConvert.DeserializeObject(json, _type, settings);
            }
        }

        public void Write(Message message, object obj)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new MyConcreteConverter<IPrintAdapter, PrintAdapter>());

            string json = JsonConvert.SerializeObject(obj,settings);
            message.BodyStream = new MemoryStream(encoding.GetBytes(json));
        }

        class MyConcreteConverter<I,C> : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return (objectType == typeof(I));
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                return serializer.Deserialize(reader, typeof(C));
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value, typeof(C));
            }
        }

    }
}


