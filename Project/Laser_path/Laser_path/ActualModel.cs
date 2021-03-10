//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using OpenTK.Graphics.OpenGL;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using ObjParser.Types;
//using ObjParser;
//using Tao.OpenGl;
//using Tao.FreeGlut;
//using OpenTK;

//namespace Laser_path
//{
//    class ActualModel
//    {
        
//           public ActualModel(Obj obj, int upAxis, float[] laser_param)
//            {
//                this.obj = obj;
//                this.upAxis = 5;//upAxis;

//                set_vertices();

//                this.actualView = new View(vertices);
//                this.actual_vertices = vertices;
//                this.laser_param = laser_param;
                
//            }
//            private int upAxis;
//            private List<Vertex> vertices = new List<Vertex>();
//            private List<Vertex> path;
//            private View actualView;
//            private List<Vertex> actual_vertices = new List<Vertex>();

//            private bool showFaces;
//            private Obj obj;
//            private int after = 0;
//            private float[] laser_param = { 2.5f, 2.0f, 5.21f, 200000 };
//            private bool jarvisbool = false;

            
//            private void set_vertices()
//            {
//                vertices.Clear();
//                if (upAxis == 0)
//                {
//                    foreach (Vertex v in obj.VertexList)
//                    {
//                        Vertex vx = new Vertex();
//                        vx.X = v.Y; vx.Y = v.Z; vx.Z = v.X; vx.Index = v.Index;
//                        vertices.Add(vx);
//                    }
//                }
//                else if (upAxis == 1)
//                {
//                    foreach (Vertex v in obj.VertexList)
//                    {
//                        Vertex vx = new Vertex();
//                        vx.X = v.X; vx.Y = v.Z; vx.Z = v.Y; vx.Index = v.Index;
//                        vertices.Add(vx);
//                    }
//                }
//                else
//                {
//                    foreach (Vertex v in obj.VertexList)
//                    {
//                        vertices.Add(v);
//                    }
//                }
//            }
//            private void paintAxis()
//            {
//                GL.Clear(ClearBufferMask.AccumBufferBit);
//                //GL.Enable(EnableCap.LineStipple);
//                GL.LineWidth(0.5f);
//                GL.Begin(BeginMode.LineStripAdjacency);
//                GL.Color3(System.Drawing.Color.Red);
//                GL.Vertex3(0, 0, 0);
//                GL.Vertex3(1, 0, 0);
//                GL.Vertex3(0.9, 0.1, 0);
//                GL.Vertex3(0.9, -0.1, 0);
//                GL.Vertex3(1, 0, 0);
//                GL.Color3(System.Drawing.Color.Green);
//                GL.Vertex3(0, 0, 0);
//                GL.Vertex3(0, 1, 0);
//                GL.Vertex3(0.1, 0.9, 0);
//                GL.Vertex3(-0.1, 0.9, 0);
//                GL.Vertex3(0, 1, 0);
//                GL.Color3(System.Drawing.Color.Blue);
//                GL.Vertex3(0, 0, 0);
//                GL.Vertex3(0, 0, 0.1);
//                GL.End();
//                GL.PointSize(10f);
//                GL.Begin(BeginMode.Points);

//                GL.Color3(System.Drawing.Color.DarkCyan);
//                GL.Vertex3(0, 0, 0);
//                GL.End();


//            }

//            private void paint_List()
//            {

//                GL.Color3(System.Drawing.Color.Black);
//                GL.PointSize(5.0f);
//                GL.Begin(BeginMode.Points);
//                foreach (Vertex ver in vertices)
//                {
//                    float[] vs = { (float)ver.X / 100, (float)ver.Y / 100, (float)ver.Z / 100 };
//                    GL.Vertex3(vs);
//                }
//                GL.End();
//            }
//            private void paint_Faces()
//            {
//                //GL.Color3(System.Drawing.Color.LightGray);
//                //GL.Begin(BeginMode.Lines);
//                //Random rand = new Random();
//                //Random rand2 = new Random();
//                //Random rand3 = new Random();
//                double selected_axis = 0;
//                GL.Color3(System.Drawing.Color.Gray);



