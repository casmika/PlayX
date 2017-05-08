using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayX
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private NAudio.Wave.WaveFileReader wave = null;
        private NAudio.Wave.DirectSoundOut output = null;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Wave File (*.wav)|*.wav;";
            if(op.ShowDialog() == DialogResult.OK)
            {
                dispose();
                wave = new NAudio.Wave.WaveFileReader(op.FileName);
                output = new NAudio.Wave.DirectSoundOut();
                output.Init(new NAudio.Wave.WaveChannel32(wave));
                output.Play();
                button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(output != null)
            {
                if (output.PlaybackState == NAudio.Wave.PlaybackState.Playing) output.Pause();
                else if(output.PlaybackState == NAudio.Wave.PlaybackState.Paused) output.Play();
            }
        }

        private void dispose()
        {
            if (output != null)
            {
                if (output.PlaybackState == NAudio.Wave.PlaybackState.Playing) output.Stop();
                output.Dispose();
                output = null;
            }
            if(wave != null)
            {
                wave.Dispose();
                wave = null;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            dispose();
        }

        private NAudio.Wave.BlockAlignReductionStream stream = null;
        private NAudio.Wave.DirectSoundOut ouputMp3 = null;

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Wave File (*.mp3)|*.mp3;";
            if (op.ShowDialog() == DialogResult.OK)
            {
                disposeMp3();
                NAudio.Wave.WaveStream pcm = NAudio.Wave.WaveFormatConversionStream.CreatePcmStream(new NAudio.Wave.Mp3FileReader(op.FileName));
                stream = new NAudio.Wave.BlockAlignReductionStream(pcm);
                ouputMp3 = new NAudio.Wave.DirectSoundOut();
                ouputMp3.Init(stream);
                ouputMp3.Play();
                button3.Enabled = true;
            }
        }

        private void disposeMp3()
        {
            if (ouputMp3 != null)
            {
                if (ouputMp3.PlaybackState == NAudio.Wave.PlaybackState.Playing) ouputMp3.Stop();
                ouputMp3.Dispose();
                ouputMp3 = null;
            }
            if (stream != null)
            {
                stream.Dispose();
                stream = null;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ouputMp3 != null)
            {
                if (ouputMp3.PlaybackState == NAudio.Wave.PlaybackState.Playing) ouputMp3.Pause();
                else if (ouputMp3.PlaybackState == NAudio.Wave.PlaybackState.Paused) ouputMp3.Play();
            }
        }
    }
}
