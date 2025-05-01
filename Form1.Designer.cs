namespace Nuwton
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
            LoadDataButton = new Button();
            SolveButton = new Button();
            listBox1 = new ListBox();
            ClearButton = new Button();
            SuspendLayout();
            // 
            // LoadDataButton
            // 
            LoadDataButton.Location = new Point(14, 303);
            LoadDataButton.Name = "LoadDataButton";
            LoadDataButton.Size = new Size(75, 23);
            LoadDataButton.TabIndex = 0;
            LoadDataButton.Text = "LoadData";
            LoadDataButton.UseVisualStyleBackColor = true;
            LoadDataButton.Click += LoadDataButton_Click;
            // 
            // SolveButton
            // 
            SolveButton.Location = new Point(206, 303);
            SolveButton.Name = "SolveButton";
            SolveButton.Size = new Size(75, 23);
            SolveButton.TabIndex = 1;
            SolveButton.Text = "Solve";
            SolveButton.UseVisualStyleBackColor = true;
            SolveButton.Click += SolveButton_Click;
            // 
            // listBox1
            // 
            listBox1.Dock = DockStyle.Top;
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(0, 0);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(800, 274);
            listBox1.TabIndex = 2;
            // 
            // ClearButton
            // 
            ClearButton.Location = new Point(111, 303);
            ClearButton.Name = "ClearButton";
            ClearButton.Size = new Size(75, 23);
            ClearButton.TabIndex = 3;
            ClearButton.Text = "ClearData";
            ClearButton.UseVisualStyleBackColor = true;
            ClearButton.Click += ClearButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 340);
            Controls.Add(ClearButton);
            Controls.Add(listBox1);
            Controls.Add(SolveButton);
            Controls.Add(LoadDataButton);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion
        private Button LoadDataButton;
        private Button SolveButton;
        private ListBox listBox1;
        private Button ClearButton;
    }
}
