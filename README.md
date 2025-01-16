# Rezervări Zboruri

Un sistem software implementat în C# (aplicație consolă) pentru gestionarea rezervării de zboruri ale unei companii aeriene. Sistemul permite gestionarea zborurilor, înregistrarea clienților, realizarea rezervărilor și calcularea costurilor.

## Contextul Sistemului

### 1. Compania aeriană:
- **Informații generale:**
  - Nume.
  - Flotă de avioane.
  - Listă de rute disponibile (oraș de plecare, oraș de destinație, număr de kilometri).
- **Funcționalități:**
  - Permite rezervarea biletelor pentru pasageri.
  - Înregistrează toate rezervările efectuate și plățile aferente.

### 2. Zborurile:
- **Identificare:** Fiecare zbor are un cod unic (ex. `RO101`).
- **Atribute:** Ruta, ora plecării, durata, capacitatea avionului, numărul de locuri disponibile.
- **Tipuri de zboruri:**
  - **Zboruri interne:** Preț bilet = `50 RON + 0.5 RON/km`.
  - **Zboruri internaționale:** Preț bilet = `200 RON + 1 RON/km`.

### 3. Pasagerii:
- **Atribute:** Nume, CNP (format românesc), listă de rezervări anterioare.
- **Autentificare:** Cont format din username și parolă unice.
- **Restricții:** Pasagerii neautentificați pot doar vizualiza lista zborurilor disponibile.

### 4. Rezervările:
- Asociate unui pasager, unui zbor și unui număr de locuri rezervate.
- Cost total calculat în funcție de numărul de locuri și tipul zborului.

## Funcționalități

### 1. Autentificare și înregistrare:
- Înregistrare cont nou de pasager.
- Autentificare utilizator existent.

### 2. Gestionare zboruri (administratori):
- Adăugare, ștergere și actualizare zboruri.
- Vizualizare lista completă de zboruri și rute disponibile.
- Adăugare/ștergere rute.

### 3. Rezervări pentru pasageri:
- Vizualizare lista de zboruri disponibile.
- Rezervare/anulare locuri pe un zbor.
- Vizualizare istoricul rezervărilor proprii.

### 4. Rapoarte și statistici (administratori):
- Zboruri cu cele mai multe locuri rezervate.
- Venituri generate de un zbor.
- Raport zilnic al veniturilor totale.
- Vizualizare plăți efectuate de un pasager.

## Cerințe Tehnice

### Pentru nota maximă 8:
- Model OO clar, respectând principiile POO (încapsulare, moștenire, polimorfism, compoziție).
- Salvarea/încărcarea stării aplicației într-un fișier.
- Tratarea erorilor (fișier inexistent, date invalide, locuri insuficiente).
- Utilizarea GitHub pentru colaborare (commit-uri relevante).
- Prezentarea design-ului și evoluției aplicației (slide-uri PPT).

### Pentru nota maximă 10:
- Cerințele de mai sus.
- Izolarea funcționalităților externe (wrapper pentru fișiere și consolă).
- Utilizarea .NET Core GenericHost pentru gestionarea dependențelor.
- Mecanism de logging cu `ILogger`.

### Opțional (bonus):
- Stocarea datelor într-o bază de date SQL.
- Utilizarea LINQ pentru manipularea colecțiilor.
- Mecanism de notificare prin email (simulat).
- Implementarea modelului arhitectural MVC.
- Crearea de teste unitare pentru validarea funcționalităților.
