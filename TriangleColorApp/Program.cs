using System;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace TriangleColorApp
{
    class Program : GameWindow
    {
        private Vector3[] vertices;
        private float[] colors = { 1.0f, 0.0f, 0.0f, 1.0f }; // Initial rosu
        private float angle = 0.0f;

        public Program() : base(800, 600, new OpenTK.Graphics.GraphicsMode(32, 24, 0, 4))
        {
            Title = "Modificare Culoare si Unghiul Triunghiului";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.1f, 0.1f, 0.1f, 0.0f);
            GL.Enable(EnableCap.DepthTest);

            // Setare matrice pentru a vizualiza triunghiul
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Width / (float)Height, 1.0f, 100.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            LoadTriangleCoordinates("triangle.txt");
        }

        private void LoadTriangleCoordinates(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            vertices = new Vector3[3];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(' ');
                vertices[i] = new Vector3(float.Parse(parts[0]), float.Parse(parts[1]), float.Parse(parts[2]));
            }
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            KeyboardState input = Keyboard.GetState();

            // Modificare canal roșu (tastele R si T)
            if (input.IsKeyDown(Key.R))
                colors[0] = Math.Min(colors[0] + 0.01f, 1.0f);
            if (input.IsKeyDown(Key.T))
                colors[0] = Math.Max(colors[0] - 0.01f, 0.0f);

            // Modificare canal verde (tastele G si H)
            if (input.IsKeyDown(Key.G))
                colors[1] = Math.Min(colors[1] + 0.01f, 1.0f);
            if (input.IsKeyDown(Key.H))
                colors[1] = Math.Max(colors[1] - 0.01f, 0.0f);

            // Modificare canal albastru (tastele B si N)
            if (input.IsKeyDown(Key.B))
                colors[2] = Math.Min(colors[2] + 0.01f, 1.0f);
            if (input.IsKeyDown(Key.N))
                colors[2] = Math.Max(colors[2] - 0.01f, 0.0f);

            // Modificare unghi camera cu mouse
            MouseState mouse = Mouse.GetState();
            angle = mouse.X / (float)Width * 360.0f;

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Setare matrice
            Matrix4 modelView = Matrix4.LookAt(Vector3.UnitZ * 5, Vector3.Zero, Vector3.UnitY); // Camera plasata pe axa Z,spre origine
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelView);

            GL.Rotate(angle, 0.0f, 1.0f, 0.0f);

            // Setare culoare triunghi
            GL.Color4(colors[0], colors[1], colors[2], colors[3]);

            // Desenare triunghi
            GL.Begin(PrimitiveType.Triangles);
            foreach (var vertex in vertices)
            {
                GL.Vertex3(vertex);
            }
            GL.End();

            SwapBuffers();
        }

        static void Main(string[] args)
        {
            using (Program example = new Program())
            {
                example.Run(60.0);
            }
        }
    }
}
