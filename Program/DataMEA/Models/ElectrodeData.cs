﻿

using System.Collections.Generic;

namespace MEATaste.DataMEA.Models
{
    public class ElectrodeData
    {
        public ElectrodeProperties Electrode { get; set; }
        public ushort[] RawSignalUShort { get; set; }
        public double[] RawSignalDouble { get; set; }
        public List<SpikeDetected> SpikeTimes { get; set; }

        public ElectrodeData(ElectrodeProperties ec)
        {
            Electrode = ec;
        }
    }


}
 