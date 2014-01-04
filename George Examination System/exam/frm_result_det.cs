using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;




using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;


namespace exam
{
    public partial class frm_result_det : Form
    {
        public frm_result_det()
        {
            InitializeComponent();
        }


        public CrystalDecisions.Windows.Forms.CrystalReportViewer  CrystalReport
        {
            get { return crystalReportViewer1; }
            set { crystalReportViewer1 = value; }
        }

    


        }
    }


