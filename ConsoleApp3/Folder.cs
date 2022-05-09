using MyApp1;
using System.Drawing;
using System.Drawing.Imaging;
using System;
using System.Diagnostics;

namespace MyApp
{
    class Folder
    {
        public Folder() { Directory.CreateDirectory(@$"C:\Users\{Environment.UserName}\Desktop\Pngfolder"); }
        private string Path { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private void EnterButton()
        {
            string Time = DateTime.Now.ToString("yyyy-MM-dd-hh.mm.ss");
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Image Imagefile = ScreenCapture.CaptureDesktop();
            Imagefile.Save($"{Path}/Pngfolder/{Time}.png", ImageFormat.Png);
            Console.WriteLine("\tCompilate");
            Thread.Sleep(1000);
        }
        private string SelectPng(int id)
        {
            int num = 0;
            var directory = new DirectoryInfo($"{Path}/Pngfolder");
            foreach (var file in directory.GetFiles())
            {
                if (num+1 == id) return file.Name;
                num++;
            }
            return "(|NO|)";
        }
        private void Open()
        {
            int number = Showfile();
            string filename;
            if (number == -1) { Console.WriteLine("No file");Thread.Sleep(888); return; };
            while (true)
            {
                Console.WriteLine("Enter file number: ");
                int num = Convert.ToInt32(Console.ReadLine());

                if (num > 0 && num <= number + 1) { filename=SelectPng(num);
                    if (filename== "(|NO|)")Console.WriteLine("No png");
                    else break;
                    continue;
                }
                else { Console.Clear(); Showfile(); continue; };
            }
            Console.WriteLine(filename);
            Thread.Sleep(1000);

            
            ProcessStartInfo psi = new ProcessStartInfo
            {

                FileName = "mspaint.exe",
                Arguments = @$"{Path}\Pngfolder\{filename}",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true
            };
            Process.Start(psi);
            return;
        }
        private int Showfile()
        {
            int num = -1;
           var directory = new DirectoryInfo($"{Path}/Pngfolder");

            foreach (var file in directory.GetFiles())
            {
                num++;
                Console.WriteLine($"{num + 1}) {file.Name}");
            }
            return num;
            Thread.Sleep(1000);
        }
        public void Start1()
        {
            while (true)
            {
                Console.WriteLine("______ReadKey______\nEnter = screen\ns = show png\nSpace = Open file\n\n");
                Console.Write("Enter: ");
                char symbol = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (symbol == 13)
                {
                    EnterButton();
                }
                else if (symbol == 'S'|| symbol == 's' )
                {
                    Showfile();
                    Thread.Sleep(1000);
                }
                else if (symbol == ' ')
                {
                    Open();
                }
                Console.Clear();
            }

        }
    }
}
