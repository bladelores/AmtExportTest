using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modem.Amt.Export;
using Modem.Amt.Export.Data;
using Modem.Amt.Export.Utility;
using System.IO;
using System.IO.Pipes;


namespace Modem.Amt.Export.Connections
{
    public class TestConnection: IRealtimeConnection
    {
        public NamedPipeClientStream PipeClient { get; set; }
        private long wellboreId;
        private List<Parameter> parameters;

        public void ConfigureConnection(long wellboreId, List<Parameter> parameters) {
            this.wellboreId = wellboreId;
            this.parameters = parameters;
        }
        
        public async System.Threading.Tasks.Task<decimal[]> GetNewData()
        {
            var data = await Task<decimal[]>.Run(() =>
            {
                StreamString reader = new StreamString(PipeClient);
                var s = reader.ReadString();
                if (s.Equals("end")) return null;
                List<string> parsedNumbers = s.Split(' ').ToList();
                List<decimal> numbers = parsedNumbers.Select(x => Decimal.Parse(x)).ToList();
                return numbers.ToArray();
            });
            Task.WaitAll();
            return data;
        }
    }
}
