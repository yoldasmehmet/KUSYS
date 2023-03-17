

---


# KUSYS Demo Projesi 


## 📝 İçindekiler

- [Hakkında](#about)
- [Gerekli kurulumlar](#getting_started)

## 🧐 Hakkında <a name = "about"></a>

Bu demo projesi üniversitelerin gereksinim çerçevesinde hazırlanmıştır. Bu demo projesine
nuget.org'ta yayımlanmladığım binlerce indirmesi olan kod kütüphanesinin birkaç projesi dahil edilmiştir.
Kütüphanenin geri kalanında loglama(graylog ile entegre), cacheleme (redis ile entegre),
dosya sunucusu yardımcı methodları (Min.io amazon s3 protokolü ile çalışan hızlı ve stabil dosya sunucusu),
mongodb ve elastic search ile ilgili yardımcı sınıfları içermektedir. 
Bu projede kullanılan yetkilendirme sistemi SSO yapısındadır. Yetkilendirmeler bir servis sağlayıcısının ürettiği jwt tokenlar sayesinde gerçekleşir. 
 Tüm uygulamalar jwt token üreten bir servis ile yetkilendirilebilmektedir. 
 Bu üniversite içinde çok gereksinim duyulan bir yapıdır. 
 Uygulama testleri swagger üzerinden yapılması gerekmektedir. 
 Bir arayüz geliştirilmemiştir. Benim uzmanlık alanım yazılım mimarisi ve backend olduğu için daha çok backend tarafında yoğunlaşılmıştır. Uygulamanın testleri swaggerda yapılabilmektedir.
## 🏁 Gerekli kurulumlar <a name = "getting_started"></a>

Proje de kullanılan veritabanı postgresqldir. Docker engine'e kurulumu için aşağıdaki adımlar izlemelidir.

#### Veritabanı kurulumları
- Docker desctop kurulumu yapılır. İnternetten indirilip kurulur. Kurulumunda sıkıntı yaşanırsa benimle irtibata geçebilirsiniz. (Tel: 05079404960)
- KUSYS projesnde bulunan Reinstall-KUSYSDB.bat dosyası dosya konumundan çift tıklanarak çalıştırılabilir. 
Docker desctop aktif çalışır durumda değilse "Reinstall-KUSYSDB.bat" dosyası çalışırken hata verecektir.
- Aynı şekilde Library.Security.IdentityServer projesinde bulunan Reinstall-ServiceAuthorizationDB.bat dosyası da çalıştırılır.
- Bu bat dosyası veri tabanını boşaltmak için de kullanılabilir. Tüm veri tabanı şeması silinecektir.
- Solution'a sağ tıklanıp özeliklerden multible startup sekmesi tıklanır , "KUSYS" ve "Library.Security.IdentityServer" projeleri start seçilir
- Proje çalıştırılınca KUSYS projesi için açılan swagger sekmesinde login yapılır ve crud işlemleri login yapılan user'in yetkilerine göre çalışır.
#### Graylog Kurulumu
- Graylog kurulumu için Library.Logging projesinde bulunan Reinstal-Graylog.bat dosyası çalıştırılır.
- Kullanıcı Adı:admin, Şifre:library yazılır.
- System/Inputs sekmesinde inputs'a tıklanıp. 
Sol üstteki açılır listeden Gelf UDP seçilir. 
Sadece  node alanı ve title alanı doldurulup varsayılan ayarlar ile bir input oluşturulur.
- Loglamalar search sekmesinden izlenebilir.

### Yükleme yapılabilecek yararlı genişletmeler
- Visual studio kullanıyorsanız manage extensions sekmesine Open command line aramada yazılıp sonrasında 
seçilip yüklenebilir. Yüklendikten sonra visual studio restart yapılmalıdır.
Böylelikle bat dosyalarına dosya konumuna gitmeden de sağ tıklayıp Execute edilebilir.


```
Bu genişletmenin kullanılması kolaylık sağlamaktadır.
```

## 🔧 Çalıştırma ve test <a name = "tests"></a>

- User için kullanılacak olan payload: { "userName": "testuser", "password": "library", "applicationName": "KUSYS" }

- Admin için kullanılacak olan payload: { "userName": "testadmin", "password": "library", "applicationName": "KUSYS" }

### Uygulamanın testleri

- "KUSYS" uygulamasındaki ve "Library.Security.IdentityServer" projesinde bulunan application dosyalarında bulunan connection string kullanılarak bir uygulama vassıtası ile veri tabanı bağlantısı sağlanabilir.
İki adet kullanıcı eklenmiştir. Bu kullanıcılar testuser ve testadmin'dir. Bu kullanıcıların çalıştırmaya yetkili oldukları methodlar dökümandaki gibi 
ayarlanmıştır ve uygulama çalıştırıldığı zaman devreye girecek migrationlar ile SSO yapısındaki serverin kullandığı veri tabanına gerekli yetkiler eklenecektir.
- Ldap entegrasyonunun da desteklenebileceği yapıda kullanıcılar ldap bilgileri ile de giriş yapabilecektir.
 - SSO yapısınna katılan Web uygulamaları veya Web API methodları yetkilendirilebilecektir. Bu örnekte Web API yetkilendirilmiştir.
 - Üç aşamalı geliştirme ortamı sunulmuştur örnekte. 
 Development ortamı her developerin kendi üzerindeki veri tabanı ile rahatlıkla oynama yapabileceği geliştirmeler yapabileceği ortamdır. 
 Test ortamı bir geliştirme gurubunun kullandığı uzak veri tabanını ifade etmektedir. 
 Production otartamı ise uygulamının müşteriye sunulacağı son ortamdır.
- Envirement ortamı seçilimi multible project konfigürasyonunda gerçekleştirilememektedir. 
Envirementi değiştirilecek proje startup projesi haline getirilip bu işlem gerçekleştirilebilir.
- Envirement değişimi veritabanı değişikliğinin ortamlar arasındaki senkronizasyonu için önemlidir. 
Bunun sırası Development=>Test=>Production şeklindedir. 
Bu şekilde şema değişimlerindeki yaşanabilecek sıkıntılar minimize edilir.
 - Migrationlar ayarlandığında development ortamında yapılan testler çalışıyorsa visual studio daki envirement değiştirilip test ortamına geçilir.
 Burdaki yapılan testlerde bir sorun çıkmazsa production ortamına geçilir.
 Taplo yapılarının değişmesinde aortamlar arası bir problem oluşturmamaktadır yapılan testlerde. 
 Proje çalıştırılınca ilk yapılan iş migration olduğu için gerekli şema değişikliği veri tabanına yansıtılır ve kodlar ona göre çalışır. 
 Migratiion yapıldıktan sonra bunun için harcanan zamanı da konsoldan izlemek ve ne yapıldığına dair bilgileri de görmek mümkündür.
 - Yapılacak olan migratonun kim tarafından ne zaman yapıldığı VersionInfo tablosunda kayıt altında tutulmaktadır. 
 Şema ile ilgili herhangi bir sorun yaşandığında bu tablodan faydalanılabilir.
 - Bu yöntemle development ortamındaki bir sorundan diğer ortamlar etkilenmemiş olur. 
 Envirementlerın bir birinden tam olarak yalıtılması geliştirme sürecindeki hatalardan etkilenen kullanıcı sayısını minimize etmektedir."

## 🎈 Tasarım <a name="usage"></a>
Kompleks ve değişken iş kurallarının, veri yapılarının olduğu projelerde CQRS kullanılmasını tavsiye ediyorum. 
Fakat bu projede iş kuralları olmadığı için doğrudan iş katmanındaki methodlarla problem çözülmüştür.

## 🚀 Yüklenmesi <a name = "deployment"></a>

Projenin gerçek ortama yüklenmesi için öncelikle application.json 
içine gerçek ortamın veritabanı bilgileri yazılmalıdır. Daha sonra kullanılan IDE'de production envirement'i seçilmesi ve çalıştırılması gerekmektedir.
Bu şekilde gerekli tablolar, tablolar arası ilişkiler, ön tanımlı kayıtlar oluşturulacaktır. Kullanıma hazır bir yapı saniyeler içinde oluşacaktır.


## ⛏️ Nuget'te yayılanmış olan kütüphaneler ile etkin kullanılabilecek uygulamalar <a name = "built_using"></a>

- [MongoDB](https://www.mongodb.com/) - Nosql veri tabanı
- [Min.IO](https://expressjs.com/) - Amazon S3 protokolü destekli açık kaynaklı dosya sunucusu.
- [Redis]() - Veritabanının yükünü hafifleten bir mekanizmadır. (Compile time AOP programalama ile yazılmış kütüphane mevcuttur.)
- [Graylog]() - Loglamaların tutulacağı mekanizma

## 🎉 Teknolojiler <a name = "acknowledgement"></a>

- Docker
- .Net Core 6.0
- Postgresql
- EntityFrameworkCore
- Lambda expressions
