using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using System.IO.Compression;
using Client.World.Definitions;

namespace Client.World.Network
{
    public sealed partial class InPacket
    {
        public TEnum ReadByteE<TEnum>() where TEnum : struct, IConvertible { return ReadEnum<TEnum, Byte>(); }
        public TEnum ReadInt32E<TEnum>() where TEnum : struct, IConvertible { return ReadEnum<TEnum, Int32>(); }

        private TEnum ReadEnum<TEnum, T>() where TEnum : struct, IConvertible where T : struct
        {
            var rawValue = Convert.ToInt64(ReadValue<T>());
            var value = Enum.ToObject(typeof(TEnum), rawValue);

            /*if (rawValue > 0)
                Logger.CheckForMissingValues<TEnum>(rawValue);*/

            return (TEnum)value;
        }

        private long ReadValue<T>() where T : struct
        {
            var code = Type.GetTypeCode(typeof(T));
            return ReadValue(code);
        }

        private long ReadValue(TypeCode code)
        {
            long rawValue = 0;
            switch (code)
            {
                case TypeCode.SByte:
                    rawValue = ReadSByte();
                    break;
                case TypeCode.Byte:
                    rawValue = ReadByte();
                    break;
                case TypeCode.Int16:
                    rawValue = ReadInt16();
                    break;
                case TypeCode.UInt16:
                    rawValue = ReadUInt16();
                    break;
                case TypeCode.Int32:
                    rawValue = ReadInt32();
                    break;
                case TypeCode.UInt32:
                    rawValue = ReadUInt32();
                    break;
                case TypeCode.Int64:
                    rawValue = ReadInt64();
                    break;
                case TypeCode.UInt64:
                    rawValue = (long)ReadUInt64();
                    break;
            }
            return rawValue;
        }
    }
}
