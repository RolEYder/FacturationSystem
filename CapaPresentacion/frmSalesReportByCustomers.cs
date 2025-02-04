﻿namespace CapaPresentacion
{
    using CapaNegocio;
    using Entidades;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="frmSalesReportByCustomers" />.
    /// </summary>
    public partial class frmSalesReportByCustomers : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="frmSalesReportByCustomers"/> class.
        /// </summary>
        public frmSalesReportByCustomers()
        {
            InitializeComponent();
        }

        /// <summary>
        /// The buildDataGridView.
        /// </summary>
        private void buildDataGridView()
        {
            dgvLayout.Columns.Add("Id", "Codigo venta");
            dgvLayout.Columns.Add("CodigoV", "Codigo venta");
            dgvLayout.Columns.Add("NombreCli", "Nombre Cliente");
            dgvLayout.Columns.Add("CeluarClie", "Celular Cliente");
            dgvLayout.Columns.Add("CorreoCli", "Correo Cliente");
            dgvLayout.Columns.Add("DireccionCli", "Direccion");
            dgvLayout.Columns.Add("EstadoCli", "Estado cliente");
            dgvLayout.Columns.Add("FechaVenta", "Fecha venta");

            dgvLayout.Columns[0].Visible = false;
            dgvLayout.ReadOnly = false;
            dgvLayout.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewCellStyle css = new DataGridViewCellStyle();
            css.Alignment = DataGridViewContentAlignment.TopCenter;
            dgvLayout.ColumnHeadersDefaultCellStyle = css;
            dgvLayout.AllowUserToAddRows = false;
            dgvLayout.AllowUserToResizeColumns = false;
            dgvLayout.AllowUserToResizeRows = false;
            dgvLayout.ReadOnly = false;
            dgvLayout.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        /// <summary>
        /// The uploadDates.
        /// </summary>
        private void uploadDates()
        {
            try
            {
                dgvLayout.Rows.Clear();
                List<entSale> v = IBusinessSale.Instance.ListSalesByCustomerName(txtSearch.Text);
                for (int i = 0; i < v.Count; i++)
                {
                    String[] values = new String[]
                   {
                        v[i].Id_Venta.ToString(),v[i].Codigo_Venta.ToString(), v[i].cliente.Customer_Name.ToString(), v[i].cliente.Customer_Cellphone,
                        v[i].cliente.Customer_Email, v[i].cliente.Customer_Address, v[i].cliente.Customer_State.ToString(),
                        v[i].FechaVenta.ToString("dd-MM-yyyy")

                   };

                    dgvLayout.Rows.Add(values);

                }




            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// The reportVentasXClientes_Load.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void reportVentasXClientes_Load(object sender, EventArgs e)
        {

            label2.Visible = false;
            buildDataGridView();
        }

        /// <summary>
        /// The Buscar_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void Buscar_Click(object sender, EventArgs e)
        {
            uploadDates();
        }

        /// <summary>
        /// The dgvLayout_CellClick.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="DataGridViewCellEventArgs"/>.</param>
        private void dgvLayout_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string i = Convert.ToString(dgvLayout.CurrentRow.Cells[5].Value);
            if (i == "False")
            {
                label2.Visible = true;
            }
            int id = Convert.ToInt32(dgvLayout.CurrentRow.Cells[0].Value);
            frmSalesByCustomers cargar = new frmSalesByCustomers(id);
            cargar.Show();
        }
    }
}
