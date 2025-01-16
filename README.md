# Rezervări zboruri
Să se dezvolte un sistem software, implementat în C# (aplicație consolă), pentru
gestionarea rezervării de zboruri ale unei companii aeriene. Sistemul trebuie să permită
gestionarea zborurilor, înregistrarea clienților, realizarea de rezervări, calcularea
costurilor.

  ## Contextul Sistemului
  1. Compania aeriană:
    - Are un nume, o flotă de avioane și o listă de rute disponibile. Pentru o rută se cunosc: oraș de plecare, oraș de destinație și nr. de kilometri.
    - Permite rezervarea biletelor pentru pasageri.
    - Înregistrează toate rezervările efectuate, inclusiv plățile.
     
  3. Zborurile:
    - Fiecare zbor este identificat unic printr-un cod (ex. RO101).
    - Se cunosc ruta, ora plecării, durata zborului, capacitatea avionului, și numărul de locuri disponibile.
    - Se pot organiza două tipuri de zboruri:
      - Zboruri interne: preț bilet calculat ca (50 RON + 0.5 RON/km).
      - Zboruri internaționale: preț bilet calculat ca (200 RON + 1RON/km).
        
  4. Pasagerii:
    - Fiecare pasager are un nume, un cod unic (CNP), și o listă de rezervări anterioare. CNP-ul trebuie să respecte formatul unui CNP românesc.
    - Pasagerii trebuie să aibă un cont creat în sistem (username și parola unice).
    - Dacă pasagerul nu este autentificat, poate doar să vadă lista zborurilor disponibile.
     
  6. Rezervările:
    - O rezervare este asociată unui pasager, unui zbor, și unui număr de locuri rezervate.
    - Se calculează costul total în funcție de numărul de locuri rezervate și tipul zborului.

  ## Funcționalități
  Sistemul trebuie să permită:
  1. Autentificare și înregistrare:
    o Înregistrarea unui nou cont de pasager.
    o Autentificarea utilizatorilor existenți.
  2. Gestionare zboruri (doar pentru administratori):
    o Adăugare zbor nou.
    o Ștergere zbor.
    o Vizualizare lista completă de zboruri.
    o Actualizare informații despre zboruri (ex: ora plecării, capacitatea).
    o Vizualizare rute disponibile
    o Adăugare/ștergere rute
  3. Rezervări pentru pasageri:
    o Vizualizare lista de zboruri disponibile (cu locuri libere).
    o Rezervare locuri pe un zbor specific.
    o Anulare rezervare.
    o Vizualizare istoricul rezervărilor proprii.
  4. Rapoarte și statistici (doar pentru administratori):
    o Vizualizare zboruri cu cele mai multe locuri rezervate.
    o Vizualizare veniturile generate de un zbor.
    o Generare raport zilnic al veniturilor totale.
    o Vizualizarea tuturor plăților efectuate de un pasager.
      
  ## Cerințe Tehnice
  1. Nota maximă 8:
    o Crearea unui model OO clar, utilizând corect principiile POO (încapsulare, moștenire, polimorfism, compoziție).
    o Salvarea și încărcarea stării aplicației într-un fișier.
    o Tratarea erorilor (ex: fișier inexistent, date invalide, locuri insuficiente pentru rezervare).
    o Utilizarea GitHub pentru colaborare între membri, cu commit-uri relevante și bine descrise.
    o Prezentarea design-ului și evoluției aplicației (slide-uri PPT).

  3. Nota maximă 10:
    o Toate cerințele de mai sus.
    o Izolarea funcționalităților externe folosind clase învelitoare (wrapper) pentru interacțiunea cu fișiere și consola.
    o Utilizarea .NET Core GenericHost pentru gestionarea dependențelor.
    o Implementarea unui mecanism de logging folosind ILogger.

  5. Opțional (puncte bonus):
    o Stocarea alternativă a datelor într-o bază de date SQL.
    o Utilizarea expresiilor LINQ pentru manipularea colecțiilor.
    o Adăugarea unui mecanism de notificare prin email (simulat) pentru pasageri (ex: notificare pentru plata restantă).
    o Implementarea modelului arhitectural MVC.
    o Crearea unor teste unitare pentru validarea funcționalității aplic
