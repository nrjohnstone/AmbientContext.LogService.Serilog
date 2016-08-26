using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace AmbientContext.LogService.Serilog
{
    public class AmbientLogService : AmbientService<ILogger>
    {
        protected override ILogger DefaultCreate()
        {
            return new SerilogLogAdapter(Log.Logger);
        }
    }
}
