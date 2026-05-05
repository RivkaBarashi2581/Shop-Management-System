namespace UI
{
    partial class Manager
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
            Product = new Button();
            Sales = new Button();
            button1 = new Button();
            SuspendLayout();
            // 
            // Product
            // 
            Product.Location = new Point(284, 155);
            Product.Name = "Product";
            Product.Size = new Size(182, 72);
            Product.TabIndex = 1;
            Product.Text = "מוצרים";
            Product.UseVisualStyleBackColor = true;
            Product.Click += button2_Click;
            // 
            // Sales
            // 
            Sales.Location = new Point(284, 276);
            Sales.Name = "Sales";
            Sales.Size = new Size(182, 72);
            Sales.TabIndex = 2;
            Sales.Text = "הנחות";
            Sales.UseVisualStyleBackColor = true;
            Sales.Click += Sales_Click;
            // 
            // button1
            // 
            button1.Location = new Point(284, 36);
            button1.Name = "button1";
            button1.Size = new Size(182, 72);
            button1.TabIndex = 3;
            button1.Text = "לקוחות";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Manager
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(Sales);
            Controls.Add(Product);
            Name = "Manager";
            Text = "מנהל";
            Load += Manager_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button Product;
        private Button Sales;
        private Button button1;
    }
}