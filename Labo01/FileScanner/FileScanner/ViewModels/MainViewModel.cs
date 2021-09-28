using FileScanner.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks; 
using System.Windows;
using System.Windows.Forms;


namespace FileScanner.ViewModels
{
    
    public class MainViewModel : BaseViewModel
    {
     

        private string selectedFolder;
        private ObservableCollection<TheFolder> folderItems = new ObservableCollection<TheFolder>();
         
        public DelegateCommand<string> OpenFolderCommand { get; private set; }
        public DelegateCommand<string> ScanFolderCommand { get; private set; }

        
        
      
        

        public ObservableCollection<TheFolder> FolderItems { 
            get => folderItems;
            set 
            { 
                folderItems = value;
                OnPropertyChanged();
            }
        }

       

        public string SelectedFolder
        {
            get => selectedFolder;
            set
            {
                selectedFolder = value;
                OnPropertyChanged();
                ScanFolderCommand.RaiseCanExecuteChanged();
            }
        }

        public MainViewModel()
        {
            
            OpenFolderCommand = new DelegateCommand<string>(OpenFolder);
            ScanFolderCommand = new DelegateCommand<string>( ScanFolder, CanExecuteScanFolder);
        }

        private bool CanExecuteScanFolder(string obj)
        {
            return !string.IsNullOrEmpty(SelectedFolder);
        }

        private void OpenFolder(string obj)
        {
            try
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    if (fbd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            SelectedFolder = fbd.SelectedPath;
                        }
                        catch (Exception error)
                        {
                            Console.WriteLine("Error :" + error.Message);
                            throw;
                        }

                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Error :" + error.Message);
                throw;
            }
            
        }

        private void ScanFolder(string dir)
        {
             Task.Run(() =>
             {
                
                FolderItems = new ObservableCollection<TheFolder>(GetDirs(dir));
                
                 foreach (var item in Directory.EnumerateFiles(dir, "*"))
                 {

                     try
                     {
                         FolderItems.Add(new TheFolder(item, "/Views/file.png"));
                     }
                     catch (Exception error)
                     {
                         Console.WriteLine("Error :" + error.Message);
                         continue;
                     }

                 }
             }

            );
            
            
        }
       

      
        IEnumerable<TheFolder> GetDirs(string dir)
        {
            TheFolder attemp;
           
            foreach (var d in Directory.EnumerateDirectories(dir, ""))
            {

                IEnumerable<string> files;
                try
                {
                    files = Directory.EnumerateFiles(d, "");
                }
                catch (Exception)
                {

                    continue;
                }
                

                foreach (var file in files)
                {
                    try
                    {
                        attemp = new TheFolder(file, "/Views/file.png");
                    }
                    catch (Exception)
                    {

                        attemp = new TheFolder("BLOCKED", "/Views/file.png");
                    }
                    yield return attemp;
                }
               var temp = GetDirs(d);
                foreach (var over in temp)
                {
                    try
                    {
                        attemp = over;
                    }
                    catch (Exception)
                    {

                        attemp = new TheFolder("BLOCKED", "/Views/file.png");
                    }
                    yield return attemp;
                   
                }
                try
                {
                    attemp = new TheFolder(d, "/Views/folder.jpg");
                }
                catch (Exception)
                {

                    attemp = new TheFolder("BLOCKED", "/Views/file.png");
                }
                yield return attemp;
            }
        }


        ///TODO : Tester avec un dossier avec beaucoup de fichier
        ///TODO : Rendre l'application asynchrone
        ///TODO : Ajouter un try/catch pour les dossiers sans permission


    }
    public class TheFolder
    {

        public string TheItem { get; set; }
        public string TheImage { get; set; }
        public TheFolder(string theItem, string theimage)
        {
            TheItem = theItem;
            TheImage = theimage;
        }


    }
}
