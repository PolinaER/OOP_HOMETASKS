using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoRed
{
    public partial class MainForm : Form
    {
        Photo originalPhoto;
        Photo resultPhoto;

        Panel parametrsPanel;
        List<NumericUpDown> parametrsControls;

        public MainForm()
        {         
            InitializeComponent();
        }

        private void filtersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            applyButton.Visible = true;

            if (parametrsPanel != null)
                this.Controls.Remove(parametrsPanel);

            parametrsPanel = new Panel
            {
                Left = filtersComboBox.Left,
                Top = filtersComboBox.Bottom + 10,
                Width = filtersComboBox.Width,
                Height = applyButton.Top - filtersComboBox.Bottom - 20
            };
            this.Controls.Add(parametrsPanel);

            var filter = filtersComboBox.SelectedItem as IFilter;

            if (filter == null) return;

            parametrsControls = new List<NumericUpDown>();
            var parametrsInfo = filter.GetParametrsInfo();

            for (var i = 0; i < parametrsInfo.Length; i++)
            {
                var label = new Label
                {
                    Width = parametrsPanel.Width - 60,
                    Height = 24,
                    Left = 0,
                    Top = i * Height + 10,
                    Text = parametrsInfo[i].Name,
                    Font = new Font(this.Font.FontFamily, 10)
                };
                parametrsPanel.Controls.Add(label);

                var inputBox = new NumericUpDown
                {
                    Left = label.Right,
                    Top = label.Top,
                    Width = parametrsPanel.Width - label.Width,
                    Height = label.Height,
                    Font = new Font(this.Font.FontFamily, 10),
                    Minimum = (decimal)parametrsInfo[i].MinValue,
                    Maximum = (decimal)parametrsInfo[i].MaxValue,
                    Increment = (decimal)parametrsInfo[i].Increment,
                    DecimalPlaces = 2,
                    Value = (decimal)parametrsInfo[i].DefaultValue
                };
                parametrsPanel.Controls.Add(inputBox);
                parametrsControls.Add(inputBox);
            }
            if (resultPhoto != null)
            {
                originalPhoto = resultPhoto;
                originalPictureBox.Image = resultPictureBox.Image;
                resultPhoto = null;
                resultPictureBox.Image = null;
            }
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            var filter = filtersComboBox.SelectedItem as IFilter;

            if (filter != null)
            {
                double[] parameters = new double[parametrsControls.Count];

                for (var i = 0; i < parameters.Length; i++)
                    parameters[i] = (double)parametrsControls[i].Value;

                resultPhoto = filter.Process(originalPhoto, parameters);
                resultPictureBox.Image = Convertors.PhotoToBitmap(resultPhoto);

                saveToolStripMenuItem.Enabled = true;
            }
        }

        public void AddFilter(IFilter filter)
        {
            if(filter!=null)
                filtersComboBox.Items.Add(filter);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openF = new OpenFileDialog { Filter = ".jpg;*.png;*.bmp|*.jpg;*.png;*.bmp|Other(*.*)|*.*" };

            if (openF.ShowDialog() == DialogResult.OK)
            {
                filtersComboBox.Visible = true;
                var bmp = (Bitmap)Image.FromFile(openF.FileName);
                originalPhoto = Convertors.BitmapToPhoto(bmp);
                originalPictureBox.Image = Convertors.PhotoToBitmap(Convertors.BitmapToPhoto(bmp));
                resultPictureBox.Image = null;
                resultPhoto = null;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (resultPictureBox.Image != null)
            {
                SaveFileDialog saveF = new SaveFileDialog { 
                    Title = "Сохранить картинку как...",
                    OverwritePrompt = true,
                    CheckPathExists = true,
                    Filter = "*.jpg|*.jpg|*.png|*.png|*.bmp|*.bmp|Other(*.*)|*.*" };
          
                if (saveF.ShowDialog() == DialogResult.OK)
                {
                    Convertors.PhotoToBitmap(resultPhoto).Save(saveF.FileName);
                }

            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
