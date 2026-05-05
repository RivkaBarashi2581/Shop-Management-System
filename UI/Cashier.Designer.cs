namespace UI
{
    partial class Cashier
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label1 = new Label();
            btnAddByCode = new Button();
            comboBox1 = new ComboBox();
            orderBindingSource = new BindingSource(components);
            productBindingSource = new BindingSource(components);
            lblTotal = new Label();
            dgvCart = new DataGridView();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)orderBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)productBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvCart).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1160, 62);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(81, 25);
            label1.TabIndex = 0;
            label1.Text = "קוד מוצר";
            // 
            // btnAddByCode
            // 
            btnAddByCode.Location = new Point(1097, 107);
            btnAddByCode.Margin = new Padding(2);
            btnAddByCode.Name = "btnAddByCode";
            btnAddByCode.Size = new Size(109, 33);
            btnAddByCode.TabIndex = 4;
            btnAddByCode.Text = "הוסף מוצר";
            btnAddByCode.UseVisualStyleBackColor = true;
            btnAddByCode.Click += btnAddByCode_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(1008, 62);
            comboBox1.Margin = new Padding(2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(148, 33);
            comboBox1.TabIndex = 5;
            // 
            // orderBindingSource
            // 
            orderBindingSource.DataSource = typeof(BO.Order);
            // 
            // productBindingSource
            // 
            productBindingSource.DataSource = typeof(DO.Product);
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(1160, 203);
            lblTotal.Margin = new Padding(2, 0, 2, 0);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(89, 25);
            lblTotal.TabIndex = 9;
            lblTotal.Text = "סכום סופי";
            // 
            // dgvCart
            // 
            dgvCart.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCart.Location = new Point(35, 59);
            dgvCart.Margin = new Padding(2);
            dgvCart.Name = "dgvCart";
            dgvCart.RowHeadersWidth = 72;
            dgvCart.Size = new Size(821, 218);
            dgvCart.TabIndex = 10;
            //dgvCart.CellContentClick += dgvCart_CellContentClick;
            // 
            // button1
            // 
            button1.Location = new Point(1062, 281);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(109, 33);
            button1.TabIndex = 11;
            button1.Text = "סיום הזמנה";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // Cashier
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1278, 375);
            Controls.Add(button1);
            Controls.Add(dgvCart);
            Controls.Add(lblTotal);
            Controls.Add(comboBox1);
            Controls.Add(btnAddByCode);
            Controls.Add(label1);
            Margin = new Padding(2);
            Name = "Cashier";
            Text = "dgvCart";
            ((System.ComponentModel.ISupportInitialize)orderBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)productBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvCart).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnAddByCode;
        private ComboBox comboBox1;
        private BindingSource orderBindingSource;
        private BindingSource productBindingSource;
        private Label lblTotal;
        private DataGridView dgvCart;
        private Button button1;
    }
}