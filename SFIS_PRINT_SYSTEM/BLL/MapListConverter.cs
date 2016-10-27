using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using System.Reflection;

namespace SFIS_PRINT_SYSTEM.BLL
{
   public class MapListConverter:JsonConverter
    {
        private IDictionary<string, string> _forbidden = new Dictionary<string, string>();
        private const string IGNORE_SUPER = "superclass";

        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        private object EnsureType(object value, Type targetType)
        {
            if (value == null)
            {
                return null;
            }
            if (targetType == null)
            {
                return value;
            }
            Type sourceType = value.GetType();
            if (!(sourceType != targetType))
            {
                return value;
            }
            TypeConverter converter = TypeDescriptor.GetConverter(targetType);
            if (!converter.CanConvertFrom(sourceType))
            {
                if (converter.CanConvertFrom(typeof(string)))
                {
                    string text = TypeDescriptor.GetConverter(value).ConvertToInvariantString(value);
                    return converter.ConvertFromInvariantString(text);
                }
                if (!targetType.IsAssignableFrom(sourceType))
                {
                    throw new InvalidOperationException(string.Format("Cannot convert object of type '{0}' to type '{1}'", value.GetType(), targetType));
                }
                return value;
            }
            return converter.ConvertFrom(value);
        }

        private object GetList(JsonReader reader)
        {
            IList<IDictionary<string, object>> list = new List<IDictionary<string, object>>();
            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonToken.Comment:
                        break;

                    case JsonToken.EndArray:
                        return list;

