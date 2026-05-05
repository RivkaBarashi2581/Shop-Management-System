using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace UI
{
    public partial class Cashier : Form
    {
        private BlApi.IBl bl = BlApi.Factory.Get();
        private List<BO.Product> products = new List<BO.Product>();

        private BO.Order currentOrder = new BO.Order
        {
            Products = new List<BO.ProductInOrder>(),
            IsFavorite = false
        };
        public Cashier()
        {
            InitializeComponent();
            LoadProducts();
            // טעינה של הטבלה עם המוצרים בהזמנה הנוכחית
            dgvCart.DataSource = currentOrder.Products;
            // הצגת הסכום הכולל של ההזמנה
            lblTotal.Text = "סה\"כ: 0 ₪";

        }
        private void LoadProducts()
        {
            // קריאה של כל המוצרים מהשכבה העסקית והצגתם ב-ComboBox
            products = bl.Product.ReadAll()
                                 .Where(p => p != null)
                                 .Select(p => p!)
                                 .ToList();

            comboBox1.DataSource = products;
            comboBox1.DisplayMember = "ProductName";
            comboBox1.ValueMember = "Id";
        }

       

        private void btnAddByCode_Click(object sender, EventArgs e)
        {
            // קבלת הטקסט שהמשתמש הזין ב-ComboBox
            string input = comboBox1.Text.Trim();

            BO.Product? product = null;
            // ניסיון למצוא את המוצר לפי הבחירה ב-ComboBox
            if (comboBox1.SelectedItem is BO.Product selected)
                product = selected;
            // אם לא נמצא מוצר לפי הבחירה, ננסה לפרש את הטקסט כמספר זיהוי
            if (product == null && int.TryParse(input, out int id))
                product = products.FirstOrDefault(p => p.Id == id);
            // אם עדיין לא נמצא מוצר, ננסה למצוא אותו לפי שם (התעלמות מרישיות)
            if (product == null)
                product = products.FirstOrDefault(p =>
                    p.ProductName.Equals(input, StringComparison.OrdinalIgnoreCase));
            // אם עדיין לא נמצא מוצר, נציג הודעת שגיאה למשתמש
            if (product == null)
            {
                MessageBox.Show("מוצר לא נמצא");
                return;
            }
            // הוספת המוצר להזמנה הנוכחית

            bl.Order.AddProductToOrder(currentOrder, product.Id, 1);
            // עדכון הטבלה והסכום הכולל לאחר הוספת המוצר
            var item = currentOrder.Products.FirstOrDefault(p => p.ProductId == product.Id);


            // רענון הטבלה והסכום הכולל
            dgvCart.DataSource = null;
            // הצגת המוצרים בהזמנה הנוכחית בטבלה
            dgvCart.DataSource = currentOrder.Products;
            // הצגת הסכום הכולל של ההזמנה
            lblTotal.Text = "סה\"כ: " + currentOrder.TotalPrice.ToString("0.00") + " ₪";

            comboBox1.Text = "";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // ביצוע ההזמנה הנוכחית
            bl.Order.DoOrder(currentOrder);
            // הצגת הודעה למשתמש שההזמנה בוצעה בהצלחה
            MessageBox.Show("ההזמנה בוצעה בהצלחה");
            // איפוס ההזמנה הנוכחית ליצירת הזמנה חדשה
            currentOrder = new BO.Order
            {
                Products = new List<BO.ProductInOrder>(),
                IsFavorite = true
            };
            // רענון הטבלה והסכום הכולל לאחר ביצוע ההזמנה
            dgvCart.DataSource = null;
            dgvCart.DataSource = currentOrder.Products;
            lblTotal.Text = "סה\"כ: 0 ₪";

        }

       

       
    }
}
