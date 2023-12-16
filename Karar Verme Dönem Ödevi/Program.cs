using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karar_Verme_Dönem_Ödevi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("KARAR VERME PROJESİ");
            Console.Write("Adınız ve Soyadınız: ");
            string ad = Console.ReadLine();
            Console.Write("Getiri yönelimliyse g maliyet yönelimliyse m tuşlayınız:");
            string a = Console.ReadLine();
            if (a == "g")
            {
                Console.WriteLine("GETİRİ YÖNELİMLİ KARAR VERME YAPISI ");



                Console.WriteLine("Kaç adet alternatif olacak?");
                int altboyut = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Kaç adet doğal durum olacak?");
                int dogboyut = Convert.ToInt32(Console.ReadLine());

                int[,] tablo = new int[altboyut, dogboyut];

                int girdi;

                Console.WriteLine("Alfa katsayısınuı giriniz");
                double alfa = Convert.ToDouble(Console.ReadLine());

                for (int i = 0; i < altboyut; i++)
                {
                    for (int j = 0; j < dogboyut; j++)
                    {
                        Console.WriteLine((i + 1) + ". alternatifin " + (j + 1) + ". dogal durumdaki indeksini giriniz.");
                        girdi = Convert.ToInt32(Console.ReadLine());
                        tablo[i, j] = girdi;

                    }
                }

                // tabloyu yazdırdık
                Console.WriteLine("Tablomuz:");
                for (int i = 0; i < altboyut; i++)
                {
                    for (int j = 0; j < dogboyut; j++)
                    {
                        Console.Write(tablo[i, j] + "\t");
                    }
                    Console.WriteLine();
                }


                // iyimserlik ölçütü bulmak için
                int iyimserlik = tablo[0, 0];
                for (int i = 0; i < altboyut; i++)
                {
                    for (int j = 0; j < dogboyut; j++)
                    {
                        if (tablo[i, j] > iyimserlik)
                        {
                            iyimserlik = tablo[i, j];
                        }
                    }

                }

                Console.WriteLine("İyimserlik Ölçütümüz = " + iyimserlik);

                // kötümserlik için
                int[] enkucuk = new int[altboyut];//her satırdaki en küçük değerleri bir dizinin içine aktardık

                for (int i = 0; i < altboyut; i++)
                {
                    int enkucuksatır = tablo[i, 0];
                    for (int j = 0; j < altboyut; j++)
                    {
                        if (tablo[i, j] < enkucuksatır)
                        {
                            enkucuksatır = tablo[i, j];
                        }
                    }
                    enkucuk[i] = enkucuksatır;
                }

                int kotumserlik = enkucuk[0];

                for (int i = 0; i < enkucuk.Length; i++)
                {
                    if (enkucuk[i] > kotumserlik)
                    {
                        kotumserlik = enkucuk[i];
                    }
                }
                Console.WriteLine("Kötümserlik Ölçütümüz = " + kotumserlik);

                //hurwics
                double[] enBuyukDegerler = new double[altboyut];// en büyük değerleri aldık
                double[] enKucukDegerler = new double[altboyut];// en küçük değerleri aldık

                for (int i = 0; i < altboyut; i++)
                {
                    double enBuyuk = tablo[i, 0];
                    double enKucuk = tablo[i, 0];

                    for (int j = 1; j < dogboyut; j++)
                    {
                        if (tablo[i, j] > enBuyuk)
                        {
                            enBuyuk = tablo[i, j];
                        }

                        if (tablo[i, j] < enKucuk)
                        {
                            enKucuk = tablo[i, j];
                        }
                    }

                    enBuyukDegerler[i] = enBuyuk;
                    enKucukDegerler[i] = enKucuk;

                    // her satırdaki değerleri yazdırdık
                    Console.WriteLine($"Satır {i + 1}: En Büyük = {enBuyuk}, En Küçük = {enKucuk}");
                }
                // hurwics değerlerini bir dizide topladık en büyüğünü seçebilmek için
                double[] sonucDegerler = new double[altboyut];

                for (int i = 0; i < altboyut; i++)
                {
                    double sonuc = alfa * enBuyukDegerler[i] + (1 - alfa) * enKucukDegerler[i];

                    sonucDegerler[i] = (alfa * enBuyukDegerler[i]) + ((1 - alfa) * enKucukDegerler[i]);

                    Console.WriteLine($"{i + 1}. Alternatif: (alfa * enBuyuk) + ((1 - alfa) * enKucuk) = ({alfa} * {enBuyukDegerler[i]}) + ({1 - alfa} * {enKucukDegerler[i]}) = {sonucDegerler[i]}");
                }
                //dizinin içindeki en büyük değer hurwics seçimimiz oluyor
                double enbuyuksonuc = sonucDegerler[0];

                for (int i = 0; i < altboyut; i++)
                {
                    if (sonucDegerler[i] > enbuyuksonuc)
                    {
                        enbuyuksonuc = sonucDegerler[i];
                    }
                }
                Console.WriteLine($"Hurwics = {enbuyuksonuc}");

                // laplace
                double[] satırtoplam = new double[altboyut];

                for (int i = 0; i < altboyut; i++)
                {
                    double toplam = 0;


                    for (int j = 0; j < dogboyut; j++)
                    {
                        toplam += tablo[i, j]; //her satırın sütün değerlerini topladık
                    }
                    satırtoplam[i] = toplam / dogboyut; // toplamı dogal durum sayısına böldük
                }

                for (int i = 0; i < altboyut; i++)
                {
                    Console.WriteLine($"{i + 1}. alternatifin eş olasılık değeri = {satırtoplam[i]}");
                }

                double esolasılık = satırtoplam[0]; //hangi alternatifin seçileceğini bulmak için

                for (int j = 0; j < satırtoplam.Length; j++)
                {
                    if (satırtoplam[j] > esolasılık)
                    {
                        esolasılık = satırtoplam[j];
                    }
                }
                Console.WriteLine("Eş olasılık = " + esolasılık);

                //Pişmanlık Ölçütü
                // her sütun için en büyüğü bulduk

                int[] buyuksutunlar = new int[dogboyut];

                for (int j = 0; j < dogboyut; j++)
                {
                    int buyuksutun = tablo[0, j];

                    for (int i = 0; i < altboyut; i++)
                    {

                        if (tablo[i, j] > buyuksutun)
                        {
                            buyuksutun = tablo[i, j];

                        }
                        buyuksutunlar[i] = buyuksutun;
                    }
                    Console.WriteLine($"Sütun {j + 1} En Büyük Değer: {buyuksutun}");
                    // her sütundaki en büyük değere göre fırsat kayıplarını hesaplaıyoruz
                    for (int i = 0; i < altboyut; i++)
                    {

                        if (tablo[i, j] != buyuksutun) // değer en büyük değilse
                        {
                            //sütundaki en büyük değerden diğerlerini çıkardık
                            tablo[i, j] = Math.Abs(tablo[i, j] - buyuksutun);
                        }
                        else //getirisi en yüksekse
                        {
                            tablo[i, j] = 0;
                        }

                    }


                }
                Console.WriteLine("Pişmanlık Ölçütüne Göre Tablo:");
                for (int i = 0; i < altboyut; i++)
                {
                    for (int j = 0; j < dogboyut; j++)
                    {
                        Console.Write(tablo[i, j] + "\t");
                    }
                    Console.WriteLine();
                }

                // alternatiflerden en yüksek fırsat kayıplarını seçicez

                int[] fırsatlar = new int[altboyut];

                for (int i = 0; i < altboyut; i++)
                {
                    int fırsat = tablo[i, 0];
                    for (int j = 0; j < dogboyut; j++)
                    {
                        if (tablo[i, j] > fırsat)
                        {
                            fırsat = tablo[i, j];

                        }
                    }
                    fırsatlar[i] = fırsat;
                }
                // fırsatlar dizisinin elemanlarınu görmek için
                Console.WriteLine("Her satırdaki en büyük elemanlar ");
                foreach (int item in fırsatlar)
                {
                    Console.WriteLine(item);
                }

                //en yüksek fırsat kayıpları içinden en düşüğünü seçicez
                //fail int[] pısmanlık = new int[1];

                int dusukfırsat = fırsatlar[0];

                for (int i = 0; i < fırsatlar.Length; i++)
                {
                    if (fırsatlar[i] < dusukfırsat)
                    {
                        fırsatlar[i] = dusukfırsat;

                        //fail dusukfırsat = pısmanlık[0];

                    }

                }
                Console.WriteLine("Pişmanlık Ölçütümüz = " + dusukfırsat);
            }
            else if (a == "m")
            {
                Console.WriteLine("MALİYET YÖNELİMLİ KARAR VERME YAPISI ");

                Console.WriteLine("Kaç adet alternatif olacak?");
                int altboyut = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Kaç adet doğal durum olacak?");
                int dogboyut = Convert.ToInt32(Console.ReadLine());

                int[,] tablo = new int[altboyut, dogboyut];

                int girdi;

                Console.WriteLine("Alfa katsayısınuı giriniz");
                double alfa = Convert.ToDouble(Console.ReadLine());

                for (int i = 0; i < altboyut; i++)
                {
                    for (int j = 0; j < dogboyut; j++)
                    {
                        Console.WriteLine((i + 1) + ". alternatifin " + (j + 1) + ". dogal durumdaki indeksini giriniz.");
                        girdi = Convert.ToInt32(Console.ReadLine());
                        tablo[i, j] = girdi;

                    }
                }

                // tabloyu yazdırdık
                Console.WriteLine("Tablomuz:");
                for (int i = 0; i < altboyut; i++)
                {
                    for (int j = 0; j < dogboyut; j++)
                    {
                        Console.Write(tablo[i, j] + "\t");
                    }
                    Console.WriteLine();
                }

                // iyimserlik ölçütü bulmak için
                int iyimserlik = tablo[0, 0];
                for (int i = 0; i < altboyut; i++)
                {
                    for (int j = 0; j < dogboyut; j++)
                    {
                        if (tablo[i, j] < iyimserlik)
                        {
                            iyimserlik = tablo[i, j];
                        }
                    }

                }

                Console.WriteLine("İyimserlik Ölçütümüz = " + iyimserlik);


                // kötümserlik için
                int[] enkucuk = new int[altboyut];//her satırdaki en BÜYÜK değerleri bir dizinin içine aktardık

                for (int i = 0; i < altboyut; i++)
                {
                    int enkucuksatır = tablo[i, 0];
                    for (int j = 0; j < altboyut; j++)
                    {
                        if (tablo[i, j] > enkucuksatır)
                        {
                            enkucuksatır = tablo[i, j];
                        }
                    }
                    enkucuk[i] = enkucuksatır;
                }
                // en büyük değerlerin içinden en küçüğü seçtik
                int kotumserlik = enkucuk[0];

                for (int i = 0; i < enkucuk.Length; i++)
                {
                    if (enkucuk[i] < kotumserlik)
                    {
                        kotumserlik = enkucuk[i];
                    }
                }
                Console.WriteLine("Kötümserlik Ölçütümüz = " + kotumserlik);


                //hurwics
                double[] enBuyukDegerler = new double[altboyut];// en büyük değerleri aldık
                double[] enKucukDegerler = new double[altboyut];// en küçük değerleri aldık

                for (int i = 0; i < altboyut; i++)
                {
                    double enBuyuk = tablo[i, 0];
                    double enKucuk = tablo[i, 0];

                    for (int j = 1; j < dogboyut; j++)
                    {
                        if (tablo[i, j] > enBuyuk)
                        {
                            enBuyuk = tablo[i, j];
                        }

                        if (tablo[i, j] < enKucuk)
                        {
                            enKucuk = tablo[i, j];
                        }
                    }

                    enBuyukDegerler[i] = enBuyuk;
                    enKucukDegerler[i] = enKucuk;

                    // her satırdaki değerleri yazdırdık
                    Console.WriteLine($"Satır {i + 1}: En Büyük = {enBuyuk}, En Küçük = {enKucuk}");
                }
                // hurwics değerlerini bir dizide topladık en küçüğünü seçebilmek için
                double[] sonucDegerler = new double[altboyut];

                for (int i = 0; i < altboyut; i++)
                {
                    double sonuc = alfa * enKucukDegerler[i] + (1 - alfa) * enBuyukDegerler[i];

                    sonucDegerler[i] = (alfa * enKucukDegerler[i]) + ((1 - alfa) * enBuyukDegerler[i]);

                    Console.WriteLine($"{i + 1}. Alternatif: (alfa * enBuyuk) + ((1 - alfa) * enKucuk) = ({alfa} * {enKucukDegerler[i]}) + ({1 - alfa} * {enBuyukDegerler[i]}) = {sonucDegerler[i]}");
                }
                //dizinin içindeki en küçük değer hurwics seçimimiz oluyor
                double enkucuksonuc = sonucDegerler[0];

                for (int i = 0; i < altboyut; i++)
                {
                    if (sonucDegerler[i] < enkucuksonuc)
                    {
                        enkucuksonuc = sonucDegerler[i];
                    }
                }
                Console.WriteLine($"Hurwics = {enkucuksonuc}");


                // laplace
                double[] satırtoplam = new double[altboyut];

                for (int i = 0; i < altboyut; i++)
                {
                    double toplam = 0;


                    for (int j = 0; j < dogboyut; j++)
                    {
                        toplam += tablo[i, j]; //her satırın sütün değerlerini topladık
                    }
                    satırtoplam[i] = toplam / dogboyut; // toplamı dogal durum sayısına böldük
                }

                for (int i = 0; i < altboyut; i++)
                {
                    Console.WriteLine($"{i + 1}. alternatifin eş olasılık değeri = {satırtoplam[i]}");
                }

                double esolasılık = satırtoplam[0]; // en küçüğün hangisi oldugunu bulup hangi alternatifin seçileceğini bulmak için

                for (int j = 0; j < satırtoplam.Length; j++)
                {
                    if (satırtoplam[j] < esolasılık)
                    {
                        esolasılık = satırtoplam[j];
                    }
                }
                Console.WriteLine("Eş olasılık = " + esolasılık);


                //Pişmanlık Ölçütü
                // her sütun için en küçüğü bulduk

                int[] kucuksutunlar = new int[dogboyut];

                for (int j = 0; j < dogboyut; j++)
                {
                    int kucuksutun = tablo[0, j];

                    for (int i = 0; i < altboyut; i++)
                    {

                        if (tablo[i, j] > kucuksutun)
                        {
                            kucuksutun = tablo[i, j];

                        }
                        kucuksutunlar[i] = kucuksutun;
                    }
                    Console.WriteLine($"Sütun {j + 1} En Küçük Değer: {kucuksutun}");
                    // her sütundaki en küçük değere göre fırsat kayıplarını hesaplaıyoruz
                    for (int i = 0; i < altboyut; i++)
                    {

                        if (tablo[i, j] != kucuksutun) // değer en küçük değilse
                        {
                            //sütundaki en küçük değerden diğerlerini çıkardık
                            tablo[i, j] = Math.Abs(tablo[i, j] - kucuksutun);
                        }
                        else //maliyeti en düşükse
                        {
                            tablo[i, j] = 0;
                        }

                    }


                }
                Console.WriteLine("Pişmanlık Ölçütüne Göre Tablo:");
                for (int i = 0; i < altboyut; i++)
                {
                    for (int j = 0; j < dogboyut; j++)
                    {
                        Console.Write(tablo[i, j] + "\t");
                    }
                    Console.WriteLine();
                }

                // alternatiflerden en düşük fırsat kayıplarını seçicez

                int[] fırsatlar = new int[altboyut];

                for (int i = 0; i < altboyut; i++)
                {
                    int fırsat = tablo[i, 0];
                    for (int j = 0; j < dogboyut; j++)
                    {
                        if (tablo[i, j] > fırsat)
                        {
                            fırsat = tablo[i, j];

                        }
                    }
                    fırsatlar[i] = fırsat;
                }
                // fırsatlar dizisinin elemanlarınu görmek için
                Console.WriteLine("Her satırdaki en büyük elemanlar ");
                foreach (int item in fırsatlar)
                {
                    Console.WriteLine(item);
                }

                //en yüksek fırsat kayıpları içinden en düşüğünü seçicez
                //fail int[] pısmanlık = new int[1];

                int dusukfırsat = fırsatlar[0];

                for (int i = 0; i < fırsatlar.Length; i++)
                {
                    if (fırsatlar[i] > dusukfırsat)
                    {
                        fırsatlar[i] = dusukfırsat;

                        //fail dusukfırsat = pısmanlık[0];

                    }

                }
                Console.WriteLine("Pişmanlık Ölçütümüz = " + dusukfırsat);
            }
            else
            {
                Console.WriteLine("Lütfen geçerli bir yönelim seçiniz!");
            }

            Console.ReadLine();
        }
    }
}
