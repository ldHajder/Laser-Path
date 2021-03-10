using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ObjParser;
using ObjParser.Types;

namespace Laser_path
{
    public partial class Form1 : Form
    {
        List<Vertex> VertexSortedList;
        private int up_axis;
        int[] axis_distribution = new int[3];
        float[] laser_param;// = new float[4];
        Obj obj = new Obj();
        public Form1()
        {
            InitializeComponent();

        }
        [STAThread]
        private void button3_Click(object sender, EventArgs e)
        {
            laser_param = new float[] { float.Parse(laser1.Value.ToString()), float.Parse(laser2.Value.ToString()), float.Parse(laser3.Value.ToString()), float.Parse(laser4.Value.ToString())};
            ViewForm viewForm = new ViewForm(obj, up_axis, laser_param);
            this.Hide();
            viewForm.ShowDialog();
            
            this.Close();
        }

        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            clearData();
            read_file();

            axis_distribution[0] = 1;//up_axis == 0 ? 3 : 1;
            axis_distribution[1] = 2;//up_axis == 0 ? 1 : up_axis == 2 ? 2 : 3;
            axis_distribution[2] = 3;//up_axis == 2 ? 3 : 2;
            // ____________Write OBJ file
            //obj.WriteObjFile([output path]);
            dataGrid.Rows.Clear();
            save_to_table(obj.VertexList, axis_distribution);
            dataGrid.Visible = true;
        }
        private void clearData()
        {
            obj = new Obj();
        }
        private void read_file()
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "obj files (*.obj)|*.obj";// |All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = /*"C:\\Users\\hajde\\OneDrive\\doktorat\\Additive Manufacturing\\Input data\\Simple Fuji.obj"; //*/ openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    //var fileStream = openFileDialog.OpenFile();

                    string path;
                    // Read Wavefront OBJ file
                    obj.LoadObj(filePath);
                }
            }


            
        }

        private void save_to_table(List<Vertex> VertexList, int[] axis_distribution)
        {
            for (int rows = 0; rows < VertexList.Count; rows++)
            {
                dataGrid.Rows.Add();
                dataGrid.Rows[rows].Cells[0].Value = VertexList[rows].X;
                dataGrid.Rows[rows].Cells[1].Value = VertexList[rows].Y;
                dataGrid.Rows[rows].Cells[2].Value = VertexList[rows].Z;
            }
            //foreach (DataGridViewRow row in dataGrid.Rows)
            //{
            //    row.Cells[Xaxis].Value = VertexSortedList[row.Index].X;
            //    row.Cells[Yaxis].Value = VertexSortedList[row.Index].Z;
            //    row.Cells[Zaxis].Value = VertexSortedList[row.Index].Y;
            //}
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //up_axis = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //up_axis = 1;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            //up_axis = 2;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void laser1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void laser2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void laser3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void laser4_ValueChanged(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void viewMotiveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void whiteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void grayToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void form1BindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
