
namespace Calculator
{
    partial class CalculatorForm
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
            this.divisionButton = new System.Windows.Forms.Button();
            this.sevenButton = new System.Windows.Forms.Button();
            this.buttonTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.threeButton = new System.Windows.Forms.Button();
            this.twoButton = new System.Windows.Forms.Button();
            this.oneButton = new System.Windows.Forms.Button();
            this.sixButton = new System.Windows.Forms.Button();
            this.fiveButton = new System.Windows.Forms.Button();
            this.fourButton = new System.Windows.Forms.Button();
            this.nineButton = new System.Windows.Forms.Button();
            this.eightButton = new System.Windows.Forms.Button();
            this.undoButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.multiplicationButton = new System.Windows.Forms.Button();
            this.additionButton = new System.Windows.Forms.Button();
            this.periodButton = new System.Windows.Forms.Button();
            this.zeroButton = new System.Windows.Forms.Button();
            this.signButton = new System.Windows.Forms.Button();
            this.parenthesisButton = new System.Windows.Forms.Button();
            this.subtractionButton = new System.Windows.Forms.Button();
            this.calculateButton = new System.Windows.Forms.Button();
            this.expressionLabel = new System.Windows.Forms.Label();
            this.resultLabel = new System.Windows.Forms.Label();
            this.InterfaceTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.buttonTableLayoutPanel.SuspendLayout();
            this.InterfaceTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // divisionButton
            // 
            this.divisionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.divisionButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.divisionButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(207)))), ((int)(((byte)(210)))));
            this.divisionButton.FlatAppearance.BorderSize = 5;
            this.divisionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.divisionButton.Font = new System.Drawing.Font("Segoe UI", 20.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.divisionButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            this.divisionButton.Location = new System.Drawing.Point(600, 364);
            this.divisionButton.Margin = new System.Windows.Forms.Padding(0);
            this.divisionButton.Name = "divisionButton";
            this.divisionButton.Size = new System.Drawing.Size(200, 182);
            this.divisionButton.TabIndex = 2;
            this.divisionButton.Tag = Calculator.CalculatorForm.ButtonTag.Division;
            this.divisionButton.Text = "/";
            this.divisionButton.UseVisualStyleBackColor = false;
            this.divisionButton.Click += new System.EventHandler(this.buttonClick);
            // 
            // sevenButton
            // 
            this.sevenButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sevenButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.sevenButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(207)))), ((int)(((byte)(210)))));
            this.sevenButton.FlatAppearance.BorderSize = 5;
            this.sevenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sevenButton.Font = new System.Drawing.Font("Segoe UI", 20.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sevenButton.ForeColor = System.Drawing.Color.Black;
            this.sevenButton.Location = new System.Drawing.Point(0, 182);
            this.sevenButton.Margin = new System.Windows.Forms.Padding(0);
            this.sevenButton.Name = "sevenButton";
            this.sevenButton.Size = new System.Drawing.Size(200, 182);
            this.sevenButton.TabIndex = 7;
            this.sevenButton.Tag = Calculator.CalculatorForm.ButtonTag.Seven;
            this.sevenButton.Text = "7";
            this.sevenButton.UseVisualStyleBackColor = false;
            this.sevenButton.Click += new System.EventHandler(this.buttonClick);
            // 
            // buttonTableLayoutPanel
            // 
            this.buttonTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTableLayoutPanel.ColumnCount = 4;
            this.buttonTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.buttonTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.buttonTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.buttonTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.buttonTableLayoutPanel.Controls.Add(this.threeButton, 2, 3);
            this.buttonTableLayoutPanel.Controls.Add(this.twoButton, 1, 3);
            this.buttonTableLayoutPanel.Controls.Add(this.oneButton, 0, 3);
            this.buttonTableLayoutPanel.Controls.Add(this.sixButton, 2, 2);
            this.buttonTableLayoutPanel.Controls.Add(this.fiveButton, 1, 2);
            this.buttonTableLayoutPanel.Controls.Add(this.fourButton, 0, 2);
            this.buttonTableLayoutPanel.Controls.Add(this.nineButton, 2, 1);
            this.buttonTableLayoutPanel.Controls.Add(this.eightButton, 1, 1);
            this.buttonTableLayoutPanel.Controls.Add(this.undoButton, 1, 0);
            this.buttonTableLayoutPanel.Controls.Add(this.clearButton, 0, 0);
            this.buttonTableLayoutPanel.Controls.Add(this.multiplicationButton, 3, 1);
            this.buttonTableLayoutPanel.Controls.Add(this.divisionButton, 3, 2);
            this.buttonTableLayoutPanel.Controls.Add(this.additionButton, 3, 3);
            this.buttonTableLayoutPanel.Controls.Add(this.sevenButton, 0, 1);
            this.buttonTableLayoutPanel.Controls.Add(this.periodButton, 2, 4);
            this.buttonTableLayoutPanel.Controls.Add(this.zeroButton, 1, 4);
            this.buttonTableLayoutPanel.Controls.Add(this.signButton, 0, 4);
            this.buttonTableLayoutPanel.Controls.Add(this.parenthesisButton, 2, 0);
            this.buttonTableLayoutPanel.Controls.Add(this.subtractionButton, 3, 0);
            this.buttonTableLayoutPanel.Controls.Add(this.calculateButton, 3, 4);
            this.buttonTableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.buttonTableLayoutPanel.Location = new System.Drawing.Point(0, 390);
            this.buttonTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.buttonTableLayoutPanel.Name = "buttonTableLayoutPanel";
            this.buttonTableLayoutPanel.RowCount = 5;
            this.buttonTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.buttonTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.buttonTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.buttonTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.buttonTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.buttonTableLayoutPanel.Size = new System.Drawing.Size(800, 911);
            this.buttonTableLayoutPanel.TabIndex = 8;
            // 
            // threeButton
            // 
            this.threeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.threeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.threeButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(207)))), ((int)(((byte)(210)))));
            this.threeButton.FlatAppearance.BorderSize = 5;
            this.threeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.threeButton.Font = new System.Drawing.Font("Segoe UI", 20.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.threeButton.ForeColor = System.Drawing.Color.Black;
            this.threeButton.Location = new System.Drawing.Point(400, 546);
            this.threeButton.Margin = new System.Windows.Forms.Padding(0);
            this.threeButton.Name = "threeButton";
            this.threeButton.Size = new System.Drawing.Size(200, 182);
            this.threeButton.TabIndex = 22;
            this.threeButton.Tag = Calculator.CalculatorForm.ButtonTag.Three;
            this.threeButton.Text = "3";
            this.threeButton.UseVisualStyleBackColor = false;
            this.threeButton.Click += new System.EventHandler(this.buttonClick);
            // 
            // twoButton
            // 
            this.twoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.twoButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.twoButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(207)))), ((int)(((byte)(210)))));
            this.twoButton.FlatAppearance.BorderSize = 5;
            this.twoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.twoButton.Font = new System.Drawing.Font("Segoe UI", 20.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.twoButton.ForeColor = System.Drawing.Color.Black;
            this.twoButton.Location = new System.Drawing.Point(200, 546);
            this.twoButton.Margin = new System.Windows.Forms.Padding(0);
            this.twoButton.Name = "twoButton";
            this.twoButton.Size = new System.Drawing.Size(200, 182);
            this.twoButton.TabIndex = 21;
            this.twoButton.Tag = Calculator.CalculatorForm.ButtonTag.Two;
            this.twoButton.Text = "2";
            this.twoButton.UseVisualStyleBackColor = false;
            this.twoButton.Click += new System.EventHandler(this.buttonClick);
            // 
            // oneButton
            // 
            this.oneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.oneButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.oneButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(207)))), ((int)(((byte)(210)))));
            this.oneButton.FlatAppearance.BorderSize = 5;
            this.oneButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.oneButton.Font = new System.Drawing.Font("Segoe UI", 20.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.oneButton.ForeColor = System.Drawing.Color.Black;
            this.oneButton.Location = new System.Drawing.Point(0, 546);
            this.oneButton.Margin = new System.Windows.Forms.Padding(0);
            this.oneButton.Name = "oneButton";
            this.oneButton.Size = new System.Drawing.Size(200, 182);
            this.oneButton.TabIndex = 20;
            this.oneButton.Tag = Calculator.CalculatorForm.ButtonTag.One;
            this.oneButton.Text = "1";
            this.oneButton.UseVisualStyleBackColor = false;
            this.oneButton.Click += new System.EventHandler(this.buttonClick);
            // 
            // sixButton
            // 
            this.sixButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sixButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.sixButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(207)))), ((int)(((byte)(210)))));
            this.sixButton.FlatAppearance.BorderSize = 5;
            this.sixButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sixButton.Font = new System.Drawing.Font("Segoe UI", 20.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sixButton.ForeColor = System.Drawing.Color.Black;
            this.sixButton.Location = new System.Drawing.Point(400, 364);
            this.sixButton.Margin = new System.Windows.Forms.Padding(0);
            this.sixButton.Name = "sixButton";
            this.sixButton.Size = new System.Drawing.Size(200, 182);
            this.sixButton.TabIndex = 19;
            this.sixButton.Tag = Calculator.CalculatorForm.ButtonTag.Six;
            this.sixButton.Text = "6";
            this.sixButton.UseVisualStyleBackColor = false;
            this.sixButton.Click += new System.EventHandler(this.buttonClick);
            // 
            // fiveButton
            // 
            this.fiveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fiveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.fiveButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(207)))), ((int)(((byte)(210)))));
            this.fiveButton.FlatAppearance.BorderSize = 5;
            this.fiveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fiveButton.Font = new System.Drawing.Font("Segoe UI", 20.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.fiveButton.ForeColor = System.Drawing.Color.Black;
            this.fiveButton.Location = new System.Drawing.Point(200, 364);
            this.fiveButton.Margin = new System.Windows.Forms.Padding(0);
            this.fiveButton.Name = "fiveButton";
            this.fiveButton.Size = new System.Drawing.Size(200, 182);
            this.fiveButton.TabIndex = 18;
            this.fiveButton.Tag = Calculator.CalculatorForm.ButtonTag.Five;
            this.fiveButton.Text = "5";
            this.fiveButton.UseVisualStyleBackColor = false;
            this.fiveButton.Click += new System.EventHandler(this.buttonClick);
            // 
            // fourButton
            // 
            this.fourButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fourButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.fourButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(207)))), ((int)(((byte)(210)))));
            this.fourButton.FlatAppearance.BorderSize = 5;
            this.fourButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fourButton.Font = new System.Drawing.Font("Segoe UI", 20.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.fourButton.ForeColor = System.Drawing.Color.Black;
            this.fourButton.Location = new System.Drawing.Point(0, 364);
            this.fourButton.Margin = new System.Windows.Forms.Padding(0);
            this.fourButton.Name = "fourButton";
            this.fourButton.Size = new System.Drawing.Size(200, 182);
            this.fourButton.TabIndex = 17;
            this.fourButton.Tag = Calculator.CalculatorForm.ButtonTag.Four;
            this.fourButton.Text = "4";
            this.fourButton.UseVisualStyleBackColor = false;
            this.fourButton.Click += new System.EventHandler(this.buttonClick);
            // 
            // nineButton
            // 
            this.nineButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nineButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.nineButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(207)))), ((int)(((byte)(210)))));
            this.nineButton.FlatAppearance.BorderSize = 5;
            this.nineButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nineButton.Font = new System.Drawing.Font("Segoe UI", 20.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nineButton.ForeColor = System.Drawing.Color.Black;
            this.nineButton.Location = new System.Drawing.Point(400, 182);
            this.nineButton.Margin = new System.Windows.Forms.Padding(0);
            this.nineButton.Name = "nineButton";
            this.nineButton.Size = new System.Drawing.Size(200, 182);
            this.nineButton.TabIndex = 16;
            this.nineButton.Tag = Calculator.CalculatorForm.ButtonTag.Nine;
            this.nineButton.Text = "9";
            this.nineButton.UseVisualStyleBackColor = false;
            this.nineButton.Click += new System.EventHandler(this.buttonClick);
            // 
            // eightButton
            // 
            this.eightButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eightButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.eightButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(207)))), ((int)(((byte)(210)))));
            this.eightButton.FlatAppearance.BorderSize = 5;
            this.eightButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.eightButton.Font = new System.Drawing.Font("Segoe UI", 20.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.eightButton.ForeColor = System.Drawing.Color.Black;
            this.eightButton.Location = new System.Drawing.Point(200, 182);
            this.eightButton.Margin = new System.Windows.Forms.Padding(0);
            this.eightButton.Name = "eightButton";
            this.eightButton.Size = new System.Drawing.Size(200, 182);
            this.eightButton.TabIndex = 15;
            this.eightButton.Tag = Calculator.CalculatorForm.ButtonTag.Eight;
            this.eightButton.Text = "8";
            this.eightButton.UseVisualStyleBackColor = false;
            this.eightButton.Click += new System.EventHandler(this.buttonClick);
            // 
            // undoButton
            // 
            this.undoButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.undoButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.undoButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(207)))), ((int)(((byte)(210)))));
            this.undoButton.FlatAppearance.BorderSize = 5;
            this.undoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.undoButton.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.undoButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            this.undoButton.Location = new System.Drawing.Point(200, 0);
            this.undoButton.Margin = new System.Windows.Forms.Padding(0);
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(200, 182);
            this.undoButton.TabIndex = 9;
            this.undoButton.Tag = Calculator.CalculatorForm.ButtonTag.Undo;
            this.undoButton.Text = "⌫";
            this.undoButton.UseVisualStyleBackColor = false;
            this.undoButton.Click += new System.EventHandler(this.buttonClick);
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clearButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.clearButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(207)))), ((int)(((byte)(210)))));
            this.clearButton.FlatAppearance.BorderSize = 5;
            this.clearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearButton.Font = new System.Drawing.Font("Segoe UI", 20.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.clearButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            this.clearButton.Location = new System.Drawing.Point(0, 0);
            this.clearButton.Margin = new System.Windows.Forms.Padding(0);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(200, 182);
            this.clearButton.TabIndex = 8;
            this.clearButton.Tag = Calculator.CalculatorForm.ButtonTag.Clear;
            this.clearButton.Text = "C";
            this.clearButton.UseVisualStyleBackColor = false;
            this.clearButton.Click += new System.EventHandler(this.buttonClick);
            // 
            // multiplicationButton
            // 
            this.multiplicationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.multiplicationButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.multiplicationButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(207)))), ((int)(((byte)(210)))));
            this.multiplicationButton.FlatAppearance.BorderSize = 5;
            this.multiplicationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.multiplicationButton.Font = new System.Drawing.Font("Segoe UI", 20.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.multiplicationButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            this.multiplicationButton.Location = new System.Drawing.Point(600, 182);
            this.multiplicationButton.Margin = new System.Windows.Forms.Padding(0);
            this.multiplicationButton.Name = "multiplicationButton";
            this.multiplicationButton.Size = new System.Drawing.Size(200, 182);
            this.multiplicationButton.TabIndex = 12;
            this.multiplicationButton.Tag = Calculator.CalculatorForm.ButtonTag.Multiplication;
            this.multiplicationButton.Text = "×";
            this.multiplicationButton.UseVisualStyleBackColor = false;
            this.multiplicationButton.Click += new System.EventHandler(this.buttonClick);
            // 
            // additionButton
            // 
            this.additionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.additionButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.additionButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(207)))), ((int)(((byte)(210)))));
            this.additionButton.FlatAppearance.BorderSize = 5;
            this.additionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.additionButton.Font = new System.Drawing.Font("Segoe UI", 20.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.additionButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            this.additionButton.Location = new System.Drawing.Point(600, 546);
            this.additionButton.Margin = new System.Windows.Forms.Padding(0);
            this.additionButton.Name = "additionButton";
            this.additionButton.Size = new System.Drawing.Size(200, 182);
            this.additionButton.TabIndex = 13;
            this.additionButton.Tag = Calculator.CalculatorForm.ButtonTag.Addition;
            this.additionButton.Text = "+";
            this.additionButton.UseVisualStyleBackColor = false;
            this.additionButton.Click += new System.EventHandler(this.buttonClick);
            // 
            // periodButton
            // 
            this.periodButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.periodButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.periodButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(207)))), ((int)(((byte)(210)))));
            this.periodButton.FlatAppearance.BorderSize = 5;
            this.periodButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.periodButton.Font = new System.Drawing.Font("Segoe UI", 20.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.periodButton.ForeColor = System.Drawing.Color.Black;
            this.periodButton.Location = new System.Drawing.Point(400, 728);
            this.periodButton.Margin = new System.Windows.Forms.Padding(0);
            this.periodButton.Name = "periodButton";
            this.periodButton.Size = new System.Drawing.Size(200, 183);
            this.periodButton.TabIndex = 24;
            this.periodButton.Tag = Calculator.CalculatorForm.ButtonTag.Period;
            this.periodButton.Text = ".";
            this.periodButton.UseVisualStyleBackColor = false;
            this.periodButton.Click += new System.EventHandler(this.buttonClick);
            // 
            // zeroButton
            // 
            this.zeroButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zeroButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(246)))));
            this.zeroButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(207)))), ((int)(((byte)(210)))));
            this.zeroButton.FlatAppearance.BorderSize = 5;
            this.zeroButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zeroButton.Font = new System.Drawing.Font("Segoe UI", 20.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.zeroButton.ForeColor = System.Drawing.Color.Black;
            this.zeroButton.Location = new System.Drawing.Point(200, 728);
            this.zeroButton.Margin = new System.Windows.Forms.Padding(0);
            this.zeroButton.Name = "zeroButton";
            this.zeroButton.Size = new System.Drawing.Size(200, 183);
            this.zeroButton.TabIndex = 23;
            this.zeroButton.Tag = Calculator.CalculatorForm.ButtonTag.Zero;
            this.zeroButton.Text = "0";
            this.zeroButton.UseVisualStyleBackColor = false;
            this.zeroButton.Click += new System.EventHandler(this.buttonClick);
            // 
            // signButton
            // 
            this.signButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.signButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.signButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(207)))), ((int)(((byte)(210)))));
            this.signButton.FlatAppearance.BorderSize = 5;
            this.signButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.signButton.Font = new System.Drawing.Font("Segoe UI", 20.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.signButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            this.signButton.Location = new System.Drawing.Point(0, 728);
            this.signButton.Margin = new System.Windows.Forms.Padding(0);
            this.signButton.Name = "signButton";
            this.signButton.Size = new System.Drawing.Size(200, 183);
            this.signButton.TabIndex = 10;
            this.signButton.Tag = Calculator.CalculatorForm.ButtonTag.Sign;
            this.signButton.Text = "+/-";
            this.signButton.UseVisualStyleBackColor = false;
            this.signButton.Click += new System.EventHandler(this.buttonClick);
            // 
            // parenthesisButton
            // 
            this.parenthesisButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.parenthesisButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.parenthesisButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(207)))), ((int)(((byte)(210)))));
            this.parenthesisButton.FlatAppearance.BorderSize = 5;
            this.parenthesisButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.parenthesisButton.Font = new System.Drawing.Font("Segoe UI", 20.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.parenthesisButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            this.parenthesisButton.Location = new System.Drawing.Point(400, 0);
            this.parenthesisButton.Margin = new System.Windows.Forms.Padding(0);
            this.parenthesisButton.Name = "parenthesisButton";
            this.parenthesisButton.Size = new System.Drawing.Size(200, 182);
            this.parenthesisButton.TabIndex = 11;
            this.parenthesisButton.Tag = Calculator.CalculatorForm.ButtonTag.Parenthesis;
            this.parenthesisButton.Text = "( )";
            this.parenthesisButton.UseVisualStyleBackColor = false;
            this.parenthesisButton.Click += new System.EventHandler(this.buttonClick);
            // 
            // subtractionButton
            // 
            this.subtractionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.subtractionButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.subtractionButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(207)))), ((int)(((byte)(210)))));
            this.subtractionButton.FlatAppearance.BorderSize = 5;
            this.subtractionButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.subtractionButton.Font = new System.Drawing.Font("Segoe UI", 20.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.subtractionButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            this.subtractionButton.Location = new System.Drawing.Point(600, 0);
            this.subtractionButton.Margin = new System.Windows.Forms.Padding(0);
            this.subtractionButton.Name = "subtractionButton";
            this.subtractionButton.Size = new System.Drawing.Size(200, 182);
            this.subtractionButton.TabIndex = 14;
            this.subtractionButton.Tag = Calculator.CalculatorForm.ButtonTag.Subtraction;
            this.subtractionButton.Text = "-";
            this.subtractionButton.UseVisualStyleBackColor = false;
            this.subtractionButton.Click += new System.EventHandler(this.buttonClick);
            // 
            // calculateButton
            // 
            this.calculateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.calculateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
            this.calculateButton.Enabled = false;
            this.calculateButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(207)))), ((int)(((byte)(210)))));
            this.calculateButton.FlatAppearance.BorderSize = 5;
            this.calculateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.calculateButton.Font = new System.Drawing.Font("Segoe UI", 20.1F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.calculateButton.ForeColor = System.Drawing.Color.Black;
            this.calculateButton.Location = new System.Drawing.Point(600, 728);
            this.calculateButton.Margin = new System.Windows.Forms.Padding(0);
            this.calculateButton.Name = "calculateButton";
            this.calculateButton.Size = new System.Drawing.Size(200, 183);
            this.calculateButton.TabIndex = 25;
            this.calculateButton.Text = "=";
            this.calculateButton.UseVisualStyleBackColor = false;
            this.calculateButton.Click += new System.EventHandler(this.buttonClick);
            // 
            // expressionLabel
            // 
            this.expressionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.expressionLabel.BackColor = System.Drawing.Color.Transparent;
            this.expressionLabel.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.expressionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(78)))), ((int)(((byte)(78)))));
            this.expressionLabel.Location = new System.Drawing.Point(0, 0);
            this.expressionLabel.Margin = new System.Windows.Forms.Padding(0);
            this.expressionLabel.Name = "expressionLabel";
            this.expressionLabel.Size = new System.Drawing.Size(800, 195);
            this.expressionLabel.TabIndex = 9;
            this.expressionLabel.Text = "Expression";
            this.expressionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // resultLabel
            // 
            this.resultLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultLabel.BackColor = System.Drawing.Color.Transparent;
            this.resultLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.resultLabel.ForeColor = System.Drawing.Color.Black;
            this.resultLabel.Location = new System.Drawing.Point(0, 195);
            this.resultLabel.Margin = new System.Windows.Forms.Padding(0);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(800, 195);
            this.resultLabel.TabIndex = 10;
            this.resultLabel.Text = "Result";
            this.resultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // InterfaceTableLayoutPanel
            // 
            this.InterfaceTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InterfaceTableLayoutPanel.ColumnCount = 1;
            this.InterfaceTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.InterfaceTableLayoutPanel.Controls.Add(this.resultLabel, 0, 1);
            this.InterfaceTableLayoutPanel.Controls.Add(this.buttonTableLayoutPanel, 0, 2);
            this.InterfaceTableLayoutPanel.Controls.Add(this.expressionLabel, 0, 0);
            this.InterfaceTableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.InterfaceTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.InterfaceTableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.InterfaceTableLayoutPanel.Name = "InterfaceTableLayoutPanel";
            this.InterfaceTableLayoutPanel.RowCount = 3;
            this.InterfaceTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.InterfaceTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.InterfaceTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.InterfaceTableLayoutPanel.Size = new System.Drawing.Size(800, 1301);
            this.InterfaceTableLayoutPanel.TabIndex = 11;
            // 
            // CalculatorForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(207)))), ((int)(((byte)(210)))));
            this.ClientSize = new System.Drawing.Size(800, 1300);
            this.Controls.Add(this.InterfaceTableLayoutPanel);
            this.MinimumSize = new System.Drawing.Size(278, 467);
            this.Name = "CalculatorForm";
            this.Text = "Calculator";
            this.buttonTableLayoutPanel.ResumeLayout(false);
            this.InterfaceTableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button divisionButton;
        private System.Windows.Forms.Button sevenButton;
        private System.Windows.Forms.TableLayoutPanel buttonTableLayoutPanel;
        private System.Windows.Forms.Button calculateButton;
        private System.Windows.Forms.Button periodButton;
        private System.Windows.Forms.Button zeroButton;
        private System.Windows.Forms.Button threeButton;
        private System.Windows.Forms.Button twoButton;
        private System.Windows.Forms.Button oneButton;
        private System.Windows.Forms.Button sixButton;
        private System.Windows.Forms.Button fiveButton;
        private System.Windows.Forms.Button fourButton;
        private System.Windows.Forms.Button nineButton;
        private System.Windows.Forms.Button eightButton;
        private System.Windows.Forms.Button parenthesisButton;
        private System.Windows.Forms.Button signButton;
        private System.Windows.Forms.Button undoButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button multiplicationButton;
        private System.Windows.Forms.Button additionButton;
        private System.Windows.Forms.Button subtractionButton;
        private System.Windows.Forms.Label expressionLabel;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.TableLayoutPanel InterfaceTableLayoutPanel;
    }
}

