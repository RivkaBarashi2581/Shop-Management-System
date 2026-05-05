using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BlApi;
using BO;

namespace UI
{
    public enum Types { PRODUCT, SALE, CUSTOMER }

    public partial class ManagerController : Form
    {
        // שומר איזה סוג ישות צריך להציג במסך הזה
        private readonly Types _type;

        // גישה לשכבת ה-BL כדי לקרוא נתונים
        private readonly IBl _bl = Factory.Get();

        // הטבלה הראשית שבה יוצגו כל הנתונים
        private readonly DataGridView _dgv;

        // כפתורי הפעולות העליונים
        private readonly Button _showAllButton;
        private readonly Button _showOneButton;
        private readonly Button _updateButton;
        private readonly Button _removeButton;
        private readonly Button _addButton;
        private readonly Panel _topPanel;
        private readonly ComboBox _filterComboBox;
        private readonly Button _filterButton;

        /// <summary>
        /// בונה את המסך: שורת כפתורים למעלה וטבלה מתחת.
        /// מקבל סוג ישות (לקוח/מוצר/מבצע) כדי לדעת מה להציג ומה לבצע.
        /// </summary>
        public ManagerController(Types type)
        {
            InitializeComponent();
            _type = type;
            Text = $"Manager - {type}";

            _topPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 60,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = false,
                Padding = new Padding(10, 10, 10, 10)
            };

            _showAllButton = new Button
            {
                Text = "הצג הכל",
   
                Width = 110,
                Height = 40
            };

            _showOneButton = new Button
            {
                Text = "הצג אחד",
              
                Width = 110,
                Height = 40
            };

            _updateButton = new Button
            {
                Text = "עדכן",
                
                Width = 110,
                Height = 40
            };

            _removeButton = new Button
            {
                Text = "מחק",
             
                Width = 110,
                Height = 40
            };

            _addButton = new Button
            {
                Text = "הוסף",
                
                Width = 110,
                Height = 40
            };
            // כפתור של בחירת הקטגוריה מתול הרשימה
            _filterComboBox = new ComboBox
            {
                Width = 160,
                Height = 40,
                DropDownStyle = ComboBoxStyle.DropDownList
               
            };
            _filterComboBox.Items.Add("הכל");
            // טוען את הרשימה
            if (_type == Types.PRODUCT)
            {
                foreach (var category in Enum.GetNames(typeof(Categries)))
                {
                    _filterComboBox.Items.Add(category);
                }
            }
            // כפתור אישו רהיחפוש של הקטגוריה
            _filterComboBox.SelectedIndex = 0;
            _filterButton = new Button
            {
                Text = "סנן",
              
                Width = 90,
                Height = 40
            };

            _filterButton.Click += (_, _) => LoadData(_filterComboBox.Text);
            _topPanel.Controls.Add(_filterComboBox);
            _topPanel.Controls.Add(_filterButton);

            _showAllButton.Click += (_, _) => LoadData();
            _showOneButton.Click += (_, _) => ShowSelectedEntity();
            _updateButton.Click += (_, _) => UpdateSelectedEntity();
            _removeButton.Click += (_, _) => DeleteSelectedEntity();
            _addButton.Click += AddButton_Click;

            _topPanel.Controls.Add(_showAllButton);
            _topPanel.Controls.Add(_showOneButton);
            _topPanel.Controls.Add(_updateButton);
            _topPanel.Controls.Add(_removeButton);
            _topPanel.Controls.Add(_addButton);

            // יצירת טבלה שתמלא את כל הטופס
            _dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            Controls.Add(_dgv);
            Controls.Add(_topPanel);

            Load += ManagerController_Load;
        }

        /// <summary>
        /// אירוע טעינת הטופס – מציג ישר את הרשומות בטבלה.
        /// </summary>
        private void ManagerController_Load(object? sender, EventArgs e)
        {
            // בטעינת המסך מציגים את כל הנתונים
            LoadData();
        }

