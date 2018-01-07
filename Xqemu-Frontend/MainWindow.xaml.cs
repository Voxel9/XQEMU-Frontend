using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Xqemu_Frontend {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            try {
                string[] readlines = File.ReadAllLines("frontend.cfg");
                textBox.Text = readlines[0];
                textBox1.Text = readlines[1];
                textBox2.Text = readlines[2];
                textBox3.Text = readlines[3];
                if (readlines[4] == "true")
                    checkBox.IsChecked = true;
                else if (readlines[4] == "false")
                    checkBox.IsChecked = false;
                if (readlines[5] == "true")
                    RamSlct.IsChecked = true;
                else if (readlines[5] == "false")
                    RamSlct.IsChecked = false;
            }
            catch { }
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            if (textBox.Text == "" || textBox1.Text == "" || textBox3.Text == "") {
                System.Windows.MessageBox.Show("You must fill in BIOS, MCPX and HDD fields!");
            } else {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "qemu-system-xbox.exe";
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;

                // Generate XQEMU launch command line 
                startInfo.Arguments = "-cpu pentium3 -machine xbox," + ((bool)checkBox.IsChecked ? "" : "short_animation,") + "bootrom=" + textBox1.Text +
                      " -m " + ((bool)RamSlct.IsChecked ? "128" : "64") + " -drive file=" + textBox3.Text + ",index=0,media=disk,locked=on -drive file=" +
                      textBox2.Text + ",index=1,media=cdrom -bios " + textBox.Text +
                      " -usb -device usb-hub,bus=usb-bus.0,port=3 -device usb-xbox-gamepad,bus=usb-bus.0,port=3.2";

                try {
                    using (Process exeProcess = Process.Start(startInfo)) {
                        exeProcess.WaitForExit();
                    }
                }
                catch {
                    // Display File Dialog
                    OpenFileDialog openFileDialog5 = new OpenFileDialog();
                    openFileDialog5.Filter = "Executable|*.exe";
                    openFileDialog5.Title = "Please Locate XQEMU Executable";

                    // Show the Dialog
                    if (openFileDialog5.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                        // Append Text Box
                        startInfo.FileName = openFileDialog5.FileName;
                        try {
                            using (Process exeProcess = Process.Start(startInfo)) {
                                exeProcess.WaitForExit();
                            }
                        }
                        catch { }
                    }
                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e) {
            // Display File Dialog
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Xbox BIOS File|*.bin";
            openFileDialog1.Title = "Select an Xbox BIOS";

            // Show the Dialog
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                // Append Text Box
                textBox.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e) {
            // Display File Dialog
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            openFileDialog2.Filter = "MCPX ROM|*.bin";
            openFileDialog2.Title = "Select the MCPX ROM";

            // Show the Dialog
            if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                // Append Text Box
                textBox1.Text = openFileDialog2.FileName;
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e) {
            // Display File Dialog
            OpenFileDialog openFileDialog3 = new OpenFileDialog();
            openFileDialog3.Filter = "Xbox ISO|*.iso";
            openFileDialog3.Title = "Select an Xbox ISO";

            // Show the Dialog
            if (openFileDialog3.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                // Append Text Box
                textBox2.Text = openFileDialog3.FileName;
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e) {
            // Display File Dialog
            OpenFileDialog openFileDialog4 = new OpenFileDialog();
            openFileDialog4.Filter = "QEMU Harddisk|*.qcow2";
            openFileDialog4.Title = "Select an Xbox Harddisk";

            // Show the Dialog
            if (openFileDialog4.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                // Append Text Box
                textBox3.Text = openFileDialog4.FileName;
            }
        }

        string BootTickBool, RamTickBool;
        private void SaveCfg_Click(object sender, RoutedEventArgs e) {
            // Store Bootanim checkbox bool as a string
            if (checkBox.IsChecked == true)
                BootTickBool = "true";
            else if (checkBox.IsChecked == false)
                BootTickBool = "false";
            // Store RAM selection checkbox bool as a string
            if (RamSlct.IsChecked == true)
                RamTickBool = "true";
            else if (RamSlct.IsChecked == false)
                RamTickBool = "false";
            string[] lines = { textBox.Text, textBox1.Text, textBox2.Text, textBox3.Text, BootTickBool, RamTickBool };
            File.WriteAllLines("frontend.cfg", lines);
            System.Windows.MessageBox.Show("Config saved to frontend.cfg!");
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e) { }
        private void textBox1_TextChanged(object sender, TextChangedEventArgs e) { }
        private void textBox2_TextChanged(object sender, TextChangedEventArgs e) { }
        private void textBox3_TextChanged(object sender, TextChangedEventArgs e) { }
        private void checkBox_Checked(object sender, RoutedEventArgs e) { }
        private void RamSlct_Checked(object sender, RoutedEventArgs e) { }
    }
}
