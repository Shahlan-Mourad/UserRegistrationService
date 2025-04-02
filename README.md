# UserRegistrationService

UserRegistrationService är ett projekt som implementerar en tjänst för att hantera användarregistrering. Projektet innehåller validerare för användarnamn, lösenord och e-postadresser samt en tjänst för att registrera användare. Det inkluderar också enhetstester för att säkerställa att alla komponenter fungerar korrekt.

## Funktioner
- **Validering av användarnamn:** Kontrollera att användarnamn är mellan 5 och 20 tecken långt och endast innehåller alfanumeriska tecken.
- **Validering av lösenord:** Kontrollera att lösenord är minst 8 tecken långt och innehåller minst en stor bokstav, en siffra och ett specialtecken.
- **Validering av e-postadresser:** Kontrollera att e-postadresser följer ett giltigt format.
- **Användarregistrering:** Registrera användare och säkerställ att användarnamn är unika.

## Teknologier
- **.NET 9.0**
- **MSTest** för enhetstester
- **Moq** för att skapa mockade validerare i tester

## Projektstruktur
Projektet är organiserat i två huvudkataloger: `src` och `tests`. Denna struktur gör det enkelt att separera produktionskod från tester och följer bästa praxis för mjukvaruutveckling.

### **`src/`**
Denna katalog innehåller all produktionskod för projektet.

- **`Models/`:** Innehåller datamodeller som används i projektet.
  - `RegistrationResult.cs`: Representerar resultatet av en användarregistrering, inklusive framgångsstatus och eventuella felmeddelanden.
  - `User.cs`: Representerar en användare med egenskaper som användarnamn, e-post och lösenord.

- **`Services/`:** Innehåller tjänster som hanterar affärslogik.
  - `UserService.cs`: Den huvudsakliga tjänsten som hanterar användarregistrering och använder validerare för att kontrollera användarnamn, lösenord och e-post.

- **Validerare:**
  - `EmailValidator.cs`: En klass som validerar att en e-postadress följer ett korrekt format.
  - `PasswordValidator.cs`: En klass som säkerställer att lösenord uppfyller säkerhetskraven (minst 8 tecken, en stor bokstav, en siffra och ett specialtecken).
  - `UsernameValidator.cs`: En klass som kontrollerar att användarnamn är mellan 5 och 20 tecken långt och endast innehåller alfanumeriska tecken.

### **`tests/`**
Denna katalog innehåller alla enhetstester för projektet. Varje komponent i `src/` har en motsvarande testfil i `tests/`.

- **`EmailValidatorTests.cs`:** Testar olika scenarier för e-postvalidering, inklusive giltiga och ogiltiga e-postadresser.
- **`PasswordValidatorTests.cs`:** Testar lösenordsvalidering, inklusive krav på längd, specialtecken, stora bokstäver och siffror.
- **`UsernameValidatorTests.cs`:** Testar användarnamnsvalidering, inklusive längd och tillåtna tecken.
- **`UserServiceTests.cs`:** Testar `UserService` och dess integration med validerare. Täcker scenarier som giltiga och ogiltiga användare, dubblettanvändarnamn och hantering av tomma eller null-värden.


## Mockar används i tester för att simulera beteendet hos beroenden, vilket gör det möjligt att isolera den kod som testas. I detta projekt används mockar för att simulera validerare som injiceras i `UserService`.
