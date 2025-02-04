﻿namespace CapaAccesoDatos
{
    using Entidades;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// Defines the IDataAccessCustomer class <see cref="IDataAccessCustomer" />.
    /// </summary>
    public class IDataAccessCustomer
    {
        /// <summary>
        /// Defines the _instancia.
        /// </summary>
        private static readonly IDataAccessCustomer _instance = new IDataAccessCustomer();

        /// <summary>
        /// Gets the Instancia.
        /// </summary>
        public static IDataAccessCustomer Instance
        {
            get { return IDataAccessCustomer._instance; }
        }

        /// <summary>
        /// Yield the customer data by name.
        /// </summary>
        /// <param name="nom_cli">The nom_cli<see cref="String"/>.</param>
        /// <returns>The <see cref="List{entCustomer}"/>.</returns>
        public List<entCustomer> IAdvancedSearchCustomer(String nom_cli)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entCustomer> list = null;
            try
            {
                SqlConnection cn = Conexion.Instance.sqlConnectionCursor();
                cmd = new SqlCommand("spSearchCustomerByName", cn);
                cmd.Parameters.AddWithValue("@prmName_Customer", nom_cli);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                list = new List<entCustomer>();
                while (dr.Read())
                {
                    entCustomer cliente = new entCustomer();
                    cliente.Customer_Id = Convert.ToInt32(dr["Id_Cliente"]);
                    entTipoDocumento td = new entTipoDocumento();
                    td.Id_TipDoc = Convert.ToInt32(dr["Id_TipDoc_Cliente"].ToString());
                    td.Nombre_TipDoc = dr["Nombre_TipDoc"].ToString();
                    cliente.tipodocumento = td;
                    cliente.CustomerDoc_Number = dr["NumeroDoc_Cliente"].ToString();
                    cliente.Customer_Phone = dr["Telefono_Cliente"].ToString();
                    cliente.Customer_Cellphone = dr["Celular_Cliente"].ToString();
                    cliente.Customer_Email = dr["Telefono_Cliente"].ToString();
                    cliente.Customer_Address = dr["Direccion_Cliente"].ToString();
                    cliente.CustomeDate_Born = dr["FechaNac_Cliente"].ToString();
                    list.Add(cliente);



                }
            }
            catch (Exception) { throw; }
            finally { cmd.Connection.Close(); }
            return list;
        }

        /// <summary>
        ///  Seach a customer by id and DNI.
        /// </summary>
        /// <param name="id_cli">The id_cli<see cref="int"/>.</param>
        /// <param name="nro_Doc">The nro_Doc<see cref="String"/>.</param>
        /// <returns>The <see cref="entCustomer"/>.</returns>
        public entCustomer ISearchCustomer(int id_cli, String nro_Doc)
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            entCustomer cliente = null;

            try
            {
                SqlConnection cn = Conexion.Instance.sqlConnectionCursor();
                cmd = new SqlCommand("spBuscarCliente", cn);
                cmd.Parameters.AddWithValue("@prmidCliente", id_cli);
                cmd.Parameters.AddWithValue("@prmNroDoc", nro_Doc);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cliente = new entCustomer();
                    cliente.Customer_Id = Convert.ToInt32(dr["Id_Cliente"]);
                    entTipoDocumento td = new entTipoDocumento();
                    td.Id_TipDoc = Convert.ToInt32(dr["Id_TipDoc_Cliente"].ToString());
                    td.Nombre_TipDoc = dr["Nombre_TipDoc"].ToString();
                    cliente.tipodocumento = td;
                    cliente.CustomerDoc_Number = dr["NumeroDoc_Cliente"].ToString();
                    cliente.Customer_Name = dr["Nombre_Cliente"].ToString();
                    cliente.Customer_Phone = dr["Telefono_Cliente"].ToString();
                    cliente.Customer_Cellphone = dr["Celular_Cliente"].ToString();
                    cliente.Customer_Email = dr["Correo_Cliente"].ToString();
                    cliente.Customer_Address = dr["Direccion_Cliente"].ToString();
                    cliente.CustomeDate_Born = dr["FechaNac_Cliente"].ToString();
                    cliente.Customer_Sex = dr["Sexo_Cliente"].ToString();

                }
            }

            catch (Exception) { throw; }
            finally { cmd.Connection.Close(); }
            return cliente;
        }

        /// <summary>
        /// List all Customers.
        /// </summary>
        /// <returns>The <see cref="List{entCustomer}"/>.</returns>
        public List<entCustomer> IListCustomer()
        {
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entCustomer> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instance.sqlConnectionCursor();
                cmd = new SqlCommand("spCustomerList", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entCustomer>();
                while (dr.Read())
                {
                    entCustomer customer = new entCustomer();
                    customer.Customer_Id = Convert.ToInt32(dr["Id_Cliente"]);
                    entTipoDocumento td = new entTipoDocumento();
                    td.Nombre_TipDoc = dr["Nombre_TipDoc"].ToString();
                    customer.tipodocumento = td;
                    customer.CustomerDoc_Number = dr["NumeroDoc_Cliente"].ToString();
                    customer.Customer_Name = dr["Nombre_Cliente"].ToString();
                    customer.Customer_Phone = dr["Telefono_Cliente"].ToString();
                    customer.Customer_Cellphone = dr["Celular_Cliente"].ToString();
                    customer.Customer_Email = dr["Correo_Cliente"].ToString();
                    customer.Customer_Address = dr["Direccion_Cliente"].ToString();
                    Lista.Add(customer);
                }


            }
            catch (Exception) { throw; }
            finally { cmd.Connection.Close(); }
            return Lista;
        }

        /// <summary>
        /// Get the number of all customers store.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int IShowNumCustomers()
        {
            SqlCommand cmd = null;
            var resultado = 0;
            try
            {
                SqlConnection cnd = Conexion.Instance.sqlConnectionCursor();

                cmd = new SqlCommand("spNumClientes", cnd);
                cnd.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                resultado = (Int32)cmd.ExecuteScalar();
                return resultado;

            }

            catch (Exception) { throw; }
            finally { cmd.Connection.Close(); }
        }

        /// <summary>
        /// Method witch handle the customer such insert, update, or disable a customer.
        /// </summary>
        /// <param name="cadXml">The cadXml<see cref="String"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int ICustomerMagament(String cadXml)
        {
            SqlCommand cmd = null;
            var resultado = 0;

            try
            {
                SqlConnection cn = Conexion.Instance.sqlConnectionCursor();
                cmd = new SqlCommand("spInsEditElimCliente", cn);
                cmd.Parameters.AddWithValue("@prmCadXml", cadXml);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                resultado = cmd.ExecuteNonQuery();
                return resultado;
            }
            catch (Exception) { throw; }
            finally { cmd.Connection.Close(); }
        }

        /// <summary>
        /// List document Type.
        /// </summary>
        /// <returns>The <see cref="List{entTipoDocumento}"/>.</returns>
        public List<entTipoDocumento> IListTipDocument()
        {

            SqlCommand cmd = null;
            SqlDataReader dr = null;
            List<entTipoDocumento> Lista = null;
            try
            {
                SqlConnection cn = Conexion.Instance.sqlConnectionCursor();
                cmd = new SqlCommand("spListarTipDoc", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                dr = cmd.ExecuteReader();
                Lista = new List<entTipoDocumento>();
                while (dr.Read())
                {
                    entTipoDocumento td = new entTipoDocumento();
                    td.Id_TipDoc = Convert.ToInt32(dr["Id_TipDoc"]);
                    td.Abreviatura_TipDoc = dr["Abreviatura_TipDoc"].ToString();
                    Lista.Add(td);
                }



            }
            catch (Exception) { throw; }
            finally { cmd.Connection.Close(); }

            return Lista;
        }
    }
}
