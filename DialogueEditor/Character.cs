using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace DialogueEditor
{
    public class Character : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public string Name { get { return _name; } set { _name = value; OnPropertyChanged(); } }
        private string _name = "";
        public string SpritePath { get { return _spritePath; } set { _spritePath = value; OnPropertyChanged(); } }
        private string _spritePath = "";
        private ImageSource _spriteSource = new BitmapImage();
        [XmlIgnore]
        public ImageSource SpriteSource { get { return _spriteSource; } set { _spriteSource = new BitmapImage(new Uri(SpritePath)); OnPropertyChanged(); } }
        public Character() { }
        public void LoadCharacter(string inputPath)
        {
            FileStream fs = new FileStream(inputPath, FileMode.Open);
            XmlSerializer xs = new XmlSerializer(typeof(Character));
            Character character;
            character= (Character)xs.Deserialize(fs);
            this.Name = character.Name;
            this.SpritePath = character.SpritePath;
            this.SpriteSource = character.SpriteSource;
        }
        public void SaveCharacter(string OutputPath)
        {
            FileStream fs = new FileStream(OutputPath, FileMode.Create);

            XmlSerializer xs = new XmlSerializer(typeof(Character));
            xs.Serialize(fs, this);
            fs.Close();
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
