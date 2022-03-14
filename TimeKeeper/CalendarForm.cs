using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeKeeper
{
    public partial class CalendarForm : Form
    {
        public DateTime mdsNewToday = new DateTime();

        public CalendarForm()
        {
            InitializeComponent();
        }

        private void OnDateChanged_CalendarCLD(object aroS, DateRangeEventArgs aroEA)
        {
            mdsNewToday = mroCalendarCLD.SelectionStart;
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
