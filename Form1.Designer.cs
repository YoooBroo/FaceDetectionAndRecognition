namespace FaceDetectionAndRecognition
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            imageBox1 = new Emgu.CV.UI.ImageBox();
            button1 = new Button();
            saveButton = new Button();
            nameLabel = new Label();
            nameTextBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)imageBox1).BeginInit();
            SuspendLayout();
            // 
            // imageBox1
            // 
            imageBox1.Location = new Point(46, 40);
            imageBox1.Name = "imageBox1";
            imageBox1.Size = new Size(682, 636);
            imageBox1.TabIndex = 2;
            imageBox1.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(854, 85);
            button1.Name = "button1";
            button1.Size = new Size(305, 87);
            button1.TabIndex = 3;
            button1.Text = "Start Detection And Recognition";
            button1.UseVisualStyleBackColor = true;
            button1.Click += StartCamera_Click;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(865, 455);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(305, 84);
            saveButton.TabIndex = 4;
            saveButton.Text = "Save Face";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += SaveFace_Click;
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new Point(854, 378);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(49, 20);
            nameLabel.TabIndex = 5;
            nameLabel.Text = "Name";
            // 
            // nameTextBox
            // 
            nameTextBox.Location = new Point(924, 375);
            nameTextBox.Name = "nameTextBox";
            nameTextBox.Size = new Size(235, 27);
            nameTextBox.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1288, 732);
            Controls.Add(nameTextBox);
            Controls.Add(nameLabel);
            Controls.Add(saveButton);
            Controls.Add(button1);
            Controls.Add(imageBox1);
            Name = "Form1";
            Text = "Face Detection And Recognition";
            ((System.ComponentModel.ISupportInitialize)imageBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox1;
        private Button button1;
        private Button saveButton;
        private Label nameLabel;
        private TextBox nameTextBox;
    }
}
