using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;
using System.Diagnostics;
namespace Baskonus
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SpeechSynthesizer synt = new SpeechSynthesizer();
        PromptBuilder pbuilder = new PromptBuilder();
        SpeechRecognitionEngine Rengine = new SpeechRecognitionEngine();
        private void button1_Click(object sender, EventArgs e)
        {
            Choices list=new Choices();
           
            list.Add(new string[] {"Hello","exit","open","chrome","open chrome"});
            Grammar gramer = new Grammar(new GrammarBuilder(list));
            try
            {
                Rengine.RequestRecognizerUpdate();
                Rengine.LoadGrammar(gramer);
                Rengine.SpeechRecognized +=Rengine_SpeechRecognized;
                Rengine.SetInputToDefaultAudioDevice();
                Rengine.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch
            {
                MessageBox.Show("hata");
                return;
            }
        }

        void Rengine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case  "open chrome":
                    System.Diagnostics.Process.Start("C:/Program Files (x86)/Google/Chrome/Application/chrome.exe");
                    pbuilder.ClearContent();
                    pbuilder.AppendText("Very well");
                    synt.Speak(pbuilder);
                    break;
                case "hello":
                    pbuilder.AppendText("hello");
                    break;
                case "exit":
                    Application.Exit();
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Rengine.Dispose();
        }

     
    }
}
