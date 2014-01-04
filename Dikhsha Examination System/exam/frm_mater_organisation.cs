using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace exam
{
    public partial class frm_mater_organisation : Form
    {


        String s;
        String Exam_code;
        DataSet ds;
        static class_Application ob;
        int row;
        string query;


        public frm_mater_organisation()
        {
            InitializeComponent();
        }

        //---load event for organisation master--->
        private void frm_mater_organisation_Load(object sender, EventArgs e)
        {
            ob = new class_Application();
            fill_grid();
        }


        //---function for filling up the datagridview-->
        private void fill_grid()
        {
            row = 0;

            txtOrgName.Text = null;
            txtOrgAddress.Text = null;
            txtOrgState.Text = null;
            txtOrgCity.Text = null;
            txtOrgPin.Text = null;
            txtOrgPhone.Text = null;
            txtOrgFax.Text = null;
            txtOrgContactPerson.Text = null;
            txtOrgContactNo.Text = null;
            txtOrgEmail.Text = null;
            txtOrgWebsite.Text = null;


            class_Application.flag = 1;
            ds = new DataSet();


            s = null;

            s = "SELECT Organisation_Master.Org_Code, Organisation_Master.Org_Name, Organisation_Master.Address, Organisation_Master.State, Organisation_Master.City, Organisation_Master.Pin, Organisation_Master.Phone_No, Organisation_Master.Fax_No, Organisation_Master.Contact_Person, Organisation_Master.Contact_No, Organisation_Master.Email,website FROM Organisation_Master;";
           //--loadingthe datagrid view with the dataset--->
            ds = ob.fill_data_set(s);
            dataGridView1.DataSource = ds.Tables[0];
            //---assigning the default cell fill type property for the datagridveiw1--->
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[3].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[4].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[5].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[6].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[7].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[8].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[9].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[10].SortMode = DataGridViewColumnSortMode.Programmatic;
            dataGridView1.Columns[11].SortMode = DataGridViewColumnSortMode.Programmatic;
            //dataGridView1.Columns[0].HeaderText = "Exam Code";
            //dataGridView1.Columns[1].HeaderText = "Exam Name";

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            fill_grid();
        }


    }
}
