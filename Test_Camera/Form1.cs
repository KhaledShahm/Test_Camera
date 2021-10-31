using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using OpenCvSharp;


namespace Test_Camera
{
    public partial class Form1 : Form
    {
        IntPtr PCamStream;
        IntPtr frameptr = new IntPtr();
        Mat img;
        public Form1()
        {
            InitializeComponent();
        }

        private const string DllFilePath = @"Cam_Stream.dll";

        [DllImport(DllFilePath, CallingConvention = CallingConvention.Cdecl)]
        static public extern IntPtr CreateCam();

        [DllImport(DllFilePath, CallingConvention = CallingConvention.Cdecl)]
        static public extern void Start(IntPtr CreateCam);

        [DllImport(DllFilePath, CallingConvention = CallingConvention.Cdecl)]
        static public extern IntPtr readFrame(IntPtr CreateCam);

        [DllImport(DllFilePath, CallingConvention = CallingConvention.Cdecl)]
        static public extern void Stop(IntPtr CreateCam);

        private void button1_Click(object sender, EventArgs e)
        {
            PCamStream = CreateCam();
            Start(PCamStream);
            frameptr = readFrame(PCamStream);
            img = new Mat(frameptr);
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stop(PCamStream);
        }

        public static Bitmap MatToBitmap(Mat image)
        {
            return OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (img != null)
            {
                try
                {
                    Cv2.ImShow("21", img);
                    Cv2.WaitKey(0);
                    //Console.WriteLine(frameptr);
                    //img = new Mat(frameptr);
                    //int x = int.Parse("3.5");

                    //pictureBox1.Image = MatToBitmap(img);
                    //pictureBox1.Refresh();

                    img = null;

                }
                catch
                {

                }
            }
        }
    }
}
