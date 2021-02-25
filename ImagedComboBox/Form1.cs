using System.Drawing;
using System.Windows.Forms;

namespace ImagedComboBox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            imagedComboBox1.Items.Add(new ComboBoxItem("Item without image"));
            imagedComboBox1.Items.Add(new ComboBoxItem("Exclamation", SystemIcons.Exclamation.ToBitmap()));
            imagedComboBox1.Items.Add(new ComboBoxItem("Information", SystemIcons.Information.ToBitmap()));
            imagedComboBox1.Items.Add(new ComboBoxItem("Error", SystemIcons.Error.ToBitmap()));
            imagedComboBox1.SelectedIndex = 1;

        }

    }
}
