using System;
using System.Collections.Generic;

class Utente
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public string Password { get; set; }
    public string PartitaIVA { get; set; }
    public string Indirizzo { get; set; }
}

class Program
{
    static Dictionary<string, Utente> utenti = new Dictionary<string, Utente>();

    static void Main(string[] args)
    {
        bool esci = false;

        while (!esci)
        {
            Console.Clear();
            Console.WriteLine("=== Sistema di Login ===");
            Console.WriteLine("1. Registrati");
            Console.WriteLine("2. Login");
            Console.WriteLine("3. Esci");
            Console.Write("Seleziona un'opzione: ");
            string scelta = Console.ReadLine();

            switch (scelta)
            {
                case "1":
                    Registrati();
                    break;
                case "2":
                    Login();
                    break;
                case "3":
                    esci = true;
                    break;
                default:
                    Console.WriteLine("Scelta non valida. Premi un tasto per continuare...");
                    Console.ReadKey();
                    break;
            }
        }

        Console.WriteLine("Arrivederci!");
    }

    static void Registrati()
    {
        Console.Clear();
        Console.WriteLine("--- Registrazione ---");

        Console.Write("Inserisci il tuo nome: ");
        string nome = Console.ReadLine();

        if (utenti.ContainsKey(nome))
        {
            Console.WriteLine("Nome già registrato. Premi un tasto per tornare al menu...");
            Console.ReadKey();
            return;
        }

        Console.Write("Inserisci una password: ");
        string password = Console.ReadLine();

        Console.Write("Inserisci il tuo ID: ");
        string id = Console.ReadLine();

        Console.Write("Inserisci la tua Partita IVA: ");
        string piva = Console.ReadLine();

        Console.Write("Inserisci il tuo indirizzo: ");
        string indirizzo = Console.ReadLine();

        Utente nuovoUtente = new Utente
        {
            Id = id,
            Nome = nome,
            Password = password,
            PartitaIVA = piva,
            Indirizzo = indirizzo
        };

        utenti[nome] = nuovoUtente;

        Console.WriteLine("Registrazione completata con successo!");
        Console.WriteLine("Premi un tasto per continuare...");
        Console.ReadKey();
    }

    static void Login()
    {
        Console.Clear();
        Console.WriteLine("--- Login ---");

        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        Console.Write("Password: ");
        string password = Console.ReadLine();

        if (utenti.TryGetValue(nome, out Utente utente) && utente.Password == password)
        {
            Console.WriteLine($"Login riuscito! Benvenuto, {utente.Nome}");
            Console.WriteLine($"ID: {utente.Id}");
            Console.WriteLine($"Partita IVA: {utente.PartitaIVA}");
            Console.WriteLine($"Indirizzo: {utente.Indirizzo}");
        }
        else
        {
            Console.WriteLine("Credenziali non valide.");
        }

        Console.WriteLine("Premi un tasto per continuare...");
        Console.ReadKey();
    }
}