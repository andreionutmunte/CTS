using AdmitereFacultate.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AdmitereFacultate
{
    public partial class FormAdmitereFac : Form
    {
        public List<Candidati> ListaStudenti = new List<Candidati>();

        public FormAdmitereFac()
        {
            InitializeComponent();
            PreluareDate();
        }

        #region Operatii CRUD  

        // create
        private void btAdaugaCandidat_Click(object sender, EventArgs e)
        {
            bool isValid = true;

            String nume = tbNume.Text;
            if (String.IsNullOrEmpty(nume) || String.IsNullOrWhiteSpace(nume))
            {
                isValid = false;
            }

            String prenume = tbPrenume.Text;
            if (String.IsNullOrEmpty(prenume) || String.IsNullOrWhiteSpace(prenume))
            {
                isValid = false;
            }

            String cnp = tbCNP.Text;
            if (String.IsNullOrEmpty(nume) || String.IsNullOrWhiteSpace(nume))
            {
                isValid = false;
            }

            String email = tbEmail.Text;
            if (String.IsNullOrEmpty(nume) || String.IsNullOrWhiteSpace(nume))
            {
                isValid = false;
            }

            String adresa = tbAdresa.Text;
            if (String.IsNullOrEmpty(nume) || String.IsNullOrWhiteSpace(nume))
            {
                isValid = false;
            }

            String telefon = tbTelefon.Text;
            if (String.IsNullOrEmpty(nume) || String.IsNullOrWhiteSpace(nume))
            {
                isValid = false;
            }

            Enum.TryParse(comboBox1.Text, out EnumFacultati optiune1);
            Enum.TryParse(comboBox2.Text, out EnumFacultati optiune2);
            Enum.TryParse(comboBox3.Text, out EnumFacultati optiune3);

            double.TryParse(tbNota1.Text, out double nota1);
            double.TryParse(tbNota2.Text, out double nota2);
            double.TryParse(tbNota3.Text, out double nota3);

            Medii media = new Medii();

            try
            {
                media.NotaProba1 = double.Parse(tbNota1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                isValid = false;
            }

            try
            {
                media.NotaProba2 = double.Parse(tbNota2.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                isValid = false;
            }

            try
            {
                media.NotaProba3 = double.Parse(tbNota3.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                isValid = false;
            }

            if (isValid)
            {
                try
                {
                    Candidati dateCandidati = new Candidati(nume, prenume, optiune1, optiune2, optiune3, media, cnp, adresa, email, telefon, nota1, nota2, nota3);
                    ListaStudenti.Add(dateCandidati);

                    populareListView();

                    curatareCampuri();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Formularul contine erori!", "Eroare",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        // read
        private void populareListView()
        {
            lvCandidati.Items.Clear();

            foreach (Candidati each in ListaStudenti)
            {
                ListViewItem item = new ListViewItem(new String[] { each.Nume, each.Prenume, each.optiune1.ToString(), each.optiune2.ToString(), each.optiune3.ToString(), each.Medie.GetMedie().ToString() });
                lvCandidati.Items.Add(item);
            }
        }

        // update
        private void btEditeazaCand_Click(object sender, EventArgs e)
        {
            if (lvCandidati.SelectedItems.Count != 0)
            {
                Candidati candidati = ListaStudenti.ElementAt(lvCandidati.SelectedIndices[0]);

                Editare_candidat editare_Candidat = new Editare_candidat(candidati);
                editare_Candidat.ShowDialog();

                populareListView();
            }
        }

        // delete
        private void btStergeCandidat_Click(object sender, EventArgs e)
        {
            if (lvCandidati.SelectedItems.Count != 0)
            {
                if (MessageBox.Show("Doresti sa stergi candidatul?", "Stergere",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ListaStudenti.RemoveAt(lvCandidati.SelectedIndices[0]);
                    populareListView();
                }
            }
        }

        #endregion

        #region Validare formular

        private void tbNume_Validating(object sender, CancelEventArgs e)
        {
            String value = tbNume.Text;
            if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value))
            {
                epNume.SetError((Control)sender, "Completeaza numele!");
                e.Cancel = true;
            }
        }

        private void tbNume_Validated(object sender, EventArgs e)
        {
            epNume.Clear();
        }

        private void tbPrenume_Validating(object sender, CancelEventArgs e)
        {
            String value = tbPrenume.Text;
            if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value))
            {
                epNume.SetError((Control)sender, "Completeaza prenumele!");
                e.Cancel = true;
            }

        }

        private void tbPrenume_VisibleChanged(object sender, EventArgs e)
        {
            epPrenume.Clear();
        }

        private void comboBox1_Validating(object sender, CancelEventArgs e)
        {
            //String value = comboBox1.Text;
            if (comboBox1.SelectedIndex == -1)
            {
                epOptiune1.SetError((Control)sender, "Selecteaza cel putin o optiune!");
                e.Cancel = true;
            }
        }

        private void comboBox1_Validated(object sender, EventArgs e)
        {
            epOptiune1.Clear();
        }

        private void tbCNP_Validating(object sender, CancelEventArgs e)
        {
            String value = tbCNP.Text;
            if (String.IsNullOrEmpty(value) || String.IsNullOrWhiteSpace(value))
            {
                epCNP.SetError((Control)sender, "Completeaza CNP!");
                e.Cancel = true;
            }
        }

        private void tbCNP_Validated(object sender, EventArgs e)
        {
            epCNP.Clear();
        }

        #endregion

        #region Implementare ContextMenuStrip

        private void cms_editeaza_Click(object sender, EventArgs e)
        {
            btEditeazaCand_Click(sender, e);
        }

        private void cms_sterge_Click(object sender, EventArgs e)
        {
            btStergeCandidat_Click(sender, e);
        }

        private void lvCandidati_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cms1.Show(Cursor.Position.X + 5, Cursor.Position.Y + 5);
            }
        }

        #endregion

        #region Event handling

        private void lvCandidati_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left &&
                lvCandidati.FocusedItem.Bounds.Contains(e.Location))
            {
                btEditeazaCand_Click(sender, e);
            }
        }

        private void BtnStergeFormular_Click(object sender, EventArgs e)
        {
            curatareCampuri();
        }

        #endregion

        #region Metode utilitare

        private void curatareCampuri()
        {
            tbNume.Clear();
            tbPrenume.Clear();
            tbEmail.Clear();
            tbTelefon.Clear();
            tbAdresa.Clear();
            tbCNP.Clear();

            // AICI ERA CHECKED LIST BOX!!!
            //for (int i = 0; i < clbOptiune.Items.Count; i++)
            //clbOptiune.SetItemCheckState(i, (state ? CheckState.Checked : CheckState.Unchecked));
            //clbOptiune.ClearSelected();

            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;

            tbNota1.Clear();
            tbNota2.Clear();
            tbNota3.Clear();
        }

        #endregion

        #region Implementare MenuStrip + (De)Serializari + Export + Clipboard

        private void powerdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Andrei Munteanu :)");
        }


        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SerializareBinara_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream fs = new FileStream("binary.dat", FileMode.Create);
            formatter.Serialize(fs, ListaStudenti);

            fs.Close();

            MessageBox.Show("Serializare binara realizata!");

        }

        private void DeserializareBinara_Click(object sender, EventArgs e)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Selecteaza fisier binar";
            ofd.Filter = "Binary files (*.dat)|*.dat|All files (*.*)|*.*";
            ofd.FilterIndex = 1;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(ofd.FileName, FileMode.Open);

                ListaStudenti = formatter.Deserialize(fs) as List<Candidati>;
                fs.Close();

                populareListView();
            }
        }

        private void export_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Salveaza in fisier text";
            sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            sfd.FilterIndex = 1;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(sfd.FileName);
                foreach (Candidati each in ListaStudenti)
                {
                    sw.Write(each.Nume + " " + each.Prenume + " " + each.optiune1 + " " + each.optiune2 + " " + each.optiune3 + " " + each.Medie.GetMedie() + "\n");
                }

                sw.Close();
            }

        }

        private void copiazaDateCandidatInClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvCandidati.SelectedItems.Count != 0)
            {
                ListViewItem item = lvCandidati.SelectedItems[0];
                string text = String.Empty;
                foreach (ListViewItem.ListViewSubItem lvsi in item.SubItems)
                {
                    text += lvsi.Text + " ";
                }
                Clipboard.SetText(text);
                clipboardTool.Text = "Copiat in Clipboard: " + text;
            }

        }
        
        #endregion

        #region Implementare ToolStrip - Print

        private void toolStripPrint_Click(object sender, EventArgs e)
        {
            pageSetupDialog.Document = printDocument;
            pageSetupDialog.PageSettings = printDocument.DefaultPageSettings;

            if (pageSetupDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.DefaultPageSettings = pageSetupDialog.PageSettings;
                printPreviewDialog.Document = printDocument;
                printPreviewDialog.ShowDialog();
            }
        }


        private void lvCandidati_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                toolStripPrint_Click(sender, e);
            }
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;

            Brush brush = Brushes.DarkGreen;
            Font font = new Font("Arial", 15);

            Pen pen = new Pen(brush);

            PageSettings settings = printDocument.DefaultPageSettings;
            var totalDrawableW = settings.PaperSize.Width - settings.Margins.Left - settings.Margins.Right;
            var totalDrawableH = settings.PaperSize.Height - settings.Margins.Bottom - settings.Margins.Top;

            if (settings.Landscape)
            {
                var temp = totalDrawableH;
                totalDrawableH = totalDrawableW;
                totalDrawableW = temp;
            }

            int cellWidth = 117;
            int cellHeight = 24;

            int x = settings.Margins.Left;
            int y = 100;

            graphics.DrawString("      Lista candidatilor" + "", font, brush, totalDrawableW / 2, y);

            y += 100;

            // desenare cap tabel
            graphics.DrawRectangle(pen, x, y, cellWidth, cellHeight);
            graphics.DrawRectangle(pen, x + cellWidth, y, cellWidth, cellHeight);
            graphics.DrawRectangle(pen, x + cellWidth + cellWidth, y, cellWidth, cellHeight);
            graphics.DrawRectangle(pen, x + cellWidth + cellWidth + cellWidth, y, cellWidth, cellHeight);
            graphics.DrawRectangle(pen, x + cellWidth + cellWidth + cellWidth + cellWidth, y, cellWidth, cellHeight);
            graphics.DrawRectangle(pen, x + cellWidth + cellWidth + cellWidth + cellWidth + cellWidth, y, cellWidth, cellHeight);

            // desensare denumire coloane
            graphics.DrawString("     Nume", font, brush, x, y);
            graphics.DrawString("   Prenume", font, brush, x + cellWidth, y);
            graphics.DrawString("  Optiunea1", font, brush, x + cellWidth + cellWidth, y);
            graphics.DrawString("  Optiunea2", font, brush, x + cellWidth + cellWidth + cellWidth, y);
            graphics.DrawString("  Optiunea3", font, brush, x + cellWidth + cellWidth + cellWidth + cellWidth, y);
            graphics.DrawString("     Medie", font, brush, x + cellWidth + cellWidth + cellWidth + cellWidth + cellWidth, y);

            y += cellHeight;

            foreach (Candidati c in ListaStudenti)
            {
                // desenare tabel
                graphics.DrawRectangle(pen, x, y, cellWidth, cellHeight);
                graphics.DrawRectangle(pen, x + cellWidth, y, cellWidth, cellHeight);
                graphics.DrawRectangle(pen, x + cellWidth + cellWidth, y, cellWidth, cellHeight);
                graphics.DrawRectangle(pen, x + cellWidth + cellWidth + cellWidth, y, cellWidth, cellHeight);
                graphics.DrawRectangle(pen, x + cellWidth + cellWidth + cellWidth + cellWidth, y, cellWidth, cellHeight);
                graphics.DrawRectangle(pen, x + cellWidth + cellWidth + cellWidth + cellWidth + cellWidth, y, cellWidth, cellHeight);

                // desensare continut
                graphics.DrawString(c.Nume, font, brush, x, y);
                graphics.DrawString(c.Prenume, font, brush, x + cellWidth, y);
                graphics.DrawString(c.optiune1.ToString(), font, brush, x + cellWidth + cellWidth, y);
                graphics.DrawString(c.optiune2.ToString(), font, brush, x + cellWidth + cellWidth + cellWidth, y);
                graphics.DrawString(c.optiune3.ToString(), font, brush, x + cellWidth + cellWidth + cellWidth + cellWidth, y);
                graphics.DrawString(c.Medie.GetMedie().ToString(), font, brush, x + cellWidth + cellWidth + cellWidth + cellWidth + cellWidth, y);

                y += cellHeight;
            }
        }

        private void toolStripStatistici_Click(object sender, EventArgs e)
        {
            ChartForm form = new ChartForm(ListaStudenti);
            form.ShowDialog();
        }

        #endregion

        #region Drag&Drop
        private void panel_DragDrop(object sender, DragEventArgs e)
        {
            string[] filePaths = e.Data.GetData(DataFormats.FileDrop, false) as string[];

            foreach (String path in filePaths)
            {
                string[] content = File.ReadAllLines(path);

                foreach (String line in content)
                {
                    string[] coloana = line.Split('\t');

                    try
                    {
                        String nume = coloana[0];
                        String prenume = coloana[1];
                        Enum.TryParse(coloana[2], out EnumFacultati optiune1);
                        Enum.TryParse(coloana[3], out EnumFacultati optiune2);
                        Enum.TryParse(coloana[4], out EnumFacultati optiune3);
                        double.TryParse(coloana[5], out double medie);

                       
                        Candidati c = new Candidati(nume, prenume, optiune1, optiune2, optiune3, medie);
                        ListaStudenti.Add(c);
                    }

                    catch(Exception ex)
                    {
                        MessageBox.Show("Informatiile din fisier nu permit instantierea.");
                        continue;
                    }
                }
                populareListView();
            }
        }

        private void panel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        #endregion

        #region Preluarea datelor din baza de date

        public void PreluareDate()
        {
            string connString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = candidati.accdb";
            OleDbConnection conexiune = new OleDbConnection(connString);
           
            try
            {

                conexiune.Open();
                OleDbCommand comanda = new OleDbCommand("SELECT * FROM UserTab");
                comanda.Connection = conexiune;

                OleDbDataReader reader = comanda.ExecuteReader();
                while (reader.Read())
                {
                    string nume;
                    string prenume;
                   

                    nume = reader["nume"].ToString();
                    prenume = reader["nume"].ToString();
                    //double notaProba1 = (double)reader["nota1"];
                    //double notaProba2 = (double)reader["nota2"];
                    //double notaProba3 = (double)reader["nota3"];
                    //Medii medie = new Medii(notaProba1, notaProba2, notaProba3);
                

                    //foreach

                    //Candidati c = new Candidati(nume, prenume);
                    //ListaStudenti.Add(c);
                }
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexiune.Close();
            }
        }

        #endregion

     

      
    }
}

