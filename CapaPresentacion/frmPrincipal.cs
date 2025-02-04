﻿namespace CapaPresentacion
{
    using CapaNegocio;
    using Entidades;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the <see cref="frmPrincipal" />.
    /// </summary>
    public partial class frmPrincipal : Form
    {
        /// <summary>
        /// Defines the u.
        /// </summary>
        internal entUser u = new entUser();

        /// <summary>
        /// Initializes a new instance of the <see cref="frmPrincipal"/> class.
        /// </summary>
        /// <param name="usu">The usu<see cref="entUser"/>.</param>
        public frmPrincipal(entUser usu)
        {
            InitializeComponent();
            u = usu;
        }

        /// <summary>
        /// The RestrincionUsuario.
        /// </summary>
        private void UserRestrictions()
        {
            if (u.access_level.Numero_NivelAcc == 1)
            {
                efecturarToolStripMenuItem.Enabled = false;
                btnSales.Enabled = false;

            }

            if (u.access_level.Numero_NivelAcc == 2)
            {
                MSIseguridad.Enabled = false;
                MSIcliente.Enabled = true;
                btnCustomers.Enabled = true;
                MSIproducts.Enabled = false;
                MSIsalir.Enabled = true;
                MSIventa.Enabled = true;
                MSIQueries.Enabled = false;
                MSIreportes.Enabled = false;
                groupBox1.Visible = false;
                MSImatenimientos.Enabled = false;
                btnProducts.Enabled = false;
                btnSales.Enabled = false;
                btnUsers.Enabled = false;
                machineLearningToolStripMenuItem.Enabled = false;


            }
        }

        /// <summary>
        /// The MSILogOut_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void MSILogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin frmLogin = new frmLogin();
            frmLogin.Show();
        }

        /// <summary>
        /// The MSILogIn_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>



        /// <summary>
        /// The set.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task set()
        {
            await Task.Run(async () =>
            {
                while (true)
                {

                    await Task.Delay(1000);

                }

            });
        }

        /// <summary>
        /// The frmPrincipal_Load.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void frmPrincipal_Load(object sender, EventArgs e)
        {

            UserRestrictions();
            try
            {
                //Looking for current open forms
                Form frmlogin = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x is frmLogin);
                //if exits a instance, change form visibility
                if (frmlogin != null)
                {
                    frmlogin.Visible = false;
                }
                LBLusuario.Text = "WELCOME " + u.User_Name + " WITH ACCESS LEVEL " + (u.access_level.Numero_NivelAcc == 1 ? " ADMIN AUTH " : " SELLER AUTH ");

            }
            catch (ArgumentNullException ne)
            {
                MessageBox.Show(ne.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception nx)
            {
                MessageBox.Show(nx.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// The contextMenuStrip1_Opening.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="CancelEventArgs"/>.</param>


        /// <summary>
        /// The clientesToolStripMenuItem_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="CancelEventArgs"/>.</param>
        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmUser frmusuario = new frmUser(u.User_Id);
                //  frmusuario.MdiParent = frmusuario;
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is frmUser)
                    {
                        frm.Show();
                        frm.Size = MinimumSize;
                        frm.WindowState = FormWindowState.Normal;
                        return;
                    }

                }
                frmusuario.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        /// <summary>
        /// The mantenimientosToolStripMenuItem_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void mantenimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmManagement frmmante = new frmManagement(u.User_Id);
                //frmmante.MdiParent = this;
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is frmManagement)
                    {
                        frm.Show();
                        frm.Size = MinimumSize;
                        frm.WindowState = FormWindowState.Normal;
                        return;
                    }
                }
                frmmante.Show();
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// The productosToolStripMenuItem_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmIndicaProducto frmpo = new frmIndicaProducto();
                // frmpo.MdiParent = this;
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is frmIndicaProducto)
                    {
                        frm.Show();
                        frm.Size = MinimumSize;
                        frm.WindowState = FormWindowState.Normal;
                        return;
                    }
                }
                frmpo.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// The reportesToolStripMenuItem_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>


        /// <summary>
        /// The incicioToolStripMenuItem_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>


        /// <summary>
        /// The MSIsalir_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void MSIsalir_Click(object sender, EventArgs e)
        {
            DialogResult anwer = new DialogResult();
            anwer = MessageBox.Show("¿Está seguro de qué quiere salir?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (anwer == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        /// <summary>
        /// The MSIcliente_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void MSIcliente_Click(object sender, EventArgs e)
        {
            try
            {
                frmCustomer frmc = new frmCustomer(u.User_Id);
                // frmc.MdiParent = this;
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is frmCustomer)
                    {
                        frm.Show();
                        frm.Size = MinimumSize;
                        frm.WindowState = FormWindowState.Normal;
                        return;
                    }
                }
                frmc.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// The reporteGenerarDeProductosToolStripMenuItem_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>


        /// <summary>
        /// The MSIventa_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void MSIventa_Click(object sender, EventArgs e)
        {
            try
            {
                frmFacturation frmFac = new frmFacturation(u.User_Id);
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is frmFacturation)
                    {
                        frm.Show();
                        frm.Size = MinimumSize;
                        frm.WindowState = FormWindowState.Normal;
                        return;
                    }
                }
                frmFac.Show();
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// The MSIventas_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void MSIventas_Click(object sender, EventArgs e)
        {
            try
            {
                frmConsultSales frmVenta = new frmConsultSales();
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is frmConsultSales)
                    {
                        frm.Show();
                        frm.Size = MinimumSize;
                        frm.WindowState = FormWindowState.Normal;
                        return;
                    }
                }
                frmVenta.Show();
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// The MachineLearningToolStripMenuItem_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>


        /// <summary>
        /// The RegresionLinearToolStripMenuItem_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>


        /// <summary>
        /// The timer1_Tick.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            int i = IBusinessSale.Instance.ShowNumSales();
            lblVentas.Text = i.ToString();

            int a = IBusinessCustomer.Instance.ShowNumCustomers();
            lblclientes.Text = a.ToString();


            int p = IBusinessManagement.Instance.ShowNumProducts();
            lblproductos.Text = p.ToString();
        }

        /// <summary>
        /// The reporteGenerarDeVentasToolStripMenuItem_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>


        /// <summary>
        /// The webBrowser1_DocumentCompleted.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="WebBrowserDocumentCompletedEventArgs"/>.</param>


        /// <summary>
        /// The reporteDeVentasPorClienteToolStripMenuItem_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void reporteDeVentasPorClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSalesReportByCustomers fmt = new frmSalesReportByCustomers();
            fmt.Show();
        }

        /// <summary>
        /// The button2_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                frmCustomer frmc = new frmCustomer(u.User_Id);
                // frmc.MdiParent = this;
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is frmCustomer)
                    {
                        frm.Show();
                        frm.Size = MinimumSize;
                        frm.WindowState = FormWindowState.Normal;
                        return;
                    }
                }
                frmc.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// The btnUsers_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void btnUsers_Click(object sender, EventArgs e)
        {
            try
            {
                frmUser frmUser = new frmUser(u.User_Id);
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is frmUser)
                    {
                        frm.Show();
                        frm.Size = MinimumSize;
                        frm.WindowState = FormWindowState.Normal;
                        return;
                    }
                }
                frmUser.Show();
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// The btnSales_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void btnSales_Click(object sender, EventArgs e)
        {
            try
            {
                frmFacturation frmFac = new frmFacturation(u.User_Id);
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is frmFacturation)
                    {
                        frm.Show();
                        frm.Size = MinimumSize;
                        frm.WindowState = FormWindowState.Normal;
                        return;
                    }
                }
                frmFac.Show();
            }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// The btnProducts_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void btnProducts_Click(object sender, EventArgs e)
        {
            try
            {
                frmIndicaProducto frmpo = new frmIndicaProducto();
                // frmpo.MdiParent = this;
                foreach (Form frm in Application.OpenForms)
                {
                    if (frm is frmIndicaProducto)
                    {
                        frm.Show();
                        frm.Size = MinimumSize;
                        frm.WindowState = FormWindowState.Normal;
                        return;
                    }
                }
                frmpo.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// The acercaDeToolStripMenuItem_Click.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="EventArgs"/>.</param>
        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout frmAbout = new frmAbout();
            frmAbout.ShowDialog();
        }
    }
}