//                foreach (Face ver in obj.FaceList)
//                {
//                    //Random rand = new Random();
//                    //Random rand2 = new Random();
//                    //Random rand3 = new Random();
//                    //GL.Color3(System.Drawing.Color.FromArgb( rand.Next(255), rand2.Next(255), rand3.Next(255))/*System.Drawing.Color.LightGray*/);
//                    //GL.Begin(BeginMode.Triangles);
//                    GL.Begin(BeginMode.Lines);
//                    if (ver.VertexIndexList.Any(x => x >= obj.FaceList.Count))
//                    {
//                    }
//                    else
//                    {
//                        for (int i = 0; i < ver.VertexIndexList.Count(); i++)
//                        {
//                            if (chosenViewType.SelectedIndex == 0)
//                            {
//                                float[] point1 = { (float)obj.VertexList[ver.VertexIndexList[i] - 1].X / 100, (float)obj.VertexList[ver.VertexIndexList[i] - 1].Y / 100, (float)obj.VertexList[ver.VertexIndexList[i] - 1].Z / 100 };
//                                GL.Vertex3(point1);
//                            }
//                            else if (chosenViewType.SelectedIndex == 1)
//                            {
//                                if ((int)obj.VertexList[ver.VertexIndexList[i] - 1].Z <= (int)thirdAxisValue.Value)
//                                {
//                                    float[] point1 = { (float)obj.VertexList[ver.VertexIndexList[i] - 1].X / 100, (float)obj.VertexList[ver.VertexIndexList[i] - 1].Y / 100, (float)obj.VertexList[ver.VertexIndexList[i] - 1].Z / 100 };
//                                    GL.Vertex3(point1);
//                                }
//                            }
//                            else if (chosenViewType.SelectedIndex == 2)
//                            {
//                                if (obj.VertexList[ver.VertexIndexList[i] - 1].Y == (int)thirdAxisValue.Value)
//                                {
//                                    float[] point1 = { (float)obj.VertexList[ver.VertexIndexList[i] - 1].X / 100, (float)obj.VertexList[ver.VertexIndexList[i] - 1].Y / 100, (float)obj.VertexList[ver.VertexIndexList[i] - 1].Z / 100 };
//                                    GL.Vertex3(point1);
//                                }
//                            }
//                            else if (chosenViewType.SelectedIndex == 3)
//                            {
//                                if (obj.VertexList[ver.VertexIndexList[i] - 1].X == (int)thirdAxisValue.Value)
//                                {
//                                    float[] point1 = { (float)obj.VertexList[ver.VertexIndexList[i] - 1].X / 100, (float)obj.VertexList[ver.VertexIndexList[i] - 1].Y / 100, (float)obj.VertexList[ver.VertexIndexList[i] - 1].Z / 100 };
//                                    GL.Vertex3(point1);
//                                }
//                            }
//                        }
//                    }
//                    //else { 
//                    //float[] point1 = { (float)obj.VertexList[ver.TextureVertexIndexList[0]-1].X / 100, (float)obj.VertexList[ver.TextureVertexIndexList[0]-1].Y / 100, (float)obj.VertexList[ver.TextureVertexIndexList[0]-1].Z / 100 };
//                    //float[] point2 = { (float)obj.VertexList[ver.TextureVertexIndexList[1]-1].X / 100, (float)obj.VertexList[ver.TextureVertexIndexList[1]-1].Y / 100, (float)obj.VertexList[ver.TextureVertexIndexList[1]-1].Z / 100 };
//                    //float[] point3 = { (float)obj.VertexList[ver.TextureVertexIndexList[2]-1].X / 100, (float)obj.VertexList[ver.TextureVertexIndexList[2]-1].Y / 100, (float)obj.VertexList[ver.TextureVertexIndexList[2]-1].Z / 100 };
//                    //GL.Vertex3(point1);
//                    //GL.Vertex3(point2);
//                    //GL.Vertex3(point3);
//                    //    }
//                    GL.End();

//                }
//                //GL.End();
//            }
//            private void paint_path()
//            {
//                double selected_axis = 0;
//                GL.Color3(System.Drawing.Color.Red);
//                GL.Begin(BeginMode.LineLoop);

//                foreach (Vertex ver in path)
//                {
//                    if (ver != null)
//                    {
//                        float[] point1 = { (float)ver.X / 100, (float)ver.Y / 100, (float)ver.Z / 100, 0 };
//                        GL.Vertex3(point1);
//                    }
//                }
//                GL.End();
//            }
//            private void paint_List(List<Vertex> vertices)
//            {
//                GL.Clear(ClearBufferMask.AccumBufferBit);

