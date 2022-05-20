using log4net.Core;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Access.Layer.Helpers.Logger
{
    public class CsvPatternLayout : PatternLayout
    {
        public override void ActivateOptions()
        {
            AddConverter("newfield", typeof(NewFieldConverter));
            AddConverter("endrow", typeof(EndRowConverter));
            base.ActivateOptions();
        }

        public override void Format(TextWriter writer, LoggingEvent loggingEvent)
        {
            var ctw = new CsvTextWritter(writer);
            ctw.WriteQuote();
            base.Format(ctw, loggingEvent);
        }
    }
}
