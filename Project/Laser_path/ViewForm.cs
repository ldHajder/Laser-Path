using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ObjParser.Types;
using ObjParser;

namespace Laser_path
{
    public partial class ViewForm : Form
    {
        public ViewForm(Obj obj, int upAxis, float[] laser_param)
        {
            InitializeComponent();
            this.obj = obj;
            this.upAxis = 5;//upAxis;

            set_vertices();

            this.actualView = new View(vertices);
            this.actual_vertices = vertices;
            this.laser_param = laser_param;
            dataGrid.Rows.Clear();
            save_to_table(vertices);
            this.dataGrid.Visible = true;
            tabControl1.SelectTab(1);
        }
        private int upAxis;
        private List<Vertex> vertices = new List<Vertex>();
        private List<Vertex> path;
        private View actualView;
        private List<Vertex> actual_vertices = new List<Vertex>();

        private bool showFaces;
        private Obj obj;
        private int after = 0;
        private float[] laser_param = { 2.5f, 2.0f, 5.21f, 200000 };
        private bool jarvisbool = false;
        private int up_axis = 1;

        private void save_to_table(List<Vertex> VertexList)
        {
            for (int rows = 0; rows < VertexList.Count; rows++)
            {
                dataGrid.Rows.Add();
                dataGrid.Rows[rows].Cells[1].Value = VertexList[rows].X;
                dataGrid.Rows[rows].Cells[2].Value = VertexList[rows].Y;
                dataGrid.Rows[rows].Cells[3].Value = VertexList[rows].Z;
            }
        }
        private void set_vertices()
        {
            vertices.Clear();
            if (upAxis == 0)
            {
                foreach (Vertex v in obj.VertexList)
                {
                    Vertex vx = new Vertex();
                    vx.X = v.Y; vx.Y = v.Z; vx.Z = v.X; vx.Index = v.Index;
                    vertices.Add(vx);
                }
            }
            else if (upAxis == 1)
            {
                foreach (Vertex v in obj.VertexList)
                {
                    Vertex vx = new Vertex();
                    vx.X = v.X; vx.Y = v.Z; vx.Z = v.Y; vx.Index = v.Index;
                    vertices.Add(vx);
                }
            }
            else
            {
                foreach (Vertex v in obj.VertexList)
                {
                    vertices.Add(v);
                }
            }
        }
        private void paintAxis()
        {
            //GL.Clear(ClearBufferMask.AccumBufferBit);
            //GL.Enable(EnableCap.LineStipple);
            GL.LineWidth(0.5f);
            GL.Begin(BeginMode.LineStripAdjacency);
            GL.Color3(System.Drawing.Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(1, 0, 0);
            GL.Vertex3(0.9, 0.1, 0);
            GL.Vertex3(0.9, -0.1, 0);
            GL.Vertex3(1, 0, 0);
            GL.Color3(System.Drawing.Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 1, 0);
            GL.Vertex3(0.1, 0.9, 0);
            GL.Vertex3(-0.1, 0.9, 0);
            GL.Vertex3(0, 1, 0);
            GL.Color3(System.Drawing.Color.Blue);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 0.1);
            GL.End();
            GL.PointSize(10f);
            GL.Begin(BeginMode.Points);

            GL.Color3(System.Drawing.Color.DarkCyan);
            GL.Vertex3(0, 0, 0);
            GL.End();


        }

