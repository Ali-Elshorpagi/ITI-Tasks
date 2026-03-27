using System;
using System.Collections.Generic;
using System.Text;

namespace Task03
{
    internal enum NICType : byte
    {
        Ethernet,
        TokenRing
    }
    internal class NIC
    {
        private static NIC? obj;
        public string Manufacture { get; }
        public string MACAddress { get; }
        public NICType Type { get; }
        private NIC(string manufacture, string macAddress, NICType type)
        {
            Manufacture = manufacture;
            MACAddress = macAddress;
            Type = type;
        }
        public static NIC GetOrCreateObj(string manufacture, string macAddress, NICType type)
        {
            if (obj is null)
                obj = new NIC(manufacture, macAddress, type);

            return obj;
        }
        public override string ToString()
        {
            return $"NIC Info:\n" +
                   $"Manufacture: {Manufacture}\n" +
                   $"MAC Address: {MACAddress}\n" +
                   $"Type: {Type}";
        }
    }
}
