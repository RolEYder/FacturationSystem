﻿namespace CapaAccesoDatos
{
    using Entidades;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Defines the <see cref="IDataAccessSale" />.
    /// </summary>
    public class IDataAccessSale
    {
        /// <summary>
        /// Defines the _instance.
        /// </summary>
        private static readonly IDataAccessSale _instance = new IDataAccessSale();

        /// <summary>
        /// Gets the Instance.
        /// </summary>
        public static IDataAccessSale Instance
        {
            get { return IDataAccessSale._instance; }
        }

        /// <summary>
        /// The IListSaleDetails.
        /// </summary>
        /// <param name="idventa">The idventa<see cref="int"/>.</param>
        /// <returns>The <see cref="entSale"/>.</returns>
        public entSale IListSaleDetails(int idventa)
        {
            SqlCommand cmd = null;
            IDataReader idr = null;
            entSale ve = null;
            List<entDetalleVenta> det = null;
            try
            {
                SqlConnection cn = Conexion.Instance.sqlConnectionCursor();
                cmd = new SqlCommand("spMostrarCabeceraVenta", cn);
                cmd.Parameters.AddWithValue("@prmid_venta", idventa);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                idr = cmd.ExecuteReader();
                if (idr.Read())
                {
                    ve = new entSale();
                    ve.Codigo_Venta = idr["Codigo_Venta"].ToString();

                    ve.Correlativo_Venta = idr["Correlativo_Venta"].ToString();
                    //ve.Igv_Venta = Convert.ToInt32(idr["Igv_Venta"]);
                    ve.FechaVenta = Convert.ToDateTime(idr["FechaVenta"]);
                    ve.Estado_Venta = idr["Estado_Venta"].ToString();

                    entCustomer c = new entCustomer();
                    c.Customer_Name = idr["Nombre_Cliente"].ToString();
                    c.CustomerDoc_Number = idr["NumeroDoc_Cliente"].ToString();

                    ve.cliente = c;
                    entUser u = new entUser();
                    u.User_Name = idr["Nombre_Usuario"].ToString();
                    ve.usuario = u;


                    if (idr.NextResult())
                    {
                        det = new List<entDetalleVenta>();
                        while (idr.Read())
                        {
                            entDetalleVenta d = new entDetalleVenta();
                            d.PrecProd_Det = Convert.ToDouble(idr["PrecProd_Det"]);
                            d.Cantidad_Det = Convert.ToInt32(idr["Cantidad_Det"]);
                            entProduct p = new entProduct();
                            p.Codigo_Prod = idr["Codigo_Prod"].ToString();
                            p.Nombre_Prod = idr["Nombre_Prod"].ToString();
                            p.Precio_Prod = Convert.ToDouble(idr["Precio_Prod"]);
                            d.producto = p;
                            det.Add(d);



                        }
                        ve.detalleventa = det;
                    }



                }
            }
            catch (Exception) { throw; }
            finally { cmd.Connection.Close(); }
            return ve;
        }

        /// <summary>
        /// The IListProductCustomerById.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="List{entDetalleVenta}"/>.</returns>
        public List<entDetalleVenta> IListProductCustomerById(int id)
        {
            SqlCommand sqlCommand = null;
            SqlDataReader sqlDataReader = null;
            List<entDetalleVenta> lista = null;


            try
            {
                SqlConnection cn = Conexion.Instance.sqlConnectionCursor();
                sqlCommand = new SqlCommand("spCargarDetallesVentasXId", cn);
                sqlCommand.Parameters.AddWithValue("@prmIdCliente", id);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                cn.Open();
                sqlDataReader = sqlCommand.ExecuteReader();
                lista = new List<entDetalleVenta>();
                while (sqlDataReader.Read())
                {
                    entProduct p = new entProduct();
                    p.Codigo_Prod = sqlDataReader["Codigo_Prod"].ToString();
                    p.Nombre_Prod = sqlDataReader["Nombre_Prod"].ToString();
                    p.Precio_Prod = Convert.ToDouble(sqlDataReader["Precio_Prod"]);


                    entSale venta = new entSale();
                    venta.Id_Venta = Convert.ToInt32(sqlDataReader["Id_Venta"]);
                    venta.FechaVenta = Convert.ToDateTime(sqlDataReader["FechaVenta"]);
                    venta.Estado_Venta = sqlDataReader["Estado_Venta"].ToString();
                    entCustomer cli = new entCustomer();
                    cli.Customer_Name = sqlDataReader["Nombre_Cliente"].ToString();

                    entUser usu = new entUser();
                    usu.User_Name = sqlDataReader["Nombre_Usuario"].ToString();
                    usu.User_Code = sqlDataReader["Codigo_Usuario"].ToString();

                    entDetalleVenta deta = new entDetalleVenta();
                    deta.Cantidad_Det = Convert.ToInt32(sqlDataReader["Cantidad_Det"]);
                    deta.PrecProd_Det = Convert.ToDouble(sqlDataReader["Precio_Prod"]);

                    deta.cliete = cli;
                    deta.usurio = usu;
                    deta.venta = venta;
                    deta.producto = p;
                    lista.Add(deta);



                }
                return lista;


            }
            catch (Exception) { throw; }
            finally { sqlCommand.Connection.Close(); }
        }

        /// <summary>
        /// The IListSalesProducts.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="List{entSale}"/>.</returns>
        public List<entSale> IListSalesProducts(string name)
        {
            SqlCommand sqlCommand = null;
            List<entSale> entVentas = null;
            SqlDataReader dataReader = null;


            try
            {
                SqlConnection sqlConnection = Conexion.Instance.sqlConnectionCursor();
                sqlCommand = new SqlCommand("spCargarVentasXCliente", sqlConnection);
                sqlCommand.Parameters.AddWithValue("@prmNombreCliente", name);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                dataReader = sqlCommand.ExecuteReader();
                entVentas = new List<entSale>();
                while (dataReader.Read())
                {
                    entSale v = new entSale();
                    v.Id_Venta = Convert.ToInt32(dataReader["Id_Venta"]);
                    v.Codigo_Venta = dataReader["Codigo_Venta"].ToString();
                    v.FechaVenta = Convert.ToDateTime(dataReader["FechaVenta"]);
                    entCustomer cli = new entCustomer();
                    cli.Customer_Id = Convert.ToInt32(dataReader["Id_Cliente"]);
                    cli.Customer_Name = dataReader["Nombre_Cliente"].ToString();
                    cli.Customer_Cellphone = dataReader["Celular_Cliente"].ToString();
                    cli.Customer_Email = dataReader["Correo_Cliente"].ToString();
                    cli.Customer_Address = dataReader["Direccion_Cliente"].ToString();
                    cli.Customer_State = Convert.ToBoolean(dataReader["Estado_Cliente"]);
                    v.cliente = cli;
                    entVentas.Add(v);
                }

                return entVentas;
            }
            catch (Exception)
            {
                throw;
            }
            finally { sqlCommand.Connection.Close(); }
        }

        /// <summary>
        /// The ICancelSaleById.
        /// </summary>
        /// <param name="idveta">The idveta<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int ICancelSaleById(int idveta)
        {
            SqlCommand cmd = null;
            var retorn = 0;
            try
            {
                SqlConnection cn = Conexion.Instance.sqlConnectionCursor();
                cmd = new SqlCommand("spAnularBoletaXid", cn);
                cmd.Parameters.AddWithValue("@prmId_venta", idveta);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                retorn = cmd.ExecuteNonQuery();
                return retorn;
            }
            catch (Exception) { throw; }
            finally { cmd.Connection.Close(); }
        }

        /// <summary>
        /// The IListSale.
        /// </summary>
        /// <param name="fechade">The fechade<see cref="String"/>.</param>
        /// <param name="fechahas">The fechahas<see cref="String"/>.</param>
        /// <param name="idusuc">The idusuc<see cref="int"/>.</param>
        /// <returns>The <see cref="List{entSale}"/>.</returns>
        public List<entSale> IListSale(String fechade, String fechahas, int idusuc)
        {

            SqlCommand cmd = null;
            List<entSale> lista = null;
            SqlDataReader dr = null;
            try
            {
                SqlConnection cn = Conexion.Instance.sqlConnectionCursor();
                cmd = new SqlCommand("spListaVenta", cn);
                cmd.Parameters.AddWithValue("@prmfinicio", fechade);
                cmd.Parameters.AddWithValue("@prmfin", fechahas);
                cmd.Parameters.AddWithValue("@prmidsucursal", idusuc);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                lista = new List<entSale>();
                while (dr.Read())
                {
                    entSale ven = new entSale();
                    ven.Id_Venta = Convert.ToInt32(dr["Id_Venta"]);
                    ven.Codigo_Venta = dr["Codigo_Venta"].ToString();
                    ven.Estado_Venta = dr["Estado_Venta"].ToString();
                    ven.Correlativo_Venta = dr["Correlativo_Venta"].ToString();
                    ven.FechaVenta = Convert.ToDateTime(dr["FechaVenta"]);
                    ven.Igv_Venta = Convert.ToInt32(dr["Igv_Venta"]);
                    ven.Total = Convert.ToDouble(dr["Total"]);
                    ven.Descuento_Venta = Convert.ToDouble(dr["Descuento_Venta"]);
                    // ven.Utilidad = Convert.ToDouble(dr["Utiliadad"]);
                    ven.Inversion = Convert.ToDouble(dr["Inversion"]);

                    entTipComprobante tip = new entTipComprobante();
                    tip.Id_TipCom = Convert.ToInt32(dr["Id_TipCom"]);
                    tip.Nombre_TipCom = dr["Nombre_TipCom"].ToString();
                    ven.tipocomprobante = tip;

                    entTipoPago tippago = new entTipoPago();
                    tippago.Id_TipPago = Convert.ToInt32(dr["Id_TipPago"]);
                    ven.tipopago = tippago;
                    lista.Add(ven);
                }
            }
            catch (Exception) { throw; }
            finally { cmd.Connection.Close(); }
            return lista;
        }

        /// <summary>
        /// The ICancelCompro.
        /// </summary>
        /// <param name="serie">The serie<see cref="String"/>.</param>
        /// <param name="correlati">The correlati<see cref="String"/>.</param>
        /// <param name="tipCompro">The tipCompro<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int ICancelCompro(String serie, String correlati, int tipCompro)
        {
            SqlCommand cmd = null;
            var resu = 0;
            try
            {
                SqlConnection cn = Conexion.Instance.sqlConnectionCursor();
                cmd = new SqlCommand("spAnularComprobante", cn);
                cmd.Parameters.AddWithValue("@Serie", serie);
                cmd.Parameters.AddWithValue("@correlativo", correlati);
                cmd.Parameters.AddWithValue("@idtipcom", tipCompro);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                resu = cmd.ExecuteNonQuery();
                return resu;
            }
            catch (Exception) { throw; }
            finally { cmd.Connection.Close(); }
        }

        /// <summary>
        /// The IListCorrelative.
        /// </summary>
        /// <param name="idtipcom">The idtipcom<see cref="int"/>.</param>
        /// <param name="serie">The serie<see cref="String"/>.</param>
        /// <returns>The <see cref="String"/>.</returns>
        public String IListCorrelative(int idtipcom, String serie)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            String correl = null;
            try
            {
                SqlConnection cn = Conexion.Instance.sqlConnectionCursor();
                cmd = new SqlCommand("spCargarCorrelativo", cn);
                cmd.Parameters.AddWithValue("@id_tipcom", idtipcom);
                cmd.Parameters.AddWithValue("@NumSerie", serie);

                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    correl = dr["Correlativo"].ToString();
                }

            }
            catch (Exception) { throw; }
            finally { cmd.Connection.Close(); }
            return correl;
        }

        /// <summary>
        /// The IListSerie.
        /// </summary>
        /// <param name="idtipcom">The idtipcom<see cref="int"/>.</param>
        /// <param name="suc">The suc<see cref="int"/>.</param>
        /// <returns>The <see cref="entSerie"/>.</returns>
        public entSerie IListSerie(int idtipcom, int suc)
        {
            SqlCommand cmd = null;
            entSerie s = null;
            SqlDataReader dr = null;
            try
            {
                SqlConnection cn = Conexion.Instance.sqlConnectionCursor();
                cmd = new SqlCommand("spCargarSerie", cn);
                cmd.Parameters.AddWithValue("@prmIdSuc", suc);
                cmd.Parameters.AddWithValue("@prmIdTipcom", idtipcom);

                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    s = new entSerie();
                    s.Id_Serie = Convert.ToInt32(dr["Id_Serie"]);
                    s.Numero_Serie = dr["Numero_Serie"].ToString();
                    s.Id_Sucursal_Serie = Convert.ToInt32(dr["Id_Sucursal_Serie"]);



                    entTipComprobante tipcom = new entTipComprobante();
                    tipcom.Id_TipCom = Convert.ToInt32(dr["Id_TipCom_Serie"]);
                    s.tipcom = tipcom;

                }
            }
            catch (Exception) { throw; }
            finally { cmd.Connection.Close(); }
            return s;
        }

        /// <summary>
        /// The ISaveSale.
        /// </summary>
        /// <param name="CadXml">The CadXml<see cref="String"/>.</param>
        /// <param name="idtidove">The idtidove<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int ISaveSale(String CadXml, int idtidove)
        {
            SqlCommand cmd = null;
            var resu = 0;
            try
            {
                SqlConnection cn = Conexion.Instance.sqlConnectionCursor();
                cmd = new SqlCommand("spGuardarVenta", cn);
                cmd.Parameters.AddWithValue("@Cadxml", CadXml);
                cmd.Parameters.AddWithValue("@TIPO_DOC_VENTA", idtidove);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                resu = cmd.ExecuteNonQuery();
                return resu;
            }
            catch (Exception) { throw; }
            finally { cmd.Connection.Close(); }
        }

        /// <summary>
        /// The IShowNumSales.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int IShowNumSales()
        {
            SqlCommand cmd = null;
            var t = 0;
            try
            {
                SqlConnection cn = Conexion.Instance.sqlConnectionCursor();
                cmd = new SqlCommand("spNumVentas", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                t = (Int32)cmd.ExecuteScalar();
                return t;
            }
            catch (Exception) { throw; }
            finally { cmd.Connection.Close(); }
        }

        /// <summary>
        /// The IGetLastIdSale.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int IGetLastIdSale()
        {
            SqlCommand cmd = null;
            var a = 0;
            try
            {
                SqlConnection cn = Conexion.Instance.sqlConnectionCursor();
                cmd = new SqlCommand("spGetLastSale", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                a = (Int32)cmd.ExecuteScalar();
                return a;
            }
            catch (Exception) { throw; }
            finally { cmd.Connection.Close(); }
        }
    }
}
