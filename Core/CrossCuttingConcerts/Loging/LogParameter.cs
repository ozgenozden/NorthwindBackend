using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerts.Loging
{
    public class LogParameter
    {
        //Product nesnesini ismi
        public string Name { get; set; }
        public string Value { get; set; }
        //Product type ı olmuş olabilir örnek olarak
        public object Type { get; set; }
    }
}
