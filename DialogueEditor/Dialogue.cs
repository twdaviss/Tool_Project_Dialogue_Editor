using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static DialogueEditor.Character;

namespace DialogueEditor
{
    public class Dialogue : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public Character Character { get { return _character; } set { _character = value; OnPropertyChanged(); } } 
        private Character _character = new Character();
        public enum ResponseType {Monologue,Response};
        public ResponseType responseType = ResponseType.Monologue;
        public string Dialogue1 { get { return _dialogue1; } set { _dialogue1 = value; OnPropertyChanged(); } }
        private string _dialogue1 = "";
        public string Dialogue2 { get { return _dialogue2; } set { _dialogue2 = value; OnPropertyChanged(); } }
        private string _dialogue2 = "";
        public string Dialogue3 { get { return _dialogue3; } set { _dialogue3 = value; OnPropertyChanged(); } }
        private string _dialogue3 = "";
        public string Dialogue4 { get { return _dialogue4; } set { _dialogue4 = value; OnPropertyChanged(); } }
        private string _dialogue4 = "";
        public string Response1 { get { return _response1; } set { _response1 = value; OnPropertyChanged(); } }
        private string _response1 = "";
        public string Response2 { get { return _response2; } set { _response2 = value; OnPropertyChanged(); } }
        private string _response2 = "";
        public string Response3 { get { return _response3; } set { _response3 = value; OnPropertyChanged(); } }
        private string _response3 = "";
        public string Response4 { get { return _response4; } set { _response4 = value; OnPropertyChanged(); } }
        private string _response4 = "";
        //public Dialogue next = null;
        //public Dialogue prev = null;

        public void GetResponse()
        {
            int choice =Console.Read();

            switch (choice)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }

        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