        private void paint_List()
        {

            GL.Color3(System.Drawing.Color.Black);
            GL.PointSize(5.0f);
            GL.Begin(BeginMode.Points);
            foreach (Vertex ver in vertices)
            {
                float[] vs = { (float)ver.X / 100, (float)ver.Y / 100, (float)ver.Z / 100 };
                GL.Vertex3(vs);
            }
            GL.End();
        }
        private void paint_Faces()
        {
            //GL.Color3(System.Drawing.Color.LightGray);
            //GL.Begin(BeginMode.Lines);
            //Random rand = new Random();
            //Random rand2 = new Random();
            //Random rand3 = new Random();
            double selected_axis = 0;
            GL.Color3(System.Drawing.Color.Gray);



            foreach (Face ver in obj.FaceList)
            {
                //Random rand = new Random();
                //Random rand2 = new Random();
                //Random rand3 = new Random();
                //GL.Color3(System.Drawing.Color.FromArgb( rand.Next(255), rand2.Next(255), rand3.Next(255))/*System.Drawing.Color.LightGray*/);
                //GL.Begin(BeginMode.Triangles);
                GL.Begin(BeginMode.Lines);
                if (ver.VertexIndexList.Any(x => x >= obj.FaceList.Count))
                {
                }
                else
                {
                    for (int i = 0; i < ver.VertexIndexList.Count(); i++)
                    {
                        if (chosenViewType.SelectedIndex == 0)
                        {
                            float[] point1 = { (float)obj.VertexList[ver.VertexIndexList[i] - 1].X / 100, (float)obj.VertexList[ver.VertexIndexList[i] - 1].Y / 100, (float)obj.VertexList[ver.VertexIndexList[i] - 1].Z / 100 };
                            GL.Vertex3(point1);
                        }
                        else if (chosenViewType.SelectedIndex == 1)
                        {
                            if ((int)obj.VertexList[ver.VertexIndexList[i] - 1].Z <= (int)thirdAxisValue.Value)
                            {
                                float[] point1 = { (float)obj.VertexList[ver.VertexIndexList[i] - 1].X / 100, (float)obj.VertexList[ver.VertexIndexList[i] - 1].Y / 100, (float)obj.VertexList[ver.VertexIndexList[i] - 1].Z / 100 };
                                GL.Vertex3(point1);
                            }
                        }
                        else if (chosenViewType.SelectedIndex == 2)
                        {
                            if (obj.VertexList[ver.VertexIndexList[i] - 1].Y == (int)thirdAxisValue.Value)
                            {
                                float[] point1 = { (float)obj.VertexList[ver.VertexIndexList[i] - 1].X / 100, (float)obj.VertexList[ver.VertexIndexList[i] - 1].Y / 100, (float)obj.VertexList[ver.VertexIndexList[i] - 1].Z / 100 };
                                GL.Vertex3(point1);
                            }
                        }
                        else if (chosenViewType.SelectedIndex == 3)
                        {
                            if (obj.VertexList[ver.VertexIndexList[i] - 1].X == (int)thirdAxisValue.Value)
                            {
                                float[] point1 = { (float)obj.VertexList[ver.VertexIndexList[i] - 1].X / 100, (float)obj.VertexList[ver.VertexIndexList[i] - 1].Y / 100, (float)obj.VertexList[ver.VertexIndexList[i] - 1].Z / 100 };
                                GL.Vertex3(point1);
                            }
                        }
                    }
                }
                //else { 
                //float[] point1 = { (float)obj.VertexList[ver.TextureVertexIndexList[0]-1].X / 100, (float)obj.VertexList[ver.TextureVertexIndexList[0]-1].Y / 100, (float)obj.VertexList[ver.TextureVertexIndexList[0]-1].Z / 100 };
                //float[] point2 = { (float)obj.VertexList[ver.TextureVertexIndexList[1]-1].X / 100, (float)obj.VertexList[ver.TextureVertexIndexList[1]-1].Y / 100, (float)obj.VertexList[ver.TextureVertexIndexList[1]-1].Z / 100 };
                //float[] point3 = { (float)obj.VertexList[ver.TextureVertexIndexList[2]-1].X / 100, (float)obj.VertexList[ver.TextureVertexIndexList[2]-1].Y / 100, (float)obj.VertexList[ver.TextureVertexIndexList[2]-1].Z / 100 };
                //GL.Vertex3(point1);
                //GL.Vertex3(point2);
                //GL.Vertex3(point3);
                //    }
                GL.End();

            }
            //GL.End();
        }
        private void paint_path()
        {
            double selected_axis = 0;
            GL.Color3(System.Drawing.Color.Red);
            GL.Begin(BeginMode.LineLoop);

            foreach (Vertex ver in path)
            {
                if (ver != null)
                {
                    float[] point1 = { (float)ver.X / 100, (float)ver.Y / 100, (float)ver.Z / 100, 0 };
                    GL.Vertex3(point1);
                }
            }
            GL.End();
        }
        private void paint_List(List<Vertex> vertices)
        {
            //GL.Clear(ClearBufferMask.ColorBufferBit);

            //if (after == 0)
            //{
            foreach (Vertex ver in vertices)
            {
                if (ver.Index >= obj.VertexList.Count)
                {
                    GL.Color3(System.Drawing.Color.LightGray);
                    GL.PointSize(3.0f);
                    GL.Begin(BeginMode.Points);
                }
                else
                {
                    GL.Color3(System.Drawing.Color.Black);
                    GL.PointSize(3.0f);
                    GL.Begin(BeginMode.Points);
                }


                float[] vs = { (float)ver.X/* / 100*/, (float)ver.Y /*/ 100*/, (float)ver.Z /*/ 100*/ };
                GL.Vertex3(vs);

            }
            GL.End();
            //}
            //else
            //{
            //    GL.Color3(System.Drawing.Color.LightGray);
            //            GL.PointSize(3.0f);
            //            GL.Begin(BeginMode.Points);
            //    for (int i =0; i<vertices.Count; i++)
            //    {
            //        float[] vs = { (float)vertices[i].X / 100, (float)vertices[i].Y / 100, (float)vertices[i].Z / 100 };
            //        GL.Vertex3(vs);
            //    }
            //    GL.End();
            //}
            //glControl1.SwapBuffers();

        }

        #region GLUT Events
        private int mouseBeforeX = 0;
        private int mouseBeforeY = 0;
        public float CameraZoom { get; private set; }
        public float CameraRotX { get; private set; }
        public float CameraRotY { get; private set; }
        public float CameraRotZ { get; private set; }
        public int[,,] index { get; private set; }

