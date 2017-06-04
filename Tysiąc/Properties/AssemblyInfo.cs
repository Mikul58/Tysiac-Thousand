using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Ogólne informacje o zestawie są kontrolowane poprzez następujący 
// zestaw atrybutów. Zmień wartości tych atrybutów, aby zmodyfikować informacje
// powiązane z zestawem.
[assembly: AssemblyTitle("Tysiąc")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Tysiąc")]
[assembly: AssemblyCopyright("Copyright ©  2017")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Ustawienie elementu ComVisible na wartość false sprawia, że typy w tym zestawie są niewidoczne
// dla składników COM. Jeśli potrzebny jest dostęp do typu w tym zestawie z
// COM, ustaw wartość true dla atrybutu ComVisible tego typu.
[assembly: ComVisible(false)]

// Następujący identyfikator GUID jest identyfikatorem biblioteki typów w przypadku udostępnienia tego projektu w modelu COM
[assembly: Guid("b17e0142-e915-46f4-bb00-12bb0e28ecf0")]

// Informacje o wersji zestawu zawierają następujące cztery wartości:
//
//      Wersja główna
//      Wersja pomocnicza
//      Numer kompilacji
//      Rewizja
//
// Możesz określić wszystkie wartości lub użyć domyślnych numerów kompilacji i poprawki
// przy użyciu symbolu „*”, tak jak pokazano poniżej:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

/*NOTATKI
 *  //////////////////////////////////////////////////////////////////////
 *  
 *  ROZWIĄZANIA
 *  
 *  1. Jak sprawić, by wynajdować meldunki w talii?
 *      
 *   Zasada działania:
 *   
 *      Program po stworzeniu *KartyWRece* dla kazdego gracza powinien sprawdzać czy jest w tabeli *potencjalnyMeldunek* składający się ciągu znaków "Dama" oraz "Król" z tej samej pozycji w tablicy *Kolor*. Jeśli *potencjalnyMeldunek* będzie obecny, program powinien przygotować wartość  do dopisania się do ilości *Punkty"(w przypadku ugrania określonej liczby punktów).
 *      
 *   Potencjalne rozwiązania:
 *   
 *      1. 
 *      - Sprawdzenie listy *KartyWRece* w poszukiwaniu "Dama".
 *          SUKCES) Program pobiera "Dama Kolor" i działa wg kolejnego punktu
 *          PORAZKA) W przypadku nie znalezienia "Dama" program wychodzi z metody
 *      - Kiedy program pobierze "Dama Kolor" sprawdza listę w poszukiwaniu "Krol Kolor".
 *          SUKCES) Program dodaje *potencjalnyMeldunek* graczowi wg punktacji *punktyKoloruMeldunku*, wraca do początku metody i                     sprawdza, czy "Dama Kolor" != "Dama Kolor" z poprzedniej iteracji
 *          PORAZKA) a) Program wraca do początku metody i sprawdza, czy "Dama Kolor" != "Dama Kolor" z poprzedniej iteracji        
 *                      wykonując te same instrukcje 
 *                   b) W przypadku nie znalezienia "Krol" program wychodzi z metody.
 *      
 *  PROBLEMY
 *  
 *      Metoda CzyKartyDoKoloru() nie mogą być zagnieżdżone w metodzie WybierzKarte(), ponieważ WybierzKarte() wykonuje się również w zwykłym poborze kart do Listy kartyJednoturowe. Brak kart przy sprawdzeniu z CzyKartyDoKoloru() sprawia, że metoda nie ma skąd tych kart wziąć. Zagnieżdżanie metod w metodzie pojednyczej to też zły pomysł.
 *  
 *      
 *  //////////////////////////////////////////////////////////////////////
 */
