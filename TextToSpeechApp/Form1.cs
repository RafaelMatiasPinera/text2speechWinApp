using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;


namespace TextToSpeechApp
{
    // La clase Form1 hereda de Form, que es una clase base para crear formularios en Windows Forms
    public partial class Form1 : Form
    {
        // Declara un objeto SpeechSynthesizer que se usará para convertir texto a voz
        private SpeechSynthesizer synthesizer;

        // Constructor del formulario
        public Form1()
        {
            InitializeComponent();               // Inicializa los componentes del formulario
            InitializeSpeechSynthesizer();       // Inicializa el sintetizador de voz
        }

        // Método para inicializar el sintetizador de voz
        private void InitializeSpeechSynthesizer()
        {
            synthesizer = new SpeechSynthesizer();               // Crea una nueva instancia de SpeechSynthesizer
            synthesizer.SetOutputToDefaultAudioDevice();         // Configura la salida del sintetizador al dispositivo de audio predeterminado

            // Agrega las voces instaladas al comboBox1
            foreach (var voice in synthesizer.GetInstalledVoices())
            {
                comboBox1.Items.Add(voice.VoiceInfo.Name);       // Añade el nombre de la voz al comboBox
            }

            // Si hay voces disponibles, selecciona la primera por defecto
            if (comboBox1.Items.Count > 0)
                comboBox1.SelectedIndex = 0;
        }

        // Método que se ejecuta cuando se hace clic en el botón
        private void button1_Click(object sender, EventArgs e)
        {
            // Verifica si el cuadro de texto richTextBox1 está vacío
            if (string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                MessageBox.Show("Please enter some text to speak.");  // Muestra un mensaje si el cuadro de texto está vacío
                return;
            }

            // Si hay una voz seleccionada en el comboBox, la establece en el sintetizador
            if (comboBox1.SelectedItem != null)
            {
                synthesizer.SelectVoice(comboBox1.SelectedItem.ToString());
            }

            // Convierte el texto del richTextBox1 a voz de forma asíncrona
            synthesizer.SpeakAsync(richTextBox1.Text);
        }
    }
}