//                //if (after == 0)
//                //{
//                foreach (Vertex ver in vertices)
//                {
//                    if (ver.Index >= obj.VertexList.Count)
//                    {
//                        GL.Color3(System.Drawing.Color.LightGray);
//                        GL.PointSize(3.0f);
//                        GL.Begin(BeginMode.Points);
//                    }
//                    else
//                    {
//                        GL.Color3(System.Drawing.Color.Black);
//                        GL.PointSize(3.0f);
//                        GL.Begin(BeginMode.Points);
//                    }


//                    float[] vs = { (float)ver.X / 100, (float)ver.Y / 100, (float)ver.Z / 100 };
//                    GL.Vertex3(vs);

//                }
//                GL.End();
//                //}
//                //else
//                //{
//                //    GL.Color3(System.Drawing.Color.LightGray);
//                //            GL.PointSize(3.0f);
//                //            GL.Begin(BeginMode.Points);
//                //    for (int i =0; i<vertices.Count; i++)
//                //    {
//                //        float[] vs = { (float)vertices[i].X / 100, (float)vertices[i].Y / 100, (float)vertices[i].Z / 100 };
//                //        GL.Vertex3(vs);
//                //    }
//                //    GL.End();
//                //}

//            }

//            #region GLUT Events
//            private int mouseBeforeX = 0;
//            private int mouseBeforeY = 0;
//            public float CameraZoom { get; private set; }
//            public float CameraRotX { get; private set; }
//            public float CameraRotY { get; private set; }
//            public float CameraRotZ { get; private set; }
//            public int[,,] index { get; private set; }

//            Vector3 EyePosition = new Vector3(0f, 0f, 15);
//            Vector2 CameraPosition = new Vector2(-10, -10);

//            #region OnLoad
//            protected override void OnLoad(EventArgs e)
//            {
//                //base.OnLoad(e);

//                //glControl1.KeyDown += new KeyEventHandler(glControl1_KeyDown);
//                //glControl1.KeyUp += new KeyEventHandler(glControl1_KeyUp);
//                //glControl1.Resize += new EventHandler(glControl1_Resize);
//                //glControl1.Paint += new PaintEventHandler(glControl1_Paint);

//                ////info about Graphic :)
//                ////Text =
//                ////    GL.GetString(StringName.Vendor) + " " +
//                ////    GL.GetString(StringName.Renderer) + " " +
//                ////    GL.GetString(StringName.Version);

//                //GL.ClearColor(System.Drawing.Color.White);
//                //GL.Enable(EnableCap.DepthTest);

//                //// Ensure that the viewport and projection matrix are set correctly.
//                //glControl1_Resize(glControl1, EventArgs.Empty);
//            }
//            #endregion

//            #region glControl Paint
//            private void glControl1_Paint(object sender, PaintEventArgs e)
//            {
//                GL.Light(LightName.Light0, LightParameter.Diffuse, System.Drawing.Color.Black);
//                GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, 5);

//                Render();
//            }
//            #endregion

//            #region glControl Resize
//            private void glControl1_Resize(object sender, EventArgs e)
//            {
//                OpenTK.GLControl c = sender as OpenTK.GLControl;

//                if (c.ClientSize.Height == 0)
//                    c.ClientSize = new System.Drawing.Size(c.ClientSize.Width, 1);

//                GL.Viewport(0, 0, c.ClientSize.Width, c.ClientSize.Height);

//                float aspect_ratio = Width / (float)Height;
//                GL.MatrixMode(MatrixMode.Projection);
//                Matrix4 perpective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 0.1f, 100.0f);
//                GL.LoadMatrix(ref perpective);

//                GL.MatrixMode(MatrixMode.Modelview);
//                Matrix4 mv = Matrix4.LookAt(Vector3.UnitZ, Vector3.Zero, Vector3.UnitY);
//                GL.LoadMatrix(ref mv);

//            }
//            #endregion

//            #region OnClosing
//            protected override void OnClosing(CancelEventArgs e)
//            {
//                base.OnClosing(e);
//            }
//            #endregion
//            float transX = 0;
//            float transY = 0;
//            #region Render()
//            private void Render()
//            {
//                //Matrix4 lookat = Matrix4.LookAt(0, 1, 1, 0, 0, 0, 0, 1, 0);
//                //GL.MatrixMode(MatrixMode.Modelview);
//                //GL.LoadMatrix(ref lookat);
//                // Camera
//                EyePosition.Z = 1;
//                Vector3 target = new Vector3(0, 0, 0);
//                GL.MatrixMode(MatrixMode.Modelview);
//                Matrix4 mv = Matrix4.LookAt(EyePosition, target, Vector3.UnitY);
//                GL.LoadMatrix(ref mv);