                    default:
                        {
                            IDictionary<string, object> item = (IDictionary<string, object>)this.GetObject(reader);
                            list.Add(item);
                            break;
                        }
                }
            }
            throw new JsonSerializationException("Unexpected end when deserializing array.");
        }

        private object GetObject(JsonReader reader)
        {
            object list = null;
            do
            {
                switch (reader.TokenType)
                {
                    case JsonToken.StartObject:
                        return this.SetObject(reader);

                    case JsonToken.StartArray:
                        list = this.GetList(reader);
                        break;

                    case JsonToken.StartConstructor:
                    case JsonToken.EndConstructor:
                        return reader.Value.ToString();

                    case JsonToken.Integer:
                        return this.EnsureType(reader.Value, typeof(int));

                    case JsonToken.Float:
                        return this.EnsureType(reader.Value, typeof(decimal));

                    case JsonToken.String:
                        if (!string.IsNullOrEmpty((string)reader.Value))
                        {
                            return reader.Value.ToString();
                        }
                        return null;

                    case JsonToken.Boolean:
                        return this.EnsureType(reader.Value, typeof(bool));

                    case JsonToken.Null:
                    case JsonToken.Undefined:
                        return null;

                    case JsonToken.EndObject:
                    case JsonToken.EndArray:
                        break;

                    case JsonToken.Date:
                        return this.EnsureType(reader.Value, typeof(DateTime));

                    default:
                        throw new JsonSerializationException("Unexpected token whil deserializing object: " + reader.TokenType);
                }
            }
            while (reader.Read());
            return list;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }
            if (((reader.TokenType != JsonToken.StartObject) && (reader.TokenType != JsonToken.StartArray)) && !((reader.TokenType != JsonToken.None) || reader.Read()))
            {
                return null;
            }
            return this.GetObject(reader);
        }

        private void SerializeDictionary(JsonWriter writer, IDictionary values)
        {
            writer.WriteStartObject();
            foreach (DictionaryEntry entry in values)
            {
                writer.WritePropertyName(entry.Key.ToString());
                this.SerializeValue(writer, entry.Value);
            }
            writer.WriteEndObject();
        }

        private void SerializeEnumValue(JsonWriter writer, object value, string enumName)
        {
            if ((this.EnumConverter == null) || this.EnumConverter.ContainsKey(enumName))
            {
                writer.WriteValue(value.ToString());
            }
            else
            {
                writer.WriteValue(value);
            }
        }

        private void SerializeList(JsonWriter writer, IList values)
        {
            writer.WriteStartArray();
            for (int i = 0; i < values.Count; i++)
            {
                this.SerializeValue(writer, values[i]);
            }
            writer.WriteEndArray();
        }

        private void SerializeObject(JsonWriter writer, object value)
        {
            Type type = value.GetType();
            TypeConverter converter = TypeDescriptor.GetConverter(type);
            if ((((converter != null) && !(converter is ComponentConverter)) && (converter.GetType() != typeof(TypeConverter))) && converter.CanConvertTo(typeof(string)))
            {
                writer.WriteValue(converter.ConvertToInvariantString(value));
            }
            else
            {
                writer.WriteStartObject();
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo info in properties)
                {
                    this.WriteMemberInfoProperty(writer, value, info);
                }
                writer.WriteEndObject();
            }
        }

        private void SerializeValue(JsonWriter writer, object value)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else if (!(value is IConvertible))
            {
                if (value is IList)
                {
                    this.SerializeList(writer, (IList)value);
                }
                else if (value is IDictionary)
                {
                    this.SerializeDictionary(writer, (IDictionary)value);
                }
                else
                {
                    Type type = value.GetType();
                    this.SerializeObject(writer, value);
                }
            }
            else
            {
                IConvertible convertible = value as IConvertible;
                switch (convertible.GetTypeCode())
                {
                    case TypeCode.Boolean:
                        writer.WriteValue((bool)convertible);
                        return;

                    case TypeCode.Char:
                        writer.WriteValue((char)convertible);
                        return;

                    case TypeCode.SByte:
                        writer.WriteValue((sbyte)convertible);
                        return;

                    case TypeCode.Byte:
                        writer.WriteValue((byte)convertible);
                        return;

                    case TypeCode.Int16:
                        writer.WriteValue((short)convertible);
                        return;

                    case TypeCode.UInt16:
                        writer.WriteValue((ushort)convertible);
                        return;

                    case TypeCode.Int32:
                        writer.WriteValue((int)convertible);
                        return;

                    case TypeCode.UInt32:
                        writer.WriteValue((uint)convertible);
                        return;

                    case TypeCode.Int64:
                        writer.WriteValue((long)convertible);
                        return;

                    case TypeCode.UInt64:
                        writer.WriteValue((ulong)convertible);
                        return;

                    case TypeCode.Single:
                        writer.WriteValue((float)convertible);
                        return;

                    case TypeCode.Double:
                        writer.WriteValue((double)convertible);
                        return;

                    case TypeCode.Decimal:
                        writer.WriteValue((decimal)convertible);
                        return;

                    case TypeCode.DateTime:
                        writer.WriteValue((DateTime)convertible);
                        return;

                    case TypeCode.String:
                        writer.WriteValue((string)convertible);
                        return;
                }
                this.SerializeObject(writer, value);
            }
        }

        private object SetObject(JsonReader reader)
        {
            IDictionary<string, object> target = new Dictionary<string, object>();
            while (reader.Read())
            {
                JsonToken tokenType = reader.TokenType;
                if (tokenType != JsonToken.PropertyName)
                {
                    if (tokenType != JsonToken.EndObject)
                    {
                        throw new JsonSerializationException("Unexpected token when deserializing object: " + reader.TokenType);
                    }
                    return target;
                }
                string key = reader.Value.ToString();
                if (key != "superclass")
                {
                    this.SetObjectProperty(reader, target, key);
                }
                else
                {
                    while (reader.Read())
                    {
                        if (reader.TokenType == JsonToken.EndObject)
                        {
                            break;
                        }
                    }
                }
            }
            throw new JsonSerializationException("Unexpected end when deserializing object.");
        }

        private void SetObjectProperty(JsonReader reader, object target, string key)
        {
            if (!reader.Read())
            {
                throw new JsonSerializationException(string.Format("Unexpected end when setting {0}'s value.", target));
            }
            if (!(target is IDictionary<string, object>))
            {
                throw new JsonSerializationException(string.Format("Object of type is '{0}', is not IDictionary", target.GetType().Name));
            }
            if (!this.NoSubmitMap.ContainsKey(key))
            {
                ((IDictionary<string, object>)target).Add(key, this.GetObject(reader));
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("jsonWriter");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            this.SerializeValue(writer, value);
        }

        private void WriteMemberInfoProperty(JsonWriter writer, object value, PropertyInfo member)
        {
            object obj2 = member.GetValue(value, null);
            writer.WritePropertyName(member.Name);
            if (obj2.GetType().IsEnum)
            {
                this.SerializeEnumValue(writer, obj2, member.Name);
            }
            else
            {
                this.SerializeValue(writer, obj2);
            }
        }

        public static string DictionaryToJson(IDictionary<string, object> dic)
        {
            return JsonConvert.SerializeObject(dic);
        }

        public static string ListDictionaryToJson(IList<IDictionary<string, object>> list)
        {
            return JsonConvert.SerializeObject(list);
        }

        public static IDictionary<string, object> JsonToDictionary(string jsonStr)
        {
            return (IDictionary<string, object>)JsonConvert.DeserializeObject(jsonStr, typeof(IDictionary<string, object>));
        }

        public static IList<IDictionary<string, object>> JsonToListDictionary(string jsonStr)
        {
            return (IList<IDictionary<string, object>>)JsonConvert.DeserializeObject(jsonStr, typeof(IList<IDictionary<string, object>>));
        }

        #region 添加压缩功能
        public byte[] GetDataSetSurrogateZipBytes(DataSet dataSet)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataSetSurrogate dss = new DataSetSurrogate(dataSet);
                BinaryFormatter ser = new BinaryFormatter();
                ser.Serialize(ms, dss);
                byte[] buffer = ms.ToArray();
                byte[] zipBuffer = Compress(buffer);

                ms.Close();
                ms.Dispose();
                return zipBuffer;
            }
        }

        private static byte[] Compress(byte[] data)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Stream zipStream = null;
                    zipStream = new GZipStream(ms, CompressionMode.Compress, true);
                    zipStream.Write(data, 0, data.Length);
                    zipStream.Close();
                    ms.Position = 0;
                    byte[] compressed_data = new byte[ms.Length];
                    ms.Read(compressed_data, 0, int.Parse(ms.Length.ToString()));

                    ms.Close();
                    ms.Dispose();
                    return compressed_data;
                }
            }
            catch
            {
                return null;
            }
        }

        public byte[] GetDataSetZipBytes(DataSet dataSet)
        {
            XmlSerializer ser = new XmlSerializer(typeof(DataSet));
            MemoryStream ms = new MemoryStream();
            ser.Serialize(ms, dataSet);

            byte[] buffer = ms.ToArray();
            byte[] zipBuffer = Compress(buffer);

            ms.Close();
            ms.Dispose();
            return zipBuffer;
        }
        #endregion

        #region 解压缩功能
        public DataTable arrByteToDataTable(byte[] zipBuffer)
        {
            if (zipBuffer == null || zipBuffer.Length < 1)
                return null;
            byte[] buffer = Decompress(zipBuffer);
            BinaryFormatter ser = new BinaryFormatter();
            DataSetSurrogate dss = ser.Deserialize(new MemoryStream(buffer)) as DataSetSurrogate;
            DataSet dataSet = dss.ConvertToDataSet();

            return dataSet.Tables[0];
        }

        public DataSet arrByteToDataSet(byte[] zipBuffer)
        {
            if (zipBuffer == null || zipBuffer.Length < 1)
                return null;
            byte[] buffer = Decompress(zipBuffer);
            BinaryFormatter ser = new BinaryFormatter();
            DataSetSurrogate dss = ser.Deserialize(new MemoryStream(buffer)) as DataSetSurrogate;
            DataSet dataSet = dss.ConvertToDataSet();

            return dataSet;
        }

        private static byte[] Decompress(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream(data);
                Stream zipStream = null;
                zipStream = new GZipStream(ms, CompressionMode.Decompress);
                byte[] dc_data = null;
                dc_data = ExtractBytesFromStream(zipStream, data.Length);
                return dc_data;
            }
            catch
            {
                return null;
            }
        }

        private static byte[] ExtractBytesFromStream(Stream zipStream, int dataBlock)
        {
            byte[] data = null;
            int totalBytesRead = 0;
            try
            {
                while (true)
                {
                    Array.Resize(ref data, totalBytesRead + dataBlock + 1);
                    int bytesRead = zipStream.Read(data, totalBytesRead, dataBlock);
                    if (bytesRead == 0)
                    {
                        break;
                    }
                    totalBytesRead += bytesRead;
                }
                Array.Resize(ref data, totalBytesRead);
                return data;
            }
            catch
            {
                return null;
            }
        }
        #endregion


        public IDictionary<string, bool> EnumConverter { get; set; }

        public IDictionary<string, string> NoSubmitMap
        {
            get
            {
                return this._forbidden;
            }
        }
    }


}
