using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using static DialogueEditor.Dialogue;

namespace DialogueEditor
{
    public class DialogueList
    {
        public List<Dialogue> DialogueEntries = new List<Dialogue>();
        public DialogueList() { }

        public void LoadDialogue(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            XmlSerializer xs = new XmlSerializer(typeof(List<Dialogue>));
            DialogueEntries = (List<Dialogue>)xs.Deserialize(fs);
            foreach(Dialogue element in DialogueEntries){
                if(element.Character.SpritePath !="")
                element.Character.SpriteSource = new BitmapImage(new Uri(element.Character.SpritePath));
            }
        }
        public void SaveDialogue(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create);
            XmlSerializer xs = new XmlSerializer(typeof(List<Dialogue>));
            xs.Serialize(fs, DialogueEntries);
            fs.Close();
        }

    }
}
