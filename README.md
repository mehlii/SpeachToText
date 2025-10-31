# SpeachToText
PROJE ADI:
C# Vosk ile Gerçek Zamanlı Konuşma Tanıma Uygulaması

AÇIKLAMA:
Bu proje, C# ve .NET platformu kullanılarak geliştirilmiş bir konsol uygulamasıdır. NAudio kütüphanesi aracılığıyla varsayılan mikrofondan ses kaydı alır ve bu sesi Vosk konuşma tanıma motorunu kullanarak gerçek zamanlı olarak metne dönüştürür.

Uygulama, ses kaydı (AudioRecorder) ve konuşma tanıma (SpeechRecognizer) sorumluluklarını ayırmak için nesne tabanlı programlama (OOP) prensiplerine uygun olarak tasarlanmıştır.

TEMEL ÖZELLİKLER:
- Varsayılan mikrofondan canlı ses akışı.
- Gerçek zamanlı kısmi (partial) ve nihai (final) metin transkripsiyonu.
- Çevrimdışı (offline) çalışma için Vosk dil modeli kullanımı.
- Sorumlulukların ayrıldığı (OOP) modüler kod yapısı.

GEREKSİNİMLER:
1. .NET SDK (Sürüm 6.0 veya daha yenisi)
2. Vosk Dil Modeli (Örn: vosk-model-tr-0.3)

KURULUM:
1. Proje dosyalarını bir dizine kopyalayın.
2. Bir dil modeli (örn: 'vosk-model-tr-0.3') indirin.
3. İndirilen model klasörünün adını 'model' olarak değiştirin.
4. Bu 'model' klasörünü, projenin ana .csproj dosyası (VoskTurkceDemo.csproj) ile aynı dizine yerleştirin.
5. Terminal veya komut istemcisini proje dizininde (VoskTurkceDemo) açın.
6. Gerekli kütüphaneleri (paketleri) yüklemek için aşağıdaki komutu çalıştırın:
   dotnet restore

KULLANIM:
1. Kurulum adımları tamamlandıktan sonra, terminalde aşağıdaki komutu çalıştırarak uygulamayı başlatın:
   dotnet run
2. Uygulama, mikrofon erişimi için izin isteyebilir. Gerekli izni verin.
3. "Dinleme başladı..." mesajını gördükten sonra konuşmaya başlayın.
4. Konuşmanız konsola metin olarak yansıtılacaktır.
5. Uygulamayı durdurmak ve çıkış yapmak için 'Enter' tuşuna basın.

KULLANILAN TEKNOLOJİLER:
- C# / .NET
- Vosk (Konuşma Tanıma Motoru)
- NAudio (Ses Yakalama Kütüphanesi)
- System.Text.Json (JSON Çözümleme)
