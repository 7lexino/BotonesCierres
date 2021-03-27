using System;
//using System.Collections.Generic;
//using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Data.OleDb;

//using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace BotonesCierres
{
    public partial class frmReporteProduccion : Form
    {
        ReportesProduccion Reportes = new ReportesProduccion(); //Inicializamos la clase para los reportes

        //Declaramos las variables que vamos a utilizar para manejar un archivo de Excel
        Excel.Application appExcel;
        Excel.Workbooks misLibros;
        Excel.Workbook miLibro;
        Excel.Sheets misHojas;
        Excel.Worksheet miHoja;

        //Variables globales para ejecutar queries
        string sql;
        DataSet ds;

        //Arrays donde almacenaremos las columnas que se podrán editar en los datagridview
        byte[] columnasSacos = new byte[] { 9, 10, 11, 13, 14 };
        byte[] columnasFaldasPantalones = new byte[] { 9, 10, 19 };
        byte[] columnasBlusas = new byte[] { 9, 10, 11, 14 };

        public frmReporteProduccion()
        {
            InitializeComponent();
            VentanaChica(); //Inicializamos la ventana en modo chica
        }

        private void btnElegirArchivo_Click(object sender, EventArgs e)
        {
            this.txtRutaArchivo.Text = Funciones.SeleccionarArchivo("C:\\", "Archivos Excel (*.xls)|*.xlsx"); //Pedimos que seleccione el archivo
        }

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            string rutaExcel = this.txtRutaArchivo.Text.Trim(); //Guardamos el Path del archivo seleccionado
            if (string.IsNullOrEmpty(rutaExcel)) return; //Validamos que tenga una ruta seleccionada

            this.pbCargandoArchivo.Visible = true;//Mostramos la barra de progreso
            this.pbCargandoArchivo.Value = 10;

            appExcel = new Excel.Application(); //Inicializamos la aplicación
            misLibros = appExcel.Workbooks;
            miLibro = misLibros.Open(Filename: rutaExcel, UpdateLinks: false, ReadOnly: true); //Abrimos el libro en la ruta seleccionada
            misHojas = miLibro.Worksheets;
            miHoja = (Excel.Worksheet)misHojas.get_Item(1);//Estamos posicionados en el archivo seleccionado en la primera hoja

            //Validamos que el archivo abierto tiene el formato que requerimos
            if (miHoja.get_Range("J2").Value != "Reporte Status" || miHoja.get_Range("A8").Value != "Pedido")
            {
                
                Mensajes.NoExito("El archivo seleccionado no tiene el formato requerido");
                miLibro.Close(false); //Cerramos el libro abierto
                //GC.Collect();
                appExcel.Quit(); //Salimos de la aplicación Excel
                //GC.Collect();
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(miLibro); //Eliminamos archivos basura
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(misLibros); //Eliminamos archivos basura
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(appExcel); //Eliminamos archivos basura
                this.pbCargandoArchivo.Visible = false;
                return;
            }

            //Declaramos las variables que vamos a utilizar para obtener la fecha del archivo
            string loteString, mesString;
            byte diaLote, mesLote;
            ushort anoLote;
            DateTime fechaLote;

            //Convertimos la fecha que se nos da en el archivo en una fecha real ////////////////////
            loteString = miHoja.get_Range("C8").End[Excel.XlDirection.xlDown].Value; //Guardamos la cadena que contiene la fecha del reporte
            //Vamos desglozando la cadena hasta tener todos sus componentes de la fecha (dia, mes y año)
            loteString = loteString.Substring(loteString.IndexOf(" ") + 1, loteString.Length - loteString.IndexOf(" ") - 1); //Quitamos el dia literal (Ej. Lunes)
            diaLote = byte.Parse(loteString.Substring(0, loteString.IndexOf(" "))); //Dia
            loteString = loteString.Substring(loteString.IndexOf("de") + 3, loteString.Length - loteString.IndexOf("de") - 3);
            mesString = loteString.Substring(0, loteString.IndexOf(" ")); //Mes en string
            
            //Obtenemos el mes en numero
            switch (mesString)
            {
                case "Enero": mesLote = 1; break;
                case "Febrero": mesLote = 2; break;
                case "Marzo": mesLote = 3; break;
                case "Abril": mesLote = 4; break;
                case "Mayo": mesLote = 5; break;
                case "Junio": mesLote = 6; break;
                case "Julio": mesLote = 7; break;
                case "Agosto": mesLote = 8; break;
                case "Septiembre": mesLote = 9; break;
                case "Octubre": mesLote = 10; break;
                case "Noviembre": mesLote = 11; break;
                case "Diciembre": mesLote = 12; break;
                default: mesLote = 0; break;
            }

            loteString = loteString.Substring(loteString.IndexOf("de") + 3, loteString.Length - loteString.IndexOf("de") - 3);
            anoLote = ushort.Parse(loteString); //Año
            fechaLote = new DateTime(anoLote, mesLote, diaLote);//Fecha completa
            /////////////////////////////////////////////////////////////////////////////////////////

            this.pbCargandoArchivo.Value = 20;

            try
            {
                sql = string.Format("SELECT TOP 1 Id FROM pedidos_reportes WHERE Lote=#{0}#;", fechaLote.ToString("MM/dd/yyyy"));
                ds = Conexion.Ejecutar(sql); //Hacemos una consulta para verificar si ya existe un reporte con ese lote (fecha)
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //Esto se ejecuta porque ya existe un reporte con ese lote
                    DialogResult opcion = Mensajes.Confirmar("Ya existe un reporte con esa fecha, ¿deseas reemplazar el reporte ?");
                    //DialogResult opcion = MessageBox.Show("", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (opcion == DialogResult.Yes) //Preguntamos al usuario si quiere reemplazar el reporte
                    {
                        sql = string.Format("DELETE FROM pedidos_reportes WHERE Lote=#{0}#", fechaLote.ToString("MM/dd/yyyy"));
                        Conexion.Ejecutar(sql);//Borramos todos los registros con la id recogida (por conjunto)
                        GuardarReporte(fechaLote); //Ahora si llamamos a la función para insertar los nuevos registros del reporte
                    }//Si decide no reemplazar salimos de la función
                }
                else
                {
                    //Esto se ejecuta si no existe un reporte con ese lote
                    GuardarReporte(fechaLote);//Llamamos a la función para guardar el reporte en la DB
                }
                miLibro.Close(false);
                //GC.Collect();
                appExcel.Quit();
                //GC.Collect();
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(miLibro); //Eliminamos archivos basura
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(misLibros); //Eliminamos archivos basura
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(appExcel); //Eliminamos archivos basura
                this.pbCargandoArchivo.Value = 1000;
                LlenarReportes();
                this.tabReportes.SelectTab("tEditar"); //Activamos la tab para visualizar el reporte generado
                this.cbReportes.Text = fechaLote.ToShortDateString(); //Escogemos la fecha
                this.btnSacos.PerformClick(); //Simulamos un clic en el botón de sacos
            }
            catch (Exception err)
            {
                Mensajes.Excepcion(err.Message);
                return;
            }
            
            this.txtRutaArchivo.Clear(); //Limpiamos el textobox
            this.pbCargandoArchivo.Visible = false;
        }

        private void GuardarReporte(DateTime fechaLote)
        {
            //Declaración
            bool esNumero;
            byte conjunto;
            ushort cantSaco, cantFalda, cantPant, cantBlusa;
            uint pedido;
            string empresa, color, estiloSaco, materialSaco, cierreSaco, botonSaco, estiloFalda, materialFalda, cierreFalda, botonFalda, estiloPant, materialPant, botonPant, estiloBlusa, materialBlusa, cierreBlusa, botonBlusa;
            DateTime fechaEntrega;

            //Elementos de botones
            ushort estiloBotonSaco, estiloBotonBlusa;
            byte tallaFrenteSaco, tallaMangaSaco, tallaPant, tallaFalda, tallaBlusa;
            byte cantFrenteSaco, cantMangaSaco;

            //Elementos de cierres
            string estiloCierreSaco, estiloCierreFalda, estiloCierrePant, estiloCierreBlusa;
            string colorCierreSaco, colorCierreFalda, colorCierrePant, colorCierreBlusa;
            byte tallaCierreSaco, tallaCierreFalda, tallaCierrePant, tallaCierreBlusa;

            this.pbCargandoArchivo.Value = 50;

            int filas=0;//Aquí guardaremos la cantidad de filas que sean en total
            int avance=0;//Aquí gaurdaremos el avance que iremos aumentando en el progressbar por fila
            
            Excel.Range mirango = miHoja.get_Range("A10");//Nos posicionamos en la celda A10

            esNumero = true;
            while (esNumero)
            {
                esNumero = uint.TryParse(Convert.ToString(mirango.Value), out pedido);
                if (esNumero == false) break; //Si el dato seleccionado no es numero salimos del while y terminamos de recoger datos
                filas++;
                mirango = mirango.Offset[3, 0]; //Avancamos de 3 en 3 celdas
            }
            avance = Convert.ToInt32(900 / filas);

            mirango = miHoja.get_Range("A10");
            esNumero = true;
            while (esNumero)//Recorremos la celdas hasta que no haya pedidos
            {
                esNumero = uint.TryParse(Convert.ToString(mirango.Value), out pedido);
                if (esNumero == false) break; //Si el dato seleccionado no es numero salimos del while y terminamos de recoger datos

                //Asignamos valores a variables
                empresa = mirango.Offset[0, 3].Value;
                conjunto = Convert.ToByte(mirango.Offset[0, 5].Value);
                color = mirango.Offset[0, 6].Value;
                estiloSaco = mirango.Offset[0, 7].Value;
                materialSaco = mirango.Offset[0, 8].Value;
                cantSaco = Convert.ToUInt16(mirango.Offset[0, 9].Value);
                cierreSaco = mirango.Offset[0, 10].Value;
                botonSaco = mirango.Offset[0, 11].Value;

                estiloFalda = mirango.Offset[0, 12].Value;
                materialFalda = mirango.Offset[0, 13].Value;
                cantFalda = Convert.ToUInt16(mirango.Offset[0, 14].Value);
                cierreFalda = mirango.Offset[0, 15].Value;
                botonFalda = mirango.Offset[0, 16].Value;

                estiloPant = mirango.Offset[0, 17].Value;
                materialPant = mirango.Offset[0, 18].Value;
                cantPant = Convert.ToUInt16(mirango.Offset[0, 19].Value);
                botonPant = mirango.Offset[0, 20].Value;

                estiloBlusa = mirango.Offset[0, 21].Value;
                materialBlusa = mirango.Offset[0, 22].Value;
                cantBlusa = Convert.ToUInt16(mirango.Offset[0, 23].Value);
                cierreBlusa = mirango.Offset[0, 24].Value;
                botonBlusa = mirango.Offset[0, 25].Value;

                fechaEntrega = Convert.ToDateTime(mirango.Offset[0, 26].Value);

                //Desglozamos los componentes de botones ///
                unsafe
                {
                    if (cantSaco > 0)
                        DesglozarBoton(botonSaco, &estiloBotonSaco, &tallaFrenteSaco, &cantFrenteSaco, &tallaMangaSaco, &cantMangaSaco);//Desglozamos botón de saco
                    else
                    {
                        estiloBotonSaco = 0;
                        tallaFrenteSaco = 0;
                        cantFrenteSaco = 0;
                        tallaMangaSaco = 0;
                        cantMangaSaco = 0;
                    }

                    if (cantFalda > 0)
                        DesglozarBoton(botonFalda, &tallaFalda); //Desglozamos botón de falda
                    else
                    {
                        tallaFalda = 0;
                    }

                    if (cantPant > 0)
                        DesglozarBoton(botonPant, &tallaPant); //Desglozamos botón de pantalón
                    else
                    {
                        tallaPant = 0;
                    }

                    if (cantBlusa > 0)
                        DesglozarBoton(botonBlusa, &estiloBotonBlusa, &tallaBlusa); //Desglozamos botón de blusa
                    else
                    {
                        estiloBotonBlusa = 0;
                        tallaBlusa = 0;
                    }
                } //Aquí termina el bloque de unsafe

                ////////////////////////////////////////////

                //Desglozamos los componentes de cierres /////

                if (!string.IsNullOrEmpty(cierreSaco)) //Si la prenda tiene cierre lo desglozamos
                {
                    if (cierreSaco.IndexOf("-") >= 0)
                    {
                        colorCierreSaco = cierreSaco.Substring(0, cierreSaco.IndexOf("-"));
                        estiloCierreSaco = cierreSaco.Substring(cierreSaco.IndexOf("-") + 1, cierreSaco.LastIndexOf("-") - cierreSaco.IndexOf("-") - 1);
                        tallaCierreSaco = byte.Parse(cierreSaco.Substring(cierreSaco.LastIndexOf("-") + 1, cierreSaco.Length - cierreSaco.LastIndexOf("-") - 1));
                    }
                    else
                    { 
                        colorCierreSaco = "";
                        estiloCierreSaco = "";
                        tallaCierreSaco = 0;
                    }
                }
                else //De lo contrario lo igualamos a 0
                {
                    colorCierreSaco = "";
                    estiloCierreSaco = "";
                    tallaCierreSaco = 0;
                }

                if (!string.IsNullOrEmpty(cierreFalda)) //Si la prenda tiene cierre lo desglozamos
                {
                    if (cierreFalda.IndexOf("-") >= 0)
                    {
                        colorCierreFalda = cierreFalda.Substring(0, cierreFalda.IndexOf("-"));
                        estiloCierreFalda = cierreFalda.Substring(cierreFalda.IndexOf("-") + 1, cierreFalda.LastIndexOf("-") - cierreFalda.IndexOf("-") - 1);
                        tallaCierreFalda = byte.Parse(cierreFalda.Substring(cierreFalda.LastIndexOf("-") + 1, cierreFalda.Length - cierreFalda.LastIndexOf("-") - 1));
                    }
                    else
                    {
                        colorCierreFalda = "";
                        estiloCierreFalda = "";
                        tallaCierreFalda = 0;
                    }
                }
                else //De lo contrario lo igualamos a 0
                {
                    colorCierreFalda = "";
                    estiloCierreFalda = "";
                    tallaCierreFalda = 0;
                }

                if (cantPant > 0) //Si la cantidad de faldas > 0, ponemos los datos de la falda al pantalón
                {
                    colorCierrePant = colorCierreFalda;
                    estiloCierrePant = "N";
                    tallaCierrePant = tallaCierreFalda;
                }
                else //De lo contrario igualamos todo a 0
                {
                    colorCierrePant = "";
                    estiloCierrePant = "";
                    tallaCierrePant = 0;
                }

                if (cantFalda == 0) //Si la cantidad de faltas es igual a 0 entonces borramos la información que tiene dentro
                {
                    colorCierreFalda = "";
                    estiloCierreFalda = "";
                    tallaCierreFalda = 0;
                }

                if (!string.IsNullOrEmpty(cierreBlusa)) //Si la prenda tiene cierre lo desglozamos
                {
                    if (cierreBlusa.IndexOf("-") >= 0)
                    {
                        colorCierreBlusa = cierreBlusa.Substring(0, cierreBlusa.IndexOf("-"));
                        estiloCierreBlusa = cierreBlusa.Substring(cierreBlusa.IndexOf("-") + 1, cierreBlusa.LastIndexOf("-") - cierreBlusa.IndexOf("-") - 1);
                        tallaCierreBlusa = byte.Parse(cierreBlusa.Substring(cierreBlusa.LastIndexOf("-") + 1, cierreBlusa.Length - cierreBlusa.LastIndexOf("-") - 1));
                    }
                    else
                    {
                        colorCierreBlusa = "";
                        estiloCierreBlusa = "";
                        tallaCierreBlusa = 0;
                    }
                }
                else //De lo contrario lo igualamos a 0
                {
                    colorCierreBlusa = "";
                    estiloCierreBlusa = "";
                    tallaCierreBlusa = 0;
                }
                /////////////////////////////////////////////

                //Preparamos las consultas|
                string campos = "Lote, Pedido, Empresa, Conjunto, Color, EstiloSaco, MaterialSaco, CantSaco, ColorCierreSaco, EstiloCierreSaco, TallaCierreSaco, EstiloBotonSaco, TallaFrenteBotonSaco, CantFrenteBotonSaco, TallaMangaBotonSaco, CantMangaBotonSaco, EstiloPant, MaterialPant, CantPant, ColorCierrePant, EstiloCierrePant, TallaCierrePant, EstiloBotonPant, TallaBotonPant, EstiloFalda, MaterialFalda, CantFalda, ColorCierreFalda, EstiloCierreFalda, TallaCierreFalda, EstiloBotonFalda, TallaBotonFalda, EstiloBlusa, MaterialBlusa, CantBlusa, ColorCierreBlusa, EstiloCierreBlusa, TallaCierreBlusa, EstiloBotonFrenteBlusa, TallaBotonBlusa, CantFrenteBotonBlusa, EstiloBotonEscondidoBlusa, CantEscondidoBotonBlusa, FechaEntrega";
                string valores = "#" + fechaLote.ToString("MM/dd/yyyy") + "#, " + pedido + ", '" + empresa + "', " + conjunto + ", '" + color + "', '" + estiloSaco + "', '" + materialSaco + "', " + cantSaco + ", '" + colorCierreSaco + "', '" + estiloCierreSaco + "', " + tallaCierreSaco + ", " + estiloBotonSaco + ", " + tallaFrenteSaco + ", " + cantFrenteSaco + ", " + tallaMangaSaco + ", " + cantMangaSaco + ", '" + estiloPant + "', '" + materialPant + "', " + cantPant + ", '" + colorCierrePant + "', '" + estiloCierrePant + "', " + tallaCierrePant + ", '" + estiloBotonSaco + "', " + tallaPant + ", '" + estiloFalda + "', '" + materialFalda + "', " + cantFalda + ", '" + colorCierreFalda + "', '" + estiloCierreFalda + "', " + tallaCierreFalda + ", '" + estiloBotonSaco + "', " + tallaFalda + ", '" + estiloBlusa + "', '" + materialBlusa + "', " + cantBlusa + ", '" + colorCierreBlusa + "', '" + estiloCierreBlusa + "', " + tallaCierreBlusa + ", " + estiloBotonBlusa + ", " + tallaBlusa + ", " + 0 + ", " + 1 + ", " + 0 + ", #" + fechaEntrega.ToString("MM/dd/yyyy") + "#";
                sql = string.Format("INSERT INTO pedidos_reportes ({0}) VALUES ({1});", campos, valores);
                Conexion.Ejecutar(sql); //Ejecutamos la consulta para insertar el registro en la DB
                mirango = mirango.Offset[3, 0]; //Avancamos de 3 en 3 celdas

                this.pbCargandoArchivo.Value += avance;
            }
        }

        //Función para desglozar los componentes del botón (6 parámetros)
        private unsafe void DesglozarBoton(string cadenaBoton, ushort* estilo, byte* tallaFrente, byte* cantFrente, byte* tallaManga, byte* cantManga)
        {
            //Inicializamos todo en 0
            *estilo = 0;
            *tallaFrente = 0;
            *cantFrente = 0;
            *tallaManga = 0;
            *cantManga = 0;

            bool convertir;
            string cadenaManga;
            if (string.IsNullOrEmpty(cadenaBoton) || cadenaBoton.IndexOf("BT") < 0 || cadenaBoton.IndexOf(" ") < 0) return;
            convertir = ushort.TryParse(cadenaBoton.Substring(cadenaBoton.IndexOf("BT") + 2, cadenaBoton.IndexOf(" ") - 2), out *estilo); //Obtenemos el estilo del botón
            if (convertir == false) return;
            cadenaBoton = cadenaBoton.Substring(cadenaBoton.IndexOf(" ") + 1, cadenaBoton.Length - cadenaBoton.IndexOf(" ") - 1); //Le quitamos el estilo a la cadena
            if (cadenaBoton.IndexOf("T") < 0)//Si no tiene almenos una T el botón es incorrecto, borramos datos y salimos de la función
            {
                *estilo = 0;
                return;
            }
            //MessageBox.Show(cadenaBoton.Substring(cadenaBoton.IndexOf("T") + 1, cadenaBoton.Length - cadenaBoton.IndexOf(" ") - 1));
            convertir = byte.TryParse(cadenaBoton.Substring(cadenaBoton.IndexOf("T") + 1, cadenaBoton.IndexOf(" ") - cadenaBoton.IndexOf("T") - 1), out *tallaFrente);
            if (convertir == false) return;
            if (cadenaBoton.IndexOf("M") >= 0) // Con esto sabemos que tiene manga
            {
                cadenaManga = cadenaBoton.Substring(cadenaBoton.IndexOf("M"), cadenaBoton.IndexOf(")")-cadenaBoton.IndexOf("M"));
                cadenaBoton = cadenaBoton.Substring(0, cadenaBoton.IndexOf("M"));
                if (cadenaManga.IndexOf(" ") >= 0) //Evitamos el error de -1
                {
                    convertir = byte.TryParse(cadenaManga.Substring(1, cadenaManga.IndexOf(" ") - 1), out *cantManga);
                    if (convertir == false)
                    {
                        *tallaManga = 0;
                        *cantManga = 0;

                    }

                    convertir = byte.TryParse(cadenaManga.Substring(cadenaManga.IndexOf("T")+1,2),out *tallaManga);
                    if(convertir == false)
                    {
                        *tallaManga = 0;
                        *cantManga = 0;
                    }
                }

            }
            if (cadenaBoton.IndexOf("F") >= 0) //Verficamos que tenga cantidad de botones en frente
            {
                convertir = byte.TryParse(cadenaBoton.Substring(cadenaBoton.IndexOf("F")+1,1),out *cantFrente);
            }
        }

        //Función para desglozar los componentes del botón (3 parámetros)
        private unsafe void DesglozarBoton(string cadenaBoton, ushort* estilo, byte* tallaFrente)
        {
            //Inicializamos todo en 0
            *estilo = 0;
            *tallaFrente = 0;

            bool convertir;
            if (string.IsNullOrEmpty(cadenaBoton) || cadenaBoton.IndexOf("BT") < 0 || cadenaBoton.IndexOf(" ") < 0) return;
            convertir = ushort.TryParse(cadenaBoton.Substring(cadenaBoton.IndexOf("BT") + 2, cadenaBoton.IndexOf(" ") - 2), out *estilo); //Obtenemos el estilo del botón
            if (convertir == false) return;
            cadenaBoton = cadenaBoton.Substring(cadenaBoton.IndexOf(" ") + 1, cadenaBoton.Length - cadenaBoton.IndexOf(" ") - 1); //Le quitamos el estilo a la cadena
            if (cadenaBoton.IndexOf("T") < 0)//Si no tiene almenos una T el botón es incorrecto, borramos datos y salimos de la función
            {
                *estilo = 0;
                return;
            }

            convertir = byte.TryParse(cadenaBoton.Substring(cadenaBoton.IndexOf("T") + 1,2), out *tallaFrente);
            if (convertir == false) return;
        }

        //Función para desglozar los componentes del botón (2 parámetros)
        private unsafe void DesglozarBoton(string cadenaBoton, byte* tallaFrente)
        {
            //Inicializamos todo en 0
            *tallaFrente = 0;

            bool convertir;
            if (string.IsNullOrEmpty(cadenaBoton) || cadenaBoton.IndexOf("BT") < 0 || cadenaBoton.IndexOf(" ") < 0) return;
            cadenaBoton = cadenaBoton.Substring(cadenaBoton.IndexOf(" ") + 1, cadenaBoton.Length - cadenaBoton.IndexOf(" ") - 1); //Le quitamos el estilo a la cadena
            if (cadenaBoton.IndexOf("T") < 0) return; //Si no tiene almenos una T el botón es incorrecto, borramos datos y salimos de la función
            convertir = byte.TryParse(cadenaBoton.Substring(cadenaBoton.IndexOf("T") + 1, 2), out *tallaFrente);
            if (convertir == false) return;
        }

        private void tEditar_Enter(object sender, EventArgs e)
        {
            VentanaGrande();
            this.FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void tBotones_Enter(object sender, EventArgs e)
        {
            VentanaChica();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void VentanaChica()
        {
            this.Width = 608;
            this.Height = 376;
            tabReportes.Width = 568;
            tabReportes.Height = 314;
        }
        private void VentanaGrande()
        {
            frmPrincipal wPrincipal = Funciones.TraerVentana<frmPrincipal>();

            int ancho = wPrincipal.Width;
            this.Width = Convert.ToInt32(ancho - (0.40*ancho));
            this.Height = 650;
        }

        private void frmReporteProduccion_Load(object sender, EventArgs e)
        {
            this.pbCargandoArchivo.Visible = false;
            this.pbExportandoArchivo.Visible = false;
            LlenarReportes();
        }

        private void LlenarReportes()
        {
            cbReportes.Items.Clear(); //Limpiamos el combobox
            if (Reportes.ObtenerLotesReportes() != null) //Verificamos que haya reportes existentes
            {
                foreach (DateTime lote in Reportes.ObtenerLotesReportes())
                {
                    cbReportes.Items.Add(lote);
                }
            }
        }
        

        private void LlenarGrid(string prenda)
        {
            dgVerReporte.DataSource = null; //Limpiamos el datagridview
            
            //Verificamos cual botón presionó para hacer su respectiva consulta
            switch (prenda)
            {
                case "Sacos":
                    sql = string.Format("SELECT Id, Pedido, Empresa, Conjunto, Color, EstiloSaco, MaterialSaco, CantSaco, EstiloBotonSaco, ColorBoton, TallaFrenteBotonSaco, CantFrenteBotonSaco, (CantFrenteBotonSaco * CantSaco) AS [TotalFrenteS], TallaMangaBotonSaco, CantMangaBotonSaco, (CantMangaBotonSaco * CantSaco) as [TotalMangaS], ColorCierreSaco, EstiloCierreSaco, TallaCierreSaco, (CantSaco) as [TotalC], FechaEntrega FROM pedidos_reportes WHERE Lote=#{0}#;", cbReportes.Text);
                    break;

                case "FaldasPantalones":
                    sql = string.Format("SELECT Id, Pedido, Empresa, Conjunto, Color, EstiloFalda, MaterialFalda, CantFalda, EstiloBotonFalda, ColorBoton, TallaBotonFalda, (CantFalda) as [TotalBotonesF], ColorCierreFalda, EstiloCierreFalda, TallaCierreFalda, (CantFalda) as [TotalCierresF], EstiloPant, MaterialPant, CantPant, TallaBotonPant, (CantPant) as [TotalBotonesP], ColorCierrePant, EstiloCierrePant, TallaCierrePant, (CantPant) AS [TotalCierresP], FechaEntrega FROM pedidos_reportes WHERE Lote=#{0}#;", cbReportes.Text);
                    break;

                case "Blusas":
                    sql = string.Format("SELECT Id, Pedido, Empresa, Conjunto, Color, EstiloBlusa, MaterialBlusa, CantBlusa, EstiloBotonFrenteBlusa, ColorBotonBlusa, TallaBotonBlusa, CantFrenteBotonBlusa, (CantFrenteBotonBlusa * CantBlusa) AS [TotalFrenteBlusa], EstiloBotonEscondidoBlusa, CantEscondidoBotonBlusa, (CantEscondidoBotonBlusa * CantBlusa) AS [TotalEscondido], ColorCierreBlusa, EstiloCierreBlusa, TallaCierreBlusa, (CantBlusa) AS [TotalCierres], FechaEntrega FROM pedidos_reportes WHERE Lote=#{0}#;", cbReportes.Text);
                    break;

                default:
                    Mensajes.Excepcion("FATAL ERROR, favor de comunicarse con el administrador del sistema.");
                    return;
            }

            try
            {
                ds = Conexion.Ejecutar(sql); //Ejecutamos la consulta
                dgVerReporte.DataSource = ds.Tables[0]; //Llenamos el datagrid con la información
            }
            catch (Exception err)
            {
                Mensajes.Excepcion(err.Message);
                return;
            }

            dgVerReporte.Columns[0].Visible = false; //Ocultamos el ID
            dgVerReporte.Columns[3].Frozen = true; //Congelamos hasta la columna conjunto

            //Verificamos de que prenda se trata para aplicar sus cambios visuales al DataGridView
            switch (prenda)
            {
                case "Sacos":
                    //Renombramos el datagrid y también pasamos el width que queremos que tenga cada columna
                    // HeaderText , Width
                    Funciones.EditarEncabezadoYAnchoDataGrid(dgVerReporte,new string[,] {
                        { "Id","20" },
                        { "Pedido","50" },
                        { "Empresa","200" },
                        { "Conj.", "30" },
                        { "Color","65"},
                        {"Estilo","60"},
                        {"Material","60"},
                        {"Cant. Sacos","40"},
                        {"Modelo Botón","40"},
                        {"Color Botón","60"},
                        {"Talla Frente","40"},
                        {"Cant. Frente","40"},
                        {"Total Frente","60"},
                        {"Talla Manga","40"},
                        {"Cant. Manga","40"},
                        {"Total Manga","60"},
                        {"Color Cierre","40"},
                        {"Modelo Cierre","40"},
                        {"Talla Cierre","40"},
                        {"Total Cierres","60"},
                        {"Fecha de entrega","70"}});

                    //Cambiamos el color de algunas columnas
                    dgVerReporte.Columns[12].DefaultCellStyle.BackColor = Color.LightYellow;
                    dgVerReporte.Columns[15].DefaultCellStyle.BackColor = Color.LightYellow;
                    dgVerReporte.Columns[19].DefaultCellStyle.BackColor = Color.LightYellow;
                    //Cambiamos el bold a los totales
                    dgVerReporte.Columns[12].DefaultCellStyle.Font = new Font(dgVerReporte.Font, FontStyle.Bold);
                    dgVerReporte.Columns[15].DefaultCellStyle.Font = new Font(dgVerReporte.Font, FontStyle.Bold);
                    dgVerReporte.Columns[19].DefaultCellStyle.Font = new Font(dgVerReporte.Font, FontStyle.Bold);

                    //Ahora permitimos editar ciertas columnas
                    foreach (DataGridViewColumn c in dgVerReporte.Columns)
                    {
                        if (Array.IndexOf(columnasSacos, Convert.ToByte(c.Index))<0)
                            c.ReadOnly = true;
                    }

                    break;

                case "FaldasPantalones":
                    //Renombramos el datagrid y también pasamos el width que queremos que tenga cada columna
                    // HeaderText , Width
                    Funciones.EditarEncabezadoYAnchoDataGrid(dgVerReporte, new string[,]
                    {
                        {"Id","20" },
                        {"Pedido","50" },
                        {"Empresa","200" },
                        {"Conj.","30" },
                        {"Color","65" },
                        {"Estilo","60" },
                        {"Material","60" },
                        {"Cant. Faldas","40" },
                        {"Modelo Botón","40" },
                        {"Color Botón","60" },
                        {"Talla Botón","40" },
                        {"Total Botones Falda","60" },
                        {"Color Cierre","40" },
                        {"Modelo Cierre","40" },
                        {"Talla Cierre","40" },
                        {"Total Cierres","60" },
                        {"Estilo Pant.","60" },
                        {"Material Pant.","60" },
                        {"Cant. Pants.","40" },
                        {"Talla Botón","40" },
                        {"Total Botones Pant.","60" },
                        {"Color Cierre","40" },
                        {"Modelo Cierre","40" },
                        {"Talla Cierre","40" },
                        {"Total Cierres Pant.","60" },
                        {"Fecha de entrega","70" }
                    });

                    //Cambiamos el color de algunas columnas
                    dgVerReporte.Columns[11].DefaultCellStyle.BackColor = Color.LightYellow;
                    dgVerReporte.Columns[15].DefaultCellStyle.BackColor = Color.LightYellow;
                    dgVerReporte.Columns[20].DefaultCellStyle.BackColor = Color.LightYellow;
                    dgVerReporte.Columns[24].DefaultCellStyle.BackColor = Color.LightYellow;
                    //Cambiamos el bold a los totales
                    dgVerReporte.Columns[11].DefaultCellStyle.Font = new Font(dgVerReporte.Font, FontStyle.Bold);
                    dgVerReporte.Columns[15].DefaultCellStyle.Font = new Font(dgVerReporte.Font, FontStyle.Bold);
                    dgVerReporte.Columns[20].DefaultCellStyle.Font = new Font(dgVerReporte.Font, FontStyle.Bold);
                    dgVerReporte.Columns[24].DefaultCellStyle.Font = new Font(dgVerReporte.Font, FontStyle.Bold);

                    //Ahora permitimos editar ciertas columnas
                    foreach (DataGridViewColumn c in dgVerReporte.Columns)
                    {
                        if (Array.IndexOf(columnasFaldasPantalones, Convert.ToByte(c.Index)) < 0)
                            c.ReadOnly = true;
                    }

                    break;

                case "Blusas":
                    //Renombramos el datagrid y también pasamos el width que queremos que tenga cada columna
                    // HeaderText , Width
                    Funciones.EditarEncabezadoYAnchoDataGrid(dgVerReporte, new string[,]
                    {
                        {"Id","20" },
                        {"Pedido","50" },
                        {"Empresa","200" },
                        {"Conj.","30" },
                        {"Color","65" },
                        {"Estilo","60" },
                        {"Material","60" },
                        {"Cant. Blusas","40" },
                        {"Modelo Botón","40" },
                        {"Color Botón","60" },
                        {"Talla Botón","40" },
                        {"Cant. Botones Frente","40" },
                        {"Total Botones Frente","60" },
                        {"Modelo Botón Escondido","40" },
                        {"Cant. Botón Escondido","40" },
                        {"Total Botón Escondido","60" },
                        {"Color Cierre","40" },
                        {"Modelo Cierre","40" },
                        {"Talla Cierre","40" },
                        {"Total Cierres","60" },
                        {"Fecha de entrega","70" }
                    });

                    //Cambiamos el color de algunas columnas
                    dgVerReporte.Columns[12].DefaultCellStyle.BackColor = Color.LightYellow;
                    dgVerReporte.Columns[15].DefaultCellStyle.BackColor = Color.LightYellow;
                    dgVerReporte.Columns[19].DefaultCellStyle.BackColor = Color.LightYellow;
                    //Cambiamos el bold a los totales
                    dgVerReporte.Columns[12].DefaultCellStyle.Font = new Font(dgVerReporte.Font, FontStyle.Bold);
                    dgVerReporte.Columns[15].DefaultCellStyle.Font = new Font(dgVerReporte.Font, FontStyle.Bold);
                    dgVerReporte.Columns[19].DefaultCellStyle.Font = new Font(dgVerReporte.Font, FontStyle.Bold);

                    //Ahora permitimos editar ciertas columnas
                    foreach (DataGridViewColumn c in dgVerReporte.Columns)
                    {
                        if (Array.IndexOf(columnasBlusas, Convert.ToByte(c.Index)) < 0)
                            c.ReadOnly = true;
                    }

                    break;

                default:
                    Mensajes.Excepcion("FATAL ERROR, favor de comunicarse con el administrador del sistema.");
                    return;
            }

        }

        private void btnSacos_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbReportes.Text)) return; //Validamos que no esté vacío el combobox del reporte
            LlenarGrid("Sacos"); //Llamamos la función para llenar el DataGridView con el parametro correspondiente

            //Activamos 
            this.btnFaldas.Enabled = true;
            this.btnBlusas.Enabled = true;

            //Desactivamos
            this.btnSacos.Enabled = false;
        }

        private void btnFaldas_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbReportes.Text)) return; //Validamos que no esté vacío el combobos del reporte
            LlenarGrid("FaldasPantalones"); //Llamamos la función para llenar el DataGridView con el parametro correspondiente

            //Activamos
            this.btnSacos.Enabled = true;
            this.btnBlusas.Enabled = true;

            //Desactivamos
            this.btnFaldas.Enabled = false;
        }

        private void btnBlusas_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbReportes.Text)) return; //Validamos que no esté vacío el combobos del reporte
            LlenarGrid("Blusas"); //Llamamos la función para llenar el DataGridView con el parametro correspondiente

            //Activamos
            this.btnSacos.Enabled = true;
            this.btnFaldas.Enabled = true;

            //Desactivamos
            this.btnBlusas.Enabled = false;
        }

        private void btnExportarRep_Click(object sender, EventArgs e)
        {
            if (this.btnSacos.Enabled == false)
            {
                ExportarReporteSacos();
            }else if (this.btnFaldas.Enabled == false)
            {
                ExportarReporteFaldasPantalones();
            }else if (this.btnBlusas.Enabled == false)
            {
                ExportarReporteBlusas();
            }
        }

        private void ExportarReporteSacos()
        {
            SaveFileDialog fichero = new SaveFileDialog();
            fichero.Filter = "Excel (*.xlsx)|*.xlsx"; //Le decimos a que formato va a exportar
            if (fichero.ShowDialog() == DialogResult.OK)
            {
                this.pbExportandoArchivo.Visible = true;
                this.pbExportandoArchivo.Value = 10;

                Microsoft.Office.Interop.Excel.Application aplicacion;
                Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
                Microsoft.Office.Interop.Excel.Worksheet hoja_botones;
                Microsoft.Office.Interop.Excel.Worksheet hoja_cierres;
                Microsoft.Office.Interop.Excel.Range mirango;
                aplicacion = new Microsoft.Office.Interop.Excel.Application();
                libros_trabajo = aplicacion.Workbooks.Add();

                this.pbExportandoArchivo.Value = 20;

                //A partir de aquí empezamos a modificar la hoja de botones
                hoja_botones = (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);
                hoja_botones.Name = "Botones";
                mirango = hoja_botones.get_Range("A3");

                int avance = (350 / this.dgVerReporte.Rows.Count);

                //Recorremos el DataGridView rellenando la hoja de trabajo
                for (int i = 0; i < this.dgVerReporte.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < this.dgVerReporte.Columns.Count; j++)
                    {
                        mirango.Offset[i, j].Value = this.dgVerReporte.Rows[i].Cells[j].Value;
                    }
                    this.pbExportandoArchivo.Value += avance;
                }

                hoja_botones.get_Range("A:A").EntireColumn.Hidden = true; //Ocutlamos la columna del ID
                hoja_botones.get_Range("Q:T").EntireColumn.Delete();

                //Ponemos los encabezados del reporte
                hoja_botones.get_Range("A2").Value = "ID";
                hoja_botones.get_Range("B2").Value = "PEDIDO";
                hoja_botones.get_Range("C2").Value = "EMPRESA";
                hoja_botones.get_Range("D2").Value = "CONJ.";
                hoja_botones.get_Range("E2").Value = "COLOR";
                hoja_botones.get_Range("F2").Value = "ESTILO";
                hoja_botones.get_Range("G2").Value = "MATERIAL";
                hoja_botones.get_Range("H2").Value = "CANT.";
                hoja_botones.get_Range("I2").Value = "MODELO BOTÓN";
                hoja_botones.get_Range("J2").Value = "COLOR BOTÓN";
                hoja_botones.get_Range("K2").Value = "TALLA FRENTE";
                hoja_botones.get_Range("L2").Value = "CANT. FRENTE";
                hoja_botones.get_Range("M2").Value = "TOTAL FRENTE";
                hoja_botones.get_Range("N2").Value = "TALLA MANGA";
                hoja_botones.get_Range("O2").Value = "CANT. MANGA";
                hoja_botones.get_Range("P2").Value = "TOTAL MANGA";
                hoja_botones.get_Range("Q2").Value = "FECHA DE ENTREGA";

                mirango = hoja_botones.get_Range("A2");
                mirango.CurrentRegion.Borders.Weight = Excel.XlBorderWeight.xlMedium;
                mirango.CurrentRegion.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                mirango.CurrentRegion.Font.Name = "Arial";
                mirango.CurrentRegion.Font.Size = 8;

                hoja_botones.get_Range("B1:Q1").Merge();
                hoja_botones.get_Range("B1:Q1").Value = "BOTONES DE SACO";
                hoja_botones.get_Range("B1:Q1").Font.Size = 16;
                hoja_botones.get_Range("B1:Q1").Font.Bold = true;
                hoja_botones.get_Range("B1:Q1").Interior.Color = System.Drawing.Color.FromArgb(191, 191, 191);
                hoja_botones.get_Range("B1:Q1").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                hoja_botones.get_Range("A2:Q2").Borders.LineStyle = Excel.XlLineStyle.xlDouble;
                hoja_botones.get_Range("A2:Q2").WrapText = true;
                hoja_botones.get_Range("A2:Q2").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja_botones.get_Range("A2:Q2").Font.Bold = true;
                hoja_botones.get_Range("A2:Q2").Font.Size = 10;

                this.pbExportandoArchivo.Value = 500;

                //A partir de aquí empezamos a modificar la hoja de cierres
                hoja_cierres = (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.Add(After: hoja_botones);
                hoja_cierres.Name = "Cierres";
                mirango = hoja_cierres.get_Range("A3");

                //Recorremos el DataGridView rellenando la hoja de trabajo
                for (int i = 0; i < this.dgVerReporte.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < this.dgVerReporte.Columns.Count; j++)
                    {
                        mirango.Offset[i, j].Value = this.dgVerReporte.Rows[i].Cells[j].Value;
                    }
                    this.pbExportandoArchivo.Value += avance;
                }

                hoja_cierres.get_Range("A:A").EntireColumn.Hidden = true; //Ocutlamos la columna del ID
                hoja_cierres.get_Range("I:P").EntireColumn.Delete();

                hoja_cierres.get_Range("A2").Value = "ID";
                hoja_cierres.get_Range("B2").Value = "PEDIDO";
                hoja_cierres.get_Range("C2").Value = "EMPRESA";
                hoja_cierres.get_Range("D2").Value = "CONJ.";
                hoja_cierres.get_Range("E2").Value = "COLOR";
                hoja_cierres.get_Range("F2").Value = "ESTILO";
                hoja_cierres.get_Range("G2").Value = "MATERIAL";
                hoja_cierres.get_Range("H2").Value = "CANT.";
                hoja_cierres.get_Range("I2").Value = "COLOR CIERRE";
                hoja_cierres.get_Range("J2").Value = "MODELO CIERRE";
                hoja_cierres.get_Range("K2").Value = "TALLA CIERRE";
                hoja_cierres.get_Range("L2").Value = "TOTAL CIERRES";
                hoja_cierres.get_Range("M2").Value = "FECHA DE ENTREGA";

                mirango = hoja_cierres.get_Range("A2");
                mirango.CurrentRegion.Borders.Weight = Excel.XlBorderWeight.xlMedium;
                mirango.CurrentRegion.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                mirango.CurrentRegion.Font.Name = "Arial";
                mirango.CurrentRegion.Font.Size = 8;

                hoja_cierres.get_Range("B1:M1").Merge();
                hoja_cierres.get_Range("B1:M1").Value = "CIERRES DE SACO";
                hoja_cierres.get_Range("B1:M1").Font.Size = 16;
                hoja_cierres.get_Range("B1:M1").Font.Bold = true;
                hoja_cierres.get_Range("B1:M1").Interior.Color = System.Drawing.Color.FromArgb(191, 191, 191);
                hoja_cierres.get_Range("B1:M1").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                hoja_cierres.get_Range("A2:M2").Borders.LineStyle = Excel.XlLineStyle.xlDouble;
                hoja_cierres.get_Range("A2:M2").WrapText = true;
                hoja_cierres.get_Range("A2:M2").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja_cierres.get_Range("A2:M2").Font.Bold = true;
                hoja_cierres.get_Range("A2:M2").Font.Size = 10;

                this.pbExportandoArchivo.Value = 900;

                libros_trabajo.SaveAs(fichero.FileName,
                    Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault);
                this.pbExportandoArchivo.Value = 1000;
                aplicacion.Visible = true;
                this.pbExportandoArchivo.Visible = false;

            }
        }

        private void ExportarReporteFaldasPantalones()
        {
            SaveFileDialog fichero = new SaveFileDialog();
            fichero.Filter = "Excel (*.xlsx)|*.xlsx"; //Le decimos a que formato va a exportar
            if (fichero.ShowDialog() == DialogResult.OK)
            {
                this.pbExportandoArchivo.Visible = true;
                this.pbExportandoArchivo.Value = 10;

                Microsoft.Office.Interop.Excel.Application aplicacion;
                Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
                Microsoft.Office.Interop.Excel.Worksheet hoja_botones;
                Microsoft.Office.Interop.Excel.Worksheet hoja_cierres;
                Microsoft.Office.Interop.Excel.Range mirango;
                aplicacion = new Microsoft.Office.Interop.Excel.Application();
                libros_trabajo = aplicacion.Workbooks.Add();

                this.pbExportandoArchivo.Value = 20;

                //A partir de aquí empezamos a modificar la hoja de botones
                hoja_botones = (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);
                hoja_botones.Name = "Botones";
                mirango = hoja_botones.get_Range("A3");

                int avance = (350 / this.dgVerReporte.Rows.Count);

                //Recorremos el DataGridView rellenando la hoja de trabajo
                for (int i = 0; i < this.dgVerReporte.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < this.dgVerReporte.Columns.Count; j++)
                    {
                        mirango.Offset[i, j].Value = this.dgVerReporte.Rows[i].Cells[j].Value;
                    }
                    pbExportandoArchivo.Value += avance;
                }

                hoja_botones.get_Range("A:A").EntireColumn.Hidden = true; //Ocutlamos la columna del ID
                hoja_botones.get_Range("M:P").EntireColumn.Delete();
                hoja_botones.get_Range("R:U").EntireColumn.Delete();

                //Ponemos los encabezados del reporte
                hoja_botones.get_Range("A2").Value = "ID";
                hoja_botones.get_Range("B2").Value = "PEDIDO";
                hoja_botones.get_Range("C2").Value = "EMPRESA";
                hoja_botones.get_Range("D2").Value = "CONJ.";
                hoja_botones.get_Range("E2").Value = "COLOR";
                hoja_botones.get_Range("F2").Value = "ESTILO";
                hoja_botones.get_Range("G2").Value = "MATERIAL";
                hoja_botones.get_Range("H2").Value = "CANT.";
                hoja_botones.get_Range("I2").Value = "MODELO BOTÓN";
                hoja_botones.get_Range("J2").Value = "COLOR BOTÓN";
                hoja_botones.get_Range("K2").Value = "TALLA";
                hoja_botones.get_Range("L2").Value = "TOTAL BOTONES";
                hoja_botones.get_Range("M2").Value = "ESTILO";
                hoja_botones.get_Range("N2").Value = "MATERIAL";
                hoja_botones.get_Range("O2").Value = "CANT.";
                hoja_botones.get_Range("P2").Value = "TALLA";
                hoja_botones.get_Range("Q2").Value = "TOTAL BOTONES";
                hoja_botones.get_Range("R2").Value = "FECHA DE ENTREGA";

                mirango = hoja_botones.get_Range("A2");
                mirango.CurrentRegion.Borders.Weight = Excel.XlBorderWeight.xlMedium;
                mirango.CurrentRegion.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                mirango.CurrentRegion.Font.Name = "Arial";
                mirango.CurrentRegion.Font.Size = 8;

                hoja_botones.get_Range("B1:R1").Merge();
                hoja_botones.get_Range("B1:R1").Value = "BOTONES DE FALDAS Y PANTALONES";
                hoja_botones.get_Range("B1:R1").Font.Size = 16;
                hoja_botones.get_Range("B1:R1").Font.Bold = true;
                hoja_botones.get_Range("B1:R1").Interior.Color = System.Drawing.Color.FromArgb(191, 191, 191);
                hoja_botones.get_Range("B1:R1").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                hoja_botones.get_Range("A2:R2").Borders.LineStyle = Excel.XlLineStyle.xlDouble;
                hoja_botones.get_Range("A2:R2").WrapText = true;
                hoja_botones.get_Range("A2:R2").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja_botones.get_Range("A2:R2").Font.Bold = true;
                hoja_botones.get_Range("A2:R2").Font.Size = 10;

                this.pbExportandoArchivo.Value = 500;

                //A partir de aquí empezamos a modificar la hoja de cierres
                hoja_cierres = (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.Add(After: hoja_botones);
                hoja_cierres.Name = "Cierres";
                mirango = hoja_cierres.get_Range("A3");

                //Recorremos el DataGridView rellenando la hoja de trabajo
                for (int i = 0; i < this.dgVerReporte.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < this.dgVerReporte.Columns.Count; j++)
                    {
                        mirango.Offset[i, j].Value = this.dgVerReporte.Rows[i].Cells[j].Value;
                    }
                    this.pbExportandoArchivo.Value += avance;
                }

                hoja_cierres.get_Range("A:A").EntireColumn.Hidden = true; //Ocutlamos la columna del ID
                hoja_cierres.get_Range("I:L").EntireColumn.Delete();
                hoja_cierres.get_Range("P:Q").EntireColumn.Delete();

                hoja_cierres.get_Range("A2").Value = "ID";
                hoja_cierres.get_Range("B2").Value = "PEDIDO";
                hoja_cierres.get_Range("C2").Value = "EMPRESA";
                hoja_cierres.get_Range("D2").Value = "CONJ.";
                hoja_cierres.get_Range("E2").Value = "COLOR";
                hoja_cierres.get_Range("F2").Value = "ESTILO";
                hoja_cierres.get_Range("G2").Value = "MATERIAL";
                hoja_cierres.get_Range("H2").Value = "CANT.";
                hoja_cierres.get_Range("I2").Value = "COLOR CIERRE";
                hoja_cierres.get_Range("J2").Value = "MODELO CIERRE";
                hoja_cierres.get_Range("K2").Value = "TALLA CIERRE";
                hoja_cierres.get_Range("L2").Value = "TOTAL CIERRES";
                hoja_cierres.get_Range("M2").Value = "ESTILO";
                hoja_cierres.get_Range("N2").Value = "MATERIAL";
                hoja_cierres.get_Range("O2").Value = "CANT.";
                hoja_cierres.get_Range("P2").Value = "COLOR CIERRE";
                hoja_cierres.get_Range("Q2").Value = "MODELO CIERRE";
                hoja_cierres.get_Range("R2").Value = "TALLA CIERRE";
                hoja_cierres.get_Range("S2").Value = "TOTAL CIERRES";
                hoja_cierres.get_Range("T2").Value = "FECHA DE ENTREGA";

                mirango = hoja_cierres.get_Range("A2");
                mirango.CurrentRegion.Borders.Weight = Excel.XlBorderWeight.xlMedium;
                mirango.CurrentRegion.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                mirango.CurrentRegion.Font.Name = "Arial";
                mirango.CurrentRegion.Font.Size = 8;

                hoja_cierres.get_Range("B1:T1").Merge();
                hoja_cierres.get_Range("B1:T1").Value = "CIERRES DE FALDAS Y PANTALONES";
                hoja_cierres.get_Range("B1:T1").Font.Size = 16;
                hoja_cierres.get_Range("B1:T1").Font.Bold = true;
                hoja_cierres.get_Range("B1:T1").Interior.Color = System.Drawing.Color.FromArgb(191, 191, 191);
                hoja_cierres.get_Range("B1:T1").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                hoja_cierres.get_Range("A2:T2").Borders.LineStyle = Excel.XlLineStyle.xlDouble;
                hoja_cierres.get_Range("A2:T2").WrapText = true;
                hoja_cierres.get_Range("A2:T2").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja_cierres.get_Range("A2:T2").Font.Bold = true;
                hoja_cierres.get_Range("A2:T2").Font.Size = 10;

                this.pbExportandoArchivo.Value = 900;

                libros_trabajo.SaveAs(fichero.FileName,
                    Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault);
                this.pbExportandoArchivo.Value = 1000;
                aplicacion.Visible = true;
                this.pbExportandoArchivo.Visible = false;
            }
        }

        private void ExportarReporteBlusas()
        {
            SaveFileDialog fichero = new SaveFileDialog();
            fichero.Filter = "Excel (*.xlsx)|*.xlsx"; //Le decimos a que formato va a exportar
            if (fichero.ShowDialog() == DialogResult.OK)
            {
                this.pbExportandoArchivo.Visible = true;
                this.pbExportandoArchivo.Value = 10;

                Microsoft.Office.Interop.Excel.Application aplicacion;
                Microsoft.Office.Interop.Excel.Workbook libros_trabajo;
                Microsoft.Office.Interop.Excel.Worksheet hoja_botones;
                Microsoft.Office.Interop.Excel.Worksheet hoja_cierres;
                Microsoft.Office.Interop.Excel.Range mirango;
                aplicacion = new Microsoft.Office.Interop.Excel.Application();
                libros_trabajo = aplicacion.Workbooks.Add();

                this.pbExportandoArchivo.Value = 20;

                //A partir de aquí empezamos a modificar la hoja de botones
                hoja_botones = (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.get_Item(1);
                hoja_botones.Name = "Botones";
                mirango = hoja_botones.get_Range("A3");

                int avance = (350 / this.dgVerReporte.Rows.Count);

                //Recorremos el DataGridView rellenando la hoja de trabajo
                for (int i = 0; i < this.dgVerReporte.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < this.dgVerReporte.Columns.Count; j++)
                    {
                        mirango.Offset[i, j].Value = this.dgVerReporte.Rows[i].Cells[j].Value;
                    }
                    this.pbExportandoArchivo.Value += avance;
                }

                hoja_botones.get_Range("A:A").EntireColumn.Hidden = true; //Ocutlamos la columna del ID
                hoja_botones.get_Range("Q:T").EntireColumn.Delete();

                //Ponemos los encabezados del reporte
                hoja_botones.get_Range("A2").Value = "ID";
                hoja_botones.get_Range("B2").Value = "PEDIDO";
                hoja_botones.get_Range("C2").Value = "EMPRESA";
                hoja_botones.get_Range("D2").Value = "CONJ.";
                hoja_botones.get_Range("E2").Value = "COLOR";
                hoja_botones.get_Range("F2").Value = "ESTILO";
                hoja_botones.get_Range("G2").Value = "MATERIAL";
                hoja_botones.get_Range("H2").Value = "CANT.";
                hoja_botones.get_Range("I2").Value = "MODELO BOTÓN";
                hoja_botones.get_Range("J2").Value = "COLOR BOTÓN";
                hoja_botones.get_Range("K2").Value = "TALLA";
                hoja_botones.get_Range("L2").Value = "CANT. FRENTE";
                hoja_botones.get_Range("M2").Value = "TOTAL FRENTE";
                hoja_botones.get_Range("N2").Value = "MODELO";
                hoja_botones.get_Range("O2").Value = "CANT. ESCONDIDO";
                hoja_botones.get_Range("P2").Value = "TOTAL ESCONDIDO";
                hoja_botones.get_Range("Q2").Value = "FECHA DE ENTREGA";

                mirango = hoja_botones.get_Range("A2");
                mirango.CurrentRegion.Borders.Weight = Excel.XlBorderWeight.xlMedium;
                mirango.CurrentRegion.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                mirango.CurrentRegion.Font.Name = "Arial";
                mirango.CurrentRegion.Font.Size = 8;

                hoja_botones.get_Range("B1:Q1").Merge();
                hoja_botones.get_Range("B1:Q1").Value = "BOTONES DE BLUSAS";
                hoja_botones.get_Range("B1:Q1").Font.Size = 16;
                hoja_botones.get_Range("B1:Q1").Font.Bold = true;
                hoja_botones.get_Range("B1:Q1").Interior.Color = System.Drawing.Color.FromArgb(191, 191, 191);
                hoja_botones.get_Range("B1:Q1").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                hoja_botones.get_Range("A2:Q2").Borders.LineStyle = Excel.XlLineStyle.xlDouble;
                hoja_botones.get_Range("A2:Q2").WrapText = true;
                hoja_botones.get_Range("A2:Q2").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja_botones.get_Range("A2:Q2").Font.Bold = true;
                hoja_botones.get_Range("A2:Q2").Font.Size = 10;

                this.pbExportandoArchivo.Value = 500;

                //A partir de aquí empezamos a modificar la hoja de cierres
                hoja_cierres = (Microsoft.Office.Interop.Excel.Worksheet)libros_trabajo.Worksheets.Add(After: hoja_botones);
                hoja_cierres.Name = "Cierres";
                mirango = hoja_cierres.get_Range("A3");

                //Recorremos el DataGridView rellenando la hoja de trabajo
                for (int i = 0; i < this.dgVerReporte.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < this.dgVerReporte.Columns.Count; j++)
                    {
                        mirango.Offset[i, j].Value = this.dgVerReporte.Rows[i].Cells[j].Value;
                    }
                    this.pbExportandoArchivo.Value += avance;
                }

                hoja_cierres.get_Range("A:A").EntireColumn.Hidden = true; //Ocutlamos la columna del ID
                hoja_cierres.get_Range("I:P").EntireColumn.Delete();

                hoja_cierres.get_Range("A2").Value = "ID";
                hoja_cierres.get_Range("B2").Value = "PEDIDO";
                hoja_cierres.get_Range("C2").Value = "EMPRESA";
                hoja_cierres.get_Range("D2").Value = "CONJ.";
                hoja_cierres.get_Range("E2").Value = "COLOR";
                hoja_cierres.get_Range("F2").Value = "ESTILO";
                hoja_cierres.get_Range("G2").Value = "MATERIAL";
                hoja_cierres.get_Range("H2").Value = "CANT.";
                hoja_cierres.get_Range("I2").Value = "COLOR CIERRE";
                hoja_cierres.get_Range("J2").Value = "MODELO CIERRE";
                hoja_cierres.get_Range("K2").Value = "TALLA CIERRE";
                hoja_cierres.get_Range("L2").Value = "TOTAL CIERRES";
                hoja_cierres.get_Range("M2").Value = "FECHA DE ENTREGA";

                mirango = hoja_cierres.get_Range("A2");
                mirango.CurrentRegion.Borders.Weight = Excel.XlBorderWeight.xlMedium;
                mirango.CurrentRegion.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                mirango.CurrentRegion.Font.Name = "Arial";
                mirango.CurrentRegion.Font.Size = 8;

                hoja_cierres.get_Range("B1:M1").Merge();
                hoja_cierres.get_Range("B1:M1").Value = "CIERRES DE BLUSAS";
                hoja_cierres.get_Range("B1:M1").Font.Size = 16;
                hoja_cierres.get_Range("B1:M1").Font.Bold = true;
                hoja_cierres.get_Range("B1:M1").Interior.Color = System.Drawing.Color.FromArgb(191, 191, 191);
                hoja_cierres.get_Range("B1:M1").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                hoja_cierres.get_Range("A2:M2").Borders.LineStyle = Excel.XlLineStyle.xlDouble;
                hoja_cierres.get_Range("A2:M2").WrapText = true;
                hoja_cierres.get_Range("A2:M2").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                hoja_cierres.get_Range("A2:M2").Font.Bold = true;
                hoja_cierres.get_Range("A2:M2").Font.Size = 10;

                this.pbExportandoArchivo.Value = 900;

                libros_trabajo.SaveAs(fichero.FileName,
                    Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault);
                this.pbExportandoArchivo.Value = 1000;
                aplicacion.Visible = true;
                this.pbExportandoArchivo.Visible = false;
            }
        }

        private void dgVerReporte_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; //Ponemos esto porque al momento de cambiar los encabezados el DataGrid lo detecta con un cambio de valor

            //Buscamos cual es la opción seleccionada y mandamos llamar la función con su parámetro correspondiente
            if (this.btnSacos.Enabled == false)
            {
                ActualizarTabla("Sacos", e.ColumnIndex, e.RowIndex);
            }else if(this.btnFaldas.Enabled == false)
            {
                ActualizarTabla("FaldasPantalones", e.ColumnIndex, e.RowIndex);
            }
            else
            {
                ActualizarTabla("Blusas", e.ColumnIndex, e.RowIndex);
            }
        }

        //Esta función sirve para actualizar la el DataGridView
        private void ActualizarTabla(string prenda, int columna, int fila)
        {
            int posColumna = 0; //Almacenara la posicion en el array donde se encuentra la columna encontrada
            int id = Convert.ToInt32(dgVerReporte[0, fila].Value);//Obtenemos el ID del conjunto
            
            //Verificamos cual prenda está modificando
            switch (prenda)
            {
                case "Sacos":
                    //Verificamos que columna se modificó
                    posColumna = Array.IndexOf(columnasSacos, Convert.ToByte(columna));
                    if (posColumna >= 0)
                    {
                        try
                        {
                            sql = string.Format("UPDATE pedidos_reportes SET {0}='{1}' WHERE Id={2}",dgVerReporte.Columns[columna].Name,dgVerReporte[columna,fila].Value, id);
                            
                            Conexion.Ejecutar(sql);
                        }
                        catch (Exception err)
                        {
                            Mensajes.Excepcion(err.Message);
                        }
                    }
                    break;

                case "FaldasPantalones":
                    //Verificamos que columna se modificó
                    posColumna = Array.IndexOf(columnasFaldasPantalones, Convert.ToByte(columna));
                    if (posColumna >= 0)
                    {
                        try
                        {
                            sql = string.Format("UPDATE pedidos_reportes SET {0}='{1}' WHERE Id={2}", dgVerReporte.Columns[columna].Name, dgVerReporte[columna, fila].Value, id);
                            
                            Conexion.Ejecutar(sql);
                        }
                        catch (Exception err)
                        {
                            Mensajes.Excepcion(err.Message);
                        }
                    }
                    break;

                case "Blusas":
                    //Verificamos que columna se modificó
                    posColumna = Array.IndexOf(columnasBlusas, Convert.ToByte(columna));
                    if (posColumna >= 0)
                    {
                        try
                        {
                            sql = string.Format("UPDATE pedidos_reportes SET {0}='{1}' WHERE Id={2}", dgVerReporte.Columns[columna].Name, dgVerReporte[columna, fila].Value, id);
                            
                            Conexion.Ejecutar(sql);
                        }
                        catch (Exception err)
                        {
                            Mensajes.Excepcion(err.Message);
                        }
                    }
                    break;

                default:
                    Mensajes.Excepcion("FATAL ERROR, favor de comunicarse con el administrador del sistema.");
                    return;
            }
        }

        private void btnActualizarTabla_Click(object sender, EventArgs e)
        {
            //Buscamos cual es la opción seleccionada y mandamos llamar la función con su parámetro correspondiente
            if (this.btnSacos.Enabled == false)
            {
                LlenarGrid("Sacos");
            }
            else if (this.btnFaldas.Enabled == false)
            {
                LlenarGrid("FaldasPantalones");
            }
            else if (this.btnBlusas.Enabled==false)
            {
                LlenarGrid("Blusas");
            }
        }

        private void btnEtiquetas_Click(object sender, EventArgs e)
        {
            frmPrincipal wPrincipal = Funciones.TraerVentana<frmPrincipal>();

            try
            {
                frmEtiquetas wEtiquetas = Funciones.TraerVentana<frmEtiquetas>();
                wEtiquetas.BringToFront();
            }
            catch (IndexOutOfRangeException ex)
            {
                frmEtiquetas wEtiquetas = new frmEtiquetas();
                wEtiquetas.MdiParent = wPrincipal;
                wEtiquetas.Show();
                wPrincipal.Tag = wEtiquetas;
                wEtiquetas.Show();
            }

        }
    }
}
