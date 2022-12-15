using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static DialogueEditor.CharacterCreator;

namespace DialogueEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public string _inputPath = "";
        public DialogueList DialogueLines = new DialogueList();
        //public Character CurrentCharacter { get { return _currentCharacter; } set { _currentCharacter = value; } }
        //private Character _currentCharacter = new Character();
        public Dialogue CurrentDialogueEntry { get { return _currentDialogueEntry; } set { _currentDialogueEntry = value; OnPropertyChanged(); } }
        private Dialogue _currentDialogueEntry = new Dialogue();
        public int entriesCount = 0;
        public MainWindow()
        {
            InitializeComponent();
            DialogueLines.DialogueEntries.Add(new Dialogue());
            DataContext= this;
        }

        private void CreateCharacter_Click(object sender, RoutedEventArgs e)
        {
            CharacterCreator creator = new CharacterCreator();
            creator.ShowDialog();
        }

        private void ChangeCharacter_Click(object sender, RoutedEventArgs e)
        {
            ChangeCharacter();
        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "XML | *.xml";
            dlg.ShowDialog();
            if (dlg.FileName != "")
            {
                DialogueLines.LoadDialogue(dlg.FileName);
                entriesCount = 0;
                UpdateCurrentEntry();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "XML | *.xml";
            dlg.ShowDialog();
            if (dlg.FileName != "")
            {
                DialogueLines.SaveDialogue(dlg.FileName);
            }
        }

        private void NextSame_Click(object sender, RoutedEventArgs e)
        {
            SaveDialogue();
            UpdateCurrentEntry();
        }
        public void SaveDialogue()
        {
            Dialogue dialogue = new Dialogue();
            dialogue.Character = CurrentDialogueEntry.Character;
            dialogue.Response1 = Response1Box.Text;
            dialogue.Response2 = Response2Box.Text;
            dialogue.Response3 = Response3Box.Text;
            dialogue.Response4 = Response4Box.Text;
            dialogue.Dialogue1 = Dialogue1Box.Text;
            dialogue.Dialogue2 = Dialogue2Box.Text;
            dialogue.Dialogue3 = Dialogue3Box.Text;
            dialogue.Dialogue4 = Dialogue4Box.Text;

            if (ResponseTypeButton1.IsChecked == true)
            {
                dialogue.responseType = Dialogue.ResponseType.Monologue;
            }
            else if (ResponseTypeButton2.IsChecked == true)
            {
                dialogue.responseType = Dialogue.ResponseType.Response;
            }
            if (DialogueLines.DialogueEntries.Count == entriesCount)
            {
                DialogueLines.DialogueEntries.Add(dialogue);
            }
            else
            {
                DialogueLines.DialogueEntries[entriesCount] = dialogue;
            }

            DialogueLines.DialogueEntries.Add(new Dialogue());
            entriesCount++;
        }

        private void NextNew_Clicked(object sender, RoutedEventArgs e)
        {
            SaveDialogue();
            UpdateCurrentEntry();
            ChangeCharacter();
        }
        public void ChangeCharacter()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "XML | *.xml";
            dlg.ShowDialog();
            if (dlg.FileName != "")
            {
                _inputPath = dlg.FileName;
                Character tempChar = new Character();
                tempChar.LoadCharacter(dlg.FileName);
                CurrentDialogueEntry.Character = tempChar;
                OnPropertyChanged();
            }
        }

        private void PreviousEntry_Click(object sender, RoutedEventArgs e)
        {
            SaveDialogue();
            if (entriesCount - 2 >= 0)
            {
                entriesCount -= 2;
                UpdateCurrentEntry();
            }
            else
            {
                entriesCount--;
                UpdateCurrentEntry();

            }
        }
        private void NextEntry_Click(object sender, RoutedEventArgs e)
        {
            SaveDialogue();
            UpdateCurrentEntry();
        }
        public void UpdateCurrentEntry()
        {
            CurrentDialogueEntry = DialogueLines.DialogueEntries[entriesCount];
            switch (CurrentDialogueEntry.responseType)
            {
                case Dialogue.ResponseType.Monologue:
                    ResponseTypeButton1.IsChecked = true;
                    ResponseTypeButton2.IsChecked = false;

                    break;
                case Dialogue.ResponseType.Response:
                    ResponseTypeButton1.IsChecked = false;
                    ResponseTypeButton2.IsChecked = true;
                    break;
                default:
                    ResponseTypeButton1.IsChecked = false;
                    ResponseTypeButton2.IsChecked = false;
                    break;
            }
            CurrentDialogueEntry.Character = DialogueLines.DialogueEntries[entriesCount].Character;
        }



        private void ResponseButton_Checked(object sender, RoutedEventArgs e)
        {
            Dialogue1Box.IsEnabled = true;
            Dialogue2Box.IsEnabled = true;
            Dialogue3Box.IsEnabled = true;
            Dialogue4Box.IsEnabled = true;

            Response1Box.IsEnabled = true;
            Response2Box.IsEnabled = true;
            Response3Box.IsEnabled = true;
            Response4Box.IsEnabled = true;
        }
        private void MonologueButton_Checked(object sender, RoutedEventArgs e)
        {
            Dialogue1Box.IsEnabled = true;
            Dialogue2Box.IsEnabled = false;
            Dialogue3Box.IsEnabled = false;
            Dialogue4Box.IsEnabled = false;

            Response1Box.IsEnabled = false;
            Response2Box.IsEnabled = false;
            Response3Box.IsEnabled = false;
            Response4Box.IsEnabled = false;
        }
    }
}
