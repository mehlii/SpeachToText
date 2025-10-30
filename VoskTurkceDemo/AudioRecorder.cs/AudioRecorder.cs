using NAudio.Wave;
using System;

namespace VoskTurkceDemo
{
    // Mikrofondan ses kaydı almaktan sorumlu sınıf
    public class AudioRecorder : IDisposable
    {
        private WaveInEvent? waveIn;
        
        // Ses verisi geldiğinde tetiklenecek olay
        public event EventHandler<WaveInEventArgs>? DataAvailable; // DÜZELTME 2: Buraya '?' eklendi

        // Vosk'un genellikle tercih ettiği ses formatı: 16kHz, 16-bit, 1 kanal (Mono)
        private const int SampleRate = 16000;
        private const int Channels = 1;

        public AudioRecorder()
        {
            waveIn = new WaveInEvent
            {
                WaveFormat = new WaveFormat(SampleRate, Channels)
            };
            
            // Veri geldiğinde 'OnDataAvailable' metodunu çağır
            waveIn.DataAvailable += OnDataAvailable;
        }

        private void OnDataAvailable(object? sender, WaveInEventArgs e)
        {
            // Bu olayı dinleyen (abone olan) diğer sınıflara veriyi iletiyoruz
            DataAvailable?.Invoke(this, e);
        }

        public void Start()
        {
            try
            {
                waveIn?.StartRecording(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"HATA: Mikrofon başlatılamadı. {ex.Message}");
                Console.WriteLine("Lütfen mikrofon izinlerinizi kontrol edin.");
            }
        }

        public void Stop()
        {
            waveIn?.StopRecording();
        }

        // Kaynakları temizlemek için
        public void Dispose()
        {
            waveIn?.Dispose();
            waveIn = null;
        }
    }
}