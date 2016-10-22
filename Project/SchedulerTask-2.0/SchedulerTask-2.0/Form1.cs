using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Builder;

namespace SchedulerTask_2._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BuilderScheduler builder = new BuilderScheduler(
                @"..\..\..\TestData\TestDataBuilder\test1\", 
                @"..\..\..\TestData\TestDataBuilder\test1\"
            );
            builder.Run();
        }
    }
}
