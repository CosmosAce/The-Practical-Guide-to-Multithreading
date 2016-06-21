using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The_Practical_Guide_to_Multithreading
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Delegates - same as function pointers.
        // First line declares type, second declares variable
        delegate void DelegateType(int x);
        DelegateType TheDelegate;

        // We store the Start and End values in class-variable,
        // so that 'thread' actually does only processing.
        int StartFrom, EndTo;

        private void btn_Proceso_Click(object sender, EventArgs e)
        {
            // Set the delegate.
            TheDelegate = MessageHandler;

            StartFrom = Convert.ToInt32(txtStart.Text);
            EndTo = Convert.ToInt32(txtEnd.Text);

            progressBar1.Minimum = StartFrom;
            progressBar1.Maximum = EndTo;

            // Disable button, so that user cannot start again.
            btn_Proceso.Enabled = false;

            // Setup thread and start!
            Thread MyThread = new Thread(ProcessRoutine);
            MyThread.Start();
        }

        void MessageHandler(int nProgress)
        {
            lblStatus.Text = "Processing item: " + Convert.ToString(nProgress);
            progressBar1.Value = nProgress;
        }



        void ProcessRoutine()
        {
            for (int nValue = StartFrom; nValue <= EndTo; nValue++)
            {
                // Only actual delegates be called with Invoke, and not functions!
                this.Invoke(this.TheDelegate, nValue);
            }
        }

    }
}
