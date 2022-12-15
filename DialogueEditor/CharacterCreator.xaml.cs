using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DialogueEditor
{
    /// <summary>
    /// Interaction logic for CharacterCreator.xaml
    /// </summary>
    public partial class CharacterCreator : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _spritePath = "";
        public string SpritePath { get { return _spritePath; }set { _spritePath = value; OnPropertyChanged(); } }
        public ImageSource _sprite = null;
        public ImageSource Sprite { get { return _sprite; } set { _sprite = new BitmapImage(new Uri(_spritePath)); OnPropertyChanged(); } }
        public CharacterCreator()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "XML | *.xml";
            dlg.ShowDialog();
            Character character= new Character();
            character.Name = NameBox.Text;
            character.SpritePath =SpriteBox.Text;
            character.SaveCharacter(dlg.FileName);
            this.Close();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "PNG | *.png";
            dlg.ShowDialog();
            SpritePath = dlg.FileName;
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