        Vector3 EyePosition = new Vector3(0f, 0f, 15);
        Vector2 CameraPosition = new Vector2(-10, -10);

        #region OnLoad
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            glControl1.KeyDown += new KeyEventHandler(glControl1_KeyDown);
            glControl1.KeyUp += new KeyEventHandler(glControl1_KeyUp);
            glControl1.Resize += new EventHandler(glControl1_Resize);
            glControl1.Paint += new PaintEventHandler(glControl1_Paint);

            ////info about Graphic :)
            ////Text =
            ////    GL.GetString(StringName.Vendor) + " " +
            ////    GL.GetString(StringName.Renderer) + " " +
            ////    GL.GetString(StringName.Version);

            GL.ClearColor(System.Drawing.Color.White);
            GL.Enable(EnableCap.DepthTest);

            // Ensure that the viewport and projection matrix are set correctly.
            glControl1_Resize(glControl1, EventArgs.Empty);
        }
        #endregion

        #region KeysEvents
        private void glControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                //this.Close();
            }
            else if (e.KeyData == Keys.NumPad8)
            {
                transY += 0.1f;
            }
            else if (e.KeyData == Keys.NumPad2)
            {
                transY -= 0.1f;
            }
            else if (e.KeyData == Keys.NumPad4)
            {
                transX -= 0.1f;
            }
            else if (e.KeyData == Keys.NumPad6)
            {
                transX += 0.1f;
            }
            else if (e.KeyData == Keys.Add)
            {
                CameraZoom += 0.1f;
            }
            else if (e.KeyData == Keys.Subtract)
            {
                CameraZoom -= 0.1f;
            }
            Render();
        }

        private void glControl1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "bmp File|*.bmp";
                saveFileDialog1.Title = "Zapisz obraz";
                saveFileDialog1.ShowDialog();

                // If the file name is not an empty string open it for saving.
                if (saveFileDialog1.FileName != "")
                    GrabScreenshot().Save(saveFileDialog1.FileName);
            }
            else if (e.KeyCode == Keys.S)
            {
                CameraRotY -= 1f;
            }
            else if (e.KeyCode == Keys.W)
            {
                CameraRotY += 1f;
            }
            else if (e.KeyCode == Keys.A)
            {
                CameraRotX += 1f;
            }
            else if (e.KeyCode == Keys.D)
            {
                CameraRotX -= 1f;
            }
            else if (e.KeyCode == Keys.Q)
            {
                CameraZoom -= 0.1f;
            }
            else if (e.KeyCode == Keys.E)
            {
                CameraZoom += 0.1f;
            }
            //else if (e.KeyCode == Keys.Up)
            //{
            //    transY += 0.1f;
            //}
            //else if (e.KeyCode == Keys.Down)
            //{
            //    transY -= 0.1f;
            //}
            //else if (e.KeyCode == Keys.Left)
            //{
            //    transX -= 0.1f;
            //}
            //else if (e.KeyCode == Keys.Right)
            //{
            //    transX += 0.1f;
            //}
            Render();
        }
        #endregion

        #region glControl Paint
        private void glControl1_Paint(object sender, PaintEventArgs e)
        {


            glControl1.MakeCurrent();

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Light(LightName.Light0, LightParameter.Diffuse, System.Drawing.Color.Black);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, 5);
            glControl1.SwapBuffers();


            Render();
        }
        #endregion

        #region glControl Resize
        private void glControl1_Resize(object sender, EventArgs e)
        {
            OpenTK.GLControl c = sender as OpenTK.GLControl;

            if (c.ClientSize.Height == 0)
                c.ClientSize = new System.Drawing.Size(c.ClientSize.Width, 1);

            GL.Viewport(0, 0, c.ClientSize.Width, c.ClientSize.Height);

            float aspect_ratio = Width / (float)Height;
            Matrix4 perpective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 0.1f, 100.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perpective);

            //GL.LoadIdentity();
            //GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);

            //GL.MatrixMode(MatrixMode.Modelview);
            //Matrix4 mv = Matrix4.LookAt(Vector3.UnitZ, Vector3.Zero, Vector3.UnitY);
            //GL.LoadMatrix(ref mv);

        }


        #endregion

        #region OnClosing
        protected override void OnClosing(CancelEventArgs e)
        {
            //Application.Idle -= Application_Idle;
            base.OnClosing(e);
        }
        #endregion

        float transX = 0;
        float transY = 0;

        #region Render()
        private void Render()
        {
            Matrix4 lookat = Matrix4.LookAt(0, 0, 5, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);
            // Camera
            //EyePosition.Z = 1;
            //Vector3 target = new Vector3(0, 0, 0);
            //GL.MatrixMode(MatrixMode.Modelview);
            //Matrix4 mv = Matrix4.LookAt(EyePosition, target, Vector3.UnitY);
            //GL.LoadMatrix(ref mv);
            //Matrix4 lookat = Matrix4.LookAt(0, 0, -15, 0, 0, 0, 0, 1, 0);
            //GL.MatrixMode(MatrixMode.Modelview);
            //GL.LoadMatrix(ref lookat);

            GL.Translate(transX, transY, CameraZoom);
            GL.Rotate(CameraRotX, Vector3.UnitX);
            GL.Rotate(CameraRotY, Vector3.UnitY);
            GL.Rotate(CameraRotZ, Vector3.UnitZ);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //float[] lightPosition = { 50, 100, 50, 50 };
            //GL.Light(LightName.Light0, LightParameter.Position, lightPosition);
            paintAxis();
            //if (after == 0) paint_List();
            //else 
            paint_List(actual_vertices);
            if (showFaces) paint_Faces();
            if (jarvisbool) paint_path();
            glControl1.SwapBuffers();
            //glControl1.Invalidate();

        }




        #endregion



        #region Mouse
        private void glControl1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Left))
            {
                if (mouseBeforeX < e.X)
                    CameraRotX += e.X / 300;
                else
                    CameraRotX -= e.X / 300;

                mouseBeforeX = e.X;
            }
            if (e.Button.Equals(MouseButtons.Right))
            {
                if (mouseBeforeY < e.Y)
                    CameraRotY += e.Y / 300;
                else
                    CameraRotY -= e.Y / 300;

                mouseBeforeY = e.Y;

            }
            Render();
        }

        private void glControl1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Delta != 0)
                Console.Out.WriteLine(e.Delta);
            if (e.Delta > 0)
                CameraZoom += 1.5f;
            else if (e.Delta < 0)
                CameraZoom -= 1.5f;
            Render();
        }
        #endregion

        #region Screenshot for glControl
        Bitmap GrabScreenshot()
        {
            Bitmap bmp = new Bitmap(this.glControl1.Width, this.glControl1.Height);
            Rectangle rect = new Rectangle(0, 0, this.glControl1.Width, this.glControl1.Height);
            System.Drawing.Imaging.BitmapData data =
            bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly,
            System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            GL.ReadPixels(0, 0, this.glControl1.Width, this.glControl1.Height, PixelFormat.Bgr, PixelType.UnsignedByte,
            data.Scan0);
            bmp.UnlockBits(data);
            bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
            return bmp;
        }

        private void blueButton_Click(object sender, EventArgs e)
        {
            //GL.ClearColor(Color.RoyalBlue);
            glControl1.Invalidate();
        }

        #endregion

        #endregion


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = (3 - upAxis) - chosenViewType.SelectedIndex == 0 ? 1 : 0;
            actual_vertices = actualView.chosenPoints(chosenViewType.SelectedIndex, (float)thirdAxisValue.Value);
            if (chosenViewType.SelectedIndex == 0) { }
            else if (chosenViewType.SelectedIndex == 1) { CameraRotX = 0; CameraRotY = 0; CameraRotZ = -90; thirdAxisValue.Increment = Convert.ToDecimal(laser_param[index]); }
            else if (chosenViewType.SelectedIndex == 2) { CameraRotX = 0; CameraRotY = 90; CameraRotZ = 0; thirdAxisValue.Increment = Convert.ToDecimal(laser_param[index]); }
            else if (chosenViewType.SelectedIndex == 3) { CameraRotX = 90; CameraRotY = 0; CameraRotZ = 0; thirdAxisValue.Increment = Convert.ToDecimal(laser_param[index]); }
            Render();

        }

        private void thirdAxisValue_ValueChanged(object sender, EventArgs e)
        {
            actual_vertices = actualView.chosenPoints(chosenViewType.SelectedIndex, (float)thirdAxisValue.Value);
            Render();
        }

        private void Faces_CheckedChanged(object sender, EventArgs e)
        {
            showFaces = Faces.Checked;
            Render();

        }

        private void points_generator_Click(object sender, EventArgs e)
        {
            List<Vertex> laserPath_byHand;

        }

        private void vertexDensityIncreaser_Click(object sender, EventArgs e)
        {
            //laser_param = { 1.0f, 2.0f, 5.21f, 200000f };
            float model_width = laser_param[0];

            after++;
            LaserPathProducer lpp = new LaserPathProducer(obj, laser_param, model_width, vertices);
            vertices = lpp.getMorePoints(after);
            actualView = new View(vertices);
            actual_vertices = vertices;
            //paint_List();
            //glControl1.SwapBuffers();
            Render();
        }

        private void jarvis_button_Click(object sender, EventArgs e)
        {
            Convex_hull jarvis = new Convex_hull();
            if (up_axis == 0)
            {
                path = jarvis.jarvisButton_X(vertices, laser_param);
            }
            else if (up_axis == 1)
            {
                path = jarvis.jarvisButton_Y(vertices, laser_param);
            }
            else
            {
                path = jarvis.jarvisButton_Z(vertices, laser_param);
            }
            jarvisbool = true;
            Render();
        }

        private void pathButton_Click(object sender, EventArgs e)
        {
            if (jarvisButton.Checked == true)
            {
                Convex_hull jarvis = new Convex_hull();
                if (up_axis == 0)
                {
                    path = jarvis.jarvisButton_X(vertices, laser_param);
                }
                else if (up_axis == 1)
                {
                    path = jarvis.jarvisButton_Y(vertices, laser_param);
                }
                else
                {
                    path = jarvis.jarvisButton_Z(vertices, laser_param);
                }
                jarvisbool = true;
                Render();
            }
            else
            {
                Convex_hull graham = new Convex_hull();
                if (up_axis == 0)
                {
                    //path = graham.grahamButtonZ_Click(vertices, laser_param);
                }
                else if (up_axis == 1)
                {
                    //path = jarvis.jarvisButton_Y(vertices, laser_param);
                }
                else
                {
                    //path = jarvis.jarvisButton_Z(vertices, laser_param);
                }
                //jarvisbool = true;
                //Render();
            }
            if (path != null && path.Count() > 0)
            {
                PathMaker pathMaker = new PathMaker(path, laser_param[2], laser_param[3], upAxis, continuous_laser_pow.Checked);
                double[] time = pathMaker.time_steps();
                int[] power = pathMaker.power_steps();
                dataGrid.Rows.Clear();
                if (up_axis == 0)
                {
                    for (int i = 0; i < time.Count() - 1; i++)
                    {

                        dataGrid.Rows.Add();
                        dataGrid.Rows[i].Cells[0].Value = time[i];
                        dataGrid.Rows[i].Cells[1].Value = path[i].X + laser_param[1];
                        dataGrid.Rows[i].Cells[2].Value = path[i].Y;
                        dataGrid.Rows[i].Cells[3].Value = path[i].Z;
                        dataGrid.Rows[i].Cells[4].Value = power[i];
                    }
                    dataGrid.Rows[time.Count() - 1].Cells[0].Value = time[time.Count() - 1];
                    dataGrid.Rows[time.Count() - 1].Cells[1].Value = path[time.Count() - 2].X + laser_param[1] * 2;
                    dataGrid.Rows[time.Count() - 1].Cells[2].Value = path[time.Count() - 2].Y;
                    dataGrid.Rows[time.Count() - 1].Cells[3].Value = path[time.Count() - 2].Z;
                    dataGrid.Rows[time.Count() - 1].Cells[4].Value = power[time.Count() - 1];
                    tabControl1.SelectTab(0);
                }
                else if (up_axis == 1)
                {
                    for (int i = 0; i < time.Count() - 1; i++)
                    {

                        dataGrid.Rows.Add();
                        dataGrid.Rows[i].Cells[0].Value = time[i];
                        dataGrid.Rows[i].Cells[1].Value = path[i].X;
                        dataGrid.Rows[i].Cells[2].Value = path[i].Y + laser_param[1];
                        dataGrid.Rows[i].Cells[3].Value = path[i].Z;
                        dataGrid.Rows[i].Cells[4].Value = power[i];
                    }
                    dataGrid.Rows[time.Count() - 1].Cells[0].Value = time[time.Count() - 1];
                    dataGrid.Rows[time.Count() - 1].Cells[1].Value = path[time.Count() - 2].X;
                    dataGrid.Rows[time.Count() - 1].Cells[2].Value = path[time.Count() - 2].Y + laser_param[1] * 2;
                    dataGrid.Rows[time.Count() - 1].Cells[3].Value = path[time.Count() - 2].Z;
                    dataGrid.Rows[time.Count() - 1].Cells[4].Value = power[time.Count() - 1];
                    tabControl1.SelectTab(0);
                }
                else
                {
                    for (int i = 0; i < time.Count() - 1; i++)
                    {

                        dataGrid.Rows.Add();
                        dataGrid.Rows[i].Cells[0].Value = time[i];
                        dataGrid.Rows[i].Cells[1].Value = path[i].X;
                        dataGrid.Rows[i].Cells[2].Value = path[i].Y;
                        dataGrid.Rows[i].Cells[3].Value = path[i].Z + laser_param[1];
                        dataGrid.Rows[i].Cells[4].Value = power[i];
                    }
                    dataGrid.Rows[time.Count() - 1].Cells[0].Value = time[time.Count() - 1];
                    dataGrid.Rows[time.Count() - 1].Cells[1].Value = path[time.Count() - 2].X;
                    dataGrid.Rows[time.Count() - 1].Cells[2].Value = path[time.Count() - 2].Y;
                    dataGrid.Rows[time.Count() - 1].Cells[3].Value = path[time.Count() - 2].Z + laser_param[1] * 2;
                    dataGrid.Rows[time.Count() - 1].Cells[4].Value = power[time.Count() - 1];
                    tabControl1.SelectTab(0);
                }
            }
            else
            {
                string messageBoxText = "Path was'nt made! Please check parameters.";
                string caption = "Warning";
                MessageBox.Show(messageBoxText, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void glControl1_Load(object sender, EventArgs e)
        {
            base.OnLoad(e);

            glControl1.KeyDown += new KeyEventHandler(glControl1_KeyDown);
            glControl1.KeyUp += new KeyEventHandler(glControl1_KeyUp);
            glControl1.Resize += new EventHandler(glControl1_Resize);
            glControl1.Paint += new PaintEventHandler(glControl1_Paint);

            //info about Graphic :)
            //Text =
            //    GL.GetString(StringName.Vendor) + " " +
            //    GL.GetString(StringName.Renderer) + " " +
            //    GL.GetString(StringName.Version);

            GL.ClearColor(System.Drawing.Color.White);
            GL.Enable(EnableCap.DepthTest);

            // Ensure that the viewport and projection matrix are set correctly.
            glControl1_Resize(glControl1, EventArgs.Empty);
        }

        private void save_to_file_button_Click(object sender, EventArgs e)
        {
            string lines = ""/*new string[dataGrid.RowCount]*/;
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\Users\hajde\Desktop\WriteLines2.inp"))
            {
                foreach (DataGridViewRow dgvR in dataGrid.Rows)
                {
                    lines = "";
                    for (int j = 0; j < dataGrid.Columns.Count - 1; ++j)
                    {
                        lines += dgvR.Cells[j].Value + ", ";
                    }
                    lines += (int)dgvR.Cells[4].Value;
                    if (lines != null)
                    {
                        file.WriteLine(lines);
                    }
                }

            }
        }



        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        private void view1_Click(object sender, EventArgs e)
        {
            //int index = (3 - upAxis) - chosenViewType.SelectedIndex == 0 ? 1 : 0;
            //actual_vertices = actualView.chosenPoints(chosenViewType.SelectedIndex, (float)thirdAxisValue.Value);

            CameraRotX = 0; CameraRotY = 0; CameraRotZ = 0; //thirdAxisValue.Increment = Convert.ToDecimal(laser_param[index]); }
            //glControl1.Refresh();
            Render();

        }

        private void view2_Click(object sender, EventArgs e)
        {
            CameraRotX = 180; CameraRotY = 0; CameraRotZ = 0; //thirdAxisValue.Increment = Convert.ToDecimal(laser_param[index]); }
            //glControl1.Refresh();
            Render();
        }

        private void view3_Click(object sender, EventArgs e)
        {
            CameraRotX = 0; CameraRotY = 90; CameraRotZ = 0;
            glControl1.Refresh();
            Render();
        }
        private void view4_Click(object sender, EventArgs e)
        {
            CameraRotX = 180; CameraRotY = 90; CameraRotZ = 0;
            glControl1.Refresh();
            Render();
        }

        private void view5_Click(object sender, EventArgs e)
        {
            CameraRotX = 90; CameraRotY = 0; CameraRotZ = 0;
            glControl1.Refresh();
            Render();
        }
        private void view6_Click(object sender, EventArgs e)
        {
            CameraRotX = -90; CameraRotY = 0; CameraRotZ = 0;
            glControl1.Refresh();
            Render();
        }
        private void view7_Click(object sender, EventArgs e)
        {
            CameraRotX = -45; CameraRotY = 30; CameraRotZ = -30;
            glControl1.Refresh();
            Render();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void graham_button_Click(object sender, EventArgs e)
        {

        }

        private void jarvisButton_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            //this.Close();
            //this.ParentForm.Show();
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GL.ClearColor(System.Drawing.Color.Black);
            GL.Enable(EnableCap.DepthTest);
            //System.Drawing.Color color = new System.Drawing.Color();

            //tableLayoutPanel3.BackColor = System.Drawing.Color.DarkGray;
            //int hm = tabControl2.TabPages.Count;
            //foreach(TabPage tab in tabControl1.TabPages)
            //{
            //    tab.BackColor = System.Drawing.Color.DarkGray;
            //}
            //glControl1.BackColor = System.Drawing.Color.DarkGray;
            //glControl1.Refresh();
        }

        private void whiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GL.ClearColor(System.Drawing.Color.White);
            GL.Enable(EnableCap.DepthTest);
            //tableLayoutPanel3.BackColor = System.Drawing.Color.White;
            //int hm = tabControl2.TabPages.Count;
            //foreach (TabPage tab in tabControl1.TabPages)
            //{
            //    tab.BackColor = System.Drawing.Color.White;
            //}
            //glControl1.BackColor = System.Drawing.Color.White;
            glControl1.SwapBuffers();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            up_axis = 1;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            up_axis = 0;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            up_axis = 2;
        }

        private void laser1_ValueChanged(object sender, EventArgs e)
        {
            laser_param[0] = float.Parse(laser1.Value.ToString());
        }

        private void laser2_ValueChanged(object sender, EventArgs e)
        {
            laser_param[1] = float.Parse(laser2.Value.ToString());
        }

        private void laser3_ValueChanged(object sender, EventArgs e)
        {
            laser_param[2] = float.Parse(laser3.Value.ToString());

        }

        private void laser4_ValueChanged(object sender, EventArgs e)
        {
            laser_param[3] = float.Parse(laser4.Value.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            double maxX = vertices.Max(pet => pet.X);
            double minX = vertices.Min(pet => pet.X);
            double maxY = vertices.Max(pet => pet.Y);
            double minY = vertices.Min(pet => pet.Y);
            double maxZ = vertices.Max(pet => pet.Z);
            double minZ = vertices.Min(pet => pet.Z);

            CameraZoom = (float)(5.0 - (maxX - minX) * 2);
            Render();
        }

        private void makepath2_Click(object sender, EventArgs e)
        {
            laser_param[0] = (float)laserW.Value;
            laser_param[1] = (float)laserH.Value;
            laser_param[2] = (float)laserV.Value;
            laser_param[3] = (float)laserP.Value;


            startPoint.X = (double)startX.Value; startPoint.Y = (double)startY.Value; startPoint.Z = (double)startZ.Value;
            if (cont0.Checked == true)
            {
                Lines lines_cont = new Lines();
                if (up_axis == 0)
                   path =  lines_cont.list_X(vertices, laser_param, startPoint, layerDirect, (int)((vertices.Max(p => p.X) + vertices.Min(p => p.X)) / (double)laserH.Value));
                //if (up_axis == 0)
                //{
                //    path = lines_cont.(vertices, laser_param, startPoint);
                //}
                else if (up_axis == 1)
                    path = lines_cont.list_Y(vertices, laser_param, startPoint, layerDirect, (int)((vertices.Max(p => p.Y) + vertices.Min(p => p.Y)) / (double)laserH.Value));

                //{
                //    path = jarvis.jarvisButton_Y(vertices, laser_param);
                //}
                else
                    path = lines_cont.list_Z(vertices, laser_param, startPoint, layerDirect, (int)((vertices.Max(p => p.Z) + vertices.Min(p => p.Z)) / (double)laserH.Value));

                //{
                //    path = jarvis.jarvisButton_Z(vertices, laser_param);
                //}
                jarvisbool = true;
                Render();
            }
            else
            {
                
            }
            if (path != null && path.Count() > 0)
            {
                PathMaker pathMaker = new PathMaker(path, laser_param[2], laser_param[3], up_axis, layerDirect, continuous_laser_pow.Checked);
                double[] time = pathMaker.time_steps();
                int[] power = pathMaker.power_steps();
                dataGrid.Rows.Clear();
                if (up_axis == 0)
                {
                    for (int i = 0; i < time.Count() - 1; i++)
                    {

                        dataGrid.Rows.Add();
                        dataGrid.Rows[i].Cells[0].Value = time[i];
                        dataGrid.Rows[i].Cells[1].Value = path[i].X + laser_param[1];
                        dataGrid.Rows[i].Cells[2].Value = path[i].Y;
                        dataGrid.Rows[i].Cells[3].Value = path[i].Z;
                        dataGrid.Rows[i].Cells[4].Value = power[i];
                    }
                    dataGrid.Rows[time.Count() - 1].Cells[0].Value = time[time.Count() - 1];
                    dataGrid.Rows[time.Count() - 1].Cells[1].Value = path[time.Count() - 2].X + laser_param[1] * 2;
                    dataGrid.Rows[time.Count() - 1].Cells[2].Value = path[time.Count() - 2].Y;
                    dataGrid.Rows[time.Count() - 1].Cells[3].Value = path[time.Count() - 2].Z;
                    dataGrid.Rows[time.Count() - 1].Cells[4].Value = power[time.Count() - 1];
                    tabControl1.SelectTab(0);
                }
                else if (up_axis == 1)
                {
                    for (int i = 0; i < time.Count() - 1; i++)
                    {

                        dataGrid.Rows.Add();
                        dataGrid.Rows[i].Cells[0].Value = time[i];
                        dataGrid.Rows[i].Cells[1].Value = path[i].X;
                        dataGrid.Rows[i].Cells[2].Value = path[i].Y + laser_param[1];
                        dataGrid.Rows[i].Cells[3].Value = path[i].Z;
                        dataGrid.Rows[i].Cells[4].Value = power[i];
                    }
                    dataGrid.Rows[time.Count() - 1].Cells[0].Value = time[time.Count() - 1];
                    dataGrid.Rows[time.Count() - 1].Cells[1].Value = path[time.Count() - 2].X;
                    dataGrid.Rows[time.Count() - 1].Cells[2].Value = path[time.Count() - 2].Y + laser_param[1] * 2;
                    dataGrid.Rows[time.Count() - 1].Cells[3].Value = path[time.Count() - 2].Z;
                    dataGrid.Rows[time.Count() - 1].Cells[4].Value = power[time.Count() - 1];
                    tabControl1.SelectTab(0);
                }
                else
                {
                    for (int i = 0; i < time.Count() - 1; i++)
                    {

                        dataGrid.Rows.Add();
                        dataGrid.Rows[i].Cells[0].Value = time[i];
                        dataGrid.Rows[i].Cells[1].Value = path[i].X;
                        dataGrid.Rows[i].Cells[2].Value = path[i].Y;
                        dataGrid.Rows[i].Cells[3].Value = path[i].Z + laser_param[1];
                        dataGrid.Rows[i].Cells[4].Value = power[i];
                    }
                    dataGrid.Rows[time.Count() - 1].Cells[0].Value = time[time.Count() - 1];
                    dataGrid.Rows[time.Count() - 1].Cells[1].Value = path[time.Count() - 2].X;
                    dataGrid.Rows[time.Count() - 1].Cells[2].Value = path[time.Count() - 2].Y;
                    dataGrid.Rows[time.Count() - 1].Cells[3].Value = path[time.Count() - 2].Z + laser_param[1] * 2;
                    dataGrid.Rows[time.Count() - 1].Cells[4].Value = power[time.Count() - 1];
                    tabControl1.SelectTab(0);
                }
            }
            else
            {
                string messageBoxText = "Path was'nt made! Please check parameters.";
                string caption = "Warning";
                MessageBox.Show(messageBoxText, caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }
        //_________________________________________LINES____________________________________________

        Vector3d startPoint;
        int layerDirect;
        private void dirX_CheckedChanged(object sender, EventArgs e)
        {
            if (growthX.Checked&&dirX.Checked) { MessageBox.Show("ERROR. Select different direction", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); dirX.Checked = false; }

            else
            {

                double minX = vertices.Min(pet => pet.X);
                startX.Value = (decimal)minX;
                laserW.Value = growthZ.Checked ? Convert.ToDecimal((vertices.Max(pet => pet.Y) + Math.Abs(vertices.Min(pet => pet.Y))) / 2) : Convert.ToDecimal((vertices.Max(pet => pet.Z) + Math.Abs(vertices.Min(pet => pet.Z))) / 2);
                layerDirect = 1;
            }
            

        }

        private void dirY_CheckedChanged(object sender, EventArgs e)
        {

            if (growthY.Checked&&dirY.Checked) { MessageBox.Show("ERROR. Select different direction", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); dirY.Checked = false; }
            else
            {

                double minY = vertices.Min(pet => pet.Y);
                startY.Value = (decimal)minY;
                laserW.Value = growthX.Checked ? Convert.ToDecimal((vertices.Max(pet => pet.Z) + Math.Abs(vertices.Min(pet => pet.Z))) / 2) : Convert.ToDecimal((vertices.Max(pet => pet.X) + Math.Abs(vertices.Min(pet => pet.X))) / 2);
                layerDirect = 2;
            }

        }

        private void dirZ_CheckedChanged(object sender, EventArgs e)
        {

            if (growthZ.Checked && dirZ.Checked){ MessageBox.Show("ERROR. Select different direction", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); dirZ.Checked = false; }
            else
            {
                double minZ = vertices.Min(pet => pet.Z);
                startZ.Value = (decimal)minZ;
                laserW.Value = growthX.Checked ? Convert.ToDecimal((vertices.Max(pet => pet.Y) + Math.Abs(vertices.Min(pet => pet.Y))) / 2) : Convert.ToDecimal((vertices.Max(pet => pet.X) + Math.Abs(vertices.Min(pet => pet.X))) / 2);
                layerDirect = 3;
            }

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            double min = vertices.Min(pet => pet.X);
            startX.Value = (decimal)min;
            startY.Value = (decimal)0;
            startZ.Value = (decimal)0;
            if(dirX.Checked) MessageBox.Show("ERROR. Select different direction", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            up_axis = 0;
        }

        private void growthY_CheckedChanged(object sender, EventArgs e)
        {
            double min = vertices.Min(pet => pet.Y);
            startY.Value = (decimal)min;
            startX.Value = (decimal)0;
            startZ.Value = (decimal)0;
            if (dirY.Checked) MessageBox.Show("ERROR. Select different direction", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            up_axis = 1;
        }

        private void growthZ_CheckedChanged(object sender, EventArgs e)
        {
            double min = vertices.Min(pet => pet.Z);
            startZ.Value = (decimal)min;
            startY.Value = (decimal)0;
            startX.Value = (decimal)0;
            if (dirZ.Checked) MessageBox.Show("ERROR. Select different direction", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            up_axis = 2;
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            double minX = vertices.Min(pet => pet.X);
            double minY = vertices.Min(pet => pet.Y);
            double minZ = vertices.Min(pet => pet.Z);
            if (growthX.Checked || dirX.Checked)
                startX.Value = (decimal)minX;
            else
                startX.Value = Convert.ToDecimal(Math.Abs(minX) + (vertices.Max(pet => pet.X) + Math.Abs(minX)) / 2);

            if (growthY.Checked || dirY.Checked)
                startY.Value = (decimal)minY;
            else
                startY.Value = Convert.ToDecimal(Math.Abs(minY) + (vertices.Max(pet => pet.Y) + Math.Abs(minY)) / 2);


            if (growthZ.Checked || dirZ.Checked)
                startZ.Value = (decimal)minZ;
            else
                startZ.Value = Convert.ToDecimal(Math.Abs(minZ) + (vertices.Max(pet => pet.Z) + Math.Abs(minZ)) / 2);

        }
    }
}
