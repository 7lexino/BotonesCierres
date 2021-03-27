using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Linq;
//using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
//
using Word = Microsoft.Office.Interop.Word;

namespace BotonesCierres
{
    public partial class frmEtiquetas : Form
    {
        public frmEtiquetas()
        {
            InitializeComponent();
        }

        public void LlenarLotes()
        {
            this.cbLotes.Items.Clear();
            this.cbLotes.Items.Clear(); //Limpiamos el combobox
            ReportesProduccion Reportes = new ReportesProduccion();
            if (Reportes.ObtenerLotesReportes() != null) //Verificamos que haya reportes existentes
            {
                foreach (DateTime lote in Reportes.ObtenerLotesReportes())
                {
                    cbLotes.Items.Add(lote);
                }
            }
        }

        private void frmEtiquetas_Load(object sender, EventArgs e)
        {
            ReportesProduccion lotes = new ReportesProduccion();
            this.cbLotes.Items.Clear();
            if (lotes.ObtenerLotesReportes() != null) //Verificamos que haya reportes existentes
            {
                foreach (DateTime lote in lotes.ObtenerLotesReportes())
                {
                    cbLotes.Items.Add(lote);
                }
            }

        }

        void MetodoCargarGif()
        {
            this.imgCargando.Visible = true;
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            string lote = cbLotes.Text, sql = "", strDB = "";

            //Validamos que haya seleccionado algún lote
            if (string.IsNullOrEmpty(lote))
            {
                Mensajes.NoExito("Selecciona un lote");
                return;
            }
            
            Word.Application wrdApp;
            Word._Document wrdDoc;
            Word.MailMerge wrdMailMerge;
            wrdApp = new Word.Application();
            Object oTemplate;
            Object oMissing = System.Reflection.Missing.Value;
            Object oFalse = false;
            Object oTrue = true;
            object oQuery = "";

            oTemplate = Path.GetTempFileName();

            //Elegimos la plantilla según la prenda seleccionada
            if (this.rbBotones.Checked)
            {
                if (this.rbSacos.Checked)
                {
                    File.WriteAllBytes(oTemplate.ToString(), Properties.Resources.PlantillaBotonesSacos);
                    sql = string.Format("SELECT (CantSaco * CantFrenteBotonSaco) AS TotalUno, (CantSaco * CantMangaBotonSaco) AS TotalDos, format(FechaEntrega, 'dd/mm/yyyy') as FechaEntrega, * FROM pedidos_reportes WHERE Lote=#{0}# AND CantSaco <> 0", lote);
                }
                else if (this.rbFaldasPantalones.Checked)
                {

                    File.WriteAllBytes(oTemplate.ToString(), Properties.Resources.PlantillaBotonesPantalones);
                    sql = string.Format("SELECT format(FechaEntrega, 'dd/mm/yyyy') as FechaEntrega, * FROM pedidos_reportes WHERE Lote=#{0}# AND (CantPant <> 0 OR CantFalda <> 0)", lote);
                }
                else
                {
                    File.WriteAllBytes(oTemplate.ToString(), Properties.Resources.PlantillaBotonesBlusas);
                    sql = string.Format("SELECT format(FechaEntrega, 'dd/mm/yyyy') as FechaEntrega, (CantBlusa * CantFrenteBotonBlusa) as TFrente, (CantBlusa * CantEscondidoBotonBlusa) as TOculto, * FROM pedidos_reportes WHERE Lote=#{0}# AND CantBlusa <> 0", lote);
                }
            }
            else
            {
                if (this.rbSacos.Checked)
                {
                    File.WriteAllBytes(oTemplate.ToString(), Properties.Resources.PlantillaCierresSacos);
                    sql = string.Format("SELECT format(FechaEntrega, 'dd/mm/yyyy') as FechaEntrega, * FROM pedidos_reportes WHERE Lote=#{0}# AND CantSaco <> 0 AND ColorCierreSaco <> ''", lote);
                }
                else if (this.rbFaldasPantalones.Checked)
                {
                    File.WriteAllBytes(oTemplate.ToString(), Properties.Resources.PlantillaCierresPantalones);
                    sql = string.Format("SELECT format(FechaEntrega, 'dd/mm/yyyy') as FechaEntrega, * FROM pedidos_reportes WHERE Lote=#{0}# AND (CantPant <> 0 OR CantFalda <> 0)", lote);
                }
                else
                {
                    File.WriteAllBytes(oTemplate.ToString(), Properties.Resources.PlantillaCierresBlusas);
                    sql = string.Format("SELECT format(FechaEntrega, 'dd/mm/yyyy') as FechaEntrega, * FROM pedidos_reportes WHERE Lote=#{0}# AND CantBlusa <> 0 AND ColorCierreBlusa <> ''", lote);
                }
            }
            /////////////////////////////////////////////////////////

            // Create MailMerge Data.                        
            wrdDoc = wrdApp.Documents.Open(ref oTemplate, ref oMissing, ref oTrue, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
            wrdDoc.Select();
            wrdMailMerge = wrdDoc.MailMerge;

            oQuery = sql;
            strDB = @"\\servidornvo\comun\programas\botonescierres\DB_BOTONESCIERRES.mdb";
            object conexion = @"Provider=Microsoft.JET.OLEDB.4.0;Data Source=\\servidornvo\comun\programas\botonescierres\DB_BOTONESCIERRES.mdb";
            wrdDoc.MailMerge.OpenDataSource(strDB, ref oMissing, ref oMissing, ref oFalse, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref conexion, ref oQuery, ref oMissing, ref oFalse, ref oMissing);
            wrdMailMerge.SuppressBlankLines = true;

            // Perform mail merge.
            wrdMailMerge.Destination = Word.WdMailMergeDestination.wdSendToNewDocument;

            try
            {
                wrdMailMerge.Execute(ref oFalse);
                // Close the Template document.
            }
            catch (Exception ex)
            {
                wrdDoc.Saved = false;
                Mensajes.NoExito("La consulta no tiene registros." + ex.Message);
            }

            wrdDoc.Close(ref oFalse, ref oMissing, ref oMissing);

            //Show word application
            wrdApp.Visible = true;

            // Release References.    
            File.Delete(oTemplate.ToString());
            wrdMailMerge = null;
            wrdDoc = null;
            wrdApp = null;

        }
            
    }
}
