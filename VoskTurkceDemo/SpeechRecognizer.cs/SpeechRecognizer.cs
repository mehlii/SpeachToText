using Vosk;
using System;
using System.Text.Json;

namespace VoskTurkceDemo
{
    // Ses verisini metne dönüştürmekten sorumlu sınıf
    public class SpeechRecognizer
    {
        private VoskRecognizer recognizer;
        private Model model;

        // Konuşma devam ederken tetiklenir (örn: "merhaba dün...")
        public event EventHandler<string> PartialTextRecognized;
        
        // Cümle bittiğinde veya duraksama olduğunda tetiklenir (örn: "merhaba dünya")
        public event EventHandler<string> FinalTextRecognized;

        public SpeechRecognizer(string modelPath)
        {
            Vosk.Vosk.SetLogLevel(0); // Vosk loglarını sessize al
            model = new Model(modelPath);
            recognizer = new VoskRecognizer(model, 16000.0f); // 16kHz sample rate
            recognizer.SetMaxAlternatives(0);
            recognizer.SetWords(false);
        }

        // Dışarıdan gelen ses verisini işleyen metot
        public void ProcessAudio(byte[] buffer, int bytesRecorded)
        {
            // Gelen ses verisini Vosk'a besliyoruz
            if (recognizer.AcceptWaveform(buffer, bytesRecorded))
            {
                // AcceptWaveform 'true' dönerse, bu bir cümlenin sonu demektir.
                string finalResultJson = recognizer.Result();
                var text = ParseJson(finalResultJson, "text");
                if (!string.IsNullOrEmpty(text))
                {
                    FinalTextRecognized?.Invoke(this, text);
                }
            }
            else
            {
                // Henüz cümle bitmedi, kısmi sonucu al
                string partialResultJson = recognizer.PartialResult();
                var partialText = ParseJson(partialResultJson, "partial");
                if (partialText != null) // partial boş olabilir
                {
                    PartialTextRecognized?.Invoke(this, partialText);
                }
            }
        }

        // Vosk'un döndürdüğü JSON formatını (örn: { "text" : "merhaba" }) çözümleyen yardımcı metot
        private string? ParseJson(string json, string key)
        {
            try
            {
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    JsonElement root = doc.RootElement;
                    if (root.TryGetProperty(key, out JsonElement textElement))
                    {
                        return textElement.GetString();
                    }
                }
            }
            catch (JsonException)
            {
                // Geçersiz JSON gelirse (nadir)
                return null;
            }
            return null;
        }

        // Kaydı durdurduktan sonra kalan son veriyi almak için
        public string GetFinalResult()
        {
            return ParseJson(recognizer.FinalResult(), "text") ?? "";
        }
    }
}