        /// <summary>
        /// טוען את כל הנתונים מה-BL לטבלה לפי סוג הישות.
        /// כאן מתבצע ReadAll בלבד (הצגת הכל).
        /// </summary>
        private void LoadData(string? filter = null)
        {
            try
            {
                // לפי סוג הישות בוחרים מאיזה אובייקט ב-BL לקרוא
                switch (_type)
                {
                    case Types.CUSTOMER:
                        var customers = _bl.Customer
     .ReadAll()
     .Where(c => c != null);

                        if (!string.IsNullOrWhiteSpace(filter))
                        {
                            customers = customers.Where(c =>
                                c!.CustomerName.Contains(filter, StringComparison.OrdinalIgnoreCase));
                        }

                        _dgv.DataSource = customers
                            .Select(c => new
                            {
                                c!.Id,
                                c.CustomerName,
                                c.Address,
                                c.Phone
                            })
                            .ToList();

                        SetCustomerHeaders();
                        break;

                    case Types.PRODUCT:
                        var products = _bl.Product
    .ReadAll()
    .Where(p => p != null);

                        if (!string.IsNullOrWhiteSpace(filter))
                        {
                            products = products.Where(p =>
                                p!.Category != null &&
                                p.Category.ToString()!.Contains(filter, StringComparison.OrdinalIgnoreCase));
                        }

                        _dgv.DataSource = products
                            .Select(p => new
                            {
                                p!.Id,
                                p.ProductName,
                                p.Category,
                                p.Price,
                                p.Stock
                            })
                            .ToList();

                        SetProductHeaders();
                        break;

                    case Types.SALE:
                        var sales = _bl.Sale
     .ReadAll()
     .Where(s => s != null);

                        if (!string.IsNullOrWhiteSpace(filter) && int.TryParse(filter, out int productId))
                        {
                            sales = sales.Where(s => s!.ProductId == productId);
                        }

                        _dgv.DataSource = sales
                            .Select(s => new
                            {
                                s!.Id,
                                s.ProductId,
                                s.QuantityRequier,
                                s.SalePrice,
                                s.StartSale,
                                s.EndSale
                            })
                            .ToList();

                        SetSaleHeaders();
                        break;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


                MessageBox.Show(
                    ex.ToString(),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        /// <summary>
        ///משנה את כותרות העמודות עבור לקוחות .
        /// </summary>
        private void SetCustomerHeaders()
        {
            _dgv.Columns["Id"].HeaderText = "Id";
            _dgv.Columns["CustomerName"].HeaderText = "Name";
            _dgv.Columns["Address"].HeaderText = "Address";
            _dgv.Columns["Phone"].HeaderText = "PhoneNumber";
        }

        /// <summary>
        /// משנה את כותרות העמודות עבור מוצרים.
        /// </summary>
        private void SetProductHeaders()
        {
            _dgv.Columns["Id"].HeaderText = "Id";
            _dgv.Columns["ProductName"].HeaderText = "Name";
            _dgv.Columns["Category"].HeaderText = "Category";
            _dgv.Columns["Price"].HeaderText = "Price";
            _dgv.Columns["Stock"].HeaderText = "Stock";
        }

        /// <summary>
        /// משנה את כותרות העמודות עבור מבצעים.
        /// </summary>
        private void SetSaleHeaders()
        {
            _dgv.Columns["Id"].HeaderText = "Id";
            _dgv.Columns["ProductId"].HeaderText = "ProductId";
            _dgv.Columns["QuantityRequier"].HeaderText = "Quantity";
            _dgv.Columns["SalePrice"].HeaderText = "SalePrice";
            _dgv.Columns["StartSale"].HeaderText = "StartSale";
            _dgv.Columns["EndSale"].HeaderText = "EndSale";
        }

        /// <summary>
        /// מוציא את ה-Id של השורה שנבחרה בטבלה.
        /// אם אין שורה נבחרת – זורק שגיאה כדי לעצור את הפעולה.
        /// </summary>
        private int GetSelectedId()
        {
            if (_dgv.CurrentRow == null || _dgv.CurrentRow.Cells["Id"].Value == null)
            {
                throw new Exception("יש לבחור שורה מהטבלה");
            }

            // פה מניחים שקיימת עמודה בשם "Id" ושאפשר להמיר אותה ל-int
            return Convert.ToInt32(_dgv.CurrentRow.Cells["Id"].Value);
        }

        /// <summary>
        /// "Show one" – מציג פרטי רשומה אחת לפי השורה שנבחרה.
        /// </summary>
        private void ShowSelectedEntity()
        {
            try
            {
                ShowEntity(GetSelectedId());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// "Update" – מעדכן את הרשומה שנבחרה (קולט ערכים חדשים ומפעיל BL.Update).
        /// </summary>
        private void UpdateSelectedEntity()
        {
            try
            {
                UpdateEntity(GetSelectedId());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// "Remove" – מוחק את הרשומה שנבחרה (לאחר אישור) ומרענן טבלה.
        /// </summary>
        private void DeleteSelectedEntity()
        {
            try
            {
                DeleteEntity(GetSelectedId());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// "Add" – הוספת רשומה חדשה.
        /// יוצר אובייקט BO לפי סוג הישות, קולט שדות, וקורא ל-BL.Create.
        /// </summary>
        private DateTime ReadDatePicker(string title, DateTime? defaultValue = null)
        {
            using Form form = new();
            using Label label = new();
            using DateTimePicker picker = new();
            using Button okButton = new();
            using Button cancelButton = new();

            form.Text = title;
            form.ClientSize = new Size(620, 220);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterParent;
            form.MinimizeBox = false;
            form.MaximizeBox = false;

            label.Text = title;
            label.Left = 20;
            label.Top = 15;
            label.Width = 560;
            label.Height = 30;
            label.Font = new Font("Segoe UI", 12);

            picker.Left = 20;
            picker.Top = 55;
            picker.Width = 560;
            picker.Format = DateTimePickerFormat.Short;
            picker.Value = defaultValue ?? DateTime.Today;

            okButton.Text = "אישור";
            okButton.Left = 360;
            okButton.Top = 145;
            okButton.Width = 100;
            okButton.Height = 35;
            okButton.DialogResult = DialogResult.OK;

            cancelButton.Text = "ביטול";
            cancelButton.Left = 480;
            cancelButton.Top = 145;
            cancelButton.Width = 100;
            cancelButton.Height = 35;
            cancelButton.DialogResult = DialogResult.Cancel;

            form.Controls.Add(label);
            form.Controls.Add(picker);
            form.Controls.Add(okButton);
            form.Controls.Add(cancelButton);

            form.AcceptButton = okButton;
            form.CancelButton = cancelButton;

            return form.ShowDialog() == DialogResult.OK
                ? picker.Value.Date
                : throw new Exception("הפעולה בוטלה");
        }
        private void AddButton_Click(object? sender, EventArgs e)
        {
            try
            {
                switch (_type)
                {
                    case Types.CUSTOMER:
                        // יצירת לקוח חדש מתוך נתוני קלט
                        Customer customer = new()
                        {
                            Id = ReadInt("הכנסי מזהה לקוח"),
                            CustomerName = ReadText("הכנסי שם לקוח"),
                            Address = Prompt("הכנסי כתובת"),
                            Phone = Prompt("הכנסי טלפון")
                        };
                        _bl.Customer.Create(customer);
                        break;

                    case Types.PRODUCT:
                        // יצירת מוצר חדש מתוך נתוני קלט
                        Product product = new()
                        {
                            Id = ReadInt("הכנסי מזהה מוצר"),
                            ProductName = ReadText("הכנסי שם מוצר"),
                            Category = ReadCategory(),
                            Price = ReadDouble("הכנסי מחיר"),
                            Stock = ReadInt("הכנסי כמות במלאי")
                        };
                        _bl.Product.Create(product);
                        break;

                    case Types.SALE:
                        // יצירת מבצע חדש מתוך נתוני קלט
                        Sale sale = new()
                        {
                            Id = ReadInt("הכנסי מזהה מבצע"),
                            ProductId = ReadInt("הכנסי מזהה מוצר"),
                            QuantityRequier = ReadInt("הכנסי כמות נדרשת"),
                            SalePrice = ReadDouble("הכנסי מחיר מבצע"),
                            StartSale = ReadDatePicker("הכנסי תאריך התחלה"),
                            EndSale = ReadDatePicker("הכנסי תאריך סיום"),
                            
                        };
                        _bl.Sale.Create(sale);
                        break;
                }

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// מציג רשומה בודדת (Read) לפי Id.
        /// כרגע מציג ב-MessageBox את ToString() של הישות.
        /// </summary>
        private void ShowEntity(int id)
        {
            object? entity = _type switch
            {
                Types.CUSTOMER => _bl.Customer.Read(id),
                Types.PRODUCT => _bl.Product.Read(id),
                Types.SALE => _bl.Sale.Read(id),
                _ => null
            };

            MessageBox.Show(entity?.ToString() ?? "הרשומה לא נמצאה", "פרטי רשומה");
        }

        /// <summary>
        /// מוחק רשומה (Delete) לפי Id אחרי אישור, ואז מרענן את הטבלה.
        /// </summary>
        private void DeleteEntity(int id)
        {
            if (
                MessageBox.Show(
                    $"למחוק רשומה {id}?",
                    "אישור",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                ) != DialogResult.Yes
            )
            {
                return;
            }

            try
            {
                switch (_type)
                {
                    case Types.CUSTOMER:
                        _bl.Customer.Delete(id);
                        break;
                    case Types.PRODUCT:
                        _bl.Product.Delete(id);
                        break;
                    case Types.SALE:
                        _bl.Sale.Delete(id);
                        break;
                }

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// עדכון רשומה (Update) לפי Id:
        /// קורא את הישות מה-BL, מקבל ערכים חדשים מהמשתמשת, ושולח ל-BL.Update.
        /// </summary>
        private void UpdateEntity(int id)
        {
            try
            {
                switch (_type)
                {
                    case Types.CUSTOMER:
                        Customer? customer = _bl.Customer.Read(id);
                        if (customer == null)
                        {
                            return;
                        }

                        Customer updatedCustomer = new()
                        {
                            Id = customer.Id,
                            CustomerName = ReadText("שם לקוח", customer.CustomerName),
                            Address = Prompt("כתובת", customer.Address ?? ""),
                            Phone = Prompt("טלפון", customer.Phone ?? "")
                        };
                        _bl.Customer.Update(updatedCustomer);
                        break;

                    case Types.PRODUCT:
                        Product? product = _bl.Product.Read(id);
                        if (product == null)
                        {
                            return;
                        }

                        Product updatedProduct = new()
                        {
                            Id = product.Id,
                            ProductName = ReadText("שם מוצר", product.ProductName),
                            Category = ReadCategory(product.Category),
                            Price = ReadDouble(
                                "מחיר",
                                product.Price?.ToString() ?? ""
                            ),
                            Stock = ReadInt("מלאי", product.Stock?.ToString() ?? "")
                        };
                        _bl.Product.Update(updatedProduct);
                        break;

                    case Types.SALE:
                        Sale? sale = _bl.Sale.Read(id);
                        if (sale == null)
                        {
                            return;
                        }

                        Sale updatedSale = new()
                        {
                            Id = sale.Id,
                            ProductId = ReadInt(
                                "מזהה מוצר",
                                sale.ProductId.ToString()
                            ),
                            QuantityRequier = ReadInt(
                                "כמות נדרשת",
                                sale.QuantityRequier.ToString()
                            ),
                            SalePrice = ReadDouble(
                                "מחיר מבצע",
                                sale.SalePrice?.ToString() ?? ""
                            ),
                            StartSale = ReadDate(
                                "תאריך התחלה",
                                sale.StartSale?.ToString("dd/MM/yyyy") ?? ""
                            ),
                            EndSale = ReadDate(
                                "תאריך סיום",
                                sale.EndSale?.ToString("dd/MM/yyyy") ?? ""
                            )
                        };
                        _bl.Sale.Update(updatedSale);
                        break;
                }

                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// חלון קלט קטן (Dialog) שמחזיר טקסט.
        /// אם המשתמשת לוחצת "ביטול" – נזרקת חריגה כדי לעצור את הפעולה.
        /// </summary>
        private string Prompt(string title, string defaultValue = "")
        {
            using Form form = new();
            using Label label = new();
            using TextBox textBox = new();
            using Button okButton = new();
            using Button cancelButton = new();

            form.Text = title;
            form.ClientSize = new Size(620, 260);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterParent;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.BackColor = Color.White;
            form.Font = new Font("Segoe UI", 10);

            // Label
            label.Text = title;
            label.Left = 30;
            label.Top = 30;
            label.Width = 430;
            label.Height = 40;
            label.Font = new Font("Segoe UI", 12);

            // TextBox
            textBox.Left = 30;
            textBox.Top = 90;
            textBox.Width = 560;
            textBox.Height = 35;
            textBox.Font = new Font("Segoe UI", 12);
            textBox.Text = defaultValue;

            // OK button
            okButton.Text = "אישור";
            okButton.Left = 360;
            okButton.Top = 165;
            okButton.Width = 100;
            okButton.Height = 35;
            okButton.DialogResult = DialogResult.OK;

            // Cancel button
            cancelButton.Text = "ביטול";
            cancelButton.Left = 475;
            cancelButton.Top = 165;
            cancelButton.Width = 100;
            cancelButton.Height = 35;
            cancelButton.DialogResult = DialogResult.Cancel;

            form.Controls.Add(label);
            form.Controls.Add(textBox);
            form.Controls.Add(okButton);
            form.Controls.Add(cancelButton);

            form.AcceptButton = okButton;
            form.CancelButton = cancelButton;

            return form.ShowDialog() == DialogResult.OK
                ? textBox.Text
                : throw new Exception("הפעולה בוטלה");
        }
        /// <summary>
        /// קלט טקסט חובה: לא מאפשר ריק/רווחים.
        /// </summary>
        private string ReadText(string title, string defaultValue = "")
        {
            string value = Prompt(title, defaultValue);

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new Exception("חובה להזין ערך");
            }

            return value;
        }

        /// <summary>
        /// קלט מספר שלם (int) מתוך טקסט.
        /// </summary>
        private int ReadInt(string title, string defaultValue = "")
        {
            return int.Parse(ReadText(title, defaultValue));
        }

        /// <summary>
        /// קלט מספר עשרוני (double) מתוך טקסט.
        /// </summary>
        private double ReadDouble(string title, string defaultValue = "")
        {
            return double.Parse(ReadText(title, defaultValue));
        }

        /// <summary>
        /// קלט ערך בוליאני (bool) מתוך טקסט.
        /// </summary>
        private bool ReadBool(string title, string defaultValue = "")
        {
            return bool.Parse(ReadText(title, defaultValue));
        }

        /// <summary>
        /// קלט תאריך (DateTime) מתוך טקסט.
        /// </summary>
        private DateTime ReadDate(string title, string defaultValue = "")
        {
            return DateTime.Parse(ReadText(title, defaultValue));
        }

        /// <summary>
        /// קלט קטגוריה מתוך enum:
        /// המשתמשת מקלידה שם קטגוריה (מתוך הרשימה), ואנחנו עושים Enum.Parse.
        /// </summary>
        private Categries ReadCategory(Categries? current = null)
        {
            string options = string.Join(", ", Enum.GetNames(typeof(Categries)));
            string value = ReadText($"קטגוריה ({options})", current?.ToString() ?? "");

            return Enum.Parse<Categries>(value, true);
        }
    }
}
