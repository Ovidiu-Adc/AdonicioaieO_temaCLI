using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace OpenTK_ControlApp
{
    class Program : GameWindow
    {
        private float _rotationX = 0.0f;
        private float _rotationY = 0.0f;

        private bool _moveLeft = false;
        private bool _moveRight = false;

        public Program() : base(800, 600, new OpenTK.Graphics.GraphicsMode(32, 24, 0, 4))
        {
            Title = "Controlul cubului cu tastatura si mouse-ul";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.1f, 0.1f, 0.1f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Width / (float)Height, 1.0f, 100.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState input = Keyboard.GetState();
            _moveLeft = input.IsKeyDown(Key.A);
            _moveRight = input.IsKeyDown(Key.D);

            MouseState mouse = Mouse.GetState();
            _rotationX = mouse.X / (float)Width * 360.0f;
            _rotationY = mouse.Y / (float)Height * 360.0f;

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 modelView = Matrix4.LookAt(Vector3.UnitZ * 5, Vector3.Zero, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelView);

            GL.Rotate(_rotationX, 0.0f, 1.0f, 0.0f);
            GL.Rotate(_rotationY, 1.0f, 0.0f, 0.0f);

            if (_moveLeft)
            {
                GL.Translate(-0.1f, 0.0f, 0.0f); 
            }
            else if (_moveRight)
            {
                GL.Translate(0.1f, 0.0f, 0.0f); 
            }

            DrawCube();

            SwapBuffers();
        }

        private void DrawCube()
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(1.0f, 0.0f, 0.0f); // Rosu
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.Color3(0.0f, 1.0f, 0.0f); // Verde
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            GL.Color3(0.0f, 0.0f, 1.0f); // Albastru
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            GL.Color3(1.0f, 1.0f, 0.0f); // Galben
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            GL.Color3(1.0f, 0.0f, 1.0f); // Magenta
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            GL.Color3(0.0f, 1.0f, 1.0f); // Cyan
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);

            GL.End();
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
