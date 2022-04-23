# Konfiguracja lokalnej bazy danych

## Windows + Visual Studio
Szczęśliwi użytkownicy Windowsa mogą bardzo łatwo skonfigurować lokalną bazę danych z wykorzystaniem SQL Server LocalDB.

1. W _Solution Explorer_ klikamy prawy przycisk myszy na _Korepetynder.API_ -> _Manage User Secrets_.
2. Dodajemy wpis:
```json
    "ConnectionStrings:Korepetynder": "Server=(localdb)\\mssqllocaldb;Database=KorepetynderDb;Trusted_Connection=True;MultipleActiveResultSets=True"
```
3. Otwieramy _Package Manager Console_ (_Tools_ -> _NuGet Package Manager_ -> _Package Manager Console_).
4. Jako _Default project_ wybieramy _Korepetynder.Data_.
5. Wpisujemy komendę:
```powershell
Update-Database
```
6. Volla!

## Linux / macOS

Niestety, SQL Server LocalDB jest dostępny tylko dla Windowsa. Na innych platformach konieczne jest zainstalowanie SQL Server Developer i skonfigurowanie bazy danych.

Poradnik zakłada użycie Ubuntu 20.04. Informacje, jak zainstalować SQL Server na innych systemach (dostępny jest także kontener Dockera) można znaleźć [tutaj](https://docs.microsoft.com/en-us/sql/linux/sql-server-linux-setup).

1. Importujemy klucze GPG repozytorium:
```bash
wget -qO- https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
```

2. Rejestrujemy repozytorium:
```bash
sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/20.04/mssql-server-2019.list)"
```

3. Instalujemy SQL Servera:
```bash
sudo apt-get update
sudo apt-get install -y mssql-server mssql-tools
```

4. Uruchamiamy wstępną konfigurację:
```bash
sudo /opt/mssql/bin/mssql-conf setup
```
Wybieramy edycję _Developer_ (2). Akceptujemy warunki licencji. Definiujemy hasło roota (minimum 8 znaków, wielkie litery, małe litery oraz cyfry lub symbole).

5. Sprawdzamy, czy wszystko działa:
```bash
systemctl status mssql-server --no-pager
```

Następnie, instalujemy narzędzia do łączenia się z SQL Serverem z wiersza poleceń (alternatywnie, można połączyć się za pomocą np. [VS Code'a](https://docs.microsoft.com/en-us/sql/tools/visual-studio-code/sql-server-develop-use-vscode)):

1. Importujemy repozytorium:
```bash
curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.list
```
2. Instalujemy pakiety:
```bash
sudo apt-get update 
sudo apt-get install mssql-tools unixodbc-dev
```
3. Dodajemy narzędzia do zmiennej środowiskowej PATH:
```bash
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
```

Teraz możemy połączyć się z serwerem:
```bash
sqlcmd -S localhost -U SA -P '<password>'
```

Po połączeniu się z serwerem musimy utworzyć bazę danych na potrzeby aplikacji:
```sql
CREATE DATABASE KorepetynderDb;
```

Dobrą praktyką jest utworzenie osobnego użytkownika dla tej bazy danych, ale nie jest to konieczne. Więcej informacji:
* [tworzenie użytkownika](https://docs.microsoft.com/en-us/sql/relational-databases/security/authentication-access/create-a-database-user#TsqlProcedure)
* [nadawanie uprawnień](https://docs.microsoft.com/en-us/sql/t-sql/statements/alter-role-transact-sql) (rola *db_owner*)

Teraz konfigurujemy połączenie aplikacji z bazą danych. Będąc w folderze _Korepetynder.Api_ wywołujemy komendę:
```bash
dotnet user-secrets set "ConnectionStrings:Korepetynder" "Server=localhost;Database=KorepetynderDb;User Id=<user>;Password=<password>;MultipleActiveResultSets=True"
```
(jeśli nie utworzyliśmy nowego usera, to używamy usera _SA_)

Ostatni krok - aplikujemy migracje. Instalujemy konsolowe narzędzie do Entity Frameworka:
```bash
dotnet tool install --global dotnet-ef
```

i będac w folderze "Korepetynder.Data" uruchamiamy:
```bash
dotnet ef database update --startup-project ../Korepetynder.Api/Korepetynder.Api.csproj
```

# Aktualizacja struktury bazy danych
## Windows + Visual Studio
1. Otwieramy _Package Manager Console_ (_Tools_ -> _NuGet Package Manager_ -> _Package Manager Console_).
2. Jako _Default project_ wybieramy _Korepetynder.Data_.
3. Jeśli to my zmodyfikowaliśmy strukturę bazy danych, to wpisujemy komendę:
```powershell
Add-Migration [nazwa]
```
i czytamy migrację, upewniając się, że ma sens.

4. Niezależnie od tego, kto zmodyfikował strukturę, wpisujemy komendę:
```powershell
Update-Database
```

## Linux / macOS
1. Przechodzimy do folderu _Korepetynder.Data_.
2. Jeśli to my zmodyfikowaliśmy strukturę bazy danych, to wpisujemy komendę:
```bash
dotnet ef migrations add <nazwa> --startup-project ../Korepetynder.Api/Korepetynder.Api.csproj
```
i czytamy migrację, upewniając się, że ma sens.

3. Niezależnie od tego, kto zmodyfikował strukturę, wpisujemy komendę:
```
dotnet ef database update --startup-project ../Korepetynder.Api/Korepetynder.Api.csproj
```
