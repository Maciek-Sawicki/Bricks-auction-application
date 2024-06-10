# Platforma do sprzedaży klocków LEGO KlocX

## Opis

Platforma do sprzedaży klocków LEGO umożliwiająca użytkownikom zakup i sprzedaż zestawów klocków. Nasza platforma zapewnia wygodne przeglądanie aukcji, przeglądanie parametrów wystawionych zestawów, dodawanie do koszyka oraz filtrowanie aukcji po wybranych parametrach. Dodatkowo, administratorzy mają możliwość dodawania zestawów oraz kategorii.

## Zespół

- Mateusz Pruszyński
- Maciej Sawicki

## Cele

Stworzenie ciekawej platformy dla fanów klocków LEGO.

## Zrealizowane funkcje

- Wyszukiwanie przedmiotów
- Możliwość zakupu przedmiotów
- Możliwość sprzedaży i tworzenia aukcji przedmiotów
- Profil użytkownika
- Filtracja wyników wyszukiwania po różnych parametrach
- Wyświetlanie produktów jako lista i kafelki
- Sortowanie po parametrach
- Logowanie i rejestracja użytkowników
- Możliwość dodawania zdjęć i opisów produktów przez sprzedających
- Dodawanie przedmiotów do koszyka
- Zarządzanie kontem
- Wyświetlanie w koszyku z podziałem na sprzedających
- Powiadomienie e-mail dla kupującego z podsumowaniem zamówienia
- Czyszczenie koszyka po zakupie

## Funkcje do zrealizowania w przyszłości

- Powiadomienie e-mail dla sprzedającego z podsumowaniem zamówienia
- Dezaktywacja aukcji zrealizowanych

## Konfiguracja

Poniżej znajduje się przykładowa konfiguracja aplikacji, wraz z opisem parametrów:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "System": "el3349;Encrypt=True"
  },
  "AllowedHosts": "*",
  "Smtp": {
    "Server": "smtpook.com",
    "Port": 57,
    "User": "klocx@ok.com",
    "Password": "sG9rk"
  }
}
```
### Parametry do ustawienia:

- **ConnectionStrings**: Konfiguracja połączenia do bazy danych.
  - **System**: Ciąg połączenia do systemu baz danych.

- **AllowedHosts**: Konfiguracja dozwolonych hostów, które mogą korzystać z aplikacji.

- **Smtp**: Konfiguracja serwera SMTP do wysyłania powiadomień e-mail.
  - **Server**: Adres serwera SMTP.
  - **Port**: Port serwera SMTP.
  - **User**: Nazwa użytkownika do uwierzytelniania w serwerze SMTP.
  - **Password**: Hasło użytkownika do uwierzytelniania w serwerze SMTP.



Dzięki właściwej konfiguracji można dostosować działanie aplikacji do własnych potrzeb oraz zapewnić poprawne działanie połączenia z bazą danych i serwerem SMTP do wysyłania powiadomień e-mail.



