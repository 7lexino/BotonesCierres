using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace BotonesCierres
{

    class Conexion
    {
        private static DataSet ds; // es culpa de esto andres
        public static DataSet Ejecutar(string SQL)
        {
            //string ruta = Directory.GetCurrentDirectory();
            string cadenaConexion = @"Provider=Microsoft.JET.OLEDB.4.0;Data Source=C:\Juguel\DB_BOTONESCIERRES.mdb";

            try
            {
                OleDbConnection conexion = new OleDbConnection(cadenaConexion);
                conexion.Open();
                ds = new DataSet();
                OleDbDataAdapter da = new OleDbDataAdapter(SQL, conexion);
                da.Fill(ds);
                conexion.Close();
                conexion.Dispose();
                da.Dispose();
                return ds;
            }
            catch (InvalidOperationException err)
            {
                Mensajes.Excepcion(err.Message);
                return new DataSet();
            }
            catch (OleDbException err)
            {
                Mensajes.Excepcion(err.Message);
                return new DataSet();
            }
            catch (Exception err)
            {
                Mensajes.Excepcion(err.Message);
                return new DataSet();
            }
        }
        ~Conexion()
        {
            ds.Dispose();
        }
    }

    class Mensajes
    {
        public static DialogResult Excepcion(string mensaje) {
            return MessageBox.Show("Se ha producido un error: " + mensaje, "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult Confirmar(string mensaje)
        {
            return MessageBox.Show(mensaje, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        public static DialogResult Exito(string mensaje)
        {
            return MessageBox.Show(mensaje, "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult NoExito(string mensaje)
        {
            return MessageBox.Show(mensaje, "Operación no exitosa", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

    class Funciones
    {
        public static string SeleccionarArchivo(string rutaInicial, string archivosAdmitidos)
        {
            OpenFileDialog archivo = new OpenFileDialog();
            archivo.InitialDirectory = rutaInicial;
            archivo.Filter = archivosAdmitidos;
            archivo.RestoreDirectory = true;
            if (archivo.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return archivo.FileName;
            }
            return "";
        }

        public static object NZ(object valor)
        {
            if (valor == null) return 0;
            else return valor;
        }

        public static void EditarEncabezadoYAnchoDataGrid(DataGridView DataGridView, string[,] Encabezados)
        {
            int i = 0, ancho;
            foreach (DataGridViewColumn c in DataGridView.Columns)
            {
                if (c.Index < Encabezados.GetLength(0))
                {
                    c.HeaderText = Encabezados[i, 0];
                    if (Int32.TryParse(Encabezados[i, 1].ToString(), out ancho)) c.Width = ancho;
                    i++;
                }
            }
        }

        public static DialogResult InputBox(string title, string promptText, ref string value, int tipo = 0)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            if (tipo == 1) textBox.UseSystemPasswordChar = true;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        public static bool EstaAbiertoFormulario(Form frm)
        {
            Form existe = Application.OpenForms.OfType<Form>().Where(pre => pre.Name == frm.Name).SingleOrDefault<Form>();
            if (existe != null) return true;
            else return false;
        }

        //Consigue instancia de ventana de frmInventarioBotones
        public static T TraerVentana<T>()
        {
            Form frmResultado = null;
            foreach (Form actual in Application.OpenForms)
            {
                frmResultado = actual.GetType() == typeof(T) ? actual : frmResultado;
            }
            if (frmResultado == null) {
                //Si no está la ventana abierta lanzamos una excepción
                throw new IndexOutOfRangeException("La ventana " + typeof(T).Name + " no está abierta.");
            }
            return (T)Convert.ChangeType(frmResultado, typeof(T));
        }

    }

    public class InventarioBotones
    {
        //MÉTODO CONSTRUCTOR Y VARIABLES GLOBALES ////////////////////////////
        /// <summary>
        /// Método constructor de la clase Inventario Botones
        /// </summary>
        public InventarioBotones()
        {
            NombreInventario = "NuevoInventarioBotones";
        }
        protected string sql;
        protected DataSet ds;

        public enum TipoLog
        {
            NuevoProducto, ProductoEliminado, UnidadesAgregadas, UnidadesDescontadas
        }
        //FIN DE LA INICIACIÓN DE LA CLASE //////////////////////////////////

        //PROPIEDADES ///////////////////////////////////////////////////////
        /// <summary>
        /// Nombre del inventario
        /// </summary>
        public string NombreInventario { get; set; }

        /// <summary>
        /// Cantidad de productos en el inventario.
        /// </summary>
        public uint CantidadDeProductos
        {
            get
            {
                sql = "SELECT COUNT(*) AS Cantidad FROM inv_botones";
                ds = Conexion.Ejecutar(sql); //Ejecutamos la consulta para obtener la cantidad de registros en el inventario
                return UInt32.Parse(ds.Tables[0].Rows[0]["Cantidad"].ToString());
            }
        }
        //FIN DE LAS PROPIEDADES ////////////////////////////////////////////

        //METODOS ///////////////////////////////////////////////////////////
        /// <summary>
        /// Verifica si existe el producto especificado por su Id.
        /// </summary>
        /// <param name="IdProducto"></param>
        /// <returns></returns>
        public bool ExisteProducto(int idProducto)
        {
            sql = string.Format("SELECT Id FROM inv_botones WHERE Id={0}", idProducto);
            ds = Conexion.Ejecutar(sql); //Ejecutamos la consulta a la DB
            return ds.Tables[0].Rows.Count == 0 ? false : true; //Si la consulta devuelve 0, retorna false y si no devuelve 0 retorna true
        }
        /// <summary>
        /// Verifica si existe el producto especificado por el modelo, color y talla.
        /// </summary>
        /// <param name="modelo"></param>
        /// <param name="color"></param>
        /// <param name="talla"></param>
        /// <returns></returns>
        public bool ExisteProducto(short modelo, string color, short talla)
        {
            sql = string.Format("SELECT Id FROM inv_botones WHERE Modelo={0} AND Color='{1}' AND Talla={2}", modelo, color, talla);
            ds = Conexion.Ejecutar(sql); // Ejecutamos la consulta a la DB
            return ds.Tables[0].Rows.Count == 0 ? false : true; //Si la consulta devuelve 0, retorna false y si no devuelve 0 retorna true
        }

        /// <summary>
        /// Nos devuelve la Id del producto que estamos buscando especificándole el modelo, color y talla. Devuelve null si el producto no existe.
        /// </summary>
        /// <param name="Modelo">Modelo del botón</param>
        /// <param name="Color">Color del botón</param>
        /// <param name="Talla">Talla del botón</param>
        /// <returns></returns>
        public int? ObtenerIdProducto(short modelo, string color, byte talla)
        {
            sql = string.Format("SELECT Id FROM inv_botones WHERE Modelo={0} AND Color='{1}' AND Talla={2}", modelo, color, talla);
            ds = Conexion.Ejecutar(sql); //Ejecutamos la consulta para obtener el Id del producto
            if (ds.Tables[0].Rows.Count != 0) return Int32.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
            else return null; //En caso de que el producto no exista devolvemos null
        }

        /// <summary>
        /// Le pasamos la Id de un producto y nos devolverá por referencia el modelo, color y talla del mismo.
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="modelo"></param>
        /// <param name="color"></param>
        /// <param name="talla"></param>
        public void ObtenerDatosProducto(int idProducto, ref short modelo, ref string color, ref byte talla)
        {
            if (ExisteProducto(idProducto))
            {
                sql = string.Format("SELECT Modelo, Color, Talla FROM inv_botones WHERE Id={0}", idProducto);
                ds = Conexion.Ejecutar(sql);
                modelo = Int16.Parse(ds.Tables[0].Rows[0]["Modelo"].ToString());
                color = ds.Tables[0].Rows[0]["Color"].ToString();
                talla = byte.Parse(ds.Tables[0].Rows[0]["Talla"].ToString());
            }
            else throw new ArgumentException("El producto no existe"); //En caso de que no exista el producto
        }

        /// <summary>
        /// Devuelve la existencia actual del producto especificado por el id.
        /// </summary>
        /// <param name="idProducto"></param>
        /// <returns></returns>
        public int? Existencia(int idProducto)
        {
            sql = string.Format("SELECT Inventario FROM inv_botones WHERE Id={0}", idProducto);
            ds = Conexion.Ejecutar(sql); //Ejecutamos la consulta para obtener la existencia del producto
            if (ds.Tables[0].Rows.Count != 0) return Int32.Parse(ds.Tables[0].Rows[0]["Inventario"].ToString());
            else return null; //En caso de que el producto no exista devolvemos null
        }
        /// <summary>
        /// Devuelve la existencia actual del producto especificado por su modelo, color y talla.
        /// </summary>
        /// <param name="modelo"></param>
        /// <param name="color"></param>
        /// <param name="talla"></param>
        /// <returns></returns>
        public int? Existencia(short modelo, string color, byte talla)
        {
            sql = string.Format("SELECT Inventario FROM inv_botones WHERE Modelo={0} AND Color='{1}' AND Talla={2}", modelo, color, talla);
            ds = Conexion.Ejecutar(sql); //Ejecutamos la consulta para obtener la existencia del producto
            if (ds.Tables[0].Rows.Count != 0) return Int32.Parse(ds.Tables[0].Rows[0]["Inventario"].ToString());
            else return null; //En caso de que el producto no exista devolvemos null
        }

        /// <summary>
        /// Inserta un nuevo producto en la base de datos.
        /// </summary>
        /// <param name="modelo"></param>
        /// <param name="color"></param>
        /// <param name="talla"></param>
        /// <param name="cantidadInicial"></param>
        public void NuevoProducto(short modelo, string color, byte talla, int cantidadInicial)
        {
            if (!ExisteProducto(modelo, color, talla))
            {
                sql = string.Format("INSERT INTO inv_botones (Modelo,Color,Talla,Inventario) VALUES ({0},'{1}',{2},{3})", modelo, color, talla, cantidadInicial);
                ds = Conexion.Ejecutar(sql); //Ejecutamos la consulta para añadir el registro al a DB
                AgregarEventoLog(ObtenerIdProducto(modelo, color, talla) ?? 0, 0, cantidadInicial, cantidadInicial, TipoLog.NuevoProducto);
            }
            else throw new ArgumentException("El producto ya existe en la base de datos"); //En caso de que el producto ya exista, lanzamos una exepción
        }

        /// <summary>
        /// Agregamos la cantidad de unidades especificada a la existencia del producto.
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="cantidad"></param>
        public void AgregarUnidades(int idProducto, int cantidad)
        {
            if (ExisteProducto(idProducto))
            {
                int existenciaActual = Existencia(idProducto) ?? 0; //Obtenemos la existencia actual
                int existenciaPosterior = existenciaActual + cantidad; //A la existencia actual le sumamos la cantidad que se quiere agregar
                sql = string.Format("UPDATE inv_botones SET Inventario={0} WHERE Id={1}", existenciaPosterior, idProducto);
                ds = Conexion.Ejecutar(sql); //Ejecutamos la consulta para agregar unidades al inventario
                AgregarEventoLog(idProducto, existenciaActual, cantidad, existenciaPosterior, TipoLog.UnidadesAgregadas); //Generamos un evento de historial
            }
            else throw new ArgumentException("El producto no existe en la base de datos");
        }
        /// <summary>
        /// Agregamos la cantidad de unidades especificada a la existencia del producto.
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="cantidad"></param>
        public void AgregarUnidades(short modelo, string color, byte talla, int cantidad)
        {
            if (ExisteProducto(modelo, color, talla))
            {
                int idProducto = ObtenerIdProducto(modelo, color, talla) ?? 0;
                int existenciaActual = Existencia(idProducto) ?? 0; //Obtenemos la existencia actual
                int existenciaPosterior = existenciaActual + cantidad; //A la existencia actual le sumamos la cantidad que se quiere agregar
                sql = string.Format("UPDATE inv_botones SET Inventario={0} WHERE Id={1}", existenciaPosterior, idProducto);
                ds = Conexion.Ejecutar(sql); //Ejecutamos la consulta para agregar unidades al inventario

                AgregarEventoLog(idProducto, existenciaActual, cantidad, existenciaPosterior, TipoLog.UnidadesAgregadas); //Generamos un evento de historial
            }
            else throw new ArgumentException("El producto no existe en la base de datos");
        }

        /// <summary>
        /// Descontamos la cantidad de unidades especificada a la existencia del producto
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="cantidad"></param>
        public void DescontarUnidades(int idProducto, int cantidad)
        {
            if (ExisteProducto(idProducto))
            {
                int existenciaActual = Existencia(idProducto) ?? 0; //Obtenemos la existencia actual
                int existenciaPosterior = existenciaActual - cantidad; //A la existencia actual le restamos la cantidad modificada
                sql = string.Format("UPDATE inv_botones SET Inventario={0} WHERE Id={1}", existenciaPosterior, idProducto);
                ds = Conexion.Ejecutar(sql); //Ejecutamos la consulta para agregar unidades al inventario
                AgregarEventoLog(idProducto, existenciaActual, cantidad, existenciaPosterior, TipoLog.UnidadesDescontadas);
            }
            else throw new ArgumentException("El producto no existe en la base de datos"); //Si elproducto no existe lanzamos una excepción
        }
        /// <summary>
        /// Descontamos la cantidad de unidades especificada a la existencia del producto
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="cantidad"></param>
        public void DescontarUnidades(short modelo, string color, byte talla, int cantidad)
        {
            if (ExisteProducto(modelo, color, talla))
            {
                int idProducto = ObtenerIdProducto(modelo, color, talla) ?? 0;
                int existenciaActual = Existencia(idProducto) ?? 0; //Obtenemos la existencia actual
                int existenciaPosterior = existenciaActual - cantidad; //A la existencia actual le restamos la cantidad modificada
                sql = string.Format("UPDATE inv_botones SET Inventario={0} WHERE Id={1}", existenciaPosterior, idProducto);
                ds = Conexion.Ejecutar(sql); //Ejecutamos la consulta para agregar unidades al inventario
                AgregarEventoLog(idProducto, existenciaActual, cantidad, existenciaPosterior, TipoLog.UnidadesDescontadas);
            }
            else throw new ArgumentException("El producto no existe en la base de datos"); //Si elproducto no existe lanzamos una excepción
        }

        /// <summary>
        /// Agregarmos un nuevo evento al historial del inventario
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="cantidadAnterior"></param>
        /// <param name="cantidadModificada"></param>
        /// <param name="cantidadPosterior"></param>
        /// <param name="tipo"></param>
        public void AgregarEventoLog(int idProducto, int cantidadAnterior, int cantidadModificada, int cantidadPosterior, TipoLog tipo)
        {
            short modelo = 0;
            string color = "";
            byte talla = 0;
            ObtenerDatosProducto(idProducto, ref modelo, ref color, ref talla); //Llamamos a la función para que nos devuelva los datos del producto con esa Id
            sql = string.Format("INSERT INTO log_botones (Fecha, Modelo, Color, Talla, C_anterior, C_modificada, C_posterior, Tipo) VALUES (#{0}#, {1}, '{2}', {3}, {4}, {5}, {6},'{7}')", DateTime.Now.ToString("MM/dd/yyyy H:mm:ss"), modelo, color, talla, cantidadAnterior, cantidadModificada, cantidadPosterior, tipo.ToString());
            ds = Conexion.Ejecutar(sql); //Ejecutamos la consulta para añadir el evento al historial
        }

        /// <summary>
        /// Devuelve la cantidad especificada de eventos del historial del inventario
        /// </summary>
        /// <param name="cantidad"></param>
        /// <returns></returns>
        public DataSet UltimosEventosLog(byte cantidad)
        {
            sql = string.Format("SELECT TOP {0} Tipo, Fecha, Modelo, Color, Talla, C_anterior, C_modificada, C_posterior FROM log_botones ORDER BY Fecha DESC;", cantidad);
            ds = Conexion.Ejecutar(sql); //Ejecutamos una consulta para obtener los ultimos n eventos del historial
            return ds.Tables[0].Rows.Count != 0 ? ds : null;
        }
        //FIN DE LOS MÉTODOS ////////////////////////////////////////////////

    }

    public class InventarioCierres
    {
        //MÉTODO CONSTRUCTOR Y VARIABLES GLOBALES ////////////////////////////
        /// <summary>
        /// Método constructor de la clase Inventario Cierres
        /// </summary>
        public InventarioCierres()
        {
            NombreInventario = "NuevoInventarioCierres";
        }
        protected string sql;
        protected DataSet ds;

        public enum TipoLog
        {
            NuevoProducto, ProductoEliminado, UnidadesAgregadas, UnidadesDescontadas
        }
        //FIN DE LA INICIACIÓN DE LA CLASE //////////////////////////////////

        //PROPIEDADES ///////////////////////////////////////////////////////
        /// <summary>
        /// Nombre del inventario
        /// </summary>
        public string NombreInventario { get; set; }

        /// <summary>
        /// Cantidad de productos en el inventario
        /// </summary>
        public uint CantidadDeProductos
        {
            get
            {
                sql = "SELECT COUNT(*) AS Cantidad FROM inv_cierres";
                ds = Conexion.Ejecutar(sql); //Ejecutamos la consulta para obtener la cantidad de registros en el inventario
                return UInt32.Parse(ds.Tables[0].Rows[0]["Cantidad"].ToString());
            }
        }
        //FIN DE LAS PROPIEDADES ////////////////////////////////////////////

        //METODOS ///////////////////////////////////////////////////////////
        /// <summary>
        /// Verifica si existe el producto especificado por su Id.
        /// </summary>
        /// <param name="IdProducto"></param>
        /// <returns></returns>
        public bool ExisteProducto(int idProducto)
        {
            sql = string.Format("SELECT Id FROM inv_cierres WHERE Id={0}", idProducto);
            ds = Conexion.Ejecutar(sql); //Ejecutamos la consulta a la DB
            return ds.Tables[0].Rows.Count == 0 ? false : true; //Si la consulta devuelve 0, retorna false y si no devuelve 0 retorna true
        }
        /// <summary>
        /// Verifica si existe el producto especificado por el modelo, color y talla.
        /// </summary>
        /// <param name="modelo"></param>
        /// <param name="color"></param>
        /// <param name="talla"></param>
        /// <returns></returns>
        public bool ExisteProducto(string modelo, string color, short talla)
        {
            sql = string.Format("SELECT Id FROM inv_cierres WHERE Modelo='{0}' AND Color='{1}' AND Talla={2}", modelo, color, talla);
            ds = Conexion.Ejecutar(sql); // Ejecutamos la consulta a la DB
            return ds.Tables[0].Rows.Count == 0 ? false : true; //Si la consulta devuelve 0, retorna false y si no devuelve 0 retorna true
        }

        /// <summary>
        /// Nos devuelve la Id del producto que estamos buscando especificándole el modelo, color y talla. Devuelve null si el producto no existe.
        /// </summary>
        /// <param name="Modelo">Modelo del botón</param>
        /// <param name="Color">Color del botón</param>
        /// <param name="Talla">Talla del botón</param>
        /// <returns></returns>
        public int? ObtenerIdProducto(string modelo, string color, byte talla)
        {
            sql = string.Format("SELECT Id FROM inv_cierres WHERE Modelo='{0}' AND Color='{1}' AND Talla={2}", modelo, color, talla);
            ds = Conexion.Ejecutar(sql); //Ejecutamos la consulta para obtener el Id del producto
            if (ds.Tables[0].Rows.Count != 0) return Int32.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
            else return null; //En caso de que el producto no exista devolvemos null
        }

        /// <summary>
        /// Le pasamos la Id de un producto y nos devolverá por referencia el modelo, color y talla del mismo.
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="modelo"></param>
        /// <param name="color"></param>
        /// <param name="talla"></param>
        public void ObtenerDatosProducto(int idProducto, ref string modelo, ref string color, ref byte talla)
        {
            if (ExisteProducto(idProducto))
            {
                sql = string.Format("SELECT Modelo, Color, Talla FROM inv_cierres WHERE Id={0}", idProducto);
                ds = Conexion.Ejecutar(sql);
                modelo = ds.Tables[0].Rows[0]["Modelo"].ToString();
                color = ds.Tables[0].Rows[0]["Color"].ToString();
                talla = byte.Parse(ds.Tables[0].Rows[0]["Talla"].ToString());
            }
            else throw new ArgumentException("El producto no existe"); //En caso de que no exista el producto
        }

        /// <summary>
        /// Devuelve la existencia actual del producto especificado por el id.
        /// </summary>
        /// <param name="idProducto"></param>
        /// <returns></returns>
        public int? Existencia(int idProducto)
        {
            sql = string.Format("SELECT Inventario FROM inv_cierres WHERE Id={0}", idProducto);
            ds = Conexion.Ejecutar(sql); //Ejecutamos la consulta para obtener la existencia del producto
            if (ds.Tables[0].Rows.Count != 0) return Int32.Parse(ds.Tables[0].Rows[0]["Inventario"].ToString());
            else return null; //En caso de que el producto no exista devolvemos null
        }
        /// <summary>
        /// Devuelve la existencia actual del producto especificado por su modelo, color y talla.
        /// </summary>
        /// <param name="modelo"></param>
        /// <param name="color"></param>
        /// <param name="talla"></param>
        /// <returns></returns>
        public int? Existencia(string modelo, string color, byte talla)
        {
            sql = string.Format("SELECT Inventario FROM inv_cierres WHERE Modelo='{0}' AND Color='{1}' AND Talla={2}", modelo, color, talla);
            ds = Conexion.Ejecutar(sql); //Ejecutamos la consulta para obtener la existencia del producto
            if (ds.Tables[0].Rows.Count != 0) return Int32.Parse(ds.Tables[0].Rows[0]["Inventario"].ToString());
            else return null; //En caso de que el producto no exista devolvemos null
        }

        /// <summary>
        /// Inserta un nuevo producto en la base de datos.
        /// </summary>
        /// <param name="modelo"></param>
        /// <param name="color"></param>
        /// <param name="talla"></param>
        /// <param name="cantidadInicial"></param>
        public void NuevoProducto(string modelo, string color, byte talla, int cantidadInicial)
        {
            if (!ExisteProducto(modelo, color, talla))
            {
                sql = string.Format("INSERT INTO inv_cierres (Modelo,Color,Talla,Inventario) VALUES ('{0}','{1}',{2},{3})", modelo, color, talla, cantidadInicial);
                ds = Conexion.Ejecutar(sql); //Ejecutamos la consulta para añadir el registro al a DB
                AgregarEventoLog(ObtenerIdProducto(modelo, color, talla) ?? 0, 0, cantidadInicial, cantidadInicial, TipoLog.NuevoProducto);
            }
            else throw new ArgumentException("El producto ya existe en la base de datos"); //En caso de que el producto ya exista, lanzamos una exepción
        }

        /// <summary>
        /// Agregamos la cantidad de unidades especificada a la existencia del producto.
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="cantidad"></param>
        public void AgregarUnidades(int idProducto, int cantidad)
        {
            if (ExisteProducto(idProducto))
            {
                int existencia = Existencia(idProducto) ?? 0; //Obtenemos la existencia actual
                existencia += cantidad; //A la existencia actual le sumamos la cantidad que se quiere agregar
                sql = string.Format("UPDATE inv_cierres SET Inventario={0} WHERE Id={1}", existencia, idProducto);
                ds = Conexion.Ejecutar(sql); //Ejecutamos la consulta para agregar unidades al inventario
            }
            else throw new ArgumentException("El producto no existe en la base de datos");
        }
        /// <summary>
        /// Agregamos la cantidad de unidades especificada a la existencia del producto.
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="cantidad"></param>
        public void AgregarUnidades(string modelo, string color, byte talla, int cantidad)
        {
            if (ExisteProducto(modelo, color, talla))
            {
                int idProducto = ObtenerIdProducto(modelo, color, talla) ?? 0;
                int existenciaActual = Existencia(idProducto) ?? 0; //Obtenemos la existencia actual
                int existenciaPosterior = existenciaActual + cantidad; //A la existencia actual le sumamos la cantidad que se quiere agregar
                sql = string.Format("UPDATE inv_cierres SET Inventario={0} WHERE Id={1}", existenciaPosterior, idProducto);
                ds = Conexion.Ejecutar(sql); //Ejecutamos la consulta para agregar unidades al inventario

                AgregarEventoLog(idProducto, existenciaActual, cantidad, existenciaPosterior, TipoLog.UnidadesAgregadas); //Generamos un evento de historial
            }
            else throw new ArgumentException("El producto no existe en la base de datos");
        }

        /// <summary>
        /// Descontamos la cantidad de unidades especificada a la existencia del producto
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="cantidad"></param>
        public void DescontarUnidades(int idProducto, int cantidad)
        {
            if (ExisteProducto(idProducto))
            {
                int existencia = Existencia(idProducto) ?? 0; //Obtenemos la existencia actual
                existencia -= cantidad; //A la existencia actual le sumamos la cantidad que se quiere agregar
                sql = string.Format("UPDATE inv_cierres SET Inventario={0} WHERE Id={1}", existencia, idProducto);
                ds = Conexion.Ejecutar(sql); //Ejecutamos la consulta para agregar unidades al inventario
            }
            else throw new ArgumentException("El producto no existe en la base de datos"); //Si elproducto no existe lanzamos una excepción
        }
        /// <summary>
        /// Descontamos la cantidad de unidades especificada a la existencia del producto
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="cantidad"></param>
        public void DescontarUnidades(string modelo, string color, byte talla, int cantidad)
        {
            if (ExisteProducto(modelo, color, talla))
            {
                int idProducto = ObtenerIdProducto(modelo, color, talla) ?? 0;
                int existenciaActual = Existencia(idProducto) ?? 0; //Obtenemos la existencia actual
                int existenciaPosterior = existenciaActual - cantidad; //A la existencia actual le restamos la cantidad modificada
                sql = string.Format("UPDATE inv_cierres SET Inventario={0} WHERE Id={1}", existenciaPosterior, idProducto);
                ds = Conexion.Ejecutar(sql); //Ejecutamos la consulta para agregar unidades al inventario
                AgregarEventoLog(idProducto, existenciaActual, cantidad, existenciaPosterior, TipoLog.UnidadesDescontadas);
            }
            else throw new ArgumentException("El producto no existe en la base de datos"); //Si elproducto no existe lanzamos una excepción
        }

        /// <summary>
        /// Agregarmos un nuevo evento al historial del inventario
        /// </summary>
        /// <param name="idProducto"></param>
        /// <param name="cantidadAnterior"></param>
        /// <param name="cantidadModificada"></param>
        /// <param name="cantidadPosterior"></param>
        /// <param name="tipo"></param>
        public void AgregarEventoLog(int idProducto, int cantidadAnterior, int cantidadModificada, int cantidadPosterior, TipoLog tipo)
        {
            string modelo = "";
            string color = "";
            byte talla = 0;
            ObtenerDatosProducto(idProducto, ref modelo, ref color, ref talla); //Llamamos a la función para que nos devuelva los datos del producto con esa Id
            sql = string.Format("INSERT INTO log_cierres (Fecha, Modelo, Color, Talla, C_anterior, C_modificada, C_posterior, Tipo) VALUES (#{0}#, '{1}', '{2}', {3}, {4}, {5}, {6},'{7}')", DateTime.Now.ToString("MM/dd/yyyy H:mm:ss"), modelo, color, talla, cantidadAnterior, cantidadModificada, cantidadPosterior, tipo.ToString());
            ds = Conexion.Ejecutar(sql); //Ejecutamos la consulta para añadir el evento al historial
        }

        /// <summary>
        /// Devuelve la cantidad especificada de eventos del historial del inventario
        /// </summary>
        /// <param name="cantidad"></param>
        /// <returns></returns>
        public DataSet UltimosEventosLog(byte cantidad)
        {
            sql = string.Format("SELECT TOP {0} Tipo, Fecha, Color, Modelo, Talla, C_anterior, C_modificada, C_posterior FROM log_cierres ORDER BY Fecha DESC;", cantidad);
            ds = Conexion.Ejecutar(sql); //Ejecutamos una consulta para obtener los ultimos n eventos del historial
            return ds.Tables[0].Rows.Count != 0 ? ds : null;
        }
        //FIN DE LOS MÉTODOS ////////////////////////////////////////////////
    }

    class ReportesProduccion
    {
        public DateTime[] ObtenerLotesReportes()
        {
            DataSet ds;
            ds = Conexion.Ejecutar("SELECT DISTINCT(Lote) FROM pedidos_reportes ORDER BY Lote ASC");
            if (ds.Tables[0].Rows.Count != 0)
            {
                DateTime[] lotes = new DateTime[ds.Tables[0].Rows.Count]; //Creamos el array con la cantidad de elementos que tenga la consulta
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    lotes[i] = Convert.ToDateTime(ds.Tables[0].Rows[i]["Lote"]);
                }
                return lotes;
            }
            else
            {
                return null;
            }
        }
    }

    class Usuario
    {
        public static void CerrarSesion(string usuario)
        {
            string sql = string.Format("UPDATE usuarios SET online=false WHERE usuario='{0}'", usuario);
            DataSet ds = Conexion.Ejecutar(sql);


            Application.OpenForms["frmPrincipal"].Hide();
            frmLogin wLogin = Funciones.TraerVentana<frmLogin>();
            wLogin.txtUsuario.Clear();
            wLogin.txtContra.Clear();
            wLogin.Show();
            wLogin.txtUsuario.Focus();
        }
    }
}