//                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
//                GL.Translate(transX, transY, CameraZoom);
//                GL.Rotate(CameraRotX, Vector3.UnitY);
//                GL.Rotate(CameraRotY, Vector3.UnitX);
//                GL.Rotate(CameraRotZ, Vector3.UnitZ);
//                //float[] lightPosition = { 50, 100, 50, 50 };
//                //GL.Light(LightName.Light0, LightParameter.Position, lightPosition);
//                paintAxis();
//                //if (after == 0) paint_List();
//                //else 
//                paint_List(actual_vertices);
//                if (showFaces) paint_Faces();
//                if (jarvisbool) paint_path();
//                glControl1.SwapBuffers();
//            }

//            #endregion

//            #region KeysEvents
//            private void glControl1_KeyDown(object sender, KeyEventArgs e)
//            {
//                if (e.KeyData == Keys.Escape)
//                {
//                    //this.Close();
//                }
//                else if (e.KeyData == Keys.NumPad8)
//                {
//                    transY += 0.1f;
//                }
//                else if (e.KeyData == Keys.NumPad2)
//                {
//                    transY -= 0.1f;
//                }
//                else if (e.KeyData == Keys.NumPad4)
//                {
//                    transX -= 0.1f;
//                }
//                else if (e.KeyData == Keys.NumPad6)
//                {
//                    transX += 0.1f;
//                }
//                else if (e.KeyData == Keys.Add)
//                {
//                    CameraZoom += 0.1f;
//                }
//                else if (e.KeyData == Keys.Subtract)
//                {
//                    CameraZoom -= 0.1f;
//                }
//                Render();
//            }

//            private void glControl1_KeyUp(object sender, KeyEventArgs e)
//            {
//                if (e.KeyCode == Keys.F12)
//                {
//                    SaveFileDialog saveFileDialog1 = new SaveFileDialog();
//                    saveFileDialog1.Filter = "bmp File|*.bmp";
//                    saveFileDialog1.Title = "Zapisz obraz";
//                    saveFileDialog1.ShowDialog();

//                    // If the file name is not an empty string open it for saving.
//                    if (saveFileDialog1.FileName != "")
//                        GrabScreenshot().Save(saveFileDialog1.FileName);
//                }
//                else if (e.KeyCode == Keys.S)
//                {
//                    CameraRotY -= 1f;
//                }
//                else if (e.KeyCode == Keys.W)
//                {
//                    CameraRotY += 1f;
//                }
//                else if (e.KeyCode == Keys.A)
//                {
//                    CameraRotX += 1f;
//                }
//                else if (e.KeyCode == Keys.D)
//                {
//                    CameraRotX -= 1f;
//                }
//                else if (e.KeyCode == Keys.Q)
//                {
//                    CameraZoom -= 0.1f;
//                }
//                else if (e.KeyCode == Keys.E)
//                {
//                    CameraZoom += 0.1f;
//                }
//                //else if (e.KeyCode == Keys.Up)
//                //{
//                //    transY += 0.1f;
//                //}
//                //else if (e.KeyCode == Keys.Down)
//                //{
//                //    transY -= 0.1f;
//                //}
//                //else if (e.KeyCode == Keys.Left)
//                //{
//                //    transX -= 0.1f;
//                //}
//                //else if (e.KeyCode == Keys.Right)
//                //{
//                //    transX += 0.1f;
//                //}
//                Render();
//            }
//            #endregion

//            #region Mouse
//            private void glControl1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
//            {
//                if (e.Button.Equals(MouseButtons.Left))
//                {
//                    if (mouseBeforeX < e.X)
//                        CameraRotX += e.X / 300;
//                    else
//                        CameraRotX -= e.X / 300;

//                    mouseBeforeX = e.X;
//                }
//                if (e.Button.Equals(MouseButtons.Right))
//                {
//                    if (mouseBeforeY < e.Y)
//                        CameraRotY += e.Y / 300;
//                    else
//                        CameraRotY -= e.Y / 300;

//                    mouseBeforeY = e.Y;

