namespace AirportManager
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.insert = new System.Windows.Forms.TabPage();
            this.IDUpdateLabel = new System.Windows.Forms.Label();
            this.IDUpdate = new System.Windows.Forms.NumericUpDown();
            this.Insert_Select = new System.Windows.Forms.ComboBox();
            this.select = new System.Windows.Forms.TabPage();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.reloadTableBtn = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.View_Select = new System.Windows.Forms.ComboBox();
            this.tabPage = new System.Windows.Forms.TabControl();
            this.insert.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IDUpdate)).BeginInit();
            this.select.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.tabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // insert
            // 
            this.insert.Controls.Add(this.IDUpdateLabel);
            this.insert.Controls.Add(this.IDUpdate);
            this.insert.Controls.Add(this.Insert_Select);
            this.insert.Location = new System.Drawing.Point(4, 29);
            this.insert.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.insert.Name = "insert";
            this.insert.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.insert.Size = new System.Drawing.Size(1091, 866);
            this.insert.TabIndex = 1;
            this.insert.Text = "Добавить / Изменить";
            this.insert.UseVisualStyleBackColor = true;
            // 
            // IDUpdateLabel
            // 
            this.IDUpdateLabel.AutoSize = true;
            this.IDUpdateLabel.Location = new System.Drawing.Point(208, 25);
            this.IDUpdateLabel.Name = "IDUpdateLabel";
            this.IDUpdateLabel.Size = new System.Drawing.Size(27, 20);
            this.IDUpdateLabel.TabIndex = 3;
            this.IDUpdateLabel.Text = "ID:";
            // 
            // IDUpdate
            // 
            this.IDUpdate.Location = new System.Drawing.Point(251, 23);
            this.IDUpdate.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.IDUpdate.Name = "IDUpdate";
            this.IDUpdate.Size = new System.Drawing.Size(150, 27);
            this.IDUpdate.TabIndex = 2;
            // 
            // Insert_Select
            // 
            this.Insert_Select.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Insert_Select.FormattingEnabled = true;
            this.Insert_Select.Location = new System.Drawing.Point(6, 21);
            this.Insert_Select.Name = "Insert_Select";
            this.Insert_Select.Size = new System.Drawing.Size(151, 28);
            this.Insert_Select.TabIndex = 1;
            // 
            // select
            // 
            this.select.Controls.Add(this.searchTextBox);
            this.select.Controls.Add(this.reloadTableBtn);
            this.select.Controls.Add(this.dataGridView);
            this.select.Controls.Add(this.View_Select);
            this.select.Location = new System.Drawing.Point(4, 29);
            this.select.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.select.Name = "select";
            this.select.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.select.Size = new System.Drawing.Size(1091, 866);
            this.select.TabIndex = 0;
            this.select.Text = "Просмотр";
            this.select.UseVisualStyleBackColor = true;
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(175, 21);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.PlaceholderText = "Поиск";
            this.searchTextBox.Size = new System.Drawing.Size(324, 27);
            this.searchTextBox.TabIndex = 10;
            // 
            // reloadTableBtn
            // 
            this.reloadTableBtn.Location = new System.Drawing.Point(528, 21);
            this.reloadTableBtn.Name = "reloadTableBtn";
            this.reloadTableBtn.Size = new System.Drawing.Size(94, 29);
            this.reloadTableBtn.TabIndex = 9;
            this.reloadTableBtn.Text = "Обновить";
            this.reloadTableBtn.UseVisualStyleBackColor = true;
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(6, 73);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 29;
            this.dataGridView.Size = new System.Drawing.Size(1079, 781);
            this.dataGridView.TabIndex = 8;
            // 
            // View_Select
            // 
            this.View_Select.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.View_Select.FormattingEnabled = true;
            this.View_Select.Location = new System.Drawing.Point(6, 21);
            this.View_Select.Name = "View_Select";
            this.View_Select.Size = new System.Drawing.Size(151, 28);
            this.View_Select.TabIndex = 0;
            // 
            // tabPage
            // 
            this.tabPage.Controls.Add(this.select);
            this.tabPage.Controls.Add(this.insert);
            this.tabPage.Location = new System.Drawing.Point(14, 16);
            this.tabPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage.Name = "tabPage";
            this.tabPage.SelectedIndex = 0;
            this.tabPage.Size = new System.Drawing.Size(1099, 899);
            this.tabPage.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 921);
            this.Controls.Add(this.tabPage);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1140, 958);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AirportManager";
            this.insert.ResumeLayout(false);
            this.insert.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IDUpdate)).EndInit();
            this.select.ResumeLayout(false);
            this.select.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.tabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public TabPage insert;
        public Label IDUpdateLabel;
        public NumericUpDown IDUpdate;
        public ComboBox Insert_Select;
        public TabPage select;
        public DataGridView dataGridView;
        public ComboBox View_Select;
        public TabControl tabPage;
        public Button reloadTableBtn;
        public TextBox searchTextBox;
    }
}