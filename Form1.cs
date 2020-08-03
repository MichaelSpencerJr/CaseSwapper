using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaseSwapper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)) return;

            try
            {
                Clipboard.SetText(textBox1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Error copying text to the clipboard.  Press Control-C to copy this error so it can be pasted to Reddit.\r\n{ex}",
                    "Error");
            }
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Clipboard.ContainsText())
                {
                    MessageBox.Show(this, "Couldn't find any text in the clipboard.", "Unable to Paste");
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox1.Text) || MessageBox.Show(this,
                        "Text box is not empty. Replace text with clipboard contents?\r\n\r\nYes: Overwrite existing contents with clipboard\r\nNo: Paste text where cursor is",
                        "Overwrite", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    textBox1.Text = Clipboard.GetText();
                }
                else
                {
                    textBox1.Text = string.Concat(textBox1.Text.Substring(0, textBox1.SelectionStart),
                        Clipboard.GetText(),
                        textBox1.Text.Substring(textBox1.SelectionStart + textBox1.SelectionLength));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,
                    $"Error reading text from the clipboard.  Press Control-C to copy this error so it can be pasted to Reddit.\r\n{ex}",
                    "Error");
            }
        }

        private void btnSwap_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();

            foreach (var c in textBox1.Text)
            {
                if (!char.IsLetter(c)) {
                    sb.Append(c);
                    continue;
                }

                if (char.IsUpper(c))
                {
                    sb.Append((char) (c + 'a' - 'A'));
                    continue;
                }

                sb.Append((char) (c + 'A' - 'a'));
            }

            textBox1.Text = sb.ToString();
        }
    }
}
