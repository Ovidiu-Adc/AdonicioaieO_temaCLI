using System;
using System.IO;
using System.Globalization;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace CubeApp
{
    class Program : GameWindow
    {
        private Vector3[] vertices = new Vector3[8];
        private Vector4[] faceColors = new Vector4[6];
        private int currentFace = 0;
        private float rotationX = 0.0f;
        private float rotationY = 0.0f;
        private bool[] keysPressed = new bool[4]; 

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.1f, 0.1f, 0.1f, 0.0f);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            //perspectiva
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Width / (float)Height, 1.0f, 100.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            //initializarea culorilor pentru fiecare fata a cubului
            for (int i = 0; i < faceColors.Length; i++)
            {
                faceColors[i] = new Vector4(1.0f, 0.0f, 0.0f, 1.0f); //rosu
            }

            LoadCube("cube.txt");
        }

        private void LoadCube(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(' ');
                vertices[i] = new Vector3(
                    float.Parse(parts[0], CultureInfo.InvariantCulture),
                    float.Parse(parts[1], CultureInfo.InvariantCulture),
                    float.Parse(parts[2], CultureInfo.InvariantCulture)
                );
            }
        }

        private void PrintColorToConsole(int faceIndex)
        {
            Vector4 color = faceColors[faceIndex];
            Console.WriteLine($"Fata {faceIndex + 1}: R={color.X:F2}, G={color.Y:F2}, B={color.Z:F2}, A={color.W:F2}");
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            KeyboardState input = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            //coordonatele mouse-ului
            rotationX = (mouse.Y / (float)Height) * 180.0f - 90.0f;
            rotationY = (mouse.X / (float)Width) * 360.0f;

            //verificarea tastelor
            if (input.IsKeyDown(Key.R)) keysPressed[0] = true; // R
            else if (input.IsKeyUp(Key.R)) keysPressed[0] = false;

            if (input.IsKeyDown(Key.G)) keysPressed[1] = true; // G
            else if (input.IsKeyUp(Key.G)) keysPressed[1] = false;

            if (input.IsKeyDown(Key.B)) keysPressed[2] = true; // B
            else if (input.IsKeyUp(Key.B)) keysPressed[2] = false;

            if (input.IsKeyDown(Key.T)) keysPressed[3] = true; // T
            else if (input.IsKeyUp(Key.T)) keysPressed[3] = false;

            //schimbarea culoarei pentru suprafata selectata
            if (keysPressed[0])
            {
                faceColors[currentFace].X = Math.Min(faceColors[currentFace].X + 0.01f, 1.0f); //rosu
                PrintColorToConsole(currentFace);
            }
            if (keysPressed[1])
            {
                faceColors[currentFace].Y = Math.Min(faceColors[currentFace].Y + 0.01f, 1.0f); //verde
                PrintColorToConsole(currentFace);
            }
            if (keysPressed[2])
            {
                faceColors[currentFace].Z = Math.Min(faceColors[currentFace].Z + 0.01f, 1.0f); //albastru
                PrintColorToConsole(currentFace);
            }
            if (keysPressed[3])
            {
                faceColors[currentFace].W = Math.Max(faceColors[currentFace].W - 0.01f, 0.0f); //transparent
                PrintColorToConsole(currentFace);
            }

            //resetare transparenta
            if (faceColors[currentFace].W <= 0)
            {
                faceColors[currentFace].W = 1.0f;
            }

            //fata selectata
            if (input.IsKeyDown(Key.Number1)) currentFace = 0;
            if (input.IsKeyDown(Key.Number2)) currentFace = 1;
            if (input.IsKeyDown(Key.Number3)) currentFace = 2;
            if (input.IsKeyDown(Key.Number4)) currentFace = 3;
            if (input.IsKeyDown(Key.Number5)) currentFace = 4;
            if (input.IsKeyDown(Key.Number6)) currentFace = 5;

            if (input.IsKeyDown(Key.Escape)) Exit();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 modelView = Matrix4.LookAt(new Vector3(0, 0, 5), Vector3.Zero, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelView);

            GL.PushMatrix();
            GL.Rotate(rotationX, 1.0f, 0.0f, 0.0f);
            GL.Rotate(rotationY, 0.0f, 1.0f, 0.0f);

            DrawCube();
            GL.PopMatrix();

            SwapBuffers();
        }

        private void DrawCube()
        {
            GL.Begin(PrimitiveType.Quads);

            //fata
            GL.Color4(faceColors[0].X, faceColors[0].Y, faceColors[0].Z, faceColors[0].W);
            GL.Vertex3(vertices[0]); GL.Vertex3(vertices[1]); GL.Vertex3(vertices[2]); GL.Vertex3(vertices[3]);

            //spate
            GL.Color4(faceColors[1].X, faceColors[1].Y, faceColors[1].Z, faceColors[1].W);
            GL.Vertex3(vertices[4]); GL.Vertex3(vertices[5]); GL.Vertex3(vertices[6]); GL.Vertex3(vertices[7]);

            //stanga
            GL.Color4(faceColors[2].X, faceColors[2].Y, faceColors[2].Z, faceColors[2].W);
            GL.Vertex3(vertices[0]); GL.Vertex3(vertices[3]); GL.Vertex3(vertices[7]); GL.Vertex3(vertices[4]);

            //dreapta
            GL.Color4(faceColors[3].X, faceColors[3].Y, faceColors[3].Z, faceColors[3].W);
            GL.Vertex3(vertices[1]); GL.Vertex3(vertices[2]); GL.Vertex3(vertices[6]); GL.Vertex3(vertices[5]);

            //sus
            GL.Color4(faceColors[4].X, faceColors[4].Y, faceColors[4].Z, faceColors[4].W);
            GL.Vertex3(vertices[2]); GL.Vertex3(vertices[3]); GL.Vertex3(vertices[7]); GL.Vertex3(vertices[6]);

            //jos
            GL.Color4(faceColors[5].X, faceColors[5].Y, faceColors[5].Z, faceColors[5].W);
            GL.Vertex3(vertices[0]); GL.Vertex3(vertices[1]); GL.Vertex3(vertices[5]); GL.Vertex3(vertices[4]);

            GL.End();
        }

        static void Main()
        {
            using (Program program = new Program())
            {
                program.Run(60.0);
            }
        }
    }
}
