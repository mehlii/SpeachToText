using System;
using System.IO;
using System.Threading.Tasks;

namespace VoskTurkceDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Vosk Türkçe Konuşma Tanıma Demosu");
            Console.Title = "Konuşma Tanıma Aktif";

            // 1. Vosk modelinin yolunu belirle
            string modelPath = "model";

            // 2. Model klasörü var mı kontrol et
            if (!Directory.Exists(modelPath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"HATA: '{modelPath}' klasörü bulunamadı.");
                Console.WriteLine("Lütfen 'vosk-model-tr-0.3' modelini indirip klasör adını 'model' olarak değiştirin ve programın yanına kopyalayın.");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            // 3. Nesneleri oluştur
            using (var recorder = new AudioRecorder())
            {
                var recognizer = new SpeechRecognizer(modelPath);

                // 4. Olayları (Events) birbirine bağla
                
                // AudioRecorder'dan veri geldikçe, SpeechRecognizer'a gönder
                recorder.DataAvailable += (sender, e) => 
                {
                    recognizer.ProcessAudio(e.Buffer, e.BytesRecorded);
                };

                // SpeechRecognizer kısmi sonuç buldukça...
                recognizer.PartialTextRecognized += (sender, text) =>
                {
                    // Konsolda aynı satırı güncelle (daha akıcı görünüm için)
                    Console.Write($"\r[...]: {text}     ");
                };

                // SpeechRecognizer nihai (kesin) sonuç buldukça...
                recognizer.FinalTextRecognized += (sender, text) =>
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    // \r ile satır başına dön, önceki [..] yazısını sil ve sonucu yaz
                    Console.WriteLine($"\r[METİN]: {text}     ");
                    Console.ResetColor();
                };

                // 5. Kaydı Başlat
                recorder.Start();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nDinleme başladı... Konuşabilirsiniz.");
                Console.WriteLine("(Çıkmak için Enter tuşuna basın)\n");
                Console.ResetColor();

                // Kullanıcı Enter'a basana kadar bekle
                await Task.Run(() => Console.ReadLine());

                // 6. Kaydı Durdur ve Temizle
                recorder.Stop();
                Console.WriteLine("\nDinleme durduruldu.");

                // Varsa kalan son sonucu al
                string lastResult = recognizer.GetFinalResult();
                if (!string.IsNullOrEmpty(lastResult))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[SON METİN]: {lastResult}");
                    Console.ResetColor();
                }
            }
        }
    }
}