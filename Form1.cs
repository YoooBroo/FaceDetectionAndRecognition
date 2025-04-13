using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Face;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;

namespace FaceDetectionAndRecognition
{
    public partial class Form1 : Form
    {
        private Rectangle lastFace;
        private int faceHoldCounter = 0;
        private const int FaceHoldThreshold = 10; // Hold for 10 frames
        private CascadeClassifier faceDetector;
        private VideoCapture? camera;
        private Mat? frame;
        private Image<Gray, byte>? result;
        private List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        private List<int> labelIds = new List<int>();
        private Dictionary<int, string> labelNameMap = new Dictionary<int, string>();
        private int count = 0;

        public Form1()
        {
            InitializeComponent();

            faceDetector = new CascadeClassifier("haarcascade_frontalface_default.xml");

            try
            {
                string[] labelData = File.ReadAllText("Faces/Faces.txt").Split(',', StringSplitOptions.RemoveEmptyEntries);
                count = Convert.ToInt32(labelData[0]);

                for (int i = 1; i <= count; i++)
                {
                    string facePath = $"Faces/face{i}.bmp";
                    trainingImages.Add(new Image<Gray, byte>(facePath));
                    labelIds.Add(i - 1);
                    labelNameMap[i - 1] = labelData[i];
                }
            }
            catch
            {
                MessageBox.Show("No trained faces found.");
            }
        }

        private void StartCamera_Click(object sender, EventArgs e)
        {
            camera = new VideoCapture();
            Application.Idle += ProcessFrame;
        }

        private void ProcessFrame(object? sender, EventArgs e)
        {
            if (camera == null) return;
            frame = camera.QueryFrame();


            Mat gray = new Mat();
            CvInvoke.CvtColor(frame, gray, ColorConversion.Bgr2Gray);
            Rectangle[] faces = faceDetector.DetectMultiScale(gray, 1.2, 10, Size.Empty);

            if (faces.Length > 0)
            {
                lastFace = faces[0];
                faceHoldCounter = FaceHoldThreshold;
            }
            else if (faceHoldCounter > 0)
            {
                faceHoldCounter--;
            }

            if (faceHoldCounter > 0)
            {
                CvInvoke.Rectangle(frame, lastFace, new MCvScalar(0, 255, 0), 2);
                result = gray.ToImage<Gray, byte>().Copy(lastFace).Resize(100, 100, Inter.Cubic);

          
            }


            foreach (var face in faces)
            {
                CvInvoke.Rectangle(frame, face, new MCvScalar(0, 255, 0), 2);
                result = gray.ToImage<Gray, byte>().Copy(face).Resize(100, 100, Inter.Cubic);

                if (trainingImages.Count > 0)
                {
                    var recognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 100);
                    var imagesVec = new VectorOfMat();
                    foreach (var img in trainingImages)
                        imagesVec.Push(new VectorOfMat(img.Mat));

                    var labelVec = new VectorOfInt(labelIds.ToArray());
                    recognizer.Train(imagesVec, labelVec);
                    var prediction = recognizer.Predict(result);

                    if (prediction.Label != -1 && prediction.Distance < 100)
                    {
                        string name = labelNameMap.ContainsKey(prediction.Label) ? labelNameMap[prediction.Label] : "Unknown";
                        CvInvoke.PutText(frame, name, new Point(face.X - 2, face.Y - 2), FontFace.HersheyComplex, 0.6, new MCvScalar(0, 0, 255));
                    }
                }
            }

            imageBox1.Image = frame.ToImage<Bgr, byte>();
        }

        private void SaveFace_Click(object sender, EventArgs e)
        {
            if (frame == null) return;

            Mat gray = new Mat();
            CvInvoke.CvtColor(frame, gray, ColorConversion.Bgr2Gray);
            Rectangle[] faces = faceDetector.DetectMultiScale(gray, 1.2, 10, Size.Empty);

            foreach (var face in faces)
            {
                result = gray.ToImage<Gray, byte>().Copy(face).Resize(100, 100, Inter.Cubic);
                break;
            }

            if (result != null)
            {
                trainingImages.Add(result);
            }

            labelIds.Add(count);
            labelNameMap[count] = nameTextBox.Text;
            count++;

            Directory.CreateDirectory("Faces");
            File.WriteAllText("Faces/Faces.txt", $"{count},");

            for (int i = 0; i < trainingImages.Count; i++)
            {
                trainingImages[i].Save($"Faces/face{i + 1}.bmp");
                File.AppendAllText("Faces/Faces.txt", labelNameMap[i] + ",");
            }

            MessageBox.Show($"{nameTextBox.Text} added successfully.");
        }
    }
}