//                }
//                Render();
//            }

//            private void glControl1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
//            {
//                if (e.Delta != 0)
//                    Console.Out.WriteLine(e.Delta);
//                if (e.Delta > 0)
//                    CameraZoom += 1.5f;
//                else if (e.Delta < 0)
//                    CameraZoom -= 1.5f;
//                Render();
//            }
//            #endregion

//            #region Screenshot for glControl
//            Bitmap GrabScreenshot()
//            {
//                Bitmap bmp = new Bitmap(this.glControl1.Width, this.glControl1.Height);
//                Rectangle rect = new Rectangle(0, 0, this.glControl1.Width, this.glControl1.Height);
//                System.Drawing.Imaging.BitmapData data =
//                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.WriteOnly,
//                System.Drawing.Imaging.PixelFormat.Format24bppRgb);
//                GL.ReadPixels(0, 0, this.glControl1.Width, this.glControl1.Height, PixelFormat.Bgr, PixelType.UnsignedByte,
//                data.Scan0);
//                bmp.UnlockBits(data);
//                bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
//                return bmp;
//            }

//            private void blueButton_Click(object sender, EventArgs e)
//            {
//                //GL.ClearColor(Color.RoyalBlue);
//                glControl1.Invalidate();
//            }

//            #endregion

//            #endregion


//            private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
//            {
//                int index = (3 - upAxis) - chosenViewType.SelectedIndex == 0 ? 1 : 0;
//                actual_vertices = actualView.chosenPoints(chosenViewType.SelectedIndex, (float)thirdAxisValue.Value);
//                if (chosenViewType.SelectedIndex == 0) { }
//                else if (chosenViewType.SelectedIndex == 1) { CameraRotX = 0; CameraRotY = 0; CameraRotZ = -90; thirdAxisValue.Increment = Convert.ToDecimal(laser_param[index]); }
//                else if (chosenViewType.SelectedIndex == 2) { CameraRotX = 0; CameraRotY = 90; CameraRotZ = 0; thirdAxisValue.Increment = Convert.ToDecimal(laser_param[index]); }
//                else if (chosenViewType.SelectedIndex == 3) { CameraRotX = 90; CameraRotY = 0; CameraRotZ = 0; thirdAxisValue.Increment = Convert.ToDecimal(laser_param[index]); }
//                Render();

//            }

//            private void thirdAxisValue_ValueChanged(object sender, EventArgs e)
//            {
//                actual_vertices = actualView.chosenPoints(chosenViewType.SelectedIndex, (float)thirdAxisValue.Value);
//                Render();
//            }

//            private void Faces_CheckedChanged(object sender, EventArgs e)
//            {
//                showFaces = Faces.Checked;
//                Render();

//            }

//            private void points_generator_Click(object sender, EventArgs e)
//            {
//                List<Vertex> laserPath_byHand;

//            }

//            private void vertexDensityIncreaser_Click(object sender, EventArgs e)
//            {
//                //laser_param = { 1.0f, 2.0f, 5.21f, 200000f };
//                float model_width = laser_param[0];

//                after++;
//                LaserPathProducer lpp = new LaserPathProducer(obj, laser_param, model_width, vertices);
//                vertices = lpp.getMorePoints(after);
//                actualView = new View(vertices);
//                actual_vertices = vertices;
//                //paint_List();
//                //glControl1.SwapBuffers();
//                Render();
//            }

//            private void jarvis_button_Click(object sender, EventArgs e)
//            {
//                Convex_hull jarvis = new Convex_hull();
//                path = jarvis.jarvisButton_Click(vertices, laser_param);
//                jarvisbool = true;
//                Render();
//            }

//            private void pathButton_Click(object sender, EventArgs e)
//            {
//                PathMaker pathMaker = new PathMaker(path, laser_param[2], laser_param[3]);
//                double[] time = pathMaker.time_steps();
//                int[] power = pathMaker.power_steps();
//                dataGrid.Rows.Clear();
//                for (int i = 0; i < time.Count() - 1; i++)
//                {
//                    dataGrid.Rows.Add();
//                    dataGrid.Rows[i].Cells[0].Value = time[i];
//                    dataGrid.Rows[i].Cells[1].Value = path[i].X;
//                    dataGrid.Rows[i].Cells[2].Value = path[i].Y;
//                    dataGrid.Rows[i].Cells[3].Value = path[i].Z + laser_param[1];
//                    dataGrid.Rows[i].Cells[4].Value = power[i];
//                }
//                dataGrid.Rows[time.Count() - 1].Cells[0].Value = time[time.Count() - 1];
//                dataGrid.Rows[time.Count() - 1].Cells[1].Value = path[time.Count() - 2].X;
//                dataGrid.Rows[time.Count() - 1].Cells[2].Value = path[time.Count() - 2].Y;
//                dataGrid.Rows[time.Count() - 1].Cells[3].Value = path[time.Count() - 2].Z + 100;
//                dataGrid.Rows[time.Count() - 1].Cells[4].Value = power[time.Count() - 1];
//                tabControl1.SelectTab(0);
//            }

