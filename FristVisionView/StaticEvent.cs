using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstVisionView
{
    
    class StaticEvent
    {
        public event Action<string> OnMessage;
        public event Action<string> OnmessageShow;
    }
}