//            private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
//            {

//            }

//            private void glControl1_Load(object sender, EventArgs e)
//            {
//                base.OnLoad(e);

//                glControl1.KeyDown += new KeyEventHandler(glControl1_KeyDown);
//                glControl1.KeyUp += new KeyEventHandler(glControl1_KeyUp);
//                glControl1.Resize += new EventHandler(glControl1_Resize);
//                glControl1.Paint += new PaintEventHandler(glControl1_Paint);

//                //info about Graphic :)
//                //Text =
//                //    GL.GetString(StringName.Vendor) + " " +
//                //    GL.GetString(StringName.Renderer) + " " +
//                //    GL.GetString(StringName.Version);

//                GL.ClearColor(System.Drawing.Color.White);
//                GL.Enable(EnableCap.DepthTest);

//                // Ensure that the viewport and projection matrix are set correctly.
//                glControl1_Resize(glControl1, EventArgs.Empty);
//            }

//            private void save_to_file_button_Click(object sender, EventArgs e)
//            {
//                string lines = ""/*new string[dataGrid.RowCount]*/;
//                System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
//                customCulture.NumberFormat.NumberDecimalSeparator = ".";
//                System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
//                using (System.IO.StreamWriter file =
//                new System.IO.StreamWriter(@"C:\Users\hajde\Desktop\WriteLines2.inp"))
//                {
//                    foreach (DataGridViewRow dgvR in dataGrid.Rows)
//                    {
//                        lines = "";
//                        for (int j = 0; j < dataGrid.Columns.Count - 1; ++j)
//                        {
//                            lines += dgvR.Cells[j].Value + ", ";
//                        }
//                        lines += (int)dgvR.Cells[4].Value;
//                        if (lines != null)
//                        {
//                            file.WriteLine(lines);
//                        }
//                    }

//                }
//            }



//            private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
//            {

//            }
//            private void view1_Click(object sender, EventArgs e)
//            {
//                int index = (3 - upAxis) - chosenViewType.SelectedIndex == 0 ? 1 : 0;
//                actual_vertices = actualView.chosenPoints(chosenViewType.SelectedIndex, (float)thirdAxisValue.Value);

//                CameraRotX = 0; CameraRotY = 0; CameraRotZ = 0; //thirdAxisValue.Increment = Convert.ToDecimal(laser_param[index]); }
//                Render();

//            }

//            private void view2_Click(object sender, EventArgs e)
//            {
//                CameraRotX = 180; CameraRotY = 0; CameraRotZ = 0; //thirdAxisValue.Increment = Convert.ToDecimal(laser_param[index]); }
//                Render();
//            }

//            private void view3_Click(object sender, EventArgs e)
//            {
//                CameraRotX = 0; CameraRotY = 90; CameraRotZ = 0;
//                Render();
//            }
//            private void view4_Click(object sender, EventArgs e)
//            {
//                CameraRotX = 180; CameraRotY = 90; CameraRotZ = 0;
//                Render();
//            }

//            private void view5_Click(object sender, EventArgs e)
//            {
//                CameraRotX = 90; CameraRotY = 0; CameraRotZ = 0;
//                Render();
//            }
//            private void view6_Click(object sender, EventArgs e)
//            {
//                CameraRotX = -90; CameraRotY = 0; CameraRotZ = 0;
//                Render();
//            }
//            private void view7_Click(object sender, EventArgs e)
//            {
//                CameraRotX = -45; CameraRotY = 30; CameraRotZ = -30;
//                Render();
//            }

//            private void button2_Click(object sender, EventArgs e)
//            {

//            }

//            private void button3_Click(object sender, EventArgs e)
//            {

//            }
//        }
//    }

//}
//}